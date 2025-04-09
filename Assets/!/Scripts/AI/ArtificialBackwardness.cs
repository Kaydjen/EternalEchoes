using UnityEngine;

public class ArtificialBackwardness : AI
{
    [SerializeField] private Transform _target;
    protected override void OnEnable()
    {
        base.OnEnable();
       // base.SetDestination(_target.position);
    }

    private void Update()
    {
        //base.SetDestination(_target.position);
    }
}