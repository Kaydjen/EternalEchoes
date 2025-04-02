using System.Collections.Generic;
using UnityEngine;

public class SwitchPanelMenuRotate : MonoBehaviour
{
    [SerializeField] private List<Transform> _listOfPositions;
    public void SwitchToPanel(int number)
    {
        Vector3 direction = _listOfPositions[number].position - this.transform.position;
        direction.y = 0f;
        this.transform.rotation = Quaternion.LookRotation(direction);
    }
}