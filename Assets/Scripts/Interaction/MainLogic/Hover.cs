using System;
using System.Collections;
using UnityEngine;

public class Hover : MonoBehaviour, ICameraUpdate, IUpdate
{
    #region VARIABLES
    [SerializeField] private float _raycastDistance;
    private Camera _camera;
    private IHover _lastInteractibleObj;
    private IHover _currentInteractableObj;
    private RaycastHit _hit;
    private static Type _currentInteractionType = typeof(IHover);
    #endregion
    #region PUBLIC METHODS
    public void SetRaycastDistance(float value)
    {
        _raycastDistance = Mathf.Clamp(value, 0f, 100f);
    }
    public void UpdateNeededComponents() // TODO: we dont need to get camera here, it's better to do in Init method
    {
        _camera = this.transform.GetChild(Constants.Player.CAMERA).transform.GetComponent<Camera>();
    }
    public static IEnumerable SetNewState<T>() where T : IHover
    {
        _currentInteractionType = typeof(T);
        yield return null;
        _currentInteractionType = typeof(IHover);
    }
    #endregion
    #region PRIVATE METHODS
    private void Interact()
    {
        if (Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out _hit, _raycastDistance))
        {
            IHover[] interactions = _hit.collider.GetComponents<IHover>();

            foreach (var interaction in interactions)
            {
                interaction.HoverEnter();
            }
        }
    }
    private void CheckForInteractable()
    {
        if (Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out _hit, _raycastDistance)
            && _hit.collider.TryGetComponent<IHover>(out _currentInteractableObj))
        {
            if (_currentInteractionType?.GetType() != _currentInteractableObj.GetType()) return;

            if (_currentInteractableObj != null && _currentInteractableObj != _lastInteractibleObj)
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
