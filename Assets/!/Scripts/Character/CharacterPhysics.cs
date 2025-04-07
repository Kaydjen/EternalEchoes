using UnityEngine;

public class CharacterPhysics : MonoBehaviour, ICameraUpdate, IUpdate
{
    #region Serialize Variables 
    [Header("GroundCheck")]
    [Tooltip("слой земли (Layer)")]
    [SerializeField] private LayerMask _groundLayerMask; // слой земли (Layer)
    [Tooltip("радиус сферы проверяющей стоит ли плеер на земле")]
    [SerializeField] private float _groundCheckRadius = 0.6f; // радиус сферы проверяющей стоит ли плеер на земле

    [Header("Gravity Settings")]
    [SerializeField] private float _gravity = 10f;
    [SerializeField] private float _mass = 18f;
    [SerializeField] private float _jumpForce = 35f;
    #endregion
    #region Private Variables
    private Transform _groundCheckPosition; // позиция от куда осузествляеться проверка на землю
    private CharacterController _controller;
    private bool _isGrounded; // стоит ли на земле обьект
    private float _verticalVelocity;
    private float _fallAcceleration;
    private bool _isJumping = false;
    #endregion
    #region PUBLIC
    public void UpdateNeededComponents()
    {
        if (!PlayerCore.Instance.transform.TryGetComponent(out _controller))
            Debug.LogError($"Script MovePlayer | Game obj {gameObject.name} | Missed component CharacterController");

        _groundCheckPosition = PlayerCore.Instance.Component.GetGroundCheck();
    }
    #endregion
    #region Private
    private void Jump()
    {
        if (_isGrounded)// chack if the player staying on the ground and can jump
        {
            _verticalVelocity = _jumpForce;
            _isJumping = true;
        }
    }
    #endregion
    #region Update
    public void PerformInitialUpdate()
    {
        // check if the player is staing on the ground
        _isGrounded = Physics.CheckSphere(_groundCheckPosition.position, _groundCheckRadius, _groundLayerMask);

        if (_controller.isGrounded) // if on the ground
        {
            _verticalVelocity = (-_gravity * _mass * Time.deltaTime); // gravitation is default

            _fallAcceleration = 0f; // acceleration of the player falling sets to the default because he is staying on the ground
            _isJumping = false; // make sure player isn`t jumping now
        } // _controller.isGrounded
        else if (_isJumping) // if the player is jumping
        {
            if (_verticalVelocity < (_jumpForce / 100) * 48) // last 48% of the player jump distance set gravity lower 
            {
                _verticalVelocity -= (_gravity * _mass * Time.deltaTime) / 2.68f;
            }
            else // other parts of jump
            {
                if (_verticalVelocity > 0) // when the player is rising up 
                {
                    _fallAcceleration += Time.deltaTime;
                    _verticalVelocity -= (_gravity * _mass * Time.deltaTime) + _fallAcceleration;
                }
                else // when the player is falling down after the jump is over
                {
                    _fallAcceleration += Time.deltaTime;
                    _verticalVelocity -= ((_gravity * _mass * 1.8f * (Time.deltaTime)) + _fallAcceleration) * Time.deltaTime;
                }
            } // else
        } // _isJumping
        else if (!_controller.isGrounded && !_isJumping) // is the player just fall down without jumping, like from the rock
        {
            _fallAcceleration += Time.deltaTime;
            //_verticalVelocity -= ((_gravity * _mass * 1.8f * (Time.deltaTime)) + _fallAcceleration) * Time.deltaTime;
            _verticalVelocity -= ((_gravity * _mass * 2.96f * Mathf.Sqrt(Time.deltaTime)) + _fallAcceleration) * Time.deltaTime;
        }

        _controller.Move(new Vector3(0f, _verticalVelocity, 0f) * Time.deltaTime); // apply all physics to the player
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
    #region MONOBEHAVIOUR
    private void OnEnable()
    {
        InputManager.OnJump.AddListener(Jump);
        UpdateNeededComponents();
        RegisterUpdate();
    }
    private void OnDisable()
    {
        InputManager.OnJump.RemoveListener(Jump);
        UnregisterUpdate();
    }
    #endregion
}
