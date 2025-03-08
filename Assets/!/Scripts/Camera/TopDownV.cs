using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[ComponentInfo("", "Nu, sam poczitaj, mnie len pisat")]
public class TopDownV : CameraCore, IUpdate, ICameraUpdate
{
    #region VARIABLES
    [SerializeField] private Vector3 _cameraDefOffset = new Vector3(0f, 10f, -2f);
    [SerializeField][Range(.1f, 100f)] private float _cameraMoveSensitivity = 15f;
    [SerializeField][Range(.1f, 100f)] private float _cameraClimbingStepValue = 1f;
    [SerializeField][Range(.1f, 20f)] private float _mouseDragSensitivity = 2f;
    [SerializeField] private float _smoothTime = 0.3f;
    [SerializeField] private float _zoomStepSizeY = 1.2f;
    [SerializeField] private float _zoomStepSizeZ = .7f;
    [SerializeField] private float _minCameraHeight = 10f;
    [SerializeField] private float _maxCameraHeight = 30f;
    [SerializeField] private float _horizontalEdgeTolerance = 0.08f;
    [SerializeField] private float _verticalEdgeTolerance = 0.08f;
    private Vector3 _currentMoveVelocity = Vector3.zero;
    private Vector3 _moveDestination = Vector3.zero;
    private Transform _cameraTransform;
    private int _leftEdgeThreshold;
    private int _rightEdgeThreshold;
    private int _bottomEdgeThreshold;
    private int _topEdgeThreshold;
    private Vector3 _zoomTargetPosition;
    private bool _isZooming = false;
    private Vector3 _zoomCurrentVelocity = Vector3.zero;
    private bool _isMouseDragging = false;
    #endregion
    #region PUBLIC VARIABLES
    /// <summary>
    /// Gets or sets the default camera offset from the target position.
    /// </summary>
    public Vector3 CameraDefOffset
    {
        get => _cameraDefOffset;
        set => _cameraDefOffset = value;
    }
    /// <summary>
    /// Gets or sets the sensitivity of the camera movement.
    /// </summary>
    public float CameraMoveSensitivity
    {
        get => _cameraMoveSensitivity;
        set => _cameraMoveSensitivity = Mathf.Clamp(value, 1f, 100f);
    }
    /// <summary>
    /// Gets or sets the step size for vertical camera movement.
    /// </summary>
    public float CameraClimbingStepValue
    {
        get => _cameraClimbingStepValue;
        set => _cameraClimbingStepValue = Mathf.Clamp(value, 1f, 100f);
    }
    /// <summary>
    /// Gets or sets the sensitivity for dragging the camera with the mouse.
    /// </summary>
    public float MouseDragSensitivity
    {
        get => _mouseDragSensitivity;
        set => _mouseDragSensitivity = Mathf.Clamp(value, 1f, 5f);
    }
    /// <summary>
    /// Gets or sets the smoothing time for camera movement.
    /// </summary>
    public float SmoothTime
    {
        get => _smoothTime;
        set => _smoothTime = Mathf.Max(0f, value);
    }
    /// <summary>
    /// Gets or sets the zoom step size along the Y-axis.
    /// </summary>
    public float ZoomStepSizeY
    {
        get => _zoomStepSizeY;
        set => _zoomStepSizeY = Mathf.Max(0f, value);
    }
    /// <summary>
    /// Gets or sets the zoom step size along the Z-axis.
    /// </summary>
    public float ZoomStepSizeZ
    {
        get => _zoomStepSizeZ;
        set => _zoomStepSizeZ = Mathf.Max(0f, value);
    }
    /// <summary>
    /// Gets or sets the minimum height for camera zoom.
    /// </summary>
    public float MinCameraHeight
    {
        get => _minCameraHeight;
        set => _minCameraHeight = Mathf.Max(0f, value);
    }
    /// <summary>
    /// Gets or sets the maximum height for camera zoom.
    /// </summary>
    public float MaxCameraHeight
    {
        get => _maxCameraHeight;
        set => _maxCameraHeight = Mathf.Max(_minCameraHeight, value);
    }
    /// <summary>
    /// Gets or sets the horizontal edge tolerance for camera movement.
    /// </summary>
    public float HorizontalEdgeTolerance
    {
        get => _horizontalEdgeTolerance;
        set => _horizontalEdgeTolerance = Mathf.Clamp01(value);
    }
    /// <summary>
    /// Gets or sets the vertical edge tolerance for camera movement.
    /// </summary>
    public float VerticalEdgeTolerance
    {
        get => _verticalEdgeTolerance;
        set => _verticalEdgeTolerance = Mathf.Clamp01(value);
    }
    #endregion
    #region PUBLIC METHODS
    /// <summary>
    /// 
    /// </summary>
    public void UpdateNeededComponents() // TODO: we dont need to get camera here, it's better to do in Init method
    {
        _cameraTransform = transform.GetChild(Constants.Player.CAMERA).transform;
    }
    public void ManageScreenСompatibility()
    {
        _leftEdgeThreshold = (int)(Screen.width * _horizontalEdgeTolerance);
        _rightEdgeThreshold = Screen.width - _leftEdgeThreshold;
        _bottomEdgeThreshold = (int)(Screen.height * _verticalEdgeTolerance);
        _topEdgeThreshold = Screen.height - _bottomEdgeThreshold;
        // Debug lines for verifying calculated edge tolerances (commented out).
        /*        Debug.Log($"_maxLeft {_leftEdgeThreshold}");
                Debug.Log($"_maxRight {_rightEdgeThreshold}");
                Debug.Log($"_maxDown {_bottomEdgeThreshold}");
                Debug.Log($"_maxUp {_topEdgeThreshold}");*/
    }
    #endregion
    #region PRIVATE METHODS
    private void DragMouseScreen()
    {
        this.transform.position += new Vector3(-InputHandler.MouseInput.x, 0f, -InputHandler.MouseInput.y) * Time.deltaTime * _mouseDragSensitivity;
    }
    private void ChangeScreenPositionMause()
    {
        if (Mouse.current.position.ReadValue().x > _rightEdgeThreshold)
        {
            _moveDestination.x += _cameraClimbingStepValue; // Move right.
        }
        if (Mouse.current.position.ReadValue().x < _leftEdgeThreshold)
        {
            _moveDestination.x -= _cameraClimbingStepValue; // Move left.
        }
        if (Mouse.current.position.ReadValue().y > _topEdgeThreshold)
        {
            _moveDestination.z += _cameraClimbingStepValue; // Move forward/up.
        }
        if (Mouse.current.position.ReadValue().y < _bottomEdgeThreshold)
        {
            _moveDestination.z -= _cameraClimbingStepValue; // Move backward/down.
        }

        transform.position = Vector3.SmoothDamp(
            transform.position,
            _moveDestination + new Vector3(InputHandler.WASDInput.x, 0f, InputHandler.WASDInput.y),
            ref _currentMoveVelocity,
            _smoothTime,
            _cameraMoveSensitivity,
            Time.deltaTime);
    }
    private IEnumerator TweenPosition()
    {
        _isZooming = true;
        while (Vector3.Distance(_zoomTargetPosition, _cameraTransform.localPosition) > 0.01f && _isZooming)
        {
            _cameraTransform.localPosition = Vector3.SmoothDamp(_cameraTransform.localPosition, _zoomTargetPosition, ref _zoomCurrentVelocity, .3f);
            _cameraTransform.LookAt(this.transform);
            yield return null;
        }
        _isZooming = false;
    }
    private void ZoomCamera()
    {
        float zoomHeight = Mathf.Clamp(_cameraTransform.localPosition.y + -InputHandler.WheelRotate.y * .01f * _zoomStepSizeY, _minCameraHeight, _maxCameraHeight);
        _zoomTargetPosition = new Vector3(_cameraTransform.localPosition.x, zoomHeight, _cameraTransform.localPosition.z);
        _zoomTargetPosition -= _zoomStepSizeZ * (zoomHeight - _cameraTransform.localPosition.y) * Vector3.forward;

        if (!_isZooming) StartCoroutine(TweenPosition());
    }
    #endregion
    #region Update
    public void PerformInitialUpdate()
    {
        _moveDestination = this.transform.position;

        if (_isMouseDragging) DragMouseScreen();
        else ChangeScreenPositionMause();
    }
    public void PerformPreUpdate()
    {
        throw new System.NotImplementedException();
    }
    public void PerformUpdate()
    {
        throw new System.NotImplementedException();
    }
    public void PerformFinalUpdate()
    {
        throw new System.NotImplementedException();
    }
    public void PerformLateUpdate()
    {
        throw new System.NotImplementedException();
    }
    private void RegisterUpdate()
    {
        Updater.Instance.RegisterUpdate(this, Updater.UpdateType.InitialUpdate);
    }
    private void UnregisterUpdate()
    {
        Updater.Instance.UnregisterUpdate(this, Updater.UpdateType.InitialUpdate);
    }
    #endregion
    #region MONO METHODS
    public void Init()
    {
        GameEvents.OnScreenResize?.AddListener(ManageScreenСompatibility);
        ManageScreenСompatibility();
    }
    private void OnEnable()
    {
        base.ExclusivityСheck();

        transform.position = PlayerCore.Instance.transform.position;
        transform.rotation = Quaternion.identity;

        _cameraTransform.localPosition = _cameraDefOffset;
        _cameraTransform.LookAt(this.transform);

        InputHandler.OnWheelRotate?.AddListener(ZoomCamera);
        InputHandler.OnWheelPerformed?.AddListener(EnableMouseMove);
        InputHandler.OnWheelCanceled?.AddListener(DisableMouseMove);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        RegisterUpdate();
    }
    private void OnDisable()
    {
        InputHandler.OnWheelRotate?.RemoveListener(ZoomCamera);
        InputHandler.OnWheelPerformed?.RemoveListener(EnableMouseMove);
        InputHandler.OnWheelCanceled?.RemoveListener(DisableMouseMove);

        StopCoroutine(TweenPosition());
        UnregisterUpdate();
    }
    private void OnDestroy()
    {
        GameEvents.OnScreenResize?.RemoveListener(ManageScreenСompatibility); // хз зачем, пусть будет
    }
    private void EnableMouseMove() => _isMouseDragging = true;
    private void DisableMouseMove() => _isMouseDragging = false;
    #endregion
}


/*
 using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class TopDownV : CameraCore
{
    #region SerializeField variables
    [SerializeField] private Vector3 _cameraPos = new Vector3(0f, 10f, -2f);
    [SerializeField][Range(1f, 100f)] private float _sensitivity = 15f;
    [SerializeField][Range(1f, 100f)] private float _stepSize = 1f;
    [SerializeField][Range(1f, 5f)] private float _sensitivityMouseDrag = 2f;
    [SerializeField][Range(1f, 5f)] private float _sensitivityRotate = 2f;
    [SerializeField] private float _smoothTime = 0.3f;
    [SerializeField] private float _zoomStepSizeY = 1.2f;
    [SerializeField] private float _zoomStepSizeZ = .7f;
    [SerializeField] private float _minZoomHeight = 10f;
    [SerializeField] private float _maxZoomHeight = 30f;
    [SerializeField] private float edgeToleranceGorizontal = 0.08f;
    [SerializeField] private float edgeToleranceVertical = 0.08f;
    #endregion
    #region private variables
    private Vector3 _currentVelocity = Vector3.zero;
    private Vector3 _moveDestination = Vector3.zero;
    private Transform _camera;
    private int _edgeToleranceLeft;
    private int _edgeToleranceRight;
    private int _edgeToleranceDown;
    private int _edgeToleranceUp;

    private Vector3 _targetPosition;
    private bool _isStarted = false;
    private Vector3 _zoomCurrentVelocity = Vector3.zero;

    private bool _doMouseMove = false;
    #endregion
    #region MonoBehaviour
    private void Awake()
    {
        _edgeToleranceLeft = (int)(Screen.width * edgeToleranceGorizontal);
        _edgeToleranceRight = Screen.width - _edgeToleranceLeft;
        _edgeToleranceDown = (int)(Screen.height * edgeToleranceVertical);
        _edgeToleranceUp = Screen.height - _edgeToleranceDown;

        // Debug lines for verifying calculated edge tolerances (commented out).
         Debug.Log($"_maxLeft {_maxLeft}");
           Debug.Log($"_maxRight {_maxRight}");
           Debug.Log($"_maxDown {_maxDown}");
           Debug.Log($"_maxUp {_maxUp}"); 
    }
    private void Update()
    {
        _moveDestination = this.transform.position;

        if (_doMouseMove) DragMouseScreen();
        else ChangeScreenPositionMause();
    }
    #endregion
    #region private methods
    private void DragMouseScreen()
    {
        this.transform.position += new Vector3(-InputHandler.MouseInput.x, 0f, -InputHandler.MouseInput.y) * Time.deltaTime * _sensitivityMouseDrag;
    }
    private void ChangeScreenPositionMause()
    {
        if (Mouse.current.position.ReadValue().x > _edgeToleranceRight)
        {
            _moveDestination.x += _stepSize; // Move right.
        }
        if (Mouse.current.position.ReadValue().x < _edgeToleranceLeft)
        {
            _moveDestination.x -= _stepSize; // Move left.
        }
        if (Mouse.current.position.ReadValue().y > _edgeToleranceUp)
        {
            _moveDestination.z += _stepSize; // Move forward/up.
        }
        if (Mouse.current.position.ReadValue().y < _edgeToleranceDown)
        {
            _moveDestination.z -= _stepSize; // Move backward/down.
        }

        transform.position = Vector3.SmoothDamp(
            transform.position,
            _moveDestination + new Vector3(InputHandler.WASDInput.x, 0f, InputHandler.WASDInput.y),
            ref _currentVelocity,
            _smoothTime,
            _sensitivity,
            Time.deltaTime);
    }
    private IEnumerator TweenPosition()
    {
        _isStarted = true;
        while (Vector3.Distance(_targetPosition, _camera.localPosition) > 0.01f && _isStarted)
        {
            _camera.localPosition = Vector3.SmoothDamp(_camera.localPosition, _targetPosition, ref _zoomCurrentVelocity, .3f);
            _camera.LookAt(this.transform);
            yield return null;
        }
        _isStarted = false;
    }
    private void ZoomCamera()
    {
        float zoomHeight = Mathf.Clamp(_camera.localPosition.y + -InputHandler.WheelRotate.y * .01f * _zoomStepSizeY, _minZoomHeight, _maxZoomHeight);
        _targetPosition = new Vector3(_camera.localPosition.x, zoomHeight, _camera.localPosition.z);
        _targetPosition -= _zoomStepSizeZ * (zoomHeight - _camera.localPosition.y) * Vector3.forward;

        if (!_isStarted) StartCoroutine(TweenPosition());
    }
    #endregion
    #region On event methods
    private void EnableMouseMove() => _doMouseMove = true;
    private void DisableMouseMove() => _doMouseMove = false;
    private void OnEnable()
    {
        base.ExclusivityСheck();
        transform.position = PlayerCore.Instance.transform.position;
        transform.rotation = Quaternion.identity;
        _camera = transform.GetChild(Constants.Player.CAMERA).transform;
        _camera.localPosition = _cameraPos;
        _camera.LookAt(this.transform);

        InputHandler.OnWheelRotate.AddListener(ZoomCamera);

        InputHandler.OnRMBPerformed.AddListener(EnableMouseMove);
        InputHandler.OnRMBCanceled.AddListener(DisableMouseMove);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    private void OnDisable()
    {
        InputHandler.OnWheelRotate.RemoveListener(ZoomCamera);

        InputHandler.OnRMBPerformed.RemoveListener(EnableMouseMove);
        InputHandler.OnRMBCanceled.RemoveListener(DisableMouseMove);
        StopCoroutine(TweenPosition());
    }
    #endregion
}
 
 
 */