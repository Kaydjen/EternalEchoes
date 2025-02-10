using UnityEngine; 

public class IsometricV : CameraCore, IUpdate
{
    #region Variables
    [SerializeField] private Vector3 _cameraHubOffset = new Vector3(0f, 0f, 0f);
    [SerializeField] private Vector3 _cameraOffset = new Vector3(0f, 30f, -10f);
    [SerializeField] private float _rotationSpeed = 850f;
    [SerializeField] private Transform _lookOrientation;

    private Camera _camera;
    private Vector3 _mousePos;
    private Vector3 _mouseScreenPosition;
    private Quaternion _targetRotation;
    private Vector3 _euler;
    private Transform _player;

    public float RotationSpeed
    {
        get
        {
            return _rotationSpeed;
        }
        set
        {
            _rotationSpeed = Mathf.Clamp(value, 0f, 100f);
        }
    }
    #endregion
    #region Update
    public void PerformInitialUpdate()
    {
        _mousePos = Input.mousePosition;
        _mousePos.z = _camera.WorldToScreenPoint(_lookOrientation.position).z;
        _mouseScreenPosition = _camera.ScreenToWorldPoint(_mousePos);

        if (_mouseScreenPosition == Vector3.zero) return;

        _targetRotation = Quaternion.LookRotation(_mouseScreenPosition - _lookOrientation.position);

        _euler = _targetRotation.eulerAngles;
        _euler.x = _lookOrientation.localRotation.eulerAngles.x;
        _euler.z = _lookOrientation.localRotation.eulerAngles.z;
        _targetRotation = Quaternion.Euler(_euler);

        _lookOrientation.localRotation = Quaternion.RotateTowards(_lookOrientation.localRotation, _targetRotation, _rotationSpeed * Time.deltaTime); // the last parameter is degrees per second
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
        transform.position = _player.position + _cameraHubOffset;
    }
    private void RegisterUpdate()
    {
        Updater.Instance.RegisterUpdate(this, Updater.UpdateType.InitialUpdate);
        Updater.Instance.RegisterUpdate(this, Updater.UpdateType.LateUpdate);
    }
    private void UnregisterUpdate()
    {
        Updater.Instance.UnregisterUpdate(this, Updater.UpdateType.InitialUpdate);
        Updater.Instance.UnregisterUpdate(this, Updater.UpdateType.LateUpdate);
    }
    #endregion
    #region private methods
    private void GetComponents()
    { 
        _camera = this.transform.GetChild(Constants.Player.CAMERA).transform.GetComponent<Camera>();
        _player = PlayerCore.Instance.transform;
    }
    private void SetCameraHubPosition()
    {
        transform.position = _lookOrientation.position;
        transform.rotation = Quaternion.identity;
    }
    private void SetPlayerPosition()
    {
        PlayerCore.Instance.transform.rotation = Quaternion.identity;
    }
    private void SetCameraPosition()
    {
        transform.GetChild(Constants.Player.CAMERA).transform.localPosition = _cameraOffset;
        transform.GetChild(Constants.Player.CAMERA).transform.LookAt(this.transform);
        transform.GetChild(Constants.Player.CAMERA).GetComponent<Camera>().orthographic = true;
    }
    #endregion
    #region MONO METHODS
    private void OnEnable()
    {
        base.ExclusivityСheck();

        GetComponents();
        SetCameraHubPosition();
        SetPlayerPosition();
        SetCameraPosition();
        RegisterUpdate();
    }
    private void OnDisable()
    {
        transform.GetChild(Constants.Player.CAMERA).GetComponent<Camera>().orthographic = false;
        UnregisterUpdate();
    }
    #endregion
}

/* for PlayerCore rotate
 * Replace code in Update and uncoment line in OnEnable
 
         mousePos = Input.mousePosition;
        mousePos.z = _camera.WorldToScreenPoint(_objToRotate.position).z;
        mouseScreenPosition = _camera.ScreenToWorldPoint(mousePos);

        if (mouseScreenPosition == Vector3.zero) return;

        targetRotation = Quaternion.LookRotation(mouseScreenPosition - _objToRotate.position);

        euler = targetRotation.eulerAngles;
        euler.x = _objToRotate.rotation.eulerAngles.x;
        euler.z = _objToRotate.rotation.eulerAngles.z;
        targetRotation = Quaternion.Euler(euler);

        _objToRotate.rotation = Quaternion.RotateTowards(_objToRotate.rotation, targetRotation, _speed * Time.deltaTime); // the last parameter is degrees per second
        transform.position = _objToRotate.position;
 
 
 */