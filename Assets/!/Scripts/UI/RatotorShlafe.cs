using UnityEngine;
using System.Collections.Generic;

public class RatotorShlafe : MonoBehaviour
{
    [Header("Make sure the width is int")]
    [SerializeField] private RectTransform _barTransform;
    [SerializeField] private Transform _placeToInstantiate;
    [SerializeField] private Transform _origin; // camera
    [SerializeField] private Transform _target;
    [SerializeField] private List<RectTransform> _pointsListInspector = new();
    [SerializeField] private int _widthBtwPoints;
    [SerializeField] private int _pointsWidth;

    private List<RectTransform> _pointsList = new();
    private Vector3 _directionToTarget;
    private float _oldNormalizedPosition = 0f;
    private float _barWidth;
    private float _halfBarWidth;
    private bool _isInitialized = false;

    private void Awake()
    {
        // Проверка на null и валидность параметров
        if (_barTransform == null || _placeToInstantiate == null || _origin == null || _target == null)
        {
            Debug.LogError("One or more required transforms are not assigned!");
            return;
        }

        if (_pointsListInspector.Count == 0 || _widthBtwPoints <= 0 || _pointsWidth <= 0)
        {
            Debug.LogError("Invalid setup: empty points list or non-positive width values!");
            return;
        }

        _barWidth = _barTransform.rect.width;
        if (_barWidth <= 0)
        {
            Debug.LogError("Bar width must be positive!");
            return;
        }

        // Рассчитываем количество точек с учетом их ширины и промежутков
        float totalUnitWidth = _widthBtwPoints + _pointsWidth;
        int countToPlace = Mathf.Max(1, Mathf.FloorToInt((_barWidth + _widthBtwPoints) / totalUnitWidth));

        // Создаем точки, циклически используя шаблоны из _pointsListInspector
        for (int i = 0; i < countToPlace; i++)
        {
            int templateIndex = i % _pointsListInspector.Count;
            RectTransform point = Instantiate(_pointsListInspector[templateIndex], _placeToInstantiate);
            _pointsList.Add(point);
        }

        // Распределяем точки равномерно внутри бара
        _halfBarWidth = _barWidth * 0.5f;
        float effectiveWidth = _barWidth - _pointsWidth; // Учитываем ширину последней точки
        float step = (countToPlace > 1) ? effectiveWidth / (countToPlace - 1) : 0f;

        for (int i = 0; i < _pointsList.Count; i++)
        {
            float xPos = -_halfBarWidth + i * step;
            _pointsList[i].anchoredPosition = new Vector2(xPos, 0f);
        }

        _isInitialized = true;
    }

    private void Update()
    {
        if (!_isInitialized) return;

        _directionToTarget = _target.position - _origin.position;
        float signedAngle = Vector3.SignedAngle(
            new Vector3(_origin.forward.x, 0, _origin.forward.z),
            new Vector3(_directionToTarget.x, 0, _directionToTarget.z),
            Vector3.up);

        float normalizedPosition = signedAngle / 180f;
        float difference = normalizedPosition - _oldNormalizedPosition;
        if (Mathf.Approximately(difference, 0f)) return;

        _oldNormalizedPosition = normalizedPosition;
        float movement = difference * _barWidth;

        foreach (RectTransform point in _pointsList)
        {
            float newPos = point.anchoredPosition.x + movement;

            // Перенос точки при выходе за границы бара
            if (newPos < -_halfBarWidth)
            {
                newPos += _barWidth;
            }
            else if (newPos > _halfBarWidth)
            {
                newPos -= _barWidth;
            }

            point.anchoredPosition = new Vector2(newPos, 0f);
        }
    }
}






/*
 
 [Header("Make sure the width is int")]
  [SerializeField] private RectTransform _barTransform;
  [SerializeField] private Transform _placeToInstantiate;
  [SerializeField] private Transform _origin; // camera
  [SerializeField] private Transform _target;
  [SerializeField] private List<RectTransform> _pointsListInspector = new();
  [SerializeField] private int _widthBtwPoints;
  [SerializeField] private int _pointsWidth; 

  private List<RectTransform> _pointsList = new();
  private Vector3 _directionToTarget;
  private float _oldNormalizedPosition = 0f;
  private float _barWidth;
  private float _halfBarWidth;

  private void Awake()
  {
      if (_pointsListInspector.Count == 0 || _widthBtwPoints <= 0)
      {
          Debug.LogError("Invalid setup: empty points list or zero width between points");
          return;
      }

      _barWidth = _barTransform.rect.width;
      int countToPlace = Mathf.FloorToInt(_barWidth / (_widthBtwPoints + _pointsWidth));

      // Create the required number of points
      int pointsNeeded = countToPlace;
      int pointsPerCycle = _pointsListInspector.Count;
      int cycles = Mathf.CeilToInt((float)pointsNeeded / pointsPerCycle);

      for (int i = 0; i < cycles; i++)
      {
          foreach (RectTransform el in _pointsListInspector)
          {
              if (_pointsList.Count >= pointsNeeded) break; 
              RectTransform t = Instantiate(el, _placeToInstantiate);
              _pointsList.Add(t);
          }
      }

      // distribute dots evenly within the bar
      _halfBarWidth = _barWidth * 0.5f;
      float step = _barWidth / (countToPlace - 1); // step between points

      for (int i = 0; i < _pointsList.Count; i++)
      {
          float xPos = -_halfBarWidth + i * step;
          _pointsList[i].anchoredPosition = new Vector2(xPos, 0f);
      }
  }
  private void Update()
  {
      _directionToTarget = _target.position - _origin.position;
      float signedAngle = Vector3.SignedAngle(
          new Vector3(_origin.forward.x, 0, _origin.forward.z),
          new Vector3(_directionToTarget.x, 0, _directionToTarget.z),
          Vector3.up);
      
      float normalizedPosition = signedAngle / 180f;
      float difference = normalizedPosition - _oldNormalizedPosition;
      if (difference == 0) return;
      _oldNormalizedPosition = normalizedPosition;
      float wrappedPosition;
      float newPos;
      foreach (RectTransform el in _pointsList)
      {
          newPos = el.anchoredPosition.x + difference;
          wrappedPosition = newPos;
          if (Mathf.Abs(newPos) > _halfBarWidth)
          {
              if (newPos < -_halfBarWidth) wrappedPosition += _barWidth;
              else if (newPos > _halfBarWidth) wrappedPosition -= _barWidth;
          }
          el.anchoredPosition = new Vector3(wrappedPosition, 0f);
      }
  }
 
 */




