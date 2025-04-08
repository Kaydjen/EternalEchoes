using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.Rendering;

public class Transition : MonoBehaviour
{
    static public Transition instance;

    [SerializeField] private Volume _volume;
    [SerializeField] private float _duration = 1;
    [SerializeField] private AudioMixerGroup audioMixerGroup;

    private Coroutine coroutine;

    public void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    public IEnumerator PlayTransition(float start, float end, UnityAction callback = null)
    {
        float elapsedTime = 0f;

        while (elapsedTime < _duration)
        {
            elapsedTime += Time.deltaTime;
            _volume.weight = Mathf.Lerp(start, end, elapsedTime / _duration);

            audioMixerGroup.audioMixer.SetFloat(audioMixerGroup.name+"Volume",
                Mathf.Log10(Mathf.Max(Mathf.Lerp(end, start, elapsedTime / _duration), 0.01f))*20);
            yield return null;
        }

        _volume.weight = end;
        callback?.Invoke();
    }
}
