using UnityEngine;
public static class SoundManager
{
    public static AudioSource CreateAndPlay(AudioClip clip, Transform parent = null)
    {
        AudioSource audio = new GameObject(clip.name).AddComponent<AudioSource>();
        audio.clip = clip;
        audio.Play();
        MonoBehaviour.Destroy(audio.gameObject, clip.length);
        return audio;
    }
}