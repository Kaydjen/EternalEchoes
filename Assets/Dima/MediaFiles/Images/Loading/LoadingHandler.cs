using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LoadingHandler : MonoBehaviour
{
    [Header("Content")]
    [SerializeField] RawImage content;
    [SerializeField] Texture2D[] images;
    [SerializeField][Range(0, 1f)] float min_scale, max_scale, max_offsetX, max_offsetY;
    [SerializeField, Min(.01f)] float scaleAnimationDuration = 1f;


    [Space(10), Header("Transition")]
    [SerializeField] RawImage transitionImage;
    [SerializeField, Min(0.01f)] float transitionDuration = 1f,
        blackscreenDuration = 1f;


    private int currentInd = 0;

    private int CurrentInd
    {
        get => currentInd;
        set
        {
            if (value >= images.Length) currentInd = 0;
            else currentInd = value;
        }
    }

    void Start() => StartCoroutine(LoadingScreen());
    private float Range(float max) => Random.Range(0f, max);

    IEnumerator LoadingScreen()
    {
        CurrentInd++;

        float scale = Random.Range(min_scale, max_scale);
        StartCoroutine(ContentAnimation(new Rect(Range(max_offsetX), Range(max_offsetY), scale, scale)));

        yield return new WaitForSeconds(scaleAnimationDuration-transitionDuration);

        yield return TransitionAnimation(new Color(0, 0, 0, 1));

        yield return new WaitForSeconds(blackscreenDuration);

        content.texture = images[currentInd];
        content.uvRect = new Rect(0, 0, 1, 1);

        StartCoroutine(TransitionAnimation(new Color(1, 1, 1, 1)));

        yield return LoadingScreen();
    }
    IEnumerator ContentAnimation(Rect end)
    {
        float elapsedTime = 0;
        Rect start = content.uvRect;

        yield return new WaitWhile(() =>
        {
            elapsedTime += Time.deltaTime;

            float percentage = elapsedTime / scaleAnimationDuration,
            width = start.width + percentage * (end.width - start.width),
            height = start.height + percentage * (end.height - start.height),
            offsetX = Mathf.Clamp(start.x + percentage * (end.x - start.x), 0, 1 - (content.texture.width * width) / content.texture.width),
            offsetY = Mathf.Clamp(start.y + percentage * (end.y - start.y), 0, 1 - (content.texture.height * height) / content.texture.height);

            content.uvRect = new Rect(offsetX, offsetY, width, height);

            return elapsedTime < scaleAnimationDuration;
        });

        float offsetX = Mathf.Clamp(end.x, 0, 1 - (content.texture.width * end.width) / content.texture.width),
            offsetY = Mathf.Clamp(end.y, 0, 1 - (content.texture.height * end.height) / content.texture.height);

        content.uvRect = new Rect(offsetX, offsetY, end.width, end.height);
    }
    IEnumerator TransitionAnimation(Color end)
    {
        float elapsedTime = 0;
        Color start = transitionImage.color;

        yield return new WaitWhile(() =>
        {
            elapsedTime += Time.deltaTime;

            transitionImage.color = Color.Lerp(start, end, elapsedTime/transitionDuration);

            return elapsedTime < transitionDuration;
        });

        transitionImage.color = end;
    }
}
