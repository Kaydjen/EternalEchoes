using UnityEngine;

public class EnemyPoolTir3 : EnemyPool
{
    private static EnemyPoolTir3 _instance;
    public static EnemyPoolTir3 Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.Log("EnemyPoolTir3.Instance is being initialized!");
                _instance = FindObjectOfType<EnemyPoolTir3>();
            }
            return _instance;
        }
    }
}
