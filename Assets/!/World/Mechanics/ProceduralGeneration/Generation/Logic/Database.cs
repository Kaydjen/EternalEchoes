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

        static public SeriazableLocation GetLocation(SeriazableWorld world, Predicate<SeriazableLocation> predicate) => world.locations.Find(predicate);
        static public List<SeriazableLocation> GetLocations(SeriazableWorld world, Predicate<SeriazableLocation> predicate) => world.locations.FindAll(predicate);

        static public void InitSerialzableWorlds(string path)
        {
            foreach (SeriazableWorld world in Resources.LoadAll<SeriazableWorld>(path))
            {
                if (world == null) continue;

                seriazableWorlds.Add(world);
            }

            seriazableWorlds.Sort((SeriazableWorld worldA, SeriazableWorld worldB) => worldA.layer.CompareTo(worldB.layer));
        }

        static public void CreateWorld(int index = 0)
        {

            worlds.Add(new World(seriazableWorlds[index]));
        }
    }
    
}

