using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Optimization {
    public class AreaHandler : MonoBehaviour
    {
        [NonSerialized] public List<GameObject> colliders;
        private GameObject _tiles;
        private Transform[] tiles;

        public Transform[] Tiles
        {
            get { return tiles; }
        }
        private void Start()
        {
            colliders = new List<GameObject>();
            _tiles = transform.Find("Tiles").gameObject;

            tiles = _tiles.transform.GetComponentsInChildren<Transform>(true);

        }

        private void FixedUpdate()
        {
            if (colliders.Count > 0) { 
                _tiles.SetActive(true);

                /*foreach (Transform tile in tiles)
                {
                    if (tile == null) continue;
                    bool hasTrigger = false;

                    for (int i = 0; i < colliders.Count; i++)
                    {
                        if ((colliders[i].transform.position - tile.position).magnitude > 100) continue;

                        hasTrigger = true;
                        break;
                    }

                    tile.gameObject.SetActive(hasTrigger);
                }*/
            }
            else _tiles.SetActive(false);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (colliders.Contains(other.gameObject)) return;
            colliders.Add(other.gameObject);
        }

        private void OnTriggerExit(Collider other)
        {
            if (!colliders.Contains(other.gameObject)) return;
            colliders.Remove(other.gameObject);
        }
    }
}