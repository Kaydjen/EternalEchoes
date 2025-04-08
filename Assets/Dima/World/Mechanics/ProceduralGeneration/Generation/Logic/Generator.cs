using UnityEngine;
using System.Collections.Generic;
using ProceduralGeneration.Algorithm;
using ProceduralGeneration.GameObjects;
using ProceduralGeneration.SeriazableObjects;
using System.Linq;
using System;
using UnityEngine.UIElements;
using System.Collections.ObjectModel;
using JetBrains.Annotations;

namespace ProceduralGeneration.Logic
{
    public class Generator
    {
        static public System.Random randomizer = new System.Random();
        static private int seed;
        static public int Seed
        {
            get => seed;
            set {
                seed = value;
                randomizer = new System.Random(seed);
            }
        }
        static protected bool CanGenerateDungeon(World world, Vector2Int position, HashSet<Vector2Int> layout)
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
        /*static public void GenerateDungeon(Location location)
        {
            if (location == null) return;
            if (location.world == null) return;

            SeriazableDungeon dungeon = (SeriazableDungeon)GetRandomLocation(Database.seriazableWorlds[Database.worlds.IndexOf(location.world)], x => x.GetType().Equals(typeof(SeriazableDungeon)));
            if (dungeon == null) return;

            Interpreter.CreateDungeon(dungeon, location.world, location.GetRandomPosition());
        }*/

        /*static private void GenerateDungeons(World world)
        {
            if (world == null) return;

            List<Location> locations = new List<Location>();
            locations.AddRange(world.locations);

            foreach (Location location in locations)
            {
                int iterations = RandomNext(0, location.Grid.Count/RandomNext(10, location.Grid.Count));
                for (int i = 0; i < iterations; i++) GenerateDungeon(location);
            }
        }*/

        static public Location CreateSpawn(World world)
        {
            if (world == null) return null;

            SeriazableWorld seriazableWorld = Database.seriazableWorlds[Database.GetWorldIndex(world)];

            if (seriazableWorld == null) return null;

            return new Location((SeriazableDungeon)seriazableWorld.spawn, world, Grid2Int.StringToGrid(((SeriazableDungeon)seriazableWorld.spawn).layout, seriazableWorld.startingPoint));
        }

        static private SeriazableLocation GetRandomLocation(SeriazableWorld world, Predicate<SeriazableLocation> predicate)
        {
            List<SeriazableLocation> locations = Database.GetLocations(world, predicate);

            return locations[RandomNext(locations.Count)];
        }

        static private Grid2Int CreateLocationGrid(in Vector2Int startPosition, in Vector2Int size)
        {
            Grid2Int grid = WalkAlgorithm.Rectangles(startPosition, size), walkAlgorithm = new Grid2Int();

            for (int i = 0; i < grid.Count; i++)
                if (grid.GetNeighbours(grid[i]).Count <= 3) 
                    walkAlgorithm += WalkAlgorithm.Location(grid[i], RandomNext((size.x + size.y) / 2), RandomNext((size.x + size.y) / 2));

            grid += walkAlgorithm;

            return grid;
        }
        static private Location CreateLocation(World world, Grid2Int grid)
        {
            if (world == null) return null;

            return new Location(GetRandomLocation(Database.seriazableWorlds[Database.GetWorldIndex(world)],
                x => !x.type.Equals("Corridor") && !x.GetType().Equals(typeof(SeriazableDungeon))), 
                world, grid);
        }

        static public void CreateLocations(World world, HashSet<Vector2Int> potenialLocationPositions, in float locationPercent = 1)
        {
            int locationToCreateCount = Mathf.RoundToInt(potenialLocationPositions.Count * locationPercent);
            List<Vector2Int> locationToCreatePosition = potenialLocationPositions.OrderBy(x => RandomNext(potenialLocationPositions.Count)).Take(locationToCreateCount).ToList();

            for (int i = 0; i < locationToCreateCount; i++)
            {
                int size = RandomNext(10, 16);
                CreateLocation(world, CreateLocationGrid(locationToCreatePosition[i], new Vector2Int(size, size)));
            }
                
        }
        static public Prop CreateProp(Location location, SeriazableProp prop)
        {
            if (prop == null) return null;

            int rotation = RandomNext(-1, 3);

            Vector2Int position = location.GetRandomPosition();

            Prop propToReturn = new Prop(prop, location, position + Interpreter.RotatePivot(prop.hitbox, prop.pivot, rotation), Grid2Int.LayoutArrayToGrid(Interpreter.RotateGrid(prop.hitbox, rotation), position));
            propToReturn.rotation = new Vector3(0, rotation * 90);

            return propToReturn;
        }
        static public Prop CreateProp(Location location, SeriazableProp prop, Vector2Int offset, int rotation)
        {
            if (prop == null) return null ;

            Vector2Int position = location.Grid.Min + offset;

            Prop propToReturn = new Prop(prop, location, position + Interpreter.RotatePivot(prop.hitbox, prop.pivot, rotation), Grid2Int.LayoutArrayToGrid(Interpreter.RotateGrid(prop.hitbox, rotation), position));
            propToReturn.rotation = new Vector3(0, rotation * 90);

            return propToReturn;
        }
        static private void CreateProps(World world)
        {
            List<Location> locations = new List<Location>(world.GetChildren<Location>());

            for (int iLocation = 0; iLocation < locations.Count; iLocation++)
            {
                if (locations[iLocation].GetParent() == null) continue;

                var serializedLocation = Database.GetLocation(Database.seriazableWorlds[world.Layer], x => x.name.Equals(locations[iLocation].Name));
                if (serializedLocation == null) continue;

                int gridLenght = locations[iLocation].Grid.Count;

                switch (serializedLocation)
                {
                    case SeriazableDungeon dungeon:
                        for (int iConfiguration = 0; iConfiguration < dungeon.constProps.Count; iConfiguration++)
                            CreateProp(locations[iLocation], dungeon.constProps[iConfiguration].seriazableProp,
                                dungeon.constProps[iConfiguration].offset, dungeon.constProps[iConfiguration].rotation);
                        continue;

                    case SeriazableLocation location:
                        for (int iConfiguration = 0; iConfiguration < location.possibleProps.Length; iConfiguration++)
                        {
                            for (int iAmount = 0; iAmount < serializedLocation.possibleProps[iConfiguration].maxAmount; iAmount++)
                            {
                                float chance = RandomNext(100f);

                                if (chance < serializedLocation.possibleProps[iConfiguration].chance)
                                    CreateProp(locations[iLocation], serializedLocation.possibleProps[iConfiguration].seriazableProp);
                            }
                        }
                        break;

                }
                if (gridLenght != locations[iLocation].Grid.Count) locations[iLocation].UpdateWorldGrid(x => true);
            }
        }

        static private HashSet<Vector2Int> FindDeadEnds(World world)
        {
            Grid2Int worldGrid = world.Grid;
            HashSet<Vector2Int> deadEnds = new HashSet<Vector2Int>();

            List<Location> corridors = world.GetChildren<Location>((x) => x.Type == "Corridor");

            for (int iCorridor = 0; iCorridor < corridors.Count; iCorridor++)
            {
                Grid2Int grid = corridors[iCorridor].Grid;
                for (int iPosition = 0; iPosition < grid.Count; iPosition++)
                    if (worldGrid.GetNeighbours(grid[iPosition]).Count == 1) deadEnds.Add(grid[iPosition]);
            }

            return deadEnds;
        }
        static public Location CreateCorridor(in World world, in Grid2Int grid)
        {
            if (world == null) return null;

            return new Location(GetRandomLocation(Database.seriazableWorlds[Database.GetWorldIndex(world)], x => x.type.Equals("Corridor")), world, grid);
        }
        static private void CreateCorridors(in World world, Vector2Int position, in int corridorCount, out HashSet<Vector2Int> potenialLocationPositions)
        {
            potenialLocationPositions = new HashSet<Vector2Int>();

            CreateCorridors(world, position, corridorCount, potenialLocationPositions);
        }

        static private void CreateCorridors(World world, Vector2Int position, in int corridorCount, HashSet<Vector2Int> potenialLocationPositions)
        {
            for (int i = 0; i < corridorCount; i++)
            {
                CreateCorridor(world, WalkAlgorithm.Corridor(ref position,
                    RandomNext(10, 16), default, RandomNext(1, 10)));

                potenialLocationPositions.Add(position);
            }
        }

        static private void Generate(World world, Dictionary<Vector2Int, List<Vector2Int>> possiblePositions)
        {
            HashSet<Vector2Int> potentialRoomPositions = new HashSet<Vector2Int>();
            List<Vector2Int> directions = Directions.directions.Keys.ToList();

            for (int iDirection = 0; iDirection < directions.Count; iDirection++)
            {
                List<Vector2Int> currentPossiblePositions = possiblePositions[directions[iDirection]];

                Vector2Int currentPosition = currentPossiblePositions[RandomNext(currentPossiblePositions.Count)];

                CreateCorridor(world, WalkAlgorithm.Corridor(ref currentPosition, 16, directions[iDirection], 5));
                potentialRoomPositions.Add(currentPosition);
                Directions.ignore = -directions[iDirection];

                CreateCorridors(world, currentPosition, RandomNext(1, 3), potentialRoomPositions);

                Directions.Update();
            }

            CreateLocations(world, potentialRoomPositions, 1);
        }

        static private void GenerateWorld(World world)
        {
            Location spawn = CreateSpawn(world);

            Dictionary<Vector2Int, List<Vector2Int>> possiblePositions = new Dictionary<Vector2Int, List<Vector2Int>>();

            for (int iTile = 0; iTile < spawn.Grid.Count; iTile++)
            {
                List<Vector2Int> neighbours = spawn.Grid.GetNeighboursDirections(spawn.Grid[iTile]);
                if (neighbours.Count == 4) continue;

                List<Vector2Int> directions = new List<Vector2Int>(Directions.directions.Keys).Except(neighbours).ToList();

                for (int iDirection = 0; iDirection < directions.Count; iDirection++)
                {
                    if (possiblePositions.ContainsKey(directions[iDirection])) possiblePositions[directions[iDirection]].Add(spawn.Grid[iTile]);
                    else possiblePositions.Add(directions[iDirection], new List<Vector2Int> { spawn.Grid[iTile] });
                }
            }

            Generate(world, possiblePositions);
            CreateLocations(world, FindDeadEnds(world));

            CreateProps(world);
        }
        static public int RandomNext(int min, int max) => (max <= min) ? min : randomizer.Next(min, max);
        static public int RandomNext(int max) => RandomNext(0, max);
        static public float RandomNext(float min, float max) => (max <= min) ? min : max - (max - min) * (float)randomizer.NextDouble();
        static public float RandomNext(float max) => RandomNext(0, max);
        static public World CreateWorld(int index)
        {
            World world = Database.CreateWorld(index);

            GenerateWorld(world);

            return world;
        }
    }
}