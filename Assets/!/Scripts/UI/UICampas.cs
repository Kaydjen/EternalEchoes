using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using Unity.Mathematics;

public class UICampas : MonoBehaviour
{
    [SerializeField] private List<TransformRectTransform> _points;
    public RectTransform compassBarTransform;
    public Transform cameraObjectTransform;

    void Update()
    {
        foreach (TransformRectTransform el in _points) 
            SetMarkerPosition(el.objectiveMarkerTransform, el.PointsOnMap.position);
    }
    private void SetMarkerPosition(RectTransform markerTransform, Vector3 worldPosition)
    {
        Vector3 directionToTarget = worldPosition - cameraObjectTransform.position;
        float signedAngle = Vector3.SignedAngle(new Vector3(cameraObjectTransform.forward.x, 0, cameraObjectTransform.forward.z), new Vector3(directionToTarget.x, 0, directionToTarget.z), Vector3.up);
        float compassPosition = signedAngle / Camera.main.fieldOfView;
        markerTransform.anchoredPosition = new Vector2(compassBarTransform.rect.width * compassPosition, 0);

        Image img = markerTransform.GetComponent<Image>();
        Color cl = img.color;
        cl.a = Clamp(compassPosition);
        img.color = cl;
    }
    private float Clamp(float value)
    {
        Math.Clamp(value, -1f, 1f);
        if (value == 0) // 0
            return 1;
        else if(value > 0) // +
            return 1 - value;
        else // -
            return 1 + value;
    }
}

[Serializable]
public class TransformRectTransform
{
    public RectTransform objectiveMarkerTransform;
    public Transform PointsOnMap;
}



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