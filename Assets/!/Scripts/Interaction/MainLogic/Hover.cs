using UnityEngine;

// SEE: короче, тут можно было бы переделать логику. Сделать так, что бы этот скрипт отвечал не за наводку на обьеткы и их подсветку,
// а конкретно на пускание луча, который в последствии уже будет использован другими скриптами. 
public class Hover : MonoBehaviour, ICameraUpdate, IUpdate
{
    #region VARIABLES
    public static Hover Instance { get; private set; }
    [SerializeField] private float _FPVRayDist = 10f;
    [SerializeField] private float _IsometricRayDist = 11f;
    [SerializeField] private float _TopDownRayDist = 50f; // TODO: тут можно динамично изменять дистанцию по мере поднятия камеры в TopDown 
    private float _standartRayDist = 10f;
    public static Collider HitedCollider;
    public static RaycastHit HitInfo;
    private Camera _camera;
    private Collider _lastInteractibleObj;
    #endregion
    #region PUBLIC METHODS
    public void SetRaycastDistance(float value)
    {
        _standartRayDist = Mathf.Clamp(value, 0f, 100f);
    }
    public void UpdateNeededComponents() // TODO: we dont need to get camera here, it's better to do in Init method
    {
        _camera = this.transform.GetChild(Constants.Player.CAMERA).transform.GetComponent<Camera>();
    }
    public void Enable()
    {
        RegisterUpdate();
    }
    public void Disable()
    {
        UnregisterUpdate();
        ResetLastInteractibleObj();
    }
    #endregion
    #region PRIVATE METHODS
    private void CheckForInteractable()
    {
        HitedCollider = null;
        if (Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out HitInfo, _standartRayDist)) 
        {
            HitedCollider = HitInfo.collider;
            if (_lastInteractibleObj == HitedCollider) return;

            ResetLastInteractibleObj();
            if(HitedCollider.CompareTag("Item"))
            {
                HitedCollider.GetComponent<Outline>().enabled = true;
            }
            HitedCollider.GetComponent<IHover>()?.HoverEnter();
            _lastInteractibleObj = HitedCollider;                
        }
        else 
        {
            ResetLastInteractibleObj();
        }
    }

    private void ResetLastInteractibleObj()
    {
        if (_lastInteractibleObj is null) return;

        _lastInteractibleObj.GetComponent<IHover>()?.HoverExit();
        if (_lastInteractibleObj.TryGetComponent(out Outline outLine)) outLine.enabled = false;
        _lastInteractibleObj = null;
    }
    #endregion
    #region MONO METHODS
    private void Awake()
    {
        Instance = this;
    }
    private void OnEnable()
    {
        //InputHandler.OnInteraction.AddListener(Interact);
        RegisterUpdate();
        _standartRayDist = _FPVRayDist;
        CameraSwitcher.OnFPV_Enable.AddListener(() => _standartRayDist = _FPVRayDist);
        CameraSwitcher.OnIsometricV_Enable.AddListener(() => _standartRayDist = _IsometricRayDist);
        CameraSwitcher.OnTopDownV_Enable.AddListener(() => _standartRayDist = _TopDownRayDist);
    }
    private void OnDisable()
    {
        //InputHandler.OnInteraction.RemoveListener(Interact);
        UnregisterUpdate();
        CameraSwitcher.OnFPV_Enable.RemoveListener(() => _standartRayDist = _FPVRayDist);
        CameraSwitcher.OnIsometricV_Enable.RemoveListener(() => _standartRayDist = _IsometricRayDist);
        CameraSwitcher.OnTopDownV_Enable.RemoveListener(() => _standartRayDist = _TopDownRayDist);
    }
    #endregion
    #region Update
    public void PerformInitialUpdate()
    {
        CheckForInteractable();
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


/*
 
         if (Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out HitInfo, _raycastDistance) &&
            HitInfo.collider.TryGetComponent(out _currentInteractableObj))
        {
            if (_currentInteractableObj != _lastInteractibleObj)
            {
                ResetLastInteractibleObj();
                _currentInteractableObj.HoverEnter();
                _lastInteractibleObj = _currentInteractableObj;
            }
        }
        else
        {
            ResetLastInteractibleObj();
        }
 
 
 */

/*
 
 using System;
using System.Collections;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;

public class Hover : MonoBehaviour, ICameraUpdate, IUpdate
{
    #region VARIABLES
    [SerializeField] private float _raycastDistance;
    public static RaycastHit HitInfo;
    private Camera _camera;
    private IHover _lastInteractibleObj;
    private IHover _currentInteractableObj;
    #endregion
    #region PUBLIC METHODS
    public void SetRaycastDistance(float value)
    {
        _raycastDistance = Mathf.Clamp(value, 0f, 100f);
    }
    public void GetPlayer() // TODO: we dont need to get camera here, it's better to do in Init method
    {
        _camera = this.transform.GetChild(Constants.Player.CAMERA).transform.GetComponent<Camera>();
    }
    #endregion
    #region PRIVATE METHODS
    private void Interact()
    {
        if (Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out HitInfo, _raycastDistance))
        {
            IHover[] interactions = HitInfo.collider.GetComponents<IHover>();

            foreach (var interaction in interactions)
            {
                interaction.HoverEnter();
            }
        }
    }
    private void CheckForInteractable()
    {
        if (Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out HitInfo, _raycastDistance))
        {
            if(HitInfo.collider.CompareTag("Item"))
            {
                HitInfo.collider.GetComponent<Outline>().enabled = true;
            }
            if (HitInfo.collider.TryGetComponent(out _currentInteractableObj))
            {
                if (_currentInteractableObj != _lastInteractibleObj)
                {
                    ResetLastInteractibleObj();
                    _currentInteractableObj.HoverEnter();
                    _lastInteractibleObj = _currentInteractableObj;
                }
            }
        }
        else
        {
            ResetLastInteractibleObj();
        }
    }

    private void ResetLastInteractibleObj()
    {
        if (_lastInteractibleObj is null) return;

        _lastInteractibleObj.HoverExit();
        _lastInteractibleObj = null;
    }
    #endregion
    #region MONO METHODS
    private void OnEnable()
    {
        //InputHandler.OnInteraction.AddListener(Interact);
        RegisterUpdate();
    }
    private void OnDisable()
    {
        //InputHandler.OnInteraction.RemoveListener(Interact);
        UnregisterUpdate();
    }
    #endregion
    #region Update
    public void PerformInitialUpdate()
    {
        CheckForInteractable();
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

 
 */