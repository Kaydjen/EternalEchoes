using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractStrategyContext : MonoBehaviour
{
    public IInteractStrategy ContextStrategy { get; set; }
    public InteractStrategyContext(IInteractStrategy strategy)
    {
        ContextStrategy = strategy;
    }
    public void ExecuteAlgorithm1()
    {
        ContextStrategy.Action1();
    }
    public void ExecuteAlgorithm2()
    {
        ContextStrategy.Action2();
    }
    public void ExecuteAlgorithm3()
    {
        ContextStrategy.Action3();
    }
    public void ExecuteAlgorithm4()
    {
        ContextStrategy.Action4();
    }
    public void ExecuteAlgorithm5()
    {
        ContextStrategy.Action5();
    }
}
