using UnityEngine;

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
        if(Hover.HitedCollider == null || !Hover.HitedCollider.TryGetComponent(out IInteractStrategy strategy)) // Если луч не попал, или попал, но обьект не является персонажем 
        {
            if (_isMenuActivated) // если меню активированно - вырубаем
            {
                InteractOptions.Instance.DisableManu();
                _isMenuActivated = false;
            }
        }
        else // если попал по персонажу
        {
            InteractOptions.Instance.EnableManu(strategy); // врубаем новое меню
            _isMenuActivated = true;
        }
    }
    private void HoldPerformed()
    {
        CW.I.Print("hold");

        _isHighlighted = true;

        _zona.position = Hover.HitedCollider.transform.position;
        _initialCorner = _zona.position - new Vector3(_zona.localScale.x / 2, 0, _zona.localScale.z / 2);
        _zona.gameObject.SetActive(true);
        Hover.Instance.Disable();
        RegisterUpdate();
    }
    private void HoldReleased()
    {
        CW.I.Print("released");

        if (!_isHighlighted) return;
        _isHighlighted = false;

        _zona.gameObject.SetActive(false);
        UnregisterUpdate();
        Hover.Instance.Enable();
    }
    private void ReleazeRay()
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
        ReleazeRay();
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