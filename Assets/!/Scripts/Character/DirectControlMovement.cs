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
        _velocity = (_direction.right * InputHandler.WASDInput.x + _direction.forward * InputHandler.WASDInput.y).normalized;
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
        _controller.enabled = true;
    }
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
    private void EnableFPVMovement() => _direction = CameraSwitcher.Instance.transform;
    private void DisableFPVMovement() => _direction = this.transform.root.transform;
}
