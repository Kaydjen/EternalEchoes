using System.Collections.Generic;

public delegate void AIDelegate(AI obj);
public delegate void IdDelegate(int id);

public static class EnemyRepository
{
    public static Dictionary<int, AI> Items = new();
    public static event AIDelegate OnRegister;
    public static event IdDelegate OnUnregister;
    public static event AIDelegate OnDeath;
    public static void Register(AI enemyObj, int id)
    {
        Items.Add(id, enemyObj);
        OnRegister?.Invoke(enemyObj);
    }
    public static void Unregister(AI enemyObj, int id)
    {
        Items.Remove(id);
        OnUnregister?.Invoke(id);
        OnDeath?.Invoke(enemyObj);
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