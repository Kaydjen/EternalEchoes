using UnityEngine;

public class CrimsonfallPool : Pool<Transform>
{
    public static CrimsonfallPool Instance;

    private void Awake()
    {
        Instance = this;
    }
}
