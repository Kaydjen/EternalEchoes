using UnityEngine;

public class EnemyPoolTir1 : Pool<Transform>
{
    private static EnemyPoolTir1 _instance;
    public static EnemyPoolTir1 Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.Log("EnemyPoolTir1.Instance is being initialized!");
                _instance = FindObjectOfType<EnemyPoolTir1>();
            }
            return _instance;
        }
    }
}
