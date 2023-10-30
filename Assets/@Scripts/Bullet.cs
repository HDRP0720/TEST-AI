using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : PoolableObejct
{
  public float autoDestroyTime = 5f;
  public float moveSpeed = 2f;
  public int damage = 5;
  public Rigidbody rb;
  
  private const string DisableMethodName = "Disable";

  private void Awake()
  {
    rb = GetComponent<Rigidbody>();
  }

  private void OnEnable()
  {
    CancelInvoke(DisableMethodName);
    Invoke(DisableMethodName, autoDestroyTime);
  }

  private void OnTriggerEnter(Collider other)
  {
    IDamageable damageable;
    if (other.TryGetComponent<IDamageable>(out damageable))
      damageable.TakeDamage(damage);
    
    Disable();
  }
  
  private void Disable()
  {
    CancelInvoke(DisableMethodName);
    rb.velocity = Vector3.zero;
    gameObject.SetActive(false);
  }
}
