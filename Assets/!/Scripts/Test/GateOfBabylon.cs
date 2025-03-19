using UnityEngine;

[RequireComponent(typeof(AttackHandlerOneAttack))]
public class GateOfBabylon : MonoBehaviour, IAttack
{
    [SerializeField] private GameObject _particlePref;
    [SerializeField] private GameObject _warningPref;
    [SerializeField] private float _height = 20f;
    [SerializeField] private float _timeToDisableWarning = 5f;
    public void Attack()
    {
        // GetComponent<AimPlacer>().Particle.position
        CW.I.Print($"Attack {GetComponent<AimPlacer>().GetAimTransform().position}", 10);

        Vector3 pos = GetComponent<AimPlacer>().GetAimTransform().position;
        Quaternion rotation = GetComponent<AimPlacer>().GetAimTransform().rotation;
        _warningPref.transform.rotation = rotation;
        _warningPref.transform.position = pos;
        pos.y += _height;
        _particlePref.transform.position = pos;

        _warningPref.gameObject.SetActive(true);
        _particlePref.gameObject.SetActive(true);


        Invoke(nameof(DisableWarning), _timeToDisableWarning);
    }
    private void DisableWarning()
    {
        _warningPref.gameObject.SetActive(false);
    }
    private void Start()
    {
        _particlePref = Instantiate(_particlePref);
        _warningPref = Instantiate(_warningPref);
        _particlePref.gameObject.SetActive(false);
        _warningPref.gameObject.SetActive(false);
    }
}