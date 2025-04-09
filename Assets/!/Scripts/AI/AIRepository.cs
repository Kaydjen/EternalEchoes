using System;
using System.Collections.Generic;

public delegate void AIDelegate(AI ai);
public delegate void IdDelegate(int id);

public static class AIRepository
{
    public static HashSet<AI> Enemies = new();
    public static HashSet<AI> Characters = new();
    public static event AIDelegate OnRegister;
    public static event AIDelegate OnUnregister;
    public static event AIDelegate OnDeath;
    public static event AIDelegate OnInstantiate;
    public static void Register(AI ai)
    {
        if(ai.Type == EAIType.Character)
        {
            Characters.Add(ai);
        }
        else if(ai.Type == EAIType.Enemy)
        {
            Enemies.Add(ai);
        }
        OnRegister?.Invoke(ai);
    }
    public static void Unregister(AI ai)
    {
        if(ai.Type == EAIType.Character)
        {
            Characters.Remove(ai);
        }
        else if(ai.Type == EAIType.Enemy)
        {
            Enemies.Remove(ai);
        }
        OnUnregister?.Invoke(ai);
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