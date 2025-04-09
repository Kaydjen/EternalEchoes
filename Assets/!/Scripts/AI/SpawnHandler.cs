using Optimization;
using ProceduralGeneration.GameObjects;
using UnityEngine;


public class SpawnHandler : MonoBehaviour
{
    private void Start()
    {
        ChunkHandler.onChunkEnter += Spawn;
    }
    private void Spawn(Location location, Collider collider)
    {
        if(!collider.tag.Equals("Character")) return;
        foreach(Prop prop in location.GetDescedants<Prop>((x) => x.Type.ToLower() == "spawner"))
        {
            if(prop.GameObject.TryGetComponent(out AISpawnCoordinator spawner)) spawner.RequestEnemySpawn(true);
            if(prop.GameObject.TryGetComponent(out AIGroupManager groupManager))
            {
                groupManager.AddNewTarget(collider.transform);
                groupManager.StartLogic();
            }
        }
    }
}
