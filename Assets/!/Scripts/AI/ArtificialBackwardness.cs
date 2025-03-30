using UnityEngine;

public class ArtificialBackwardness : AI
{
    [SerializeField] private Transform _target;
    protected override void OnEnable()
    {
        base.OnEnable();
        base.SetDestionaiton(_target.position);
    }
}