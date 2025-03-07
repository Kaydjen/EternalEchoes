using System.Collections;
using UnityEngine;

public class LBackgroundM : MonoBehaviour
{
    public AudioClip[] audioClips;
    private AudioSource source;
    void Start()
    {
        source = GetComponent<AudioSource>();
        if (source == null ) source = gameObject.AddComponent<AudioSource>();

        StartCoroutine(PlayMusic());
    }

    private IEnumerator PlayMusic()
    {

        if (!source.isPlaying)
        {
            source.clip = audioClips[Random.Range(0, audioClips.Length)];
            source.Play();
        }

        yield return new WaitForSeconds(source.clip.length);

        yield return PlayMusic();
    }
}
