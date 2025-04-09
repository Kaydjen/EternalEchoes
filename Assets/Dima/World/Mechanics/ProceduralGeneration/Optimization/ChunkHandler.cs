using ProceduralGeneration.GameObjects;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Optimization {
    public class ChunkHandler : MonoBehaviour
    {
        public delegate void ChunkDelegate(Location location, Collider collider);
        static public event ChunkDelegate onChunkEnter, onChunkExit, onChunkStay; 

        [NonSerialized] public List<GameObject> colliders;
        private GameObject tiles;

        [NonSerialized] private Location location;

        public Location Location
        {
            set {  location = value; }
            get => location;
        }

        public void SetLocation(Location location)
        {
            this.location = location;
        }

        //public string visibleRadiusTag;

        private void Start()
        {
            colliders = new List<GameObject>();
            tiles = transform.Find("Tiles").gameObject;

        }

        private void FixedUpdate()
        {
            if (colliders.Count > 0) tiles.SetActive(true);
            else tiles.SetActive(false);
        }

        private void OnTriggerStay(Collider other) => onChunkStay.Invoke(location, other);

        private void OnTriggerEnter(Collider other)
        {
            if (colliders.Contains(other.gameObject)) return;
            onChunkEnter.Invoke(location, other);
            colliders.Add(other.gameObject);
        }

        private void OnTriggerExit(Collider other)
        {
            if (!colliders.Contains(other.gameObject)) return;
            onChunkExit.Invoke(location, other);
            colliders.Remove(other.gameObject);
        }
    }
}