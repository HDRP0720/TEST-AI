using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class AttackRadius : MonoBehaviour
{
  public SphereCollider sphereCollider;
  public int damage = 10;
  public float attackDelay = 0.5f;
  
  private List<IDamageable> _damageables = new List<IDamageable>();
  private Coroutine _coroutine;
  
  public event Action<IDamageable> OnAttack;

  private void Awake()
  {
    sphereCollider = GetComponent<SphereCollider>();
    sphereCollider.isTrigger = true;
  }

  private void OnTriggerEnter(Collider other)
  {
    IDamageable damageable = other.GetComponent<IDamageable>();
    if (damageable != null)
    {
      _damageables.Add(damageable);
      if (_coroutine == null)
      {
        _coroutine = StartCoroutine(CoAttack());
      }
    }
  }
  private void OnTriggerExit(Collider other)
  {
    IDamageable damageable = other.GetComponent<IDamageable>();
    if (damageable != null)
    {
      _damageables.Remove(damageable);
      if (_damageables.Count == 0)
      {
        StopCoroutine(CoAttack());
        _coroutine = null;
      }
    }
  }

  private IEnumerator CoAttack()
  {
    WaitForSeconds wait = new WaitForSeconds(attackDelay);
    yield return wait;
    
    IDamageable closestDamageable = null;
    float closestDistance = float.MaxValue;
    while (_damageables.Count > 0)
    {
      for (int i = 0; i < _damageables.Count; i++)
      {
        Transform damageableTransform = _damageables[i].GetTransform();
        float distance = Vector3.Distance(transform.position, damageableTransform.position);
        if (distance < closestDistance)
        {
          closestDistance = distance;
          closestDamageable = _damageables[i];
        }
      }

      if (closestDamageable != null)
      {
        OnAttack?.Invoke(closestDamageable);
        closestDamageable.TakeDamage(damage);
      }

      closestDamageable = null;
      closestDistance = float.MaxValue;

      yield return wait;
      _damageables.RemoveAll(DisabledDamageable);
    }

    _coroutine = null;
  }

  private bool DisabledDamageable(IDamageable damageable)
  {
    return damageable != null && !damageable.GetTransform().gameObject.activeSelf;
  }
}
