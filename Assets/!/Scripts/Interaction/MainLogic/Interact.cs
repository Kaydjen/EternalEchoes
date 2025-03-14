using UnityEngine;

public class Interact : MonoBehaviour
{
    private bool _isMenuActivated = false;
    public void Init()
    {
        InputHandler.OnGetCharacterInfo.AddListener(Switch);       
    }
    private void Switch() // TODO: тут вырубать можно не опять с помощью пкм, а с помощью например Esc, надо спросить у Димы
    {
        Debug.Log("Nu ono raboraet (Interact)");
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
}
