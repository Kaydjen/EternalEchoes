using UnityEngine;

[RequireComponent(typeof(AimHandler))]
public class AimPlacer : MonoBehaviour, IAim, IUpdate
{
    [SerializeField] private GameObject _aimParticle;
    [SerializeField] private float _distance;
    [SerializeField] private LayerMask _layersToAim;
    [SerializeField] private bool _isLeaving = false;
    [SerializeField] private bool _isDisappearing = false;
    [SerializeField] private float _disappearTime = 0f;
    private Transform _aimTransform;
    public void StartAim()
    {
        CW.I.Print("Start");
        GetComponent<AttackHandler>().OnAttack.AddListener(DisableAim);
        Hover.Instance.Disable();
        RegisterUpdate();
        _aimTransform.gameObject.SetActive(true);
    }
    public void StopAim()
    {
        CW.I.Print("Stop");
        Hover.Instance.Enable();
        UnregisterUpdate();
        if (!_isLeaving) return;
        if (_isDisappearing)
        {
            CancelInvoke(nameof(DisableAim));
            Invoke(nameof(DisableAim), _disappearTime);
        }
    }
    public void DisableAim()
    {
        CW.I.Print("Disable");
        _aimTransform.gameObject.SetActive(false);
        GetComponent<AttackHandler>().OnAttack.RemoveListener(DisableAim);
    }
    public void PerformeRay()
    {
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, _distance, _layersToAim))
        {
            _aimTransform.position = hit.point;
            _aimTransform.rotation = Quaternion.LookRotation(hit.normal);
        }
    }
    public Transform GetAimTransform() => _aimTransform;
    #region MONOBEHAIVOUR
    private void Awake()
    {
        _aimTransform = Instantiate(_aimParticle, Vector3.zero, Quaternion.identity).transform;
        _aimTransform.gameObject.SetActive(false);
    }
    #endregion
    #region UPDATE
    public void PerformInitialUpdate()
    {
        PerformeRay();
    }
    public void PerformPreUpdate()
    {
        throw new System.NotImplementedException();
    }
    public void PerformUpdate()
    {
        throw new System.NotImplementedException();
    }
    public void PerformFinalUpdate()
    {
        throw new System.NotImplementedException();
    }
    public void PerformLateUpdate()
    {
        throw new System.NotImplementedException();
    }
    private void RegisterUpdate()
    {
        Updater.Instance.RegisterUpdate(this, Updater.UpdateType.InitialUpdate);
    }
    private void UnregisterUpdate()
    {
        Updater.Instance.UnregisterUpdate(this, Updater.UpdateType.InitialUpdate);
    }
    #endregion
}