using System.Collections;
using UnityEngine;
using ProceduralGeneration.Logic;
using UnityEngine.SceneManagement;
using System;

public class GenerationHandler : MonoBehaviour
{
    public string path;
    public int seed;

    public GameObject trigger;
    private void Awake()
    {
        Database.InitSerialzableWorlds(path);
        DontDestroyOnLoad(gameObject);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);

        GameObject parent = new GameObject("World");
        parent.SetActive(false);
        DontDestroyOnLoad (parent);

        Generator.Seed = seed;
        Generator.CreateWorld(0);
        StartCoroutine(ProceduralGeneration.Logic.Renederer.Render(Database.worlds[0], parent.transform, () => {
            StartCoroutine(LoadScene(() => {
                parent.SetActive(true);
                SceneManager.MoveGameObjectToScene(parent, SceneManager.GetSceneByName("Game"));
            }));
        }, trigger));
    }

    private IEnumerator LoadScene(Action callback)
    {

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Game");

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        callback.Invoke();
    }
}
