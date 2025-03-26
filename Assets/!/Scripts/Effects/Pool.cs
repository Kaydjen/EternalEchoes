using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool<T> : MonoBehaviour where T : Component
{
    [SerializeField] protected T _prefab;
    [SerializeField] protected int _initialPoolSize = 10;

    protected Queue<T> _pool = new Queue<T>();

    protected virtual void Start()
    {
        for (int i = 0; i < _initialPoolSize; i++)
        {
            CreateNew();
        }
    }
    public virtual T Get()
    {
        if (_pool.Count > 0)
        {
            T obj = _pool.Dequeue();
            obj.gameObject.SetActive(true);
            return obj;
        }
        return CreateNew();
    }
    public virtual void Return(T obj)
    {
        obj.gameObject.SetActive(false);
        _pool.Enqueue(obj);
    }
    protected virtual T CreateNew()
    {
        T obj = Instantiate(_prefab);
        obj.gameObject.SetActive(false);
        obj.transform.SetParent(transform);
        _pool.Enqueue(obj);
        return obj;
    }
    protected virtual IEnumerator DelayedReturn(T obj, float delay)
    {
        yield return new WaitForSeconds(delay);
        Return(obj);
    }
}


/*
 
using System.Collections.Generic;
using UnityEngine;

public class Pool<T> : MonoBehaviour where T : new()
{
    protected T _elementInstance;
    protected int _initialPoolSize = 10;

    protected Queue<T> _pool = new Queue<T>();

    public Pool()
    {
        for (int i = 0; i < _initialPoolSize; i++)
        {
            CreateNew();
        }
    }
    public virtual T Get()
    {
        if (_pool.Count > 0)
        {
            T element = _pool.Dequeue();
            return element;
        }
        return CreateNew();
    }
    public virtual void Return(T element) => _pool.Enqueue(element);
    protected virtual T CreateNew()
    {
        T element = new T();
        _pool.Enqueue(element);
        return element;
    }
}






*/
/*
 
 
 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    [SerializeField] protected GameObject _prefab;
    [SerializeField] protected int _initialPoolSize = 10;

    protected Queue<GameObject> _pool = new Queue<GameObject>();

    protected virtual void Start()
    {
        for (int i = 0; i < _initialPoolSize; i++)
        {
            CreateNew();
        }
    }
    public virtual GameObject Get()
    {
        if (_pool.Count > 0)
        {
            GameObject prefab = _pool.Dequeue();
            prefab.SetActive(true);
            return prefab;
        }
        return CreateNew();
    }
    public virtual void Return(GameObject prefab)
    {
        prefab.SetActive(false);
        _pool.Enqueue(prefab);
    }
    protected virtual GameObject CreateNew()
    {
        GameObject prefab = Instantiate(_prefab);
        prefab.SetActive(false);
        prefab.transform.SetParent(transform);
        _pool.Enqueue(prefab);
        return prefab;
    }
    protected virtual IEnumerator DelayedReturn(GameObject prefab, float delay)
    {
        yield return new WaitForSeconds(delay);
        Return(prefab);
    }
}
 
 
 */