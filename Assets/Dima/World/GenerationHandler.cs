using System.Collections;
using UnityEngine;
using ProceduralGeneration.Logic;
using UnityEngine.SceneManagement;
using System;

public class GenerationHandler : MonoBehaviour
{
    static public GenerationHandler instance;
    private bool isLoading = false;

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

        instance = this;
    }

    private void CreateWorld(int ind = 0)
    {
        GameObject parent = new GameObject("Game");
        parent.SetActive(false);
        DontDestroyOnLoad(parent);

        Generator.Seed = UnityEngine.Random.Range(0, int.MaxValue);
        ProceduralGeneration.GameObjects.World world = Generator.CreateWorld(ind);

        StartCoroutine(ProceduralGeneration.Logic.Renderer3D.Render(world, parent.transform, () => {
            StartCoroutine(LoadScene("Game", () => {
                parent.SetActive(true);
                SceneManager.MoveGameObjectToScene(parent, SceneManager.GetSceneByName("Game"));
            }));
        }, trigger));
    }
    public void GenerateWorld(int ind = 0)
    {
        StartCoroutine(LoadScene("Loading", () => CreateWorld(ind)));
    }

    public void Load(string sceneName, Action callback = null)
    {
        if (!isLoading)
        StartCoroutine(LoadScene(sceneName, callback));
    }

     public IEnumerator LoadScene(string sceneName, Action callback = null)
    {
        yield return new WaitWhile(() => isLoading);

        isLoading = true;

        yield return Transition.instance.PlayTransition(0f, 1f);

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        yield return new WaitWhile(() => !asyncLoad.isDone);

        callback?.Invoke();

        yield return Transition.instance.PlayTransition(1f, 0f);

        isLoading = false;
    }

    public IEnumerator LoadScene(int sceneIndex, Action callback = null)
    {
        yield return new WaitWhile(() => isLoading);

        isLoading = true;

        yield return Transition.instance.PlayTransition(0f, 1f);

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneIndex);

        yield return new WaitWhile(() => !asyncLoad.isDone);

        callback?.Invoke();

        yield return Transition.instance.PlayTransition(1f, 0f);

        isLoading = false;
    }
}
