using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : PoolableObejct, IDamageable
{
  public AttackRadius attackRadius;
  public Animator animator;
  public EnemyMovement enemyMovement;
  public NavMeshAgent agent;
  public EnemyScriptableObject enemyScriptableObject;
  public int health = 100;
  
  private Coroutine _coroutine;

  private readonly int attackTriggerHash = Animator.StringToHash("Attack");

  private void Awake()
  {
    attackRadius.OnAttack += HandleOnAttack;
  }
  
  private void HandleOnAttack(IDamageable target)
  {
    animator.SetTrigger(attackTriggerHash);
    if(_coroutine != null)
      StopCoroutine(_coroutine);

    _coroutine = StartCoroutine(CoLookAt(target.GetTransform()));
  }
  private IEnumerator CoLookAt(Transform target)
  {
    Quaternion targetRotation = Quaternion.LookRotation(target.position - transform.position);
    float time = 0;
    while (time < 1)
    {
      transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, time);
      time += Time.deltaTime * 2;
      yield return null;
    }

    transform.rotation = targetRotation;
  }
  
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
    attackRadius.attackDelay = enemyScriptableObject.attackDelay;
    attackRadius.damage = enemyScriptableObject.damage;
    attackRadius.sphereCollider.radius = enemyScriptableObject.attackRadius;
  }

  public void TakeDamage(int damage)
  {
    health -= damage;
    
    if(health <= 0)
      gameObject.SetActive(false);
  }
  public Transform GetTransform()
  {
    return transform;
  }
}
