using Unity.VisualScripting;
using UnityEngine;

[ComponentInfo("FPV Камера",
    "Відповідає за управління камерою від першої особи (FPV). " +
    "Інтерпретує введення з миші для керування обертами камери. " +
    "Містить механізми для налаштування позицій камери та об'єкта гравця, а також для оновлення кожного кадру. " +
    "Підтримує підключення до системи оновлень.")]
public class FPV : CameraCore, IUpdate, ICameraUpdate
{
    #region VARIABLES
    [SerializeField] private Transform _lookOrientation;
    [Space(7)]
    [SerializeField] private Vector3 _cameraOffset = new Vector3(0f, .8f, .5f);
    [SerializeField][Range(.25f, 10f)] private float _sensitivity = 3f;
    private Transform _player;
    private Transform _camera;
    private float _y = 0f;
    private float _x = 0f;
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
    /// Updates the sensitivity value for the hub.
    /// </summary>
    /// <param name="value">The new sensitivity value. It should be about 3f</param>
    public void ChangeSensitivity(float value) // TODO: add some logic here (ChangeSensitivity)
    {
        _sensitivity = value;
    }
    /// <summary>
    /// 
    /// </summary>
    public void UpdateNeededComponents() // TODO: we dont need to get camera here, it's better to do in Init method
    {
        _player = PlayerCore.Instance.transform;
        _camera = transform.GetChild(Constants.Player.CAMERA).transform;
    }
    #endregion
    #region Update
    public void PerformInitialUpdate()
    {
        // Manage Input
        _y -= InputHandler.MouseInput.y * _sensitivity * Time.deltaTime;
        _x += InputHandler.MouseInput.x * _sensitivity * Time.deltaTime;
        _y = Mathf.Clamp(_y, -45f, 45f);

        // Manage Rotation
        transform.rotation = Quaternion.Euler(0f, _x, 0f);
        _camera.localRotation = Quaternion.Euler(_y, 0f, 0f);

        _lookOrientation.rotation = Quaternion.Euler(0f, _x, 0f);
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

        // Camera Hub Position
        transform.position = _player.position;
        transform.rotation = _player.rotation;

        // Camera Position
        _camera.localPosition = _cameraOffset;
        _camera.localRotation = Quaternion.identity;

        // TODO: тут с обнулением бади есть вопросец, а нахера это надо, стоит задуматся (это надо, но возможно не тут)
        // Player Body Position
        _player.GetChild(Constants.Player.BODY_ANIMATIONS).transform.localPosition = Vector3.zero;
        _player.GetChild(Constants.Player.BODY_ANIMATIONS).transform.localRotation = Quaternion.identity;

        RegisterUpdate();
    }
    private void OnDisable()
    {
        UnregisterUpdate();
    }
    #endregion
}
