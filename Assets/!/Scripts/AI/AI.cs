using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

[RequireComponent(typeof(NavMeshAgent))]
public class AI : MonoBehaviour
{
    [SerializeField] protected NavMeshAgent _agent;
    [NonSerialized] public UnityEvent<GameObject> OnFirstHit; // gameobject - attacker
    [NonSerialized] public UnityEvent OnDeah;
    public HashSet<IRule> Rules = new();
    public HashSet<Transform> Targets = new();
    public EAIType Type;

    #region PUBLIC
    public virtual void Register()
    {
        AIRepository.Register(this);
    }
    public virtual void Unregister()
    {
        AIRepository.Unregister(this);
    }
    public virtual void CheckRules()
    {
        if(Rules.Count == 0)
        {
            Debug.Log($"{nameof(Rules.Count)} count = 0");
            return;
        }
        foreach (var rule in Rules)
        {
            if(rule.CanExecute())
            {
                rule.Execute();
            }
        }
    }
    public virtual void SetNewTargetList(HashSet<Transform> newTargets)
    {
        Targets.Clear();
        foreach (var target in newTargets) Targets.Add(target);
    }
    public virtual void SetDestination(Transform transform) => _agent.destination = transform.position;
    #endregion
    #region MONOBEHAIVOUR
    private void Awake()
    {
        Rules = GetComponents<IRule>().ToHashSet();
        if(Rules.Count == 0) Debug.Log("There are no rules on AI");
    }
    protected void OnDestroy()
    {
        Unregister();
    }
    protected virtual void OnEnable()
    {
        if (_agent != null) _agent.enabled = true;
        else Debug.Log($"{nameof(_agent)} is null");
    }
    protected virtual void OnDisable()
    {
        ClearAllSubcribers();
        if(_agent != null) _agent.enabled = false;
        else Debug.Log($"{nameof(_agent)} is null");
    }
    protected virtual void ClearAllSubcribers()
    {
        OnFirstHit?.RemoveAllListeners();
        OnDeah?.RemoveAllListeners();
    }
    #endregion
}







/*
 
 
[RequireComponent(typeof(NavMeshAgent))]
public class AI : MonoBehaviour
{
    [SerializeField] protected NavMeshAgent _agent;
    [NonSerialized] public UnityEvent<GameObject> OnFirstHit; // gameobject - attacker
    [NonSerialized] public UnityEvent OnDeah;
    public HashSet<IRule> Rules = new();
    public EAIType Type;

    #region PUBLIC
    public virtual void Register()
    {
        AIRepository.Register(this);
    }
    public virtual void Unregister()
    {
        AIRepository.Unregister(this);
    }
    public virtual void CheckRules()
    {
        if(Rules.Count == 0) return;
        foreach (var rule in Rules)
        {
            if(rule.CanExecute())
            {
                rule.Execute();
            }
        }
    }
    public virtual void SetDestination(Vector3 coordinates)
    {
        _agent.destination = coordinates;
    }
    #endregion
    #region MONOBEHAIVOUR
    private void Awake()
    {
        Rules = GetComponents<IRule>().ToHashSet();
        if(Rules.Count == 0) Debug.Log("There are no rules on AI");
    }
    protected void OnDestroy()
    {
        Unregister();
    }
    protected virtual void OnEnable()
    {
        if (_agent != null) _agent.enabled = true;
        else Debug.Log($"{nameof(_agent)} is null");
    }
    protected virtual void OnDisable()
    {
        ClearAllSubcribers();
        if(_agent != null) _agent.enabled = false;
        else Debug.Log($"{nameof(_agent)} is null");
    }
    protected virtual void ClearAllSubcribers()
    {
        OnFirstHit?.RemoveAllListeners();
        OnDeah?.RemoveAllListeners();
    }
    #endregion
}

 
 
 */
/*
 

public class AISpawnCoordinator : MonoBehaviour
{
    [Space(5)]
    [Header("Subordinate Enemy")]
    [SerializeField] private bool _doSpawnSubordinate = true;
    [SerializeField] private EEnemys _subordinateEnemyType;
    [SerializeField] private EEnemyRank _subordinateEnemyRank;
    [SerializeField] private float _subordinateSpawnDelay = 1f;
    [SerializeField] private byte _subordinatesCount = 8;
    [Space(5)]
    [Header("Leader Enemy")]
    [SerializeField] private bool _doSpawnLeader = true;
    [SerializeField] private EEnemys _leaderEnemyType;
    [SerializeField] private EEnemyRank _leaderEnemyRank;
    [SerializeField] private float _leaderSpawnDelay = 1f;
    [SerializeField] private byte _leadersCount = 1;
    [Space(10)]
    [SerializeField] private float _radiusOfSpawn = 5f;

    // ивент, на который подпишуться все члены группы, и если кто-то из членов группы был ранен -
    // то все члены группы атакуют нападавшего (тобиж ивент должен передавать параметр Transform attacker
    // ----
    // список юнитов для спавна
    // список главарей для спавна
    // ----
    // булевое поле, для того, что бы обозначать, будет главарь или нет,  и нужно ли спавнить его сейчас 

    private void OnEnable()
    {
        RequestEnemySpawn(); // запрос в менеджер групп для спавна 
    }
    public void RequestEnemySpawn()
    {
        if (DEnemys.List == null)
        {
            Debug.Log($"{nameof(DEnemys.List)} is null");
            return;
        }
        if (_doSpawnSubordinate && DEnemys.List.TryGetValue(_subordinateEnemyType, out Pool<Transform> subordinatesPool) && subordinatesPool != null)
        {
            if (AIManagerOfGroups.Instance == null)
            {
                if (InitEvents.OnAIManagerOfGroupsReady == null)
                {
                    Debug.Log($"{nameof(InitEvents.OnAIManagerOfGroupsReady)} is null");
                    return;
                }
                InitEvents.OnAIManagerOfGroupsReady?.AddListener(
                    () => AIManagerOfGroups.Instance
                        .AddToQueue(subordinatesPool, _subordinatesCount, this.transform.position,
                        _radiusOfSpawn, _subordinateSpawnDelay, _subordinateEnemyRank));
            }
            else
            {
                AIManagerOfGroups.Instance?.AddToQueue(
                    subordinatesPool, _subordinatesCount, this.transform.position, _radiusOfSpawn,
                    _subordinateSpawnDelay, _subordinateEnemyRank);
            }
        }
        if (_doSpawnLeader && DEnemys.List.TryGetValue(_leaderEnemyType, out Pool<Transform> leadersPool) && leadersPool != null)
        {
            if (AIManagerOfGroups.Instance == null)
            {
                if (InitEvents.OnAIManagerOfGroupsReady == null)
                {
                    Debug.Log($"{nameof(InitEvents.OnAIManagerOfGroupsReady)} is null");
                    return;
                }
                InitEvents.OnAIManagerOfGroupsReady?.AddListener(
                    () => AIManagerOfGroups.Instance
                        .AddToQueue(leadersPool, _leadersCount, this.transform.position,
                        _radiusOfSpawn, _leaderSpawnDelay, _leaderEnemyRank));
            }
            else
            {
                AIManagerOfGroups.Instance?.AddToQueue(
                    leadersPool, _leadersCount, this.transform.position, _radiusOfSpawn,
                    _leaderSpawnDelay, _leaderEnemyRank);
            }
        }
    }
}

 
 
 */
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
    /// <param name="leadersPool">The leadersPool of needed enemies</param>
    /// <param name="countToSpawn">Number of enemies needed to instantiate</param>
    /// <param name="rank">The rank of enemies</param>
    /// <param name="delay">The delay between instantiation of each enemy</param>
    /// <param name="pivotPosition">Coordinates of point where will spawn group</param>
    /// <param name="radius">The radius of enemies instantiation</param>
    /// <remarks>The defaul rank of enemy is Subordinate(the smallest one)
    /// The default delay is 2f
    /// The default radius is 5f</remarks>
    public void AddToQueue(Pool<Transform> leadersPool, byte countToSpawn, Vector3 pivotPosition, float radius = 5f, float delay = 2f, EEnemyRank rank = EEnemyRank.Subordinate)
    {
        if (_DEnemyRank == null) 
        {
            Debug.Log($"{nameof(_DEnemyRank)} in {nameof(AddToQueue)} in {nameof(AIManagerOfGroups)} was null, " +
                $"you forgot to create an instance of addToConcreteQueue dictionary {nameof(_DEnemyRank)}");
            Awake();
            return;
        }
        if (leadersPool == null)
        {
            Debug.Log($"{nameof(leadersPool)} in {nameof(AddToQueue)} in {nameof(AIManagerOfGroups)} null");
            return;
        }
        if (countToSpawn == 0)
        {
            Debug.Log($"{nameof(countToSpawn)} in {nameof(AddToQueue)} in {nameof(AIManagerOfGroups)} is 0");
            return;
        }

        Action<Tuple<Pool<Transform>, float, Vector3, byte, float>> addToConcreteQueue;
        var parameters = Tuple.Create(leadersPool, delay, pivotPosition, countToSpawn, radius);

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
            Pool<Transform> leadersPool = list.Item1;

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
                enemy = leadersPool.Get();
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
            Debug.Log($"{nameof(list)} in {nameof(AddToSubordinatesQueue)} in {nameof(AISpawnCoordinator)}");
            return;
        }
        _subordinatesQueue.Enqueue(list);
        if(!_isProcessing) StartCoroutine(InstantiateQueue());
    }
    public void AddToLeadersQueue(params Transform[] list)
    {
        if (list.Length == 0)
        {
            Debug.Log($"{nameof(list)} in {nameof(AddToLeadersQueue)} in {nameof(AISpawnCoordinator)}");
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