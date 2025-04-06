using ProceduralGeneration.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace ProceduralGeneration.GameObjects
{
    [Serializable] public class GenerationObject
    {
        [SerializeField] protected string name, type;
        protected Grid2Int grid = new Grid2Int();

        public virtual Grid2Int Grid { get { return grid; }
            set { grid = value; }
        }
        
        public string Name { get => name; }
        public string Type { get => type; }

        public UnityEvent onDestroy = new UnityEvent();
        public UnityEvent<GenerationObject> childAdded = new UnityEvent<GenerationObject>(),
            childRemoved = new UnityEvent<GenerationObject>();

        private GenerationObject parent;
        private List<GenerationObject> children = new List<GenerationObject>();

        public GenerationObject GetParent() => parent;
        public T GetParent<T>() where T : class => parent as T;

        public virtual Vector2Int GetRandomPosition() => GetRandomPosition(grid.ToList());
        public virtual Vector2Int GetRandomPosition(List<Vector2Int> grid) => grid[Generator.RandomNext(grid.Count)];
        public virtual Vector2Int GetRandomPosition(Predicate<Vector2Int> match) => GetRandomPosition(grid.FindAll(match));
        private List<GenerationObject> GetDescedants()
        {
            List<GenerationObject> descedants = new List<GenerationObject>(children);

            foreach (GenerationObject child in children)
                if (child.children.Count != 0)
                    descedants = descedants.Union(child.GetDescedants()).ToList();

            return descedants;
        }
        public List<T> GetDescedants<T>() where T : class => GetDescedants().OfType<T>().ToList();

        public List<T> GetDescedants<T>(Predicate<T> condition) where T : class => GetDescedants().OfType<T>().ToList().FindAll(condition);

        public List<GenerationObject> GetChidren(Predicate<GenerationObject> predicate) => children.FindAll(predicate);

        public List<T> GetChildren<T>(Predicate<T> condition) where T : class => children.OfType<T>().ToList().FindAll(condition);
        public List<T> GetChildren<T>() where T : class => children.OfType<T>().ToList();
        public GenerationObject FindFirstChild(Predicate<GenerationObject> predicate) => children.Find(predicate);
        public virtual bool AddChild(GenerationObject child, Predicate<GenerationObject> predicate = null)
        {
            bool condition = predicate == null;

            if (!condition) condition = predicate.Invoke(child);

            if (condition && !children.Contains(child)) {
                child.parent = this;
                children.Add(child);
                childAdded.Invoke(child);
            }

            return condition;
        }
        public virtual void RemoveChild(GenerationObject child) {

            if (!children.Contains(child)) return;

            childAdded.Invoke(child);
            child.parent = null;
            children.Remove(child);
        }
        public virtual void Destroy()
        {
            onDestroy.Invoke();
            children.Clear();

            if (parent == null) return; 
            parent.RemoveChild(this);
        }
        public GenerationObject(in string name, in string type, bool syncChild = false)
        {
            this.name = name;
            this.type = type;

            if (syncChild) childAdded.AddListener((x) => grid += x.grid);
        }

        public GenerationObject(in string name, in string type, in GenerationObject parent, bool syncChild = false)
        {
            this.name = name;
            this.type = type;

            if (syncChild) childAdded.AddListener((x) => grid += x.grid);

            parent?.AddChild(this);
        }

        public GenerationObject(in string name, in string type, in GenerationObject parent, Predicate<GenerationObject> condition, bool syncChild = false)
        {
            this.name = name;
            this.type = type;

            if (syncChild) childAdded.AddListener((x) => grid += x.grid);

            parent?.AddChild(this, condition);
        }
        public GenerationObject(in string name, in string type, in GenerationObject parent, Predicate<GenerationObject> condition, Grid2Int grid = null,  bool syncChild = false)
        {
            this.name = name;
            this.type = type;
            if (grid != null) this.grid = grid;

            if (syncChild) childAdded.AddListener((x) => this.grid += x.grid);

            parent?.AddChild(this, condition);
        }
    }
}
