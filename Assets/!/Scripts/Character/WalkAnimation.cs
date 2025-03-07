using UnityEngine;

public class WalkAnimation : MonoBehaviour, IUpdate
{
    [SerializeField] private float _smoothTime = .1f;
    [SerializeField] private float _maxSpeed = 10f;

    private Animator _animator;
    private Vector2 _currentVelocity = Vector2.zero;
    private Vector2 _newVelocity = Vector2.zero;
    private Vector2 _velocity = Vector2.zero;
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
        _newVelocity = LookOrientation.Direction;
        _currentVelocity = Vector2.SmoothDamp(_currentVelocity, _newVelocity, ref _velocity, _smoothTime, _maxSpeed);

        // float z = Mathf.Clamp(_currentVelocity.y, -1, 1);
        // float x = Mathf.Clamp(_currentVelocity.x, -1, 1);

        _animator.SetFloat(Constants.Player.Animations.Z_AXIS, _currentVelocity.y);
        _animator.SetFloat(Constants.Player.Animations.X_AXIS, _currentVelocity.x);
    }
    public void PerformFinalUpdate()
    {
        throw new System.NotImplementedException();
    }
    public void PerformLateUpdate()
    {
        throw new System.NotImplementedException();
    }
    #endregion
    private void Awake()
    {
        if (!this.transform.root.GetChild(0).transform.GetChild(0).transform.GetChild(0).transform.TryGetComponent<Animator>(out _animator))
            Debug.LogError($"{gameObject.name}, {this.GetType().Name}, the Animator is empty");
    }
    private void OnEnable()
    {
        Updater.Instance.RegisterUpdate(this, Updater.UpdateType.Update);
    }
    private void OnDisable()
    {
        Updater.Instance.UnregisterUpdate(this, Updater.UpdateType.Update);
    }
}

/* work with run aniations
 
     [SerializeField] private float _smoothTime = 10f;
    [SerializeField] private float _maxSpeed = 0f;

    private Animator _animator;
    private Vector2 _currentVelocity = Vector2.zero;
    private Vector2 _newVelocity = Vector2.zero;
    private Vector2 _velocity = Vector2.zero;

    private void Awake()
    {
        if (!this.transform.GetChild(0).transform.TryGetComponent<Animator>(out _animator))
            Debug.LogError($"{gameObject.name}, {this.GetType().Name}, the Animator is empty");
    }
    private void Update()
    {
        _newVelocity = new Vector2(InputHandler.WASDInput.x, InputHandler.WASDInput.y).normalized;
        _currentVelocity = Vector2.SmoothDamp(_currentVelocity, _newVelocity, ref _velocity, _smoothTime, _maxSpeed);

        float z = Mathf.Clamp(_currentVelocity.y, -1, 1);
        float x = Mathf.Clamp(_currentVelocity.x, -1, 1);

        _animator.SetFloat(Constants.Player.Animations.Z_AXIS, z);
        _animator.SetFloat(Constants.Player.Animations.X_AXIS, x);
    }
 
 
 */

/* rotate head

 
     private Transform _player;
    private Transform _playerNeck;
    private Transform _playerSpine;
    private float _y = 0f;
    private float _yCurrent = 0f;
    private void Awake()
    {

_player = PlayerCore.Instance.transform;
_playerNeck = this.transform.GetChild(0).transform.GetChild(1).transform.
    transform.GetChild(2).transform.transform.GetChild(0).
    transform.transform.GetChild(0).transform.transform.GetChild(1).transform;

_playerSpine = this.transform.GetChild(0).transform.GetChild(1).transform.transform.
    GetChild(2).transform.transform.GetChild(0).transform;
    }
    private void Update()
{
    _y = InputHandler.MouseInput.x * 3f * Time.deltaTime;

    if (_playerNeck.localEulerAngles.y + _y < 25f || _playerNeck.localEulerAngles.y + _y > 335f)
    {
        _playerNeck.localRotation = Quaternion.Euler(_playerNeck.localEulerAngles.x, _playerNeck.localEulerAngles.y + _y, _playerNeck.localEulerAngles.z);
        Debug.Log("1");
    }
    else if (_playerSpine.localEulerAngles.y + _y < 15f || _playerSpine.localEulerAngles.y + _y > 345f)
    {
        _playerSpine.localRotation = Quaternion.Euler(_playerSpine.localEulerAngles.x, _playerSpine.localEulerAngles.y + _y, _playerSpine.localEulerAngles.z);
        Debug.Log("2");
    }
    else
    {
        Debug.Log("3");
        _player.rotation = Quaternion.Euler(_player.eulerAngles.x, _player.eulerAngles.y + _y, _player.eulerAngles.z);
    }

}


*/

/*
 
         _oldVelocity = _newVelocity;
        _newVelocity = new Vector2(InputHandler.WASDInput.x, InputHandler.WASDInput.y).normalized;

        _currentVelocity = Vector2.SmoothDamp(_oldVelocity, _newVelocity, ref _velocity, _smoothTime, _maxSpeed);
        _currentVelocity = _currentVelocity.normalized;
        _animator.SetFloat(Constants.Player.Animations.Z_AXIS, _currentVelocity.y);
        _animator.SetFloat(Constants.Player.Animations.X_AXIS, _currentVelocity.x);
 
 */