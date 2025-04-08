using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class FreezeCharacter : MonoBehaviour
{
    public static FreezeCharacter Instance;
    private Transform _effect;
    private void Awake()
    {
        Instance = this;
    }
    public void Freeze(NavMeshAgent agent, float duration)
    {
        StartCoroutine(FreezeCoroutine(agent, duration));
        _effect = FreezeAuraPool.Instance.Get();
        var particleSystem = _effect.GetComponent<ParticleSystem>();
        var main = particleSystem.main;
        particleSystem.Stop();
        main.duration = duration;
        _effect.SetParent(agent.transform);
        _effect.localPosition = Vector3.zero;
        particleSystem.Play();
    }
    private IEnumerator FreezeCoroutine(NavMeshAgent agent, float duration)
    {
        if (agent == null) yield break;

        agent.enabled = false;

        yield return new WaitForSeconds(duration);

        agent.enabled = true;
    }
}

