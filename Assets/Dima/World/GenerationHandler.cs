using System.Collections;
using UnityEngine;
using ProceduralGeneration.Logic;
using UnityEngine.SceneManagement;
using System;

public class GenerationHandler : MonoBehaviour
{
    #region Generation

    [Header("Generation")]
    public string path;
    public int seed;

    public GameObject trigger;

    #endregion
    private void Awake()
    {
        Database.InitSerialzableWorlds(path);
        DontDestroyOnLoad(gameObject);
    }

    public void GenerateWorld(int ind = 0)
    {
        SceneManager.LoadScene("Loading");

        GameObject parent = new GameObject("Game");
        parent.SetActive(false);
        DontDestroyOnLoad(parent);

        Generator.Seed = seed;
        ProceduralGeneration.GameObjects.World world = Generator.CreateWorld(0);

        StartCoroutine(ProceduralGeneration.Logic.Renderer3D.Render(world, parent.transform, () => {
            StartCoroutine(LoadScene("Game", () => {
                parent.SetActive(true);
                SceneManager.MoveGameObjectToScene(parent, SceneManager.GetSceneByName("Game"));
            }));
        }, trigger));

        
    }

    private IEnumerator LoadScene(string sceneName, Action callback = null)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        while (!asyncLoad.isDone)
            yield return null;

        callback?.Invoke();
    }

    private IEnumerator LoadScene(int sceneIndex, Action callback)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneIndex);

        while (!asyncLoad.isDone)
            yield return null;

        callback.Invoke();
    }
}
