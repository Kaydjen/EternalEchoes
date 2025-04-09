using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public delegate void Baked();
[RequireComponent(typeof(NavMeshSurface))]
public class NavMeshManager : MonoBehaviour
{
    public static event Baked OnBaked;
    
    private NavMeshSurface _surface;
    private NavMeshData _data;
    public void BakeNavMesh()
    {
        _surface = GetComponent<NavMeshSurface>();  
        _surface.AddData();
        _surface.BuildNavMesh();
        _data = _surface.navMeshData;
        OnBaked?.Invoke();
    }
    public void UpdateNavMesh()
    {
        _surface.UpdateNavMesh(_data);
    }
    private void Start()
    {
        Invoke(nameof(BakeNavMesh), 1f);
    }
}