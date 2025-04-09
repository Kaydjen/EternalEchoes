using UnityEngine;

public class EnemyPool : Pool<Transform>
{
    public virtual Transform GetEnemy(bool state = false)
    {
        if (_pool.Count > 0)
        {
            Transform obj = _pool.Dequeue();
            obj.gameObject.SetActive(state);
            return obj;
        }
        else
        {
            Transform obj = Instantiate(_prefab, PlayerCore.Instance.transform.position, Quaternion.identity);
            obj.gameObject.SetActive(state);
            //obj.transform.SetParent(transform);
            return obj;
        }
    }
}