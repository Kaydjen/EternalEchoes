using UnityEngine;

public class FPV : CameraCore, IUpdate
{
    #region Variables
    [SerializeField] private Vector3 _cameraHubOffset = new Vector3(0f, .7f, 0f);
    [SerializeField] private Vector3 _cameraOffset = new Vector3(0f, .15f, .5f);
    [SerializeField][Range(.25f,5f)] private float _sensitivity = 3f;
    [SerializeField] private Transform _lookOrientation;
    [SerializeField] private Transform _walkDirection;

    private Transform _player;
    private Transform _camera;
    private float _y = 0f; 
    private float _x = 0f;
    #endregion
    #region Update
    public void PerformInitialUpdate()
    {
        ManageInput();
        ManageRotation();
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
    #region Methods
    private void GetComponents()
    {
        _player = PlayerCore.Instance.transform;
        _camera = transform.GetChild(Constants.Player.CAMERA).transform;
    }
    private void SetPositions()
    {
        // Camera Hub Position
        transform.position = _player.position + _cameraHubOffset;
        transform.rotation = _player.rotation;

        // Player Body Position
        _player.GetChild(Constants.Player.BODY_ANIMATIONS).transform.localPosition = Vector3.zero;
        _player.GetChild(Constants.Player.BODY_ANIMATIONS).transform.localRotation = Quaternion.identity;

        // Camera Position
        _camera.localPosition = _cameraOffset;
        _camera.localRotation = Quaternion.identity;
    }
    private void ManageInput()
    {
        _y -= InputHandler.MouseInput.y * _sensitivity * Time.deltaTime;
        _x += InputHandler.MouseInput.x * _sensitivity * Time.deltaTime;
        _y = Mathf.Clamp(_y, -85f, 85f);
    }
    private void ManageRotation()
    {
        transform.rotation = Quaternion.Euler(0f, _x, 0f);
        _walkDirection.rotation = Quaternion.Euler(0f, _x, 0f);
        _camera.localRotation = Quaternion.Euler(_y, 0f, 0f);
        _lookOrientation.rotation = Quaternion.Euler(_y, _x, 0f);
    }
    #endregion
    #region OnEvent
    private void OnEnable()
    {
        base.ExclusivityСheck();

        GetComponents();
        SetPositions();
        RegisterUpdate();
    }
    private void OnDisable()
    {
        UnregisterUpdate();
    }
    #endregion
}
