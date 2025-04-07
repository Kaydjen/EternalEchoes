using System.Collections.Generic;
using UnityEngine;

namespace ProceduralGeneration.SeriazableObjects
{
    [CreateAssetMenu(fileName = "NewWorld", menuName = "ProceduralGeneration/World")]
    public class SeriazableWorld: ScriptableObject
    {
        public string type = "Unknown";
        public int layer = 1;

        public Vector2Int size = new Vector2Int(50, 50);
        public Vector2Int startingPoint = new Vector2Int(50, 50);

        [Space(10)]
        [Header("Visualization")]
        public float scale = 1;

        [Space(10)]
        [Header("Locations")]
        public SeriazableLocation spawn;
        public List<SeriazableLocation> possibleLocations = new List<SeriazableLocation>();
    }
}
