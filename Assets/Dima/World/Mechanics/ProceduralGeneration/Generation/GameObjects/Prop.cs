using ProceduralGeneration.SeriazableObjects;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ProceduralGeneration.GameObjects
{
    public class Prop : GenerationObject
    {
        protected GameObject gameObejct;
        public GameObject GameObject
        {
            get => gameObejct;
            set
            {
                gameObejct = value;
                OnDestroyHandler onDestroyHandler = gameObejct.GetComponent<OnDestroyHandler>();

                if (onDestroyHandler == null) onDestroyHandler = gameObejct.AddComponent<OnDestroyHandler>();

                //onDestroyHandler.OnDestroyEvent.AddListener(() => );
            }
        }

        public Vector2 pivot;
        public Vector3 rotation;
        public Prop(SeriazableProp prop, Location location, in Vector2 pivot = default, in Grid2Int grid = null) : base(prop.name, prop.type, location, (generationObject) =>
        {
            Prop prop = generationObject as Prop;

            List<Prop> props = (location.GetParent() == null) ? location.GetDescedants<Prop>() : location.GetParent().GetDescedants<Prop>();

            int count = prop.Grid.Count;

            foreach (Prop worldProp in props) prop.grid -= worldProp.Grid;
            return count == prop.Grid.Count;
        }, grid, true)
        {
            gameObejct = prop.gameObject;

            this.pivot = pivot;
        }
    }
}
