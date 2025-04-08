using ProceduralGeneration.GameObjects;
using ProceduralGeneration.Algorithm;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System;
using Optimization;
using UnityEngine.UIElements;

namespace ProceduralGeneration.Logic
{
    public class Renderer3D
    {
        static private void WorldNeiborhood(Location location, Transform tile, in Vector2Int position)
        {
            World world = location.GetParent<World>();
            int count = 0;

            if (location.worldJoinType != Location.TileJoinType.walls && world != null) count = world.GetChildren<Location>().Count;
            else if (location.localJoinType != Location.TileJoinType.walls) count++;
            if (count == 0) return;

            Grid2Int grid = (count == 1) ? location.Grid : world.Grid;

            List<Vector2Int> directions = Directions.directions.Keys.ToList();

            for (int i = 0; i < directions.Count; i++)
            {
                if (!grid.Contains(directions[i] + position)) continue;
                GameObject edge = tile.Find("Edges").Find(Directions.directions[directions[i]]).gameObject;

                if (edge != null)
                {
#if UNITY_EDITOR
                    MonoBehaviour.DestroyImmediate(edge);
#else
                    MonoBehaviour.Destroy(edge);
#endif
                }
            }

            if (tile.Find("Vertixes") == null) return;

            directions = Directions.complexDirections.Keys.ToList();
            for (int i = 0; i < directions.Count; i++)
            {
                if (!grid.Contains(directions[i] + position) ||
                    !grid.Contains(new Vector2Int(directions[i].x, 0) + position) ||
                    !grid.Contains(new Vector2Int(0, directions[i].y) + position)) continue;

                GameObject vertix = tile.Find("Vertixes").Find(Directions.complexDirections[directions[i]]).gameObject;
                if (vertix == null) continue;
#if UNITY_EDITOR
                MonoBehaviour.DestroyImmediate(vertix);
#else
                MonoBehaviour.Destroy(vertix);
#endif
            }
        }
        static private GameObject GetRandomObject(List<GameObject> gameObjects) => gameObjects[Generator.RandomNext(gameObjects.Count)];
        static private GameObject GetRandomObjectWithNoise(List<GameObject> gameObjects, in Vector2Int position, in Vector2Int size)
        {
            float x = position.x / size.x + Generator.Seed,
                y = position.y / size.y + Generator.Seed,
                noiseValue = Mathf.PerlinNoise(x, y);

            return gameObjects[Mathf.RoundToInt(noiseValue * (gameObjects.Count - 1))];
        }
        static public GameObject Create(GameObject gameObject, Transform parent = null, in Vector3 position = default, in Vector3 eulerAngles = default, in bool pivot = true)
        {
            Transform created = MonoBehaviour.Instantiate(gameObject, parent).transform;
            created.transform.localEulerAngles = eulerAngles;

            if (pivot)
            {
                Transform pivotTransform = new GameObject().transform;
                pivotTransform.parent = parent;
                pivotTransform.localPosition = position + new Vector3(0, Interpreter.DeepestCoordinate(gameObject.gameObject), 0);

                created.parent = pivotTransform;
                created.localPosition = new Vector3(0, created.localPosition.y, 0);

                pivotTransform.localPosition = position;

                created.parent = parent;
#if UNITY_EDITOR
                UnityEngine.Object.DestroyImmediate(pivotTransform.gameObject);
#else
                UnityEngine.Object.Destroy(pivotTransform.gameObject);
#endif
            }
            else created.localPosition = position;

            return created.gameObject;
        }
        static private void CreateTile(in Location location, in Transform parent, in Vector2Int position) {
            World world = location.GetParent<World>();

            CreateTile(location, parent, position, world.Scale);
        }

        static private void CreateTile(in Location location, in Transform parent, in Vector2Int position, in float scale = 1)
        {
            World world = location.GetParent<World>();
            Vector2Int size = (world == null) ? location.Grid.Max - location.Grid.Min : world.Grid.Max - world.Grid.Min;

            WorldNeiborhood(location, Create(GetRandomObjectWithNoise(location.tiles, position, size), parent,
                new Vector3(position.x, 0, position.y) * scale).transform, position);
        }

        static public IEnumerator RenderGrid(Location location, Transform parent, GameObject trigger = null)
        {
            parent.name = "Tiles";

            yield return RenderGrid(location, parent, location.GetParent<World>().Scale, (uint)Mathf.Max(location.Grid.Count/10, 5));

            if (trigger != null)
            {
                Vector2 min = location.Grid.Min, max = location.Grid.Max;

                Transform newParent = MonoBehaviour.Instantiate(trigger, parent.transform.parent).transform;
                newParent.name = location.Name;
                parent.parent = newParent;

                Vector2 size = max - min, center = (max+min)/2;

                BoxCollider newTrigger = newParent.GetComponent<BoxCollider>();
                newTrigger.size = new Vector3(size.x + 1, 100, size.y + 1) * location.GetParent<World>().Scale;
                newTrigger.center = new Vector3(center.x, 0, center.y) * location.GetParent<World>().Scale;

                newParent.GetComponent<ChunkHandler>()?.SetLocation(location);
            }

            yield return null;
        }

        static public IEnumerator RenderGrid(Location location, Transform parent, uint iterationsPerFrame = 0)
        {
            yield return RenderGrid(location, parent, location.GetParent<World>().Scale, iterationsPerFrame);
        }


        static public IEnumerator RenderGrid(Location location, Transform parent, float scale = 1, uint iterationsPerFrame = 0)
        {
            for (int i = 0; i < location.Grid.Count; i++) {
                CreateTile(location, parent, location.Grid[i], scale);
                if (iterationsPerFrame <= 0) continue;
                if (i%iterationsPerFrame == 0) yield return null;
            }
        }

        static public void RenderGrid(Location location, Transform parent, in float scale = 1)
        {
            for (int i = 0; i < location.Grid.Count; i++) CreateTile(location, parent, location.Grid[i], scale);
        }
        static public void RenderGrid(Grid2Int grid, GameObject prefab, Transform parent = null, float scale = 1)
        {
            Transform tiles = new GameObject("Tiles").transform;
            tiles.parent = parent;

            for (int i = 0; i < grid.Count; i++)
                Create(prefab, tiles, new Vector3(grid[i].x, 0, grid[i].y) * scale);
        }

        static public IEnumerator RenderProps(Location location, Transform parent) {
            RenderProps(location, parent, location.GetParent<World>().Scale);
            yield return null;
        }

        static public void RenderProps(Location location, Transform parent, float scale = 1)
        {
            Transform gameObjects = new GameObject("Assets").transform;
            gameObjects.parent = parent;

            foreach (Prop prop in location.GetChildren<Prop>())
            {
                Transform propType = gameObjects.Find(prop.Type + 's');
                if (propType == null) { propType = new GameObject(prop.Type + 's').transform; propType.parent = gameObjects; }
                if (prop.prefab == null) continue;

                prop.GameObject = Create(prop.prefab, propType, new Vector3(prop.pivot.x, 0, prop.pivot.y) * scale, prop.rotation);
            }
        }

        static public IEnumerator Render(World world, Transform parent, Action callback, GameObject trigger = null)
        {
            if (parent == null) parent = new GameObject().transform;
            parent.gameObject.name = world.Name;

            List<Location> locations = world.GetChildren<Location>();

            foreach (Location location in locations)
            {
                Transform locationType;
                if (parent.Find(location.Type + 's') == null) { locationType = new GameObject(location.Type + 's').transform; locationType.parent = parent; }
                else locationType = parent.Find(location.Type + 's');

                Transform locationObject = new GameObject(location.Name).transform;

                locationObject.parent = locationType;

                yield return RenderGrid(location, locationObject, trigger);
                yield return RenderProps(location, locationObject.parent);
            }

            callback?.Invoke();
        }
    }
}
