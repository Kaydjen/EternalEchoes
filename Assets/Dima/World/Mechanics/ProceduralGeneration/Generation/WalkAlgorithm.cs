using ProceduralGeneration.Logic;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;


namespace ProceduralGeneration.Algorithm
{
    static public class Directions
    {
        static private Vector2Int lastDirection = Vector2Int.zero;

        static public readonly Dictionary<Vector2Int, string> directions = new Dictionary<Vector2Int, string>() {
            { new Vector2Int(0, 1), "Top"  },
            { new Vector2Int(0, -1), "Bottom" },
            { new Vector2Int(1, 0), "Right" },
            { new Vector2Int(-1, 0), "Left" },
        };

        static public readonly Dictionary<Vector2Int, string> complexDirections = new Dictionary<Vector2Int, string>() {
            { new Vector2Int(1, 1), "Top_Right"  },
            { new Vector2Int(-1, 1), "Top_Left" },
            { new Vector2Int(1, -1), "Bottom_Right" },
            { new Vector2Int(-1, -1), "Bottom_Left" },
        };

        static public Vector2Int ignore = Vector2Int.zero;

        static public void Update() { lastDirection = Vector2Int.zero;
            ignore = Vector2Int.zero;
        }
        static public Vector2Int GetRandomDirection(in bool includeLastDirection = false)
        {
            List<Vector2Int> directions = Directions.directions.Keys.ToList();

            if (includeLastDirection) directions = directions.FindAll(x => x != -lastDirection && x != lastDirection && x != ignore);

            int ind = Generator.RandomNext(directions.Count);

            lastDirection = directions[ind];
            return directions[ind];
        }
    }
    public class WalkAlgorithm
    {
        static internal Grid2Int Rectangles(in Vector2Int startPosition, in Vector2Int size)
        {
            Grid2Int grid = new Grid2Int { startPosition };

            Vector2Int currentPosition = startPosition;

            for (int i = 0; i < Generator.RandomNext(1, (int)Mathf.Sqrt(size.magnitude)); i++)
            {
                currentPosition = grid[Generator.RandomNext(grid.Count)];

                int width = Generator.RandomNext(size.x / 5, size.x/2), height = Generator.RandomNext(size.y / 5, size.y/2);

                for (int y = 0; y < height; y++) for (int x = 0; x < width; x++) 
                {
                        Vector2Int clamped = currentPosition + new Vector2Int(x, y);
                        clamped.Clamp(startPosition, startPosition+size);

                        grid += clamped;
                }
            }

            return grid;
        }
        
        static internal Grid2Int RandomWalk(Vector2Int position, in int steps)
        {
            Grid2Int path = new Grid2Int { position };

            for (int i = 0; i < steps; i++)
            {
                position += Directions.GetRandomDirection();
                path += position;
            }

            return path;
        }

        static internal Grid2Int RandomWalk(ref Vector2Int position, in int steps)
        {
            Grid2Int path = new Grid2Int { position };

            for (int i = 0; i < steps; i++)
            {
                position += Directions.GetRandomDirection();
                path += position;
            }

            return path;
        }

        #region location
        static internal Grid2Int Location(in Vector2Int position, in int steps, in int iterations, in bool randomize = false)
        {
            Grid2Int location = new Grid2Int();

            for (int i = 0; i < iterations; i++)
                location += (randomize) ? RandomWalk(location[Generator.RandomNext(location.Count)], steps) : RandomWalk(position, steps);

            return location;
        }

        static internal Grid2Int Location(ref Vector2Int position, in int steps, in int iterations)
        {
            Grid2Int location = new Grid2Int();

            for (int i = 0; i < iterations; i++)
            {
                location += RandomWalk(ref position, steps);
            }

            return location;
        }
        #endregion location

        #region corridor

        static internal Grid2Int Corridor(Vector2Int position, in int steps, Vector2Int direction = default, in int width = 1)
        {
            Grid2Int corridor = new Grid2Int { position };

            if (!Directions.directions.ContainsKey(direction)) direction = Directions.GetRandomDirection(true);

            for (int i = 0; i < steps; i++)
            {
                corridor += position;
                position += direction;
            }

            return corridor;
        }

        static internal Grid2Int Corridor(ref Vector2Int position, in int steps, Vector2Int direction = default, in int width = 1)
        {
            Grid2Int corridor = new Grid2Int() { position };

            if (!Directions.directions.ContainsKey(direction)) direction = Directions.GetRandomDirection(true);

            for (int i = 0; i < steps; i++) {

                position += direction;
                corridor += position;
            }

            return corridor;
        }

        #endregion corridor
    }
}

