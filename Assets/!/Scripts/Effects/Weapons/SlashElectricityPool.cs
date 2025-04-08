using UnityEngine;

public class SlashElectricityPool : Pool<Transform>
{
    public static SlashElectricityPool Instance;

    private void Awake()
    {
        Instance = this;
    }
}
