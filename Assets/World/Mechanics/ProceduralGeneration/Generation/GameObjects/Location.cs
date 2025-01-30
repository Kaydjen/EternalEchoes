using ProceduralGeneration.Logic;
using ProceduralGeneration.SeriazableObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ProceduralGeneration.GameObjects
{
    [Serializable] public class Location : Object
    {
        protected HashSet<Vector2Int> grid = new HashSet<Vector2Int>();

        protected int layer;
        public HashSet<Vector2Int> Grid 
        { 
            set 
            {  
               foreach (Location location in world.locations)
               {
                    if (location.Equals(this)) continue;
                    if (location.layer <= layer)
                    {
                        value.ExceptWith(location.Grid);
                        
                    }
                    else
                    {
                        location.Grid.ExceptWith(value);
                 
                    }
               }

               grid = value;
            }
            get { return grid; }
        }

        public TileJoinType joinType = 0;
        
        public List<GameObject> tiles;

        public World world;
        public enum TileJoinType
        {
            none = 0,
            walls = 1,
        }

        public Vector2Int GetRandomPosition()
        {
            List<Vector2Int> gridList = Grid.ToList();

            return gridList[Generator.RandomNext(0, gridList.Count)];
        }
        
        public Location(SeriazableLocation location, in World world) : base(location.name, location.type)
        {
            layer = location.layer;
            tiles = location.tiles;
            joinType = location.joinType;
            this.world = world;
            world.AddLocation(this);
        }

        public Location(SeriazableLocation location, in World world, HashSet<Vector2Int> grid) : base(location.name, location.type)
        {
            layer = location.layer;
            tiles = location.tiles;
            joinType = location.joinType;
            this.world = world;
            Grid = grid;
            
            world.AddLocation(this);
        }
    }
}

