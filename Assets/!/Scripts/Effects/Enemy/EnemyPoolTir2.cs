using UnityEngine;

public class EnemyPoolTir2 : EnemyPool
{
    private static EnemyPoolTir2 _instance;
    public static EnemyPoolTir2 Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.Log("EnemyPoolTir2.Instance is being initialized!");
                _instance = FindObjectOfType<EnemyPoolTir2>();
            }
            return _instance;
        }
    }
}
