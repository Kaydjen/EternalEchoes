using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Interact : MonoBehaviour, IUpdate
{
    #region VARIABLES
    [SerializeField] private GameObject _zonaPref;
    private Transform _zona;
    private bool _isMenuActivated;
    private bool _isHighlighted;
    private Vector3 _mousePos;
    private Vector3 _mouseScreenPosition;
    private Vector3 _initialCorner;
    #endregion
    #region PRIVETE METHODS
    private void TryOpenManu() // TODO: тут вырубать можно не опять с помощью пкм, а с помощью например Esc, надо спросить у Димы
    {
            // it's the worst code ever I wrote, but this line just check if the obj we hitted is a button
        if (Hover.HitedCollider != null && Hover.HitedCollider.CompareTag("Button") && Hover.HitedCollider.TryGetComponent(out Button button))
        {
            button.onClick?.Invoke();
            return;
        }
        if (Hover.HitedCollider.CompareTag("Shop"))
        {
            if(!Hover.HitedCollider.TryGetComponent(out ShopMenu menu))
            {
                Debug.Log("Wsm _-_ " + Hover.HitedCollider.name);
            }
            else
            {
                menu.EnableMenu();    
            }
            return;
        }
        if (Hover.HitedCollider == null || !Hover.HitedCollider.TryGetComponent(out IInteractStrategy strategy)) // Если луч не попал, или попал, но обьект не является персонажем 
        {

            if (_isMenuActivated) // если меню активированно - вырубаем
            {
                DynamicMenuManagerContext.Instance.DisableMenu(MenuModeManager.MenuType);
                _isMenuActivated = false;
            }
        }
        else // если попал по персонажу
        {
            DynamicMenuManagerContext.Instance.EnableMenu(MenuModeManager.MenuType); // врубаем новое меню
            _isMenuActivated = true;
        }
        _zona.position = Hover.HitInfo.point;
    }
    private void HoldPerformed() // Эту логику можно было бы вынести в другой скрипт
    {
        if (_zona.position == Vector3.zero) return;
        _zona.gameObject.SetActive(true);
        _initialCorner = _zona.position;
        Hover.Instance.Disable();
        _isHighlighted = true;
        RegisterUpdate();
    }
    private void HoldReleased()
    {
        if (!_isHighlighted) return;
        _isHighlighted = false;

        _zona.gameObject.SetActive(false);
        Hover.Instance.Enable();
        UnregisterUpdate();
    }
    private void ResizeZona()
    {
        _mouseScreenPosition = Input.mousePosition;
        _mouseScreenPosition.z = Camera.main.WorldToScreenPoint(this.transform.position).z;
        _mousePos = Camera.main.ScreenToWorldPoint(_mouseScreenPosition);

        Vector3 size = _mousePos - _initialCorner;

        _zona.localScale = new Vector3(size.x, 1f, size.z);

        _zona.position = _initialCorner + new Vector3(size.x / 2, 0, size.z / 2);
    }
    #endregion
    #region Update
    public void PerformInitialUpdate()
    {
        ResizeZona();
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
    #region MONOBEHAVIOUR
    public void Init()
    {
        InputHandler.OnInteractionPress.AddListener(TryOpenManu);
        InputHandler.OnInteractionHoldPerformed.AddListener(HoldPerformed);
        InputHandler.OnInteractionHoldReleased.AddListener(HoldReleased);
        _zona = Instantiate(_zonaPref).transform;
        _zona.gameObject.SetActive(false);
    }
    #endregion
}
















/*
 
             Vector3 size = hit.transform.position - _zona.position; // Get the difference

            // Set the scale based on distance
            _zona.localScale = new Vector3(size.x, 3f, size.z);

            // Adjust position to keep A corner fixed
            _zona.position = _zona.position + new Vector3(size.x / 2, 0, size.z / 2);
 
 */