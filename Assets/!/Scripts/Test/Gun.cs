using UnityEngine;

class Gun : MonoBehaviour, IAttack, IAttackDouble, IUpdate
{
    [SerializeField] private float _rayDist = 15f;
    [SerializeField] private LayerMask _layers;
    [SerializeField] private bool _doSpread;
    [Tooltip("Remember: value .1f is for shotgun, so for just a gun make it about .01f or lover")]
    [SerializeField] private float _spreadRandomX = .01f; 
    [SerializeField] private float _spreadRandomY = .01f;
    private Vector3 _spread;
    private void CastRay()
    {
        if (_doSpread) _spread = new Vector3(Random.Range(-_spreadRandomX, _spreadRandomX), Random.Range(-_spreadRandomY, _spreadRandomY), 0f);
        else _spread = Vector3.zero;

        if (Physics.Raycast(AimDirection.Direction.position, (AimDirection.Direction.forward + _spread).normalized, out RaycastHit hitInfo, _rayDist, _layers))
        {
            if (hitInfo.collider.CompareTag("Player")) CW.I.Print("Hitted");
            //Logic after shooting
            Debug.DrawRay(AimDirection.Direction.position, (AimDirection.Direction.forward + _spread).normalized * _rayDist, Color.cyan, 1f);
        }
    }
    public void Attack()
    {
        CastRay();
    }
    public void AttackPerformed()
    {
        RegisterUpdate();
    }
    public void AttackReleased()
    {
        UnregisterUpdate();
    }
    #region Update
    public void PerformInitialUpdate()
    {
        CastRay();
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
