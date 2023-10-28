using UnityEngine.AI;

public class Enemy : PoolableObejct
{
  public EnemyMovement enemyMovement;
  public NavMeshAgent agent;
  public int health = 100;

  public override void OnDisable()
  {
    base.OnDisable();
    agent.enabled = false;
  }
}
