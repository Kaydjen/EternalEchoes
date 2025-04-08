using UnityEngine;

public class ElectricityAura : MonoBehaviour
{
    private void OnParticleSystemStopped()
    {
        ElectricityAuraPool.Instance.Return(this.transform);
    }
}

