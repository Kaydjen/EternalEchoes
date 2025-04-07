using UnityEngine;

public class DirectControlMovement : MonoBehaviour, IUpdate
{
    [SerializeField] private float _speed = 7f;
    private CharacterController _controller;
    private Transform _direction;
    private Vector3 _velocity;
    public float Speed
    {
        get
        {
            return _speed;
        }
        set
        {
            _speed = Mathf.Clamp(value, 0f, 100f);
        }
    }
    #region Update
    public void PerformInitialUpdate()
    {
        throw new System.NotImplementedException();
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
        _velocity = (_direction.right * InputManager.WASDInput.x + _direction.forward * InputManager.WASDInput.y).normalized;
        _controller.Move(_velocity * _speed * Time.deltaTime);
    }
    public void PerformLateUpdate()
    {
        throw new System.NotImplementedException();
    }
    #endregion
    public void Awake()
    {
        _controller = this.transform.root.transform.GetComponent<CharacterController>();
        if (_controller == null)
            Debug.LogError($"{gameObject.name}, {this.GetType().Name}, the CharacterController is empty");
        else
            _controller.enabled = true;

        if (CameraSwitcher.Instance != null) GetCamera();
        else InitEvents.OnCameraSwitcherReady?.AddListener(GetCamera);
    }
    private void OnEnable()
    {
        if(_controller != null) _controller.enabled = true;
        if(_direction != null)
            Updater.Instance?.RegisterUpdate(this, Updater.UpdateType.FinalUpdate);
    }
    private void OnDisable()
    {
        if (_controller != null) _controller.enabled = false;
        Updater.Instance?.UnregisterUpdate(this, Updater.UpdateType.FinalUpdate);
    }
    private void GetCamera()
    {
        _direction = CameraSwitcher.Instance?.transform;
        // Enable Movement If Can
        if (this.enabled) Updater.Instance?.RegisterUpdate(this, Updater.UpdateType.FinalUpdate);
    }
}


/*
 
     private void OnEnable()
    {
        if(_controller != null) _controller.enabled = true;

        Updater.Instance.RegisterUpdate(this, Updater.UpdateType.FinalUpdate);
        CameraSwitcher.OnFPV_Enable.AddListener(EnableFPVMovement);
        DisableFPVMovement(); // TODO: тут тоже переделай, система сырая и корявая, работай
    }
    private void OnDisable()
    {
        if (_controller != null) _controller.enabled = false;

        Updater.Instance.UnregisterUpdate(this, Updater.UpdateType.FinalUpdate);
        CameraSwitcher.OnIsometricV_Enable.AddListener(DisableFPVMovement);
    }
 
 
 */