using ProceduralGeneration.Logic;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


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

        static public void Update() { lastDirection = Vector2Int.zero; }
        static public Vector2Int GetRandomDirection(in bool includeLastDirection = false)
        {
            List<Vector2Int> directions = Directions.directions.Keys.ToList();

            if (includeLastDirection) directions = directions.FindAll(x => x != lastDirection && x != -lastDirection);

            int ind = Generator.RandomNext(0, directions.Count);

            lastDirection = directions[ind];
            return directions[ind];
        }
    }
    public class WalkAlgorithm
    {
        #region location
        static internal HashSet<Vector2Int> Location(in int steps, in Vector2Int size, in Vector2Int absolutePosition)
        {
            HashSet<Vector2Int> stepsGeneration = new HashSet<Vector2Int>();
            Vector2Int position = Vector2Int.zero;

            for (int i = 0; i < steps; i++)
            {
                stepsGeneration.Add(position);

                Vector2Int randomDirection = Directions.GetRandomDirection();

                if ((position + absolutePosition + randomDirection).x <= size.x && (position + absolutePosition + randomDirection).x >= 0) position.x += randomDirection.x;
                if ((position + absolutePosition + randomDirection).y <= size.x && (position + absolutePosition + randomDirection).y >= 0) position.y += randomDirection.y;
            }

            return stepsGeneration;
        }

        static internal HashSet<Vector2Int> Location(ref Vector2Int position, in int steps, in Vector2Int size)
        {
            HashSet<Vector2Int> stepsGeneration = new HashSet<Vector2Int>();

            for (int i = 0; i < steps; i++)
            {
                stepsGeneration.Add(position);

                Vector2Int randomDirection = Directions.GetRandomDirection();

                if ((position + randomDirection).x <= size.x && (position + randomDirection).x >= 0) position.x += randomDirection.x;
                if ((position + randomDirection).y <= size.x && (position + randomDirection).y >= 0) position.y += randomDirection.y;
            }

            return stepsGeneration;
        }
        #endregion location

        #region corridor

        static internal HashSet<Vector2Int> Corridor(Vector2Int position, in int steps, in Vector2Int size)
        {
            HashSet<Vector2Int> stepsGeneration = new HashSet<Vector2Int>();

            Vector2Int randomDirection = Directions.GetRandomDirection(true);

            for (int i = 0; i < steps; i++)
            {
                stepsGeneration.Add(position);

                if ((position + randomDirection).x <= size.x
                    && (position + randomDirection).x >= 0) position.x += randomDirection.x;

                if ((position + randomDirection).y <= size.x
                    && (position + randomDirection).y >= 0) position.y += randomDirection.y;
            }

            return stepsGeneration;
        }

        static internal HashSet<Vector2Int> Corridor(ref Vector2Int position, in int steps, in Vector2Int size)
        {
            HashSet<Vector2Int> stepsGeneration = new HashSet<Vector2Int>();

            Vector2Int randomDirection = Directions.GetRandomDirection(true);
            for (int i = 0; i < steps; i++)
            {
                stepsGeneration.Add(position);

                if ((position + randomDirection).x <= size.x && (position + randomDirection).x >= 0) position.x += randomDirection.x;
                if ((position + randomDirection).y <= size.x && (position + randomDirection).y >= 0) position.y += randomDirection.y;
            }

            return stepsGeneration;
        }

        #endregion corridor

    }
}

