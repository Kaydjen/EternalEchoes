using UnityEngine;
using System.Collections.Generic;
using System;
using System.Net.Sockets;

public class UICampas : MonoBehaviour
{
    [SerializeField] private List<TransformRectTransform> _points;
    [SerializeField][Range(0.1f, 2f)] private float _pointDensity = 1f; 
    [SerializeField][Range(0.1f, 5f)] private float _fadeWidth = 1f; // NOTE: just play with it, because this value makes everything extremely different 
    [SerializeField] private RectTransform _compassBarTransform;
    [SerializeField] private Transform _cameraObjectTransform;

    private void Update() => UpdateMarkers();
    private void UpdateMarkers()
    {
        float barWidth = _compassBarTransform.rect.width;
        float halfWidth = barWidth * 0.5f;
        float spacing = barWidth / (_points.Count * _pointDensity);

        for (int i = 0; i < _points.Count; i++)
        {
            TransformRectTransform el = _points[i];
            Vector3 directionToTarget = el.TrackedObject.position - _cameraObjectTransform.position;
            float signedAngle = Vector3.SignedAngle(
                new Vector3(_cameraObjectTransform.forward.x, 0, _cameraObjectTransform.forward.z),
                new Vector3(directionToTarget.x, 0, directionToTarget.z),
                Vector3.up);

            float normalizedPosition = signedAngle / 180f;
            float mainPosition = normalizedPosition * halfWidth;
            float wrappedPosition = mainPosition;

            if (mainPosition < -halfWidth) wrappedPosition += barWidth;
            else if (mainPosition > halfWidth) wrappedPosition -= barWidth;

            el.MarkerTransform.anchoredPosition = new Vector2(wrappedPosition, 0);

            float distanceFromCenter = Mathf.Abs(normalizedPosition);
            float alpha = Mathf.Clamp01(1f - distanceFromCenter * _fadeWidth);

            if (el.MarkerTransform.TryGetComponent(out UIAlphaController img))
            {
                img.ChangeAlpha(alpha);
            }

            el.MarkerTransform.gameObject.SetActive(alpha > 0.05f);
        }
    }
}



public class RatotorShlafe : MonoBehaviour
{
    [Header("Make sure the wight is int")]
    [SerializeField] private RectTransform _barTransform; 
    [SerializeField] private List<RectTransform> _pointsListInspector;
    [SerializeField] private int _widthBtwPoints;
    private List<RectTransform> _pointsList;
    private void Awake()
    {
        int countToPlace = (int)_barTransform.rect.width / _widthBtwPoints;
        int count = countToPlace / _pointsListInspector.Count;
        for (int i = 0; i < count; i++)
        {            
            foreach (RectTransform t in _pointsListInspector)
            {
                _pointsList.Add(t);
            }
        }
        float value = 1 / countToPlace; 
        for (int i = 0; i < countToPlace; i++)
        {
            _pointsList[i].anchoredPosition = new Vector2(value * i, 0);
        }

    }
/*    private void Update()
    {
        foreach(RectTransform el in _pointsList)
        {

        }   
    }*/
}




















/*
 1. Направление от нашей позиции до обьекта
 2. signedAngle между вектором нашего направления и вектором направления на цель, тобиж угол поворота в градусах от -180 до 180 от нашего направления до цели. Если мы смотрим на север, а обьект стоит на востоке, то значение signedAngle будет 90
 3. Нормализуем позицию деля полученный signedAngle на 180 градусов. Получиться значение от -1 до 1. (-150 / 180 = -.8)
 4. Вычисляем позицию маркера на мапе (нормализированная позиция * половину ширины компаса)
 5. Создаем зацикленные позиции посредством проверки, уходит ли маркер за пределы границы (-105), если да, то к позиции добавляем полную ширину компаса (-105 + 200 = 95, теперь точка справа, а не слева и располагаеться в пределах допустимой зоны)
 6. Ставим маркер в нужную позицию посредством его якоря
 7. Вычисляем прозрачность
 8. Устанавливаем прозрачность
 9. Проверяем прозрачность, если она больше минимального значения (.05f) то активируем маркер, иначе выключаем
 */
















/*   
    public RectTransform _compassBarTransform;
    public RectTransform MarkerTransform;
    public Transform _cameraObjectTransform;
    public List<Transform> _points ;

    void Update()
    {
        foreach (Transform t in _points) 
            SetMarkerPosition(MarkerTransform, t.position);
    }
    private void SetMarkerPosition(RectTransform markerTransform, Vector3 worldPosition)
    {
        Vector3 directionToTarget = worldPosition - _cameraObjectTransform.position;
        float signedAngle = Vector3.SignedAngle(new Vector3(_cameraObjectTransform.forward.x, 0, _cameraObjectTransform.forward.z), new Vector3(directionToTarget.x, 0, directionToTarget.z), Vector3.up);
        float compassPosition = Mathf.Clamp(signedAngle / Camera.main.fieldOfView, -0.5f, 0.5f);
        markerTransform.anchoredPosition = new Vector2(_compassBarTransform.rect.width * compassPosition, 0);
    }

 */