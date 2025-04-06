using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GpuInstancerEnabler : MonoBehaviour
{
    void Awake()
    {
        MaterialPropertyBlock materialPropertyBlock = new MaterialPropertyBlock();

        foreach (var t in gameObject.GetComponentsInChildren<UnityEngine.Renderer>()) t.SetPropertyBlock(materialPropertyBlock);
    }
}
