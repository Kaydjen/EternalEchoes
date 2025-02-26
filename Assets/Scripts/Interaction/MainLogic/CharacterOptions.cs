using UnityEngine;

public class CharacterOptions : MonoBehaviour
{
    #region VARIABLES
    private static Transform _selectedCharacter;
    #endregion
    #region PUBLIC METHODS
    public static void EnableChoise(Transform characterObj)
    {
        InputHandler.Instance.ActivateOptionsNumbersMap();
        _selectedCharacter = characterObj;
    }
    public static void DisableChoise()
    {
        InputHandler.Instance.ActivateDefNumbersMap();
    }
    #endregion
    #region PRIVATE METHODS
    private void SwitchToNewCharacter()
    {
        _selectedCharacter.GetComponent<PlayerCore>().enabled = true;
    }
    private void SayHello()
    {
        Debug.Log("Hello");
    }
    #endregion
    #region MONO METHODS
    public void Init()
    {
        InputHandler.OnOptionsOne.AddListener(SwitchToNewCharacter);
        InputHandler.OnOptionsTwo.AddListener(SayHello);
    }
    #endregion
}
