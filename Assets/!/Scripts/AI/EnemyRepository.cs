using System.Collections.Generic;
using UnityEngine;

public delegate void GameObjectDelegate(GameObject obj);
public delegate void IdDelegate(int id);

public class EnemyRepository : MonoBehaviour
{
    public static EnemyRepository Instance;
    public Dictionary<int, GameObject> Items = new();
    public event GameObjectDelegate OnRegister;
    public event IdDelegate OnUnregister;
    public void Register(GameObject enemyObj, int id)
    {
        Items.Add(id, enemyObj);
        OnRegister?.Invoke(enemyObj);
    }
    public void Unregister(int id)
    {
        Items.Remove(id);
        OnUnregister?.Invoke(id);
    }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            if (Instance == this) return;
            Destroy(this);
        }
        DontDestroyOnLoad(gameObject);
    }
}




















/*public class Repository<T, TKey> where TKey : notnull
{
    public Dictionary<TKey, T> Items { get; } = new();

    public void Register(TKey id, T item) => 
        Items.Add(id, item);

    public void Unregister(TKey id) => 
        Items.Remove(id);
}*/