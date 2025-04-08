using System;
using System.Collections.Generic;

public delegate void AIDelegate(AI ai);
public delegate void IdDelegate(int id);

public static class AIRepository
{
    public static Dictionary<int, AI> Items = new();
    public static event AIDelegate OnRegister;
    public static event IdDelegate OnUnregister;
    public static event AIDelegate OnDeath;
    public static event AIDelegate OnInstantiate;
    public static void Register(AI ai, int id)
    {
        Items.Add(id, ai);
        OnRegister?.Invoke(ai);
    }
    public static void Unregister(AI ai, int id)
    {
        Items.Remove(id);
        OnUnregister?.Invoke(id);
        OnDeath?.Invoke(ai);
    }
    public static void InvokeOnInstantiate(AI ai)=> OnInstantiate?.Invoke(ai);
}




















/*public class Repository<T, TKey> where TKey : notnull
{
    public Dictionary<TKey, T> Items { get; } = new();

    public void Register(TKey id, T item) => 
        Items.Add(id, item);

    public void Unregister(TKey id) => 
        Items.Remove(id);
}*/