using System.Collections.Generic;
using UnityEngine;

public class AIGroupManager : MonoBehaviour
{
    private HashSet<AI> _group = new HashSet<AI>();

    public void ReturnMembers(HashSet<AI> members)
    {
        Debug.Log("RETURNED IT WORKS");
        foreach (var ai in members)
        {
            if(ai != null) Debug.Log("NAME: " + ai.name + "ID: " + ai.gameObject.GetInstanceID());
        }
    }
}
