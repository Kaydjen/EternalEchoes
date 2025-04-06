using UnityEngine;

public class CharacterInstantiator : MonoBehaviour
{
    public float DelayBeforeInstantiate = 10f;
    public GameObject Character1;

    private void Start()
    {
        Invoke(nameof(Instantiate), DelayBeforeInstantiate);
    }
    public void Instantiate()
    {
        Instantiate(Character1);
    }
}
