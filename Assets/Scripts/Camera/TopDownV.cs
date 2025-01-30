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
        /* Debug.Log($"_maxLeft {_maxLeft}");
           Debug.Log($"_maxRight {_maxRight}");
           Debug.Log($"_maxDown {_maxDown}");
           Debug.Log($"_maxUp {_maxUp}"); */
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
