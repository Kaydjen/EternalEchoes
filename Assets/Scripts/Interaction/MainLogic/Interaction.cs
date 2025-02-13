using UnityEngine;

public class Interaction : MonoBehaviour, ICameraUpdate
{
    #region VARIABLES
    [SerializeField] private float _raycastDistance;
    private Camera _camera;
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
        if (Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, _raycastDistance))
        {
            IInteraction[] interactions = hit.collider.GetComponents<IInteraction>();

            foreach (var interaction in interactions)
            {
                interaction.Interact();
            }
        }
    }
    #endregion
    #region MONO METHODS
    private void OnEnable()
    {
        InputHandler.OnInteraction.AddListener(Interact);
    }
    private void OnDisable()
    {
        InputHandler.OnInteraction.RemoveListener(Interact);
    }
    #endregion
}
