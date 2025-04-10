using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.Rendering;

public class CharacterDamaged : MonoBehaviour
{
    [SerializeField] private Volume _volume;

    [SerializeField] private float _endDuration, _showDuration = 2;
    void Start()
    {
        HP.OnDamage += (maxHP, damage) => { Begin((float)(damage)/(float)(maxHP)); };
    }

    public void Begin(in float end)
    {
        StopAllCoroutines();
        StartCoroutine(Play(Mathf.Clamp01(_volume.weight + end), .1f));
    }

    float last = 0;

    public void FixedUpdate()
    {
        if (last == _volume.weight) StartCoroutine(Animation()); 

        last = _volume.weight;
    }

    private IEnumerator Animation()
    {
        yield return new WaitForSeconds(_showDuration);
        yield return Play(0, _endDuration);
    }
    private IEnumerator Play(float end, float _duration)
    {
        float elapsedTime = 0f, start = _volume.weight;

        while (elapsedTime < _duration)
        {
            elapsedTime += Time.deltaTime;
            _volume.weight = Mathf.Lerp(start, end, elapsedTime / _duration);
            yield return null;
        }

        _volume.weight = end;
    }
}
