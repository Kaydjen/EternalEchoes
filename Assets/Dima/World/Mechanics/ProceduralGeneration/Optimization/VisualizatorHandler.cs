using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Optimization
{
    [RequireComponent(typeof(Collider))]
    public class VisualizatorHandler : MonoBehaviour
    {
        [NonSerialized] public HashSet<Collider> colliders = new HashSet<Collider>();
        private void Start()
        {
            StartCoroutine(Starting());
        }

        private IEnumerator Starting()
        {
            yield return new WaitWhile(() => GameObject.Find("World") == null);

            colliders = FindObjectsByType<Collider>(FindObjectsSortMode.None).ToHashSet();
            colliders.ExceptWith(Physics.OverlapSphere(transform.position, GetComponent<SphereCollider>().radius));

            foreach (Collider collider in colliders)
            {
                MeshRenderer meshRenderer = collider.GetComponent<MeshRenderer>();

                if (meshRenderer != null) meshRenderer.enabled = false;
            }

            colliders.Clear();
        }

        private void OnTriggerEnter(Collider other)
        {
            MeshRenderer meshRenderer = other.GetComponent<MeshRenderer>();

            

            if (colliders.Contains(other) || meshRenderer == null) return;
            colliders.Add(other);

            meshRenderer.enabled = true;
        }

        private void OnTriggerExit(Collider other)
        {
            if (!colliders.Contains(other)) return;

            colliders.Remove(other);

            MeshRenderer meshRenderer = other.GetComponent<MeshRenderer>();
            meshRenderer.enabled = false;
        }
    }
}
