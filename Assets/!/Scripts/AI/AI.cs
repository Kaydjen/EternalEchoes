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
    // здесь должен быть метод подписки на спавн
    // ----
    // должна быть корутина, которая будет ждать, пока не заспавняться вся группа, и только тогда спавнить следующую группу
    // время на ожидание = 


/*    private IEnumerator SpawnCoroutine()
    {
        while()
    }*/


    

   



}
