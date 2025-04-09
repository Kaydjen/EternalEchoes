using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMenuRotate : MonoBehaviour
{
    [Header("Target Settings")]
    [SerializeField] private Transform[] _targets;
    [SerializeField] private float _angleThreshold = 15f;
    [SerializeField] private float _lockOnSmoothness = 5f;

    [Space(5)]
    [Header("Rotation Settings")]
    [SerializeField] private float _rotationSpeed = 2f;
    [SerializeField][Range(0f, 50f)] private float _sensitivity = 1f;
    [SerializeField][Range(0f, 20f)] private float _sensitivityX = 0.5f;
    [SerializeField][Range(0f, 50f)] private float _sensitivityDrag = 20f;
    [SerializeField][Range(-40f, -1f)] private float _minVerticalRotation = -5f;
    [SerializeField][Range(1f, 40f)] private float _maxVerticalRotation = 5f;

    [Space(5)]
    [Header("Additional settings")]
    [SerializeField][Range(.01f, 40f)] private float _timeAfterDragNothingWorks = .2f;

    private float _currentYRotation;
    private float _currentXRotation;
    private Transform _lockedTarget = null;
    private Vector2 _mouseDelta;
    private bool _isManualRotating = false;
    private bool _tutu = false;
    private bool _isNothingWorks = false;
    private bool _isDragging = false;

    private void OnEnable()
    {
        // При включении скрипта синхронизируем текущие значения с реальным вращением камеры
        _currentXRotation = transform.rotation.eulerAngles.x;
        _currentYRotation = transform.rotation.eulerAngles.y;

        // Корректируем углы, если они выходят за пределы -180..180
        if (_currentXRotation > 180) _currentXRotation -= 360;
        if (_currentYRotation > 180) _currentYRotation -= 360;
    }

    private void Start()
    {
        OnEnable(); 
    }

    private void Update()
    {
        HandleInput();
        HandleTargetLock();
        ApplyRotation();
    }

    private void HandleInput()
    {
        _mouseDelta = Mouse.current.delta.ReadValue();

        if (Mouse.current.leftButton.isPressed)
        {
            _tutu = false;
            _isManualRotating = true;
            _isDragging = true;
            _isNothingWorks = true;
            _lockedTarget = null;

            _currentYRotation -= _mouseDelta.x * _sensitivityDrag * Time.deltaTime;
            //_currentXRotation -= _mouseDelta.y * _sensitivityDrag * Time.deltaTime;
        }
        else if (_mouseDelta.magnitude > 0.1f && !_isNothingWorks)
        {
            _tutu = true;
            _isManualRotating = true;
            _lockedTarget = null;

            _currentYRotation += _mouseDelta.x * _sensitivityX * Time.deltaTime;
            _currentXRotation -= _mouseDelta.y * _sensitivity * Time.deltaTime;
        }
        else
        {
            _isManualRotating = false;
            if (_isDragging)
            {
                Invoke(nameof(EnableMouse), _timeAfterDragNothingWorks);
                _isDragging = false;
            }
        }

        _currentXRotation = Mathf.Clamp(_currentXRotation, _minVerticalRotation, _maxVerticalRotation);
    }

    private void HandleTargetLock()
    {
        if (_tutu) return;
        if (_isManualRotating) return;

        Transform nearestTarget = null;
        float minAngle = float.MaxValue;

        foreach (Transform target in _targets)
        {
            if (target == null) continue;

            Vector3 direction = (target.position - transform.position).normalized;
            float angle = Vector3.Angle(transform.forward, direction);

            if (angle < _angleThreshold && angle < minAngle)
            {
                minAngle = angle;
                nearestTarget = target;
            }
        }

        if (nearestTarget != null)
        {
            _lockedTarget = nearestTarget;

            Vector3 targetDirection = (_lockedTarget.position - transform.position).normalized;
            Quaternion targetLookRotation = Quaternion.LookRotation(targetDirection);

            _currentYRotation = Mathf.LerpAngle(_currentYRotation, targetLookRotation.eulerAngles.y,
                                              _lockOnSmoothness * Time.deltaTime);
            _currentXRotation = Mathf.LerpAngle(_currentXRotation, targetLookRotation.eulerAngles.x,
                                              _lockOnSmoothness * Time.deltaTime);
        }
    }

    private void ApplyRotation()
    {
        Quaternion targetRotation = Quaternion.Euler(_currentXRotation, _currentYRotation, 0f);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation,
                                           _rotationSpeed * Time.deltaTime);
    }

    private void EnableMouse() => _isNothingWorks = false;
}



/*
 
         if(_tutu) return;
        if (_isManualRotating) return;

        Transform nearestTarget = null;
        float minAngle = float.MaxValue;

        foreach (Transform target in Targets)
        {
            if (target == null) continue;

            Vector3 direction = (target.position - transform.position).normalized;
            float angle = Vector3.Angle(transform.forward, direction);

            if (angle < _angleThreshold && angle < minAngle)
            {
                minAngle = angle;
                nearestTarget = target;
            }
        }
 
 */
/*
 

    [SerializeField][Range(0f, 100f)] private float _sensitivity = 3f;
    [SerializeField][Range(0f, 100f)] private float _sensitivityX = .5f;
    [SerializeField][Range(0f, 100f)] private float _sensitivityDrag = 3f;
    [SerializeField][Range(-80f, -10f)] private float _minHeadRotation = -65f;
    [SerializeField][Range(10f, 80f)] private float _maxHeadRotation = 65f;
    [SerializeField][Range(0f, 100f)] private float _rotationSpeed = 5f;
    private float _y = 0f;
    private float _x = 0f;

    private void Update()
    {
        if (Mouse.currentValue.leftButton.isPressed)
        {
            _x -= Mouse.currentValue.delta.ReadValue().x * _sensitivityDrag * Time.deltaTime;
            _y += Mouse.currentValue.delta.ReadValue().y * _sensitivityDrag * Time.deltaTime;
            _y = Mathf.Clamp(_y, _minHeadRotation, _maxHeadRotation);
        }
        else
        {
            _y -= Mouse.currentValue.delta.ReadValue().y * _sensitivity * Time.deltaTime;
            _x += Mouse.currentValue.delta.ReadValue().x * _sensitivityX * Time.deltaTime;
            _y = Mathf.Clamp(_y, _minHeadRotation, _maxHeadRotation);
        }
        for()
        if (Mathf.Abs(_x - targetNumber) <= range)
        Quaternion _targetRotation = Quaternion.Euler(_y, _x, 0f);
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            _targetRotation,
            Time.deltaTime * _rotationSpeed
        );
    } 





 
 */