using ProceduralGeneration.GameObjects;
using ProceduralGeneration.Algorithm;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System;

namespace ProceduralGeneration.Logic
{
    public class Renederer
    {
        static private void LocationNeiborhood(Location location, Transform tile, Vector2Int position)
        {
            List<Vector2Int> directions = Directions.directions.Keys.ToList();

            for (int j = 0; j < directions.Count; j++)
            {
                if (location.Grid.Contains(directions[j] + position)) {
                    GameObject edge = tile.Find("Edges").Find(Directions.directions[directions[j]]).gameObject;
#if UNITY_EDITOR
                    edge.SetActive(false);
#else
                    MonoBehaviour.Destroy(edge);
#endif
                    if (tile.Find("Vertixes") == null) continue;

                    for (int i = j; i < directions.Count; i++)
                    {
                        if (directions[i] == directions[j] || -directions[i] == directions[j]) continue;

                        if (location.Grid.Contains(directions[j] + position + directions[i]) && location.Grid.Contains(directions[i]+position))
                        {
                            GameObject vertix = tile.Find("Vertixes").Find(Directions.directions[directions[j]] + "_" + Directions.directions[directions[i]]).gameObject;
                            if (vertix == null) vertix = tile.Find("Vertixes").Find(Directions.directions[directions[i]] + "_" + Directions.directions[directions[j]]).gameObject;
                            if (vertix == null) continue;
#if UNITY_EDITOR
                            vertix.SetActive(false);
#else
                            MonoBehaviour.Destroy(vertix);
#endif
                        }
                    }
                }
            }
        }

        static private void WorldNeiborhood(in World world, Location location, Transform tile, Vector2Int position)
        {
            if (location.joinType == Location.TileJoinType.walls) return;

            List<Vector2Int> directions = Directions.directions.Keys.ToList();

            for (int j = 0; j < directions.Count; j++)
            {
                if (world.FindLocation(x => x.Grid.Contains(directions[j] + position)) != null)
                {
                    GameObject edge = tile.Find("Edges").Find(Directions.directions[directions[j]]).gameObject;

                    if (edge != null)
                    {
#if UNITY_EDITOR
                    edge.SetActive(false);
#else
                    MonoBehaviour.Destroy(edge);
#endif
                    }
                    if (tile.Find("Vertixes") == null) continue;

                    for (int i = j; i < directions.Count; i++)
                    {
                        if (directions[i] == directions[j] || -directions[i] == directions[j]) continue;

                        if (world.FindLocation(x => x.Grid.Contains(directions[i] + directions[j] + position)) != null && 
                            world.FindLocation(x => x.Grid.Contains(directions[i] + position)) != null)
                        {
                            GameObject vertix = tile.Find("Vertixes").Find(Directions.directions[directions[j]] + "_" + Directions.directions[directions[i]]).gameObject;
                            if (vertix == null) vertix = tile.Find("Vertixes").Find(Directions.directions[directions[i]] + "_" + Directions.directions[directions[j]]).gameObject;
                            if (vertix == null) continue;
#if UNITY_EDITOR
                            vertix.SetActive(false);
#else
                            MonoBehaviour.Destroy(vertix);
#endif
                        }
                    }
                }
            }
        }

        static private GameObject GetRandomGameObject(List<GameObject> gameObjects) => gameObjects[Generator.RandomNext(0, gameObjects.Count)];
        static private void CreateTile(in World world, in Location location, in Transform parent, in Vector2Int position)
        {
            Transform tile = MonoBehaviour.Instantiate(GetRandomGameObject(location.tiles), parent).transform;
            LocationNeiborhood(location, tile.transform, position);
            WorldNeiborhood(world, location, tile.transform, position);
            tile.position = new Vector3(position.x, 0, position.y) * world.Scale;
        }
        static public IEnumerator RenderGrid(World world, Location location, Transform parent, GameObject trigger = null)
        {
            Vector2 min = location.Grid.ToList()[0], max = Vector2.zero;

            Transform tiles = new GameObject("Tiles").transform;

            foreach (Vector2Int position in location.Grid)
            {
                min.x = Mathf.Min(min.x, position.x);
                min.y = Mathf.Min(min.y, position.y);
                max.x = Mathf.Max(max.x, position.x);
                max.y = Mathf.Max(max.y, position.y);
                CreateTile(world, location, tiles, position);
            }

            if (trigger != null)
            {
                Transform parentParent = parent.parent;

                MonoBehaviour.Destroy(parent.gameObject);

                parent = MonoBehaviour.Instantiate(trigger, parentParent).transform;
                parent.name = location.Name;

                Vector2 size = max - min, center = (max+min)/2;

                BoxCollider newTrigger = parent.GetComponent<BoxCollider>();
                newTrigger.size = new Vector3(size.x + 1, 100, size.y + 1) * world.Scale;
                newTrigger.center = new Vector3(center.x, 0, center.y) * world.Scale;
            }

            tiles.parent = parent;

            yield return null;
        }
        static public IEnumerator Render(World world, Transform parent, Action callback, GameObject trigger = null)
        {
            GameObject worldObject = new GameObject(world.Name);
            worldObject.transform.parent = parent;

            foreach (Location location in world.locations)
            {
                Transform locationType;
                if (worldObject.transform.Find(location.Type + 's') == null) { locationType = new GameObject(location.Type + 's').transform; locationType.parent = worldObject.transform; }
                else locationType = worldObject.transform.Find(location.Type + 's');

                Transform locationObject = new GameObject(location.Name).transform;

                locationObject.parent = locationType;

                yield return RenderGrid(world, location, locationObject, trigger);
            }

            callback?.Invoke();
        }

    }
}
