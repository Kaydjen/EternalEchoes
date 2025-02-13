using UnityEngine;

public class Interaction : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private float _raycastDistance;
    [SerializeField] private GameObject _playerHub;

    public void Interact()
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
    private void OnEnable()
    {
        InputHandler.OnRMBPerformed.AddListener(Interact);
    }
}
