using UnityEngine;

public class FreezeAura : MonoBehaviour
{
    private void OnParticleSystemStopped()
    {
        FreezeAuraPool.Instance.Return(this.transform);
    }
}

