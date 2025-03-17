using UnityEngine;

[RequireComponent(typeof(AimHandler))]
public class AimPlacer : MonoBehaviour, IAim, IUpdate
{
    [SerializeField] private GameObject _aimParticle;
    [SerializeField] private float _distance;
    [SerializeField] private LayerMask _layersToAim;
    private Transform _particle;
    public Transform Particle
    {
        get { return _particle; }
        private set { _particle = value; }
    }
    public void StartAim()
    {
        Hover.Instance.Disable();
        RegisterUpdate();
        Particle.gameObject.SetActive(true);
    }
    public void StopAim()
    {
        Hover.Instance.Enable();
        UnregisterUpdate();
        Particle.gameObject.SetActive(false);
    }
    #region MONOBEHAIVOUR
    private void Awake()
    {
        Particle = Instantiate(_aimParticle, Vector3.zero, Quaternion.identity).transform;
        Particle.gameObject.SetActive(false);
    }
    #endregion
    #region UPDATE
    public void PerformInitialUpdate()
    {
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, _distance, _layersToAim))
        {
            Particle.position = hit.point;
            Particle.rotation = Quaternion.LookRotation(hit.normal);
        }
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