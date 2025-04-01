using UnityEngine;
using System.Collections.Generic;
using static UnityEditor.PlayerSettings;

public class RatotorShlafe : MonoBehaviour
{
    [Header("Make sure the width is int")]
    [SerializeField] private RectTransform _barTransform;
    [SerializeField] private Transform _placeToInstantiate;
    [SerializeField] private List<RectTransform> _pointsListInspector = new();
    [SerializeField] private int _widthBtwPoints;
    [SerializeField] private int _pointsWidth; 

    private List<RectTransform> _pointsList = new();

    private void Awake()
    {
        if (_pointsListInspector.Count == 0 || _widthBtwPoints <= 0)
        {
            Debug.LogError("Invalid setup: empty points list or zero width between points");
            return;
        }

        float barWidth = _barTransform.rect.width;
        int countToPlace = Mathf.FloorToInt(barWidth / _widthBtwPoints);

        // Создаем нужное количество точек
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

        // Равномерно распределяем точки внутри бара
        float halfBarWidth = barWidth * 0.5f;
        float step = barWidth / (countToPlace - 1); // Шаг между точками

        for (int i = 0; i < _pointsList.Count; i++)
        {
            float xPos = -halfBarWidth + i * step;
            _pointsList[i].anchoredPosition = new Vector2(xPos, 0f);
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