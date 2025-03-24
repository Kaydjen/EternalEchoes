using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private int _initialPoolSize = 10;

    private Queue<GameObject> _pool = new Queue<GameObject>();

    protected virtual void Start()
    {
        for (int i = 0; i < _initialPoolSize; i++)
        {
            CreateNewObject();
        }
    }
    public virtual GameObject GetObject()
    {
        if (_pool.Count > 0)
        {
            GameObject obj = _pool.Dequeue();
            obj.SetActive(true);
            return obj;
        }
        return CreateNewObject();
    }
    public virtual void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);
        _pool.Enqueue(obj);
    }
    protected virtual GameObject CreateNewObject()
    {
        GameObject obj = Instantiate(_prefab);
        obj.SetActive(false);
        obj.transform.SetParent(transform);
        _pool.Enqueue(obj);
        return obj;
    }
    protected virtual IEnumerator DelayedReturnObject(GameObject effect, float delay)
    {
        yield return new WaitForSeconds(delay);
        ReturnObject(effect);
    }
}