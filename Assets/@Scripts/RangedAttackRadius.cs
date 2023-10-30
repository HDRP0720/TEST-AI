using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RangedAttackRadius : AttackRadius
{
  public NavMeshAgent agent;
  public Bullet bulletPrefab;
  public Vector3 bulletSpawnOffset = new Vector3(0, 1, 0);
  public LayerMask mask;

  private ObjectPool _bulletPool;
  private float _spherecastRadius = 0.1f;
  private RaycastHit _hit;
  private IDamageable _targetDamageable;
  private Bullet _bullet;

  protected override void Awake()
  {
    base.Awake();
    _bulletPool = ObjectPool.CreateInstance(bulletPrefab, Mathf.CeilToInt((1 / attackDelay) * bulletPrefab.autoDestroyTime));
  }

  protected override IEnumerator CoAttack()
  {
    WaitForSeconds wait = new WaitForSeconds(attackDelay);
    yield return wait;

    while (_damageables.Count > 0)
    {
      for (int i = 0; i < _damageables.Count; i++)
      {
        if(HasLineOfSightTo(_damageables[i].GetTransform()))
        {
          _targetDamageable = _damageables[i];
          OnAttack?.Invoke(_damageables[i]);
          agent.enabled = false;
          break;
        }
      }

      if (_targetDamageable != null)
      {
        PoolableObejct poolableObejct = _bulletPool.GetObject();
        if (poolableObejct != null)
        {
          _bullet = poolableObejct.GetComponent<Bullet>();
          _bullet.damage = damage;
          _bullet.transform.position = transform.position + bulletSpawnOffset;
          _bullet.transform.rotation = agent.transform.rotation;
          _bullet.rb.AddForce(agent.transform.forward * bulletPrefab.moveSpeed, ForceMode.VelocityChange);
        }
      }
      else
      {
        agent.enabled = true;
      }
    
      yield return wait;
      
      if(_targetDamageable == null || HasLineOfSightTo(_targetDamageable.GetTransform()))
      {
        agent.enabled = true;
      }

      _damageables.RemoveAll(DisabledDamageable);
    }
    agent.enabled = true;
    _coroutine = null;
  }
  
  private bool HasLineOfSightTo(Transform target)
  {
    Vector3 direction = (target.position - transform.position).normalized;
    if (Physics.SphereCast(transform.position + bulletSpawnOffset, _spherecastRadius, direction, out _hit, sphereCollider.radius, mask))
    {
      IDamageable damageable;
      if(_hit.collider.TryGetComponent<IDamageable>(out damageable))
      {
        return damageable.GetTransform() == target;
      }
    }

    return false;
  }

  protected override void OnTriggerExit(Collider other)
  {
    base.OnTriggerExit(other);
    if(_coroutine == null)
      agent.enabled = true;
  }
}
