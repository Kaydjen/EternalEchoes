using UnityEngine;
using UnityEngine.Audio;

public static class SoundManager
{
    public static AudioSource CreateAndPlay(AudioClip clip, AudioMixerGroup group, Transform parent = null)
    {
        AudioSource audio = new GameObject(clip.name).AddComponent<AudioSource>();
        audio.outputAudioMixerGroup = group; 
        audio.clip = clip;
        audio.Play();
        MonoBehaviour.Destroy(audio.gameObject, clip.length);
        return audio;
    }
}