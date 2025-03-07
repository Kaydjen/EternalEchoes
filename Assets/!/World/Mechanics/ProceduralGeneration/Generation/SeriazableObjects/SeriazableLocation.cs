using ProceduralGeneration.GameObjects;
using System.Collections.Generic;
using UnityEngine;

namespace ProceduralGeneration.SeriazableObjects
{
    [CreateAssetMenu(fileName = "NewLocation", menuName = "ProcedualGeneration/Location")]
    public class SeriazableLocation : ScriptableObject
    {
        public string type = "Unknown";
        public int layer = 1;

        [Header("Vizualization")]
        public List<GameObject> tiles;
        public Location.TileJoinType joinType;
    }
}
