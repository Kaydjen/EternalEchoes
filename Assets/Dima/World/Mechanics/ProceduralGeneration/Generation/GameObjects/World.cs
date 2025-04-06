using ProceduralGeneration.Logic;
using ProceduralGeneration.SeriazableObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ProceduralGeneration.GameObjects
{
    [Serializable]
    public class World : GenerationObject
    {
        #region private
        #region private variables
        
        [SerializeField] private Vector2Int size;
        [SerializeField] private float scale;
        [SerializeField] private int layer;

        internal int Layer { get { return layer; } }
        internal Vector2Int Size { get { return size; } }
        internal float Scale { get { return scale; } }

        public override Grid2Int Grid { 
            get => grid;
            set { grid = value; }
        }

        #endregion private variables
        #endregion private

        #region public
        #region public methods

        public override Vector2Int GetRandomPosition() =>
            (grid.Count > 0) ? grid[Generator.RandomNext(grid.Count)] : new Vector2Int(Generator.RandomNext(size.x), Generator.RandomNext(size.y));


        public World(in SeriazableWorld world) : base(world.name, world.type, true)
        {
            size = world.size;
            scale = world.scale;
            layer = world.layer;
        }


        #endregion public methods
        #endregion public
    }
}
