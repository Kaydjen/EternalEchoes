using UnityEngine; 

public class IsometricV : CameraCore, IUpdate, ICameraUpdate
{
    #region VARIABLES
    [SerializeField] private Transform _lookOrientation;
    [Space(7)]
    [SerializeField] private Vector3 _cameraOffset = new Vector3(0f, 30f, -10f);
    [SerializeField] private float _rotationSpeed = 850f;
    private Camera _camera;
    private Vector3 _mousePos;
    private Vector3 _mouseScreenPosition;
    private Quaternion _targetRotation;
    private Vector3 _euler;
    private Transform _player;
    #endregion
    #region PUBLIC METHODS
    /// <summary>
    /// It update the camera position according to requirements
    /// </summary>
    /// <param name="value">The new offset value to apply to the camera. Don't forget to make z axis about .5f</param>
    public void ChangeCameraOffset(Vector3 value) // TODO: add some logic here (ChangeHubOffset)
    {
        _cameraOffset = value;
    }
    /// <summary>
    /// Updates the RotationSpeed value for the hub.
    /// </summary>
    /// <param name="value">The new RotationSpeed value. It should be about 850ff</param>
    public void ChangeRotationSpeed(float value) // TODO: add some logic here (RotationSpeed)
    {
        _rotationSpeed = value;
    }
    public void UpdateNeededComponents()// TODO: we dont need to get camera here, it's better to do in Init method
    {
        _camera = this.transform.GetChild(Constants.Player.CAMERA).transform.GetComponent<Camera>();
        _player = PlayerCore.Instance.transform;
    }
    #endregion
    #region Update
    public void PerformInitialUpdate()
    {
        _mousePos = Input.mousePosition;
        _mousePos.z = _camera.WorldToScreenPoint(this.transform.position).z;
        _mouseScreenPosition = _camera.ScreenToWorldPoint(_mousePos);

        if (_mouseScreenPosition == Vector3.zero) return;

        _targetRotation = Quaternion.LookRotation(_mouseScreenPosition - this.transform.position);

        _euler = _targetRotation.eulerAngles;
        _euler.x = 0f;
        _euler.z = 0f;
        _targetRotation = Quaternion.Euler(_euler);

        _lookOrientation.rotation = Quaternion.RotateTowards(_lookOrientation.rotation, _targetRotation, _rotationSpeed * Time.deltaTime); // the last parameter is degrees per second
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
        transform.position = _player.position;
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
    #region MONO METHODS
    private void OnEnable()
    {
        base.ExclusivityСheck();

        // Set Camera Hub Position
        transform.position = _lookOrientation.position;
        transform.rotation = Quaternion.identity;

        // Set Camera Position
        transform.GetChild(Constants.Player.CAMERA).transform.localPosition = _cameraOffset;
        transform.GetChild(Constants.Player.CAMERA).transform.LookAt(this.transform);
        transform.GetChild(Constants.Player.CAMERA).GetComponent<Camera>().orthographic = true;

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