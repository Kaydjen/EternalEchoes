using System.Collections;
using UnityEngine;

public class JumpscareAltF : MonoBehaviour
{
    private Transform CamObj;
    public GameObject TruckObj;
    private bool isQuitting;

    public void Init()
    {
        CamObj = CameraSwitcher.Instance.transform;
            InputHandler.OnAltF.AddListener(ShoNibyd);
/*        if (Random.Range(50, 75) == 52)
        {
        }
        else
        {
            Destroy(gameObject);
        }*/
    }
    private void ShoNibyd() => StartCoroutine(nameof(Move));
    private IEnumerator Move()
    {
        CameraSwitcher.Instance.SwitchToFPV();
        CameraSwitcher.Instance.DisableFPV();
        GameObject InstObj = Instantiate(TruckObj, CamObj);
        InstObj.transform.SetParent(CamObj);
        InstObj.transform.localPosition = new Vector3(0f, 0f, 25f);
        InstObj.transform.localRotation = Quaternion.Euler(0, -90, 0);
        InstObj.transform.SetParent(null);

        while (true)
        {
            InstObj.transform.position = Vector3.MoveTowards(InstObj.transform.position, CamObj.position + new Vector3(0, -1f, 0), 20 * Time.deltaTime);
            if (Vector3.Distance(InstObj.transform.position, CamObj.position + new Vector3(0, -1f, 1.5f)) <= 1f)
            {
                isQuitting = true;
                Application.Quit();
            }
            yield return new WaitForEndOfFrame();
        }
    }
    private void OnApplicationQuit()
    {
        if (!isQuitting) Application.wantsToQuit += () => false;
    }
}
