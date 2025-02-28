using UnityEngine;

public class CharacterOptions : MonoBehaviour
{
    #region VARIABLES
    private static Transform _selectedCharacter;
    private static GameObject _manu;
    #endregion
    #region PUBLIC METHODS
    public static void EnableChoise(Transform characterObj)
    {
        InputHandler.Instance.ActivateOptionsNumbersMap();
        _selectedCharacter = characterObj;
        _manu.SetActive(true);
    }
    public static void DisableChoise()
    {
        InputHandler.Instance.ActivateDefNumbersMap();
        _manu.SetActive(false);
    }
    #endregion
    #region PRIVATE METHODS
    private void SwitchToNewCharacter()
    {
        _selectedCharacter.GetComponent<PlayerCore>().enabled = true;
        DisableChoise();
    }
    private void SayHello()
    {
        Debug.Log("Hello");
        DisableChoise();
    }
    #endregion
    #region MONO METHODS
    public void Init()
    {
        InputHandler.OnOptionsOne.AddListener(SwitchToNewCharacter);
        InputHandler.OnOptionsTwo.AddListener(SayHello);
        _manu = this.transform.GetChild(0).transform.gameObject;
    }
    #endregion
}
