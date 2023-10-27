using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent), typeof(AgentLinkMover))]
public class EnemyMovement : MonoBehaviour
{
  [Tooltip("How frequently to recalculate path based on Target transform's position")]
  public float UpdateTime = 0.1f;
  
  [SerializeField] private Transform _target;
  [SerializeField] private Animator _animator;

  private NavMeshAgent _agent;
  private AgentLinkMover _agentLinkMover;
  
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
  private void Start()
  {
    StartCoroutine(CoFollowTarget());
  }

  private void Update()
  {
    _animator.SetBool(isMovingBoolHash, _agent.velocity.magnitude > 0.01f);
  }

  private IEnumerator CoFollowTarget()
  {
    WaitForSeconds wait = new WaitForSeconds(UpdateTime);
 
    while (enabled)
    {
      _agent.SetDestination(_target.position);
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
