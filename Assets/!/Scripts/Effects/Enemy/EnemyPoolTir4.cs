using UnityEngine;

public class EnemyPoolTir4 : EnemyPool
{
    private static EnemyPoolTir4 _instance;
    public static EnemyPoolTir4 Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.Log("EnemyPoolTir4.Instance is being initialized!");
                _instance = FindObjectOfType<EnemyPoolTir4>();
            }
            return _instance;
        }
    }
}