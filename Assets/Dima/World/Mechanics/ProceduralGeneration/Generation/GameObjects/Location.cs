using ProceduralGeneration.Logic;
using ProceduralGeneration.SeriazableObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ProceduralGeneration.GameObjects
{
    [Serializable] public class Location : GenerationObject
    {
        protected int layer;
        public void UpdateWorldGrid(Predicate<Location> predicate, World world = null)
        {
            if (world == default) world = GetParent<World>();

            List<Location> locations = new List<Location>(world.GetChildren<Location>()); 

            for (int i = 0; i < locations.Count; i++)
            {
                if (grid.Count <= 0) return;

                Location location = locations[i];

                if (location.Equals(this)) continue;

                if (predicate.Invoke(location)) grid -= location.Grid;
                else location.grid -= grid;
            }

            world.Grid += grid;
        }

        public TileJoinType localJoinType = 0;
        public TileJoinType worldJoinType = 0;
        
        public List<GameObject> tiles;
        public enum TileJoinType
        {
            none = 0,
            walls = 1,
        }

        public int Layer { get { return layer; } }

        public Location(SeriazableLocation seriazableLocation, World world = null, Grid2Int grid = null) : base(seriazableLocation.name, seriazableLocation.type, world, (x) =>
        {
            Location location = x as Location;
            location.UpdateWorldGrid((y) => y.Layer > location.layer, world);

            return location.grid.Count > 0;

        }, grid, true)
        {
            layer = seriazableLocation.layer;
            tiles = seriazableLocation.tiles;
            worldJoinType = seriazableLocation.worldJoinType;
            localJoinType = seriazableLocation.localJoinType;

            this.grid.changed.AddListener(() =>
            {
                if (this.grid.Count <= 0) Destroy();
            });
        }
    }
}

