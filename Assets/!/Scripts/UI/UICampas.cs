using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine.UI;

public class UICampas : MonoBehaviour
{
    [SerializeField] private List<TransformRectTransform> _points;
    [SerializeField][Range(0.1f, 2f)] private float _pointDensity = 1f; 
    [SerializeField][Range(0.1f, 5f)] private float _fadeWidth = 1f; // NOTE: just play with it, because this value makes everything extremely different 
    public RectTransform compassBarTransform;
    public Transform cameraObjectTransform;

    void Update()
    {
        UpdateMarkers();
    }

    private void UpdateMarkers()
    {
        float barWidth = compassBarTransform.rect.width;
        float halfWidth = barWidth * 0.5f;
        float spacing = barWidth / (_points.Count * _pointDensity);

        for (int i = 0; i < _points.Count; i++)
        {
            TransformRectTransform el = _points[i];
            Vector3 directionToTarget = el.PointsOnMap.position - cameraObjectTransform.position;
            float signedAngle = Vector3.SignedAngle(
                new Vector3(cameraObjectTransform.forward.x, 0, cameraObjectTransform.forward.z),
                new Vector3(directionToTarget.x, 0, directionToTarget.z),
                Vector3.up);

            float normalizedPosition = signedAngle / 180f;
            float mainPosition = normalizedPosition * halfWidth;
            float wrappedPosition = mainPosition;

            if (mainPosition < -halfWidth) wrappedPosition += barWidth;
            else if (mainPosition > halfWidth) wrappedPosition -= barWidth;

            el.objectiveMarkerTransform.anchoredPosition = new Vector2(wrappedPosition, 0);

            float distanceFromCenter = Mathf.Abs(normalizedPosition);
            float alpha = Mathf.Clamp01(1f - distanceFromCenter * _fadeWidth);

            if (el.objectiveMarkerTransform.TryGetComponent(out Image img))
            {
                Color cl = img.color;
                cl.a = alpha;
                img.color = cl;
            }

            el.objectiveMarkerTransform.gameObject.SetActive(alpha > 0.05f);
        }
    }
}

[Serializable]
public class TransformRectTransform
{
    public RectTransform objectiveMarkerTransform;
    public Transform PointsOnMap;
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
    public RectTransform compassBarTransform;
    public RectTransform objectiveMarkerTransform;
    public Transform cameraObjectTransform;
    public List<Transform> _points ;

    void Update()
    {
        foreach (Transform t in _points) 
            SetMarkerPosition(objectiveMarkerTransform, t.position);
    }
    private void SetMarkerPosition(RectTransform markerTransform, Vector3 worldPosition)
    {
        Vector3 directionToTarget = worldPosition - cameraObjectTransform.position;
        float signedAngle = Vector3.SignedAngle(new Vector3(cameraObjectTransform.forward.x, 0, cameraObjectTransform.forward.z), new Vector3(directionToTarget.x, 0, directionToTarget.z), Vector3.up);
        float compassPosition = Mathf.Clamp(signedAngle / Camera.main.fieldOfView, -0.5f, 0.5f);
        markerTransform.anchoredPosition = new Vector2(compassBarTransform.rect.width * compassPosition, 0);
    }

 */