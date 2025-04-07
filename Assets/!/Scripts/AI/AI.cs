using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AI : MonoBehaviour
{
    [SerializeField] protected NavMeshAgent _agent;
    public EAIType Type;
    protected virtual void SetDestionaiton(Vector3 coordinates)
    {
        _agent.destination = coordinates;
    }
    protected virtual void Awake()
    {
        EnemyRepository.Instance.Register(this, this.GetInstanceID());
    }
    protected void OnDestroy()
    {
        EnemyRepository.Instance.Unregister(this, this.GetInstanceID());
    }
    protected virtual void OnEnable()
    {
        _agent.enabled = true;
    }
    protected virtual void OnDisable()
    {
        _agent.enabled = false;
    }
}


public class AIGroupManager : MonoBehaviour
{
    [SerializeField] private EEnemys _subordinateEnemyType;
    [SerializeField] private EEnemys _leaderEnemyType;
    [SerializeField] private byte _subordinatesCount = 8;
    [SerializeField] private byte _leadersCount = 1;

    // ивент, на который подпишуться все члены группы, и если кто-то из членов группы был ранен -
    // то все члены группы атакуют нападавшего (тобиж ивент должен передавать параметр Transform attacker)
    // ----
    // корутина вызывающаяся в методе, который будет передаваться в очередь спавна в менеджер групп для спавна. 
    // Она будет поочередно по кругу спавнить врагов, а в центре будет появляться их главарь
    // ----
    // должно быть публичное свойство и приватное поле в которых будет указана задержка между спавном каждого из юнитов
    // ----
    // поле и свойство для обозначения кол-ва юнитов для спавна
    // ----
    // список юнитов для спавна
    // список главарей для спавна
    // ----
    // булевое поле, для того, что бы обозначать, будет главарь или нет,  и нужно ли спавнить его сейчас 

    private void OnEnable()
    {
        // запрос в менеджер групп для спавна 
    }
}


//// IDEA: можно сделать так, что бы если через допустим 30 чекунд главарь группы все еще был жив, 
///то он спавнил всех своих воинов наново.
///


public class AIManagerOfGroups : MonoBehaviour
{
    [SerializeField] private float _delayBtwInstantiateEachEnemy = 1f;
    [SerializeField] private float _delayBtwInstantiateGroups = 1f;
    private Queue<Transform[]> _subordinatesQueue = new();
    private Queue<Transform[]> _leadersQueue = new();
    private bool _isProcessing;
    public void AddToSubordinatesQueue(Pool<Transform> pool, byte countToSpawn)
    {
        if (pool == null)
        {
            Debug.Log($"{nameof(pool)} in {nameof(AddToSubordinatesQueue)} in {nameof(AIGroupManager)} null");
            return;
        }
        if (countToSpawn == 0)
        {
            Debug.Log($"{nameof(countToSpawn)} in {nameof(AddToSubordinatesQueue)} in {nameof(AIGroupManager)} null");
            return;
        }

        Transform[] subordinates = new Transform[countToSpawn];
        for (int i = 0; i < countToSpawn; i++) subordinates[i] = pool.Get();
        
        _subordinatesQueue.Enqueue(subordinates);
        if(!_isProcessing) StartCoroutine(InstantiateQueue());
    }
    public void AddToLeadersQueue(Pool<Transform> pool, byte countToSpawn)
    {
        if (pool == null)
        {
            Debug.Log($"{nameof(pool)} in {nameof(AddToSubordinatesQueue)} in {nameof(AIGroupManager)} null");
            return;
        }
        if (countToSpawn == 0)
        {
            Debug.Log($"{nameof(countToSpawn)} in {nameof(AddToSubordinatesQueue)} in {nameof(AIGroupManager)} null");
            return;
        }

        Transform[] leaders = new Transform[countToSpawn];
        for (int i = 0; i < countToSpawn; i++) leaders[i] = pool.Get();

        _leadersQueue.Enqueue(leaders);
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