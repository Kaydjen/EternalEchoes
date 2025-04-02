using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class SwitchPanelMenuRotate : MonoBehaviour
{
    [SerializeField] private List<Transform> _listOfPositions;
    [SerializeField] private float _speed = 10f;
    [SerializeField] private float _rotationThreshold = 0.1f; // Порог для завершения поворота
    private CameraMenuRotate _rotator;
    private Coroutine _coroutine;
    private void Awake()
    {
        _rotator = GetComponent<CameraMenuRotate>();
    }

    public void SwitchToPanel(int number)
    {
        if (number < 0 || number >= _listOfPositions.Count)
        {
            Debug.LogError($"Invalid panel number: {number}");
            return;
        }

        if (_coroutine != null) StopCoroutine(_coroutine);
        _coroutine = StartCoroutine(SmoothRotation(number));
    }

    private IEnumerator SmoothRotation(int number)
    {
        if (_rotator != null)
            _rotator.enabled = false;

        Vector3 direction = _listOfPositions[number].position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(direction);

        while (Quaternion.Angle(transform.rotation, targetRotation) > _rotationThreshold)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * _speed);
            yield return null;
        }

        // Гарантированно устанавливаем конечный поворот
        transform.rotation = targetRotation;

        if (_rotator != null)
            _rotator.enabled = true;
    }
}