using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent), typeof(AgentLinkMover))]
public class EnemyMovement : MonoBehaviour
{
  [Tooltip("How frequently to recalculate path based on Target transform's position")]
  public float updateTime = 0.1f;
  public Transform target;
  
  [SerializeField] private Animator _animator;

  private NavMeshAgent _agent;
  private AgentLinkMover _agentLinkMover;
  private Coroutine _coroutine;
  
  private readonly int isMovingBoolHash = Animator.StringToHash("IsMoving");
  private readonly int jumpTriggerHash = Animator.StringToHash("Jump");
  private readonly int landTriggerHash = Animator.StringToHash("Land");

  private void Awake()
  {
    _agent = GetComponent<NavMeshAgent>();
    _agentLinkMover = GetComponent<AgentLinkMover>();

    _agentLinkMover.OnLinkStart += HandleOnLinkStart;
    _agentLinkMover.OnLinkEnd += HandleOnLinkEnd;
  }
  private void Update()
  {
    _animator.SetBool(isMovingBoolHash, _agent.velocity.magnitude > 0.01f);
  }
  
  public void StartChasing()
  {
    if (_coroutine == null)
      _coroutine = StartCoroutine(CoFollowTarget());
    else
      Debug.LogWarning("Called StartChasing on Enemy that is already chasing! This is likely a bug in some calling class!");
  }
  private IEnumerator CoFollowTarget()
  {
    WaitForSeconds wait = new WaitForSeconds(updateTime);
 
    while (enabled)
    {
      _agent.SetDestination(target.position);
      yield return wait;
    }
  }
  
  private void HandleOnLinkStart()
  {
    _animator.SetTrigger(jumpTriggerHash);
  }
  private void HandleOnLinkEnd()
  {
    _animator.SetTrigger(landTriggerHash);
  }
}
