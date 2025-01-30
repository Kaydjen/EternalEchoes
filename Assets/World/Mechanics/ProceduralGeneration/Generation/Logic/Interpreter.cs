using System.Collections.Generic;
using UnityEngine;
using ProceduralGeneration.GameObjects;
using ProceduralGeneration.SeriazableObjects;
using UnityEngine.UIElements;

namespace ProceduralGeneration.Logic
{
    public class Interpreter
    {
        static public HashSet<Vector2Int> ParseStringToGrid(in string layoutString, in Vector2Int absolutePosition)
        {
            HashSet<Vector2Int> layout = new HashSet<Vector2Int>();
            string[] layoutArray = layoutString.Replace("\r", "").Split('\n');

            for (int y = 0; y < layoutArray.Length; y++)
            {
                for (int x = 0; x < layoutArray[y].Length; x++)
                {
                    if (layoutArray[y][x] != '■' && layoutArray[y][x] != '□') Debug.Log("Syka tu typoj simvol: " + (int)layoutArray[y][x]);
                    if (layoutArray[y][x] != '■') continue;
                    layout.Add(new Vector2Int(x, y) + absolutePosition);
                }
            }

            return layout;
        }

        static public World ParseSeriazableToWorld(in SeriazableWorld world) => new World(world);
        static public Location ParseSeriazableToLocation(in SeriazableLocation location, in World world) => new Location(location, world);

        static public Location ParseSeriazableToDungeon(in SeriazableDungeon location, in World world, in Vector2Int position) => new Location(location, world, ParseStringToGrid(location.layout, position));
    }
}
