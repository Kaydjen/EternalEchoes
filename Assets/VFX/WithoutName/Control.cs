using System.Collections;
using UnityEngine;
using UnityEngine.VFX;

public class Control : MonoBehaviour
{
    [SerializeField] private float _startValue;
    [SerializeField] private float _mainValue;
    [SerializeField] private float _endValue;
    [SerializeField] private VisualEffect _effect;
    [SerializeField] private VisualEffect _effect2;
    [SerializeField] private VisualEffect _effect3;
    public void HitPlay()
    {
        StartCoroutine(PlayStart());
    }
    public void HitStop()
    {
        StartCoroutine(PlayEnd());
    }
    private IEnumerator PlayStart()
    {
        for (float i = _startValue; i < _mainValue; i += .01f)
        {
            SetEffects(ref i);
            yield return new WaitForSeconds(.01f);
        }
    }
    private IEnumerator PlayEnd()
    {
        for (float i = _mainValue; i < _endValue; i += .1f)
        {
            SetEffects(ref i);
            yield return new WaitForSeconds(.1f);
        }

        for (float i = _endValue; i > 0; i -= .2f)
        {
            SetEffects(ref i);
            yield return new WaitForSeconds(.1f);
        }
        float zero = 0;
        SetEffects(ref zero);
    }
    private void SetEffects(ref float value)
    {
        _effect.SetFloat("Intensity", value);
        _effect2.SetFloat("Intensity", value);
        _effect3.SetFloat("Intensity", value);
    }
}
