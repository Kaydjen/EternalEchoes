using ProceduralGeneration.GameObjects;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ProceduralGeneration.SeriazableObjects
{
    [Serializable]
    public class PropConfiguration
    {
        public SeriazableProp seriazableProp;
    }

    [Serializable]
    public class PropLocationConfiguration : PropConfiguration
    {
        [Space(10), Header("Spawn configuration"), Min(1)]
        public uint maxAmount = 1;
        [Range(0f, 100f)]
        public float chance;
    }

    [CreateAssetMenu(fileName = "NewLocation", menuName = "ProceduralGeneration/Location")]
    public class SeriazableLocation : ScriptableObject
    {
        public string type = "Unknown";
        public int layer = 1;

        [Header("Vizualization")]
        public List<GameObject> tiles;
        public Location.TileJoinType worldJoinType;
        public Location.TileJoinType localJoinType;

        [Header("Props")]
        public PropLocationConfiguration[] possibleProps;
    }
}
