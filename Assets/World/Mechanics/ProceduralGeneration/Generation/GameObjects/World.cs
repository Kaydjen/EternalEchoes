using ProceduralGeneration.Logic;
using ProceduralGeneration.SeriazableObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ProceduralGeneration.GameObjects
{
    [Serializable]
    public class World : Object
    {
        #region private
        #region private variables

        internal List<Location> locations = new List<Location>();
        
        [SerializeField] private Vector2Int size;
        [SerializeField] private float scale;

        internal Vector2Int Size { get { return size; } }
        internal float Scale { get { return scale; } }

        #endregion private variables
        #endregion private

        #region public
        #region public variables


        #endregion public variables
        #region public methods
        public void AddLocation(Location location)
        {
            locations.Add(location);
        }
        public Location FindLocation(Predicate<Location> predicate)
        {
            Location locationToReturn = default(Location);

            foreach (Location location  in locations)
            {
                if (predicate(location)) locationToReturn = location;  
            }

            return locationToReturn;
        }

        public Vector2Int GetRandomPosition()
        {
            List<Vector2Int> positions = new List<Vector2Int>();
            foreach (Location location in locations)
            {
                positions.AddRange(location.Grid);
            }

            return (locations.Count > 0) ? positions[Generator.randomizer.Next(positions.Count)] : new Vector2Int(Generator.randomizer.Next(size.x), Generator.randomizer.Next(size.y));
        }

        public void Destroy(Location location)
        {
            locations.Remove(location);
        }

        public void CollectGarbage()
        {
            locations.RemoveAll(location => location.Grid.Count == 0);
        }

        public World(in SeriazableWorld world) : base(world.name, world.type)
        {
            size = world.size;
            scale = world.scale;
        }


        #endregion public methods
        #endregion public
    }
}

