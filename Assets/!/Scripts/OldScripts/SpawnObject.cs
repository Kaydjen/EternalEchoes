using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    [SerializeField] private List<GameObject> _objects = new();
    [SerializeField] private float _time = 1f;
    private void Awake()
    {
        Invoke(nameof(Spawn), _time);
    }
    private void Spawn()
    {
        foreach (var e in _objects)
        {
            Instantiate(e, Vector3.zero, Quaternion.identity);
            StartCoroutine(Enable(e));
        }
    }
    private IEnumerator Enable(GameObject obj)
    {
        yield return new WaitForSeconds(_time);
        obj.SetActive(true);
    }
}