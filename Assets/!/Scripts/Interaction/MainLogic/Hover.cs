﻿using UnityEngine;

public class Hover : MonoBehaviour, ICameraUpdate, IUpdate
{
    #region VARIABLES
    [SerializeField] private float _raycastDistance;
    private RaycastHit _hitInfo;
    public static Collider HitedCollider;
    private Camera _camera;
    private Collider _lastInteractibleObj;
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
    #endregion
    #region PRIVATE METHODS
    private void Interact()
    {
        if (Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out _hitInfo, _raycastDistance))
        {
            IHover[] interactions = _hitInfo.collider.GetComponents<IHover>();

            foreach (var interaction in interactions)
            {
                interaction.HoverEnter();
            }
        }
    }
    private void CheckForInteractable()
    {
        HitedCollider = null;
        if (Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out _hitInfo, _raycastDistance)) // TODO: тут дальше можно заменить HitInfo на новое статик поле, в которое записать HitInfo.collider и так уже работать, удобнее будет
        {
            HitedCollider = _hitInfo.collider;
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
    public void UpdateNeededComponents() // TODO: we dont need to get camera here, it's better to do in Init method
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