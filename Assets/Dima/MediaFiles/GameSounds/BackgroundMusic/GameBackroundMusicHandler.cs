using Optimization;
using ProceduralGeneration.GameObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class GameBackroundMusicHandler : MonoBehaviour
{
    private AudioSource audioSource;
    private int locationsCount = 0;
    private bool toChangeMusic = false;


    public List<AudioClip> clips = new List<AudioClip>();
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        ChunkHandler.onChunkEnter.AddListener(OnLocationEnter);
        ChunkHandler.onChunkExit.AddListener(OnLocationExit);
    }

    private void Update() { 
        if (audioSource.time >= audioSource.clip.length && !audioSource.isPlaying) toChangeMusic = true;

        if (toChangeMusic)
        {
            toChangeMusic = false;
            audioSource.clip = clips[UnityEngine.Random.Range(0, clips.Count)];
            if (locationsCount > 0) audioSource.Play();
        }

    }
    void OnLocationEnter(Location location, Collider collider)
    {
        if (!collider.tag.Equals("Character") || location.Type == "Corridor") return;

        locationsCount++;

        if (!audioSource.isPlaying) audioSource.Play();
        StopAllCoroutines();

        StartCoroutine(ClipAnimation(0.25f, 1));
    }

    void OnLocationExit(Location location, Collider collider)
    {
        if (!collider.tag.Equals("Character") || location.Type == "Corridor") return;

        locationsCount--;

        if (locationsCount == 0)
        {
            StopAllCoroutines();
            StartCoroutine(ClipAnimation(3, 0, audioSource.Pause));
        }
    }

    IEnumerator ClipAnimation(float duration, float target, Action callback = null)
    {
        float currentTime = 0, startVolume = audioSource.volume;

        while (currentTime < duration)
        {
            audioSource.volume = startVolume + (target - startVolume) * (currentTime / duration);

            currentTime += Time.deltaTime;
            yield return null;
        }

        callback?.Invoke();
    }
}

