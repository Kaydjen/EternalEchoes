using System;
using UnityEngine;

namespace ProceduralGeneration.GameObjects
{
    [Serializable] public class Object
    {
        [SerializeField] protected string name, type;
        public string Name { get { return name; } }

        public string Type { get { return type; } }

        public Object(in string name, in string type)
        {
            this.name = name;
            this.type = type;
        }
    }
}
