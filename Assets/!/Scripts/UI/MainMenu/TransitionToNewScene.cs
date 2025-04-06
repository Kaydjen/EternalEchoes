using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;

public class TransitionToNewScene : MonoBehaviour
{
    [SerializeField] private Volume _volume;
    [SerializeField] private AudioClip _audioClip;
    [SerializeField] private float _duration = 1;
    public UnityEvent OnSceneLoad = new();

    public void StartTransition()
    {
        StartCoroutine(Transition());
    }
    private IEnumerator Transition()
    {
        SoundManager.CreateAndPlay(_audioClip, this.transform);
        float elapsedTime = 0f;

        while (elapsedTime < _duration)
        {
            elapsedTime += Time.deltaTime;
            _volume.weight = Mathf.Lerp(0f, 1f, elapsedTime / _duration);
            yield return null;
        }

        _volume.weight = 1f;
        OnSceneLoad?.Invoke();
    }
}
