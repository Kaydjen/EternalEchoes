using System.Collections;
using UnityEngine;

public class JumpscareAltF : MonoBehaviour
{
    private Transform CamObj;
    public GameObject TruckObj;
    private bool isQuitting = false;

    private void Awake()
    {
        Application.wantsToQuit += WantsToQuitHandler;
    }

    public void Init()
    {
        CamObj = CameraSwitcher.Instance.transform;
        InputHandler.OnAltF.AddListener(ShoNibyd);
    }

    private bool WantsToQuitHandler()
    {
        if (!isQuitting)
        {
            Debug.Log("Закриття скасовано!");
            return false; // Блокуємо вихід
        }
        return true; // Дозволяємо вихід після джамскеру
    }

    private void ShoNibyd()
    {
        StartCoroutine(nameof(Move));
    }

    private IEnumerator Move()
    {
        CameraSwitcher.Instance.SwitchToFPV();
        CameraSwitcher.Instance.DisableCurrentView();
        GameObject InstObj = Instantiate(TruckObj, CamObj);
        InstObj.transform.SetParent(CamObj);
        InstObj.transform.localPosition = new Vector3(0f, 0f, 25f);
        InstObj.transform.localRotation = Quaternion.Euler(0, -90, 0);
        InstObj.transform.SetParent(null);

        while (true)
        {
            InstObj.transform.position = Vector3.MoveTowards(InstObj.transform.position, CamObj.position, 20 * Time.deltaTime);
            if (Vector3.Distance(InstObj.transform.position, CamObj.position) <= 1f)
            {
                Debug.Log("Фура в'їхала!");
                isQuitting = true;
                Application.Quit();
            }
            yield return null;
        }
    }

    private void OnDestroy()
    {
        Application.wantsToQuit -= WantsToQuitHandler;
    }
}
