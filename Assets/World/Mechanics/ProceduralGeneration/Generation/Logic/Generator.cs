using UnityEngine;
using System.Collections.Generic;
using ProceduralGeneration.Algorithm;
using ProceduralGeneration.GameObjects;
using ProceduralGeneration.SeriazableObjects;
using System;
using UnityEngine.SocialPlatforms;

namespace ProceduralGeneration.Logic
{
    /* public class Generator2_0
     {
         #region private properties

         #region generation settings
         [InspectorName("Generation settings")]
         [SerializeField] private float scale;
         [SerializeField] private Vector2Int size;

         [NonReorderable][SerializeField] private List<GameObject> tilePrefabs;
         #endregion generation settings

         private HashSet<Vector2Int> world = new HashSet<Vector2Int>();
         #endregion private properties

         #region public properties
         public enum WalkType
         {
             random,
             current,
             constant
         }

         #endregion public properties
         #region methods

         #region public

         public void Create()
         {
             world = new HashSet<Vector2Int>();
         }
         public void Generate(ref Vector2Int startPosition, in WalkType iterationStart, int iterations = 5, int steps = 20)
         {
             HashSet<Vector2Int> iterationsGenerated = new HashSet<Vector2Int>();

             for (int i = 0; i < iterations; i++)
             {
                 if (iterationStart == WalkType.random && iterationsGenerated.Count > 0)
                 {
                     Vector2Int randomPosition = iterationsGenerated.ToArray()[UnityEngine.Random.Range(0, iterationsGenerated.Count)];

                     iterationsGenerated.UnionWith(WalkAlgorithm.WalkAlgorithm.Location(randomPosition, steps, size));
                 }
                 else if (iterationStart == WalkType.current) iterationsGenerated.UnionWith(WalkAlgorithm.Location(ref startPosition, steps, size));
                 else iterationsGenerated.UnionWith(WalkAlgorithm.WalkAlgorithm.Location(startPosition, steps, size));
             }

             world.UnionWith(iterationsGenerated);
         }
         public void GenerateMap(Vector2Int startPosition, in WalkType iterationStart, int corridors = 10)
         {

             for (int i = 0; i < corridors; i++)
             {
                 if (world.Count > 0)
                 {
                     startPosition = world.ToArray()[UnityEngine.Random.Range(0, world.Count)];
                 }
                 world.UnionWith(WalkAlgorithm.WalkAlgorithm.Corridor(ref startPosition, UnityEngine.Random.Range(21, 60), size));

                 Generate(ref startPosition, iterationStart, UnityEngine.Random.Range(10, 51), UnityEngine.Random.Range(5, 51));
             }

         }

         private void Neiborhood(Transform tile, Vector2Int position)
         {
             foreach (Vector2Int direction in WalkAlgorithm.Directions.directions.Keys.ToList())
             {
                 if (world.Contains(position + direction)) tile.Find("Edges").Find(WalkAlgorithm.Directions.directions[direction]).gameObject.SetActive(false);
             }
         }
         public void Render(Transform parent)
         {
             foreach (Vector2Int position in world)
             {
                 Transform tileTransform = MonoBehaviour.Instantiate(GetRandomTilePrefab(), parent).transform;
                 tileTransform.localPosition = new Vector3(position.x, 0, position.y) * scale;

                 Neiborhood(tileTransform, position);
             }
         }
         #endregion public

         #region private

         private GameObject GetRandomTilePrefab()
         {
             return tilePrefabs[UnityEngine.Random.Range(0, tilePrefabs.Count)];
         }
         #endregion private
         #endregion methods
     }*/
    public class Generator
    {
        static public System.Random randomizer = new System.Random();
        static public int Seed
        {
            set { randomizer = new System.Random(value); }
        }

        static private bool CanGenerateDungeon(World world, Vector2Int position, HashSet<Vector2Int> layout)
        {
            foreach (Vector2Int tilePosition in layout)
            {
                Vector2Int absolutePosition = position + tilePosition;
                if (absolutePosition.x > world.Size.x ||
                    absolutePosition.y > world.Size.y) return false;
            }

            return true;
        }

        static private Vector2Int CheckRandomPosition(World world, Vector2Int position, HashSet<Vector2Int> layout)
        {
            if (CanGenerateDungeon(world, position, layout)) return position;
            else return CheckRandomPosition(world, world.GetRandomPosition(), layout);
        }
        static public void GenerateDungeon(Location location)
        {
            if (location == null) return;
            if (location.world == null) return;

            SeriazableDungeon dungeon = (SeriazableDungeon)GetRandomLocation(Database.seriazableWorlds[Database.worlds.IndexOf(location.world)], x => x.GetType().Equals(typeof(SeriazableDungeon)));
            if (dungeon == null) return;

            Interpreter.ParseSeriazableToDungeon(dungeon, location.world, location.GetRandomPosition());
        }

        static public void CreateSpawn(World world)
        {
            if (world == null) return;

            SeriazableWorld seriazableWorld = Database.seriazableWorlds[Database.worlds.IndexOf(world)];

            if (seriazableWorld == null) return;

            Interpreter.ParseSeriazableToDungeon((SeriazableDungeon)seriazableWorld.spawn, world, seriazableWorld.spawnPosition);
        }

        static private SeriazableLocation GetRandomLocation(SeriazableWorld world, System.Predicate<SeriazableLocation> predicate)
        {
            List<SeriazableLocation> locations = Database.GetLocations(world, predicate);

            return locations[RandomNext(0, locations.Count)];
        }

        static public void GenerateCorridor(World world, ref Vector2Int position, int lenght, int iterations)
        {
            if (world == null) return;

            HashSet<Vector2Int> grid = new HashSet<Vector2Int>();

            for (int i = 0; i < iterations; i++)
            {
                grid.UnionWith(WalkAlgorithm.Corridor(ref position, lenght, world.Size));
            }

            Directions.Update();

            Location corridor = Interpreter.ParseSeriazableToLocation(GetRandomLocation(Database.seriazableWorlds[Database.worlds.IndexOf(world)], x => x.type.Equals("Corridor")), world);
            corridor.Grid = grid;
        }

        static public void GenerateLocation(World world, ref Vector2Int position, int steps, int iterations)
        {
            if (world == null) return;

            HashSet<Vector2Int> grid = new HashSet<Vector2Int>();

            for (int i = 0; i < iterations; i++)
            {
                grid.UnionWith(WalkAlgorithm.Location(ref position, steps, world.Size));
            }
            Location location = Interpreter.ParseSeriazableToLocation(GetRandomLocation(Database.seriazableWorlds[Database.worlds.IndexOf(world)],
                x => !x.type.Equals("Corridor") && !x.GetType().Equals(typeof(SeriazableDungeon))), world);

            location.Grid = grid;
        }

        static private void GenerateDungeons(World world)
        {
            if (world == null) return;

            List<Location> locations = new List<Location>();
            locations.AddRange(world.locations);

            foreach (Location location in locations)
            {
                int iterations = RandomNext(0, location.Grid.Count/RandomNext(10, location.Grid.Count));
                for (int i = 0; i < iterations; i++) GenerateDungeon(location);
            }
        }
        static private void GenerateTreeSystem(World world, Vector2Int position, int iterations = 5)
        {
            for (int i = 0; i < iterations; i++)
            {
                GenerateCorridor(world, ref position, (int)(RandomNext(1, 26) * ((double)RandomNext(1, world.Size.x * world.Size.y / 10000))), (int)((double)RandomNext(1, world.Size.x * world.Size.y / 50000)));
                GenerateLocation(world, ref position, (int)(RandomNext(1, 16) * ((double)RandomNext(1, world.Size.x * world.Size.y / 10000))),
                    (int)(RandomNext(1, 21) * ((double)RandomNext(1, world.Size.x * world.Size.y / 10000))));
                GenerateTreeSystem(world, position, iterations - RandomNext(1, iterations + 1));
            }
        }

        static private void GenerateWorld(World world)
        {
            CreateSpawn(world);
            GenerateTreeSystem(world, world.GetRandomPosition(), RandomNext(4, (world.Size.x * world.Size.y) / 50000));
            world.CollectGarbage();
            GenerateDungeons(world);
            world.CollectGarbage();
        }
        static public int RandomNext(int min, int max) => (max > min) ? randomizer.Next(min, max) : min;
        static public void CreateWorld(int index)
        {
            Database.CreateWorld(index);
            World world = Database.worlds[index];

            GenerateWorld(world);
        }
    }
}