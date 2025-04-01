using UnityEngine;
using System;

[Serializable]
public class TransformRectTransform
{
    public RectTransform MarkerTransform;
    public Transform TrackedObject;
}


















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