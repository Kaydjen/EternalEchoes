using UnityEngine;

public class CharacterSwitcher : MonoBehaviour
{
    private bool _isMenuActivated = false;
    public void Init()
    {
        InputHandler.OnGetCharacterInfo.AddListener(Switch);       
    }
    private void Switch() // TODO: тут вырубать можно не опять с помощью пкм, а с помощью например Esc, надо спросить у Димы
    {
        Debug.Log("Nu ono raboraet (CharacterSwitcher)");
        if(Hover.HitedCollider == null || !Hover.HitedCollider.CompareTag("Character")) // Если луч не попал, или попал, но обьект не является персонажем 
        {
            if (_isMenuActivated) // если меню активированно - вырубаем
            {
                CharacterOptions.DisableChoise();
                _isMenuActivated = false;
            }
        }
        else // если попал по персонажу
        {
            CharacterOptions.EnableChoise(Hover.HitedCollider.transform); // врубаем новое меню
            _isMenuActivated = true;
        }
    }
}
