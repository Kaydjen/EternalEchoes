using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class ElectrifyCharacter : MonoBehaviour
{
    public static ElectrifyCharacter Instance;
    private Transform _effect;
    private ParticleSystem partocleSystem;

    private void Awake()
    {
        Instance = this;
    }
    public void Electrify(NavMeshAgent agent, float duration)
    {
        _effect = ElectricityAuraPool.Instance.Get();
        partocleSystem = _effect.GetComponent<ParticleSystem>();
        ParticleSystem.MainModule main = partocleSystem.main;
        partocleSystem.Stop();
        main.duration = duration;
        _effect.SetParent(agent.transform);
        _effect.localPosition = Vector3.zero;
        StartCoroutine(ElectrifyCoroutine(agent, duration));
        
    }
    private IEnumerator ElectrifyCoroutine(NavMeshAgent agent, float duration)
    {
        if (agent == null) yield break;

        for (int i = 0; i < 4; i++)
        {
            agent.enabled = false;
            partocleSystem.Play();
            yield return new WaitForSeconds(duration);
            partocleSystem.Stop();
            agent.enabled = true;
            yield return new WaitForSeconds(duration);
        }
    }
}
