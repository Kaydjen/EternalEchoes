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
        
        if (Random.Range(50, 75) == 52)
            InputHandler.OnAltF.AddListener(ShoNibyd);
        else
            Destroy(gameObject);
    }

    private bool WantsToQuitHandler()
    {
        if (!isQuitting)
        {
            return false;
        }
        return true;
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
