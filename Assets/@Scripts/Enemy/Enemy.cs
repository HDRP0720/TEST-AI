using System;
using UnityEngine.AI;

public class Enemy : PoolableObejct
{
  public EnemyMovement enemyMovement;
  public NavMeshAgent agent;
  public EnemyScriptableObject enemyScriptableObject;
  public int health = 100;

  public virtual void OnEnable()
  {
    SetupAgentFromConfiguration();
  }
  public override void OnDisable()
  {
    base.OnDisable();
    agent.enabled = false;
  }

  public virtual void SetupAgentFromConfiguration()
  {
    agent.acceleration = enemyScriptableObject.acceleration;
    agent.angularSpeed = enemyScriptableObject.angularSpeed;
    agent.areaMask = enemyScriptableObject.areaMask;
    agent.avoidancePriority = enemyScriptableObject.avoidancePriority;
    agent.baseOffset = enemyScriptableObject.baseOffset;
    agent.height = enemyScriptableObject.height;
    agent.obstacleAvoidanceType = enemyScriptableObject.obstacleAvoidanceType;
    agent.radius = enemyScriptableObject.radius;
    agent.speed = enemyScriptableObject.speed;
    agent.stoppingDistance = enemyScriptableObject.stoppingDistance;
    
    enemyMovement.updateTime = enemyScriptableObject.aiUpdateInterval;
    health = enemyScriptableObject.health;
  }
}
