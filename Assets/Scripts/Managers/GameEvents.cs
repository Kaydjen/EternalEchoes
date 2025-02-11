using UnityEngine;
using UnityEngine.Events;

[ComponentInfo("Game events handler")]
public class GameEvents : MonoBehaviour
{
    public static UnityEvent OnCharacterChange = new UnityEvent();

}
