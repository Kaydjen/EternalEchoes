using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UIElements;
//// IDEA: можно сделать так, что бы если через допустим 30 чекунд главарь группы все еще был жив, 
///то он спавнил всех своих воинов наново.
///


public class AIManagerOfGroups : MonoBehaviour
{
    public static AIManagerOfGroups Instance;
    [SerializeField] private float _delayBtwInstantiateGroups = 1f;
    private Dictionary<EEnemyRank, Action<Tuple<EnemyPool, float, Vector3, byte, float>>> _DEnemyRank;
    private readonly Queue<Tuple<EnemyPool, float, Vector3, byte, float>> _subordinatesQueue = new();
    private readonly Queue<Tuple<EnemyPool, float, Vector3, byte, float>> _leadersQueue = new();
    private bool _isProcessing;
    private Coroutine _spawningCoroutine;
    private const EEnemyRank RANK = EEnemyRank.Subordinate;
    private const float RADIUS = 5f;
    private const float DELAY = 2f;
    private void Awake()
    {
        Instance = this;
        InitEvents.OnAIManagerOfGroupsReady?.Invoke();
        InitializeDictionary();
    }
    private void InitializeDictionary()
    {
        _DEnemyRank = new()
        {
            { EEnemyRank.Subordinate, _subordinatesQueue.Enqueue },
            { EEnemyRank.Leader, _leadersQueue.Enqueue },
        };
    }
    /// <summary>
    /// Use it to add needed group of enemies to spawn queue 
    /// </summary>
    /// <param name="pool">The pool of needed enemies</param>
    /// <param name="countToSpawn">Number of enemies needed to instantiate</param>
    /// <param name="pivotPosition">Coordinates of point where will spawn group</param>
    /// <param name="radius">The radius of enemies instantiation (default: 5f)</param>
    /// <param name="delay">The delay between instantiation of each enemy (default: 2f)</param>
    /// <param name="rank">The rank of enemies (default: Subordinate)</param>
    public void AddToQueue(EnemyPool pool, byte countToSpawn, Vector3 pivotPosition, float radius = RADIUS,
        float delay = DELAY, EEnemyRank rank = RANK)
    {
        if (_DEnemyRank == null)
        {
            Debug.Log($"{nameof(_DEnemyRank)} was null, initializing...");
            InitializeDictionary();
        }
        if (pool == null)
        {
            Debug.Log($"{nameof(pool)} in {nameof(AddToQueue)} in {nameof(AIManagerOfGroups)} is null");
            return;
        }
        if (countToSpawn == 0)
        {
            Debug.Log($"{nameof(countToSpawn)} in {nameof(AddToQueue)} in {nameof(AIManagerOfGroups)} is 0");
            return;
        }

        radius = Mathf.Max(0, radius);
        delay = Mathf.Max(0, delay);

        var parameters = Tuple.Create(pool, delay, pivotPosition, countToSpawn, radius);

        if (_DEnemyRank.TryGetValue(rank, out var addToConcreteQueue)) 
            addToConcreteQueue?.Invoke(parameters); 
        else 
            Debug.Log($"There is no rank like {nameof(rank)} in {nameof(AddToQueue)}");
        if (!_isProcessing && this != null && gameObject.activeInHierarchy)
            _spawningCoroutine = StartCoroutine(InstantiateQueues());        
    }
    private IEnumerator InstantiateQueues()
    {
        _isProcessing = true;
        try
        {
            while (_subordinatesQueue.Count > 0 || _leadersQueue.Count > 0)
            {
                if (_subordinatesQueue.Count > 0)
                    yield return ProcessOneGroup(_subordinatesQueue);

                if (_leadersQueue.Count > 0)
                    yield return ProcessOneGroup(_leadersQueue);

                yield return new WaitForSeconds(_delayBtwInstantiateGroups);
            }
        }
        finally
        {
            _isProcessing = false;
        }
    }

    private IEnumerator ProcessOneGroup(Queue<Tuple<EnemyPool, float, Vector3, byte, float>> queue)
    {
        var (pool, delay, pivotPosition, countToSpawn, radius) = queue.Dequeue();

        float angleStep = 360f / countToSpawn;

        float angle, x, z;
        Vector3 position;
        Transform enemy;
        for (int i = 0; i < countToSpawn; i++)
        {
            if (pool == null) Debug.LogWarning("POOL IS NULL");
            angle = i * angleStep * Mathf.Deg2Rad;
            x = pivotPosition.x + radius * Mathf.Cos(angle);
            z = pivotPosition.z + radius * Mathf.Sin(angle);
            position = new(x, pivotPosition.y, z);

            enemy = pool.GetEnemy();
            if (enemy.TryGetComponent(out AI ai))
            {
                ai.Register();
            }
            else
            {
                Debug.Log($"{nameof(ai)} can't be getted from {nameof(enemy)} in {nameof(AIManagerOfGroups)}");
                continue;
            }
            if (enemy != null) enemy.position = position;
            else Debug.LogWarning("ENEMY IS NULL");
            yield return new WaitForSeconds(delay);
        }

        yield return new WaitForSeconds(_delayBtwInstantiateGroups);        
    }

    private void OnDisable()
    {
        if (_spawningCoroutine != null)
        {
            StopCoroutine(_spawningCoroutine);
            _spawningCoroutine = null;
        }
        _isProcessing = false; // ну так, на всякий пожарный
    }
}



/*
 
 public class AIManagerOfGroups : MonoBehaviour
{
    [SerializeField] private float _delayBtwInstantiateEachEnemy = 1f;
    [SerializeField] private float _delayBtwInstantiateGroups = 1f;
    private Dictionary<EEnemyRank, Action<Tuple<Pool<Transform>, float, Vector3, byte, float>>> _DEnemyRank;
    private Queue<Tuple<Pool<Transform>, float, Vector3, byte, float>> _subordinatesQueue = new(); 
    private Queue<Tuple<Pool<Transform>, float, Vector3, byte, float>> _leadersQueue = new();// list of enemies, delay between spawn, coordinates of point
    private bool _isProcessing;

    private void Awake()
    {
        _DEnemyRank = new()
        {
            { EEnemyRank.Subordinate, _subordinatesQueue.Enqueue},
            { EEnemyRank.Leader, _leadersQueue.Enqueue},
        };
    }
    /// <summary>
    ///  Use it to add needed group of enemies to spawn queue 
    /// </summary>
    /// <param name="pool">The pool of needed enemies</param>
    /// <param name="countToSpawn">Number of enemies needed to instantiate</param>
    /// <param name="rank">The rank of enemies</param>
    /// <param name="delay">The delay between instantiation of each enemy</param>
    /// <param name="pivotPosition">Coordinates of point where will spawn group</param>
    /// <param name="radius">The radius of enemies instantiation</param>
    /// <remarks>The defaul rank of enemy is Subordinate(the smallest one)
    /// The default delay is 2f
    /// The default radius is 5f</remarks>
    public void AddToQueue(Pool<Transform> pool, byte countToSpawn, Vector3 pivotPosition, float radius = 5f, float delay = 2f, EEnemyRank rank = EEnemyRank.Subordinate)
    {
        if (_DEnemyRank == null) 
        {
            Debug.Log($"{nameof(_DEnemyRank)} in {nameof(AddToQueue)} in {nameof(AIManagerOfGroups)} was null, " +
                $"you forgot to create an instance of addToConcreteQueue dictionary {nameof(_DEnemyRank)}");
            Awake();
            return;
        }
        if (pool == null)
        {
            Debug.Log($"{nameof(pool)} in {nameof(AddToQueue)} in {nameof(AIManagerOfGroups)} null");
            return;
        }
        if (countToSpawn == 0)
        {
            Debug.Log($"{nameof(countToSpawn)} in {nameof(AddToQueue)} in {nameof(AIManagerOfGroups)} is 0");
            return;
        }

        Action<Tuple<Pool<Transform>, float, Vector3, byte, float>> addToConcreteQueue;
        var parameters = Tuple.Create(pool, delay, pivotPosition, countToSpawn, radius);

        if (_DEnemyRank.TryGetValue(rank, out addToConcreteQueue)) 
            addToConcreteQueue.Invoke(parameters); // should work
        else 
            Debug.Log($"There is no rank like {nameof(rank)} in {nameof(AddToQueue)}");
        if (!_isProcessing) StartCoroutine(InstantiateQueue());
    }
    private IEnumerator InstantiateQueue()
    {
        _isProcessing = true;
        while (_subordinatesQueue.Count > 0)
        {
            var list = _subordinatesQueue.Dequeue();
            byte countToSpawn = list.Item4;
            Pool<Transform> pool = list.Item1;

            float delay = list.Item2;
            float radius = list.Item5;
            Vector3 pivot = list.Item3;
            float angleStep = 360f / countToSpawn;
            float angle, x, z;
            Vector3 position;
            Transform enemy;

            if (radius < 0)
            {
                Debug.LogError($"{nameof(radius)} in {nameof(AddToQueue)} cannot be negative");
                radius = Mathf.Abs(radius);
            }

            for (int i = 0; i < countToSpawn; i++)
            {
                angle = i * angleStep * Mathf.Deg2Rad;
                x = pivot.x + radius * Mathf.Cos(angle);
                z = pivot.z + radius * Mathf.Sin(angle);
                position = new Vector3(x, pivot.y, z);
                enemy = pool.Get();
                enemy.position = position;
                yield return new WaitForSeconds(_delayBtwInstantiateEachEnemy);
            }
            yield return new WaitForSeconds(_delayBtwInstantiateGroups);
        }
        _isProcessing = false;
    }
}

 
 
 */

/*
 
 
 
 
 
 public class AIManagerOfGroups : MonoBehaviour
{
    [SerializeField] private float _delayBtwInstantiateEachEnemy = 1f;
    [SerializeField] private float _delayBtwInstantiateGroups = 1f;
    private Queue<Transform[]> _subordinatesQueue = new();
    private Queue<Transform[]> _leadersQueue = new();
    private bool _isProcessing;
    public void AddToSubordinatesQueue(params Transform[] list)
    {
        if (list.Length == 0)
        {
            Debug.Log($"{nameof(list)} in {nameof(AddToSubordinatesQueue)} in {nameof(AIGroupManager)}");
            return;
        }
        _subordinatesQueue.Enqueue(list);
        if(!_isProcessing) StartCoroutine(InstantiateQueue());
    }
    public void AddToLeadersQueue(params Transform[] list)
    {
        if (list.Length == 0)
        {
            Debug.Log($"{nameof(list)} in {nameof(AddToLeadersQueue)} in {nameof(AIGroupManager)}");
            return;
        }
        _leadersQueue.Enqueue(list);
        if (!_isProcessing) StartCoroutine(InstantiateQueue());
    }
    private IEnumerator InstantiateQueue()
    {
        _isProcessing = true;
        while (_subordinatesQueue.Count > 0)
        {
            Transform[] subordinatesList = _subordinatesQueue.Dequeue();
            Transform[] leadersList = _leadersQueue.Dequeue();
            for (int i = 0; i < subordinatesList.Length; i++)
            {

                // через гет пулла получать врагов нужного типа...
                yield return new WaitForSeconds(_delayBtwInstantiateEachEnemy);
            }
            for (int i = 0; i < leadersList.Length; i++)
            {
                yield return new WaitForSeconds(_delayBtwInstantiateEachEnemy);
            }
            yield return new WaitForSeconds(_delayBtwInstantiateGroups);
        }
        _isProcessing = false;
    }
}
 
 
 
 
 
 */