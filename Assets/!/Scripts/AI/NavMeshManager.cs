using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshSurface))]
public class NavMeshManager : MonoBehaviour
{
    public static NavMeshManager Instance;
    private NavMeshSurface _surface;
    private NavMeshData _data;
    public void BakeNavMesh()
    {
        _surface = GetComponent<NavMeshSurface>();  
        _surface.AddData();
        _surface.BuildNavMesh();
        _data = _surface.navMeshData;
    }
    public void UpdateNavMesh()
    {
        _surface.UpdateNavMesh(_data);
    }
    private void Start()
    {
        Instance = this;
        Invoke(nameof(BakeNavMesh), 5f);
    }
}