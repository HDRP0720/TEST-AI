using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovement : MonoBehaviour
{
  [Tooltip("How frequently to recalculate path based on Target transform's position")]
  public float UpdateTime = 0.1f;
  
  [SerializeField] private Transform target;

  private NavMeshAgent _agent;

  private void Awake()
  {
    _agent = GetComponent<NavMeshAgent>();
  }
  private void Start()
  {
    StartCoroutine(CoFollowTarget());
  }

  private IEnumerator CoFollowTarget()
  {
    WaitForSeconds wait = new WaitForSeconds(UpdateTime);
 
    while (enabled)
    {
      _agent.SetDestination(target.position);
      yield return wait;
    }
  }
}
