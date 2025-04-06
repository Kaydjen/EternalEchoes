using ProceduralGeneration.GameObjects;
using ProceduralGeneration.SeriazableObjects;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace ProceduralGeneration.Logic
{
    public class Database
    {
        static public List<World> worlds = new List<World>();
        static public List<SeriazableWorld> seriazableWorlds = new List<SeriazableWorld>();

        static public SeriazableLocation GetLocation(SeriazableWorld world, Predicate<SeriazableLocation> predicate) => world.possibleLocations.Find(predicate);
        static public List<SeriazableLocation> GetLocations(SeriazableWorld world, Predicate<SeriazableLocation> predicate) => world.possibleLocations.FindAll(predicate);

        static public void InitSerialzableWorlds(string path)
        {
            SeriazableWorld[] worlds = Resources.LoadAll<SeriazableWorld>(path);

            for (int iWorld = 0; iWorld < worlds.Length; iWorld++)
                if (worlds[iWorld] != null) seriazableWorlds.Add(worlds[iWorld]);

            seriazableWorlds.Sort((SeriazableWorld worldA, SeriazableWorld worldB) => worldA.layer.CompareTo(worldB.layer));

            for (int i = 0; i < seriazableWorlds.Count; i++)
                seriazableWorlds[i].layer = i;
        }

        static public World CreateWorld(int index = 0)
        {
            World world = new World(seriazableWorlds[index]);
            worlds.Add(world);
            return world;
        }
    }
    
}

