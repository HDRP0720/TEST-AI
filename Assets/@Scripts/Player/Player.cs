using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
  [SerializeField] private AttackRadius _attackRadius;
  [SerializeField] private Animator _animator;
  [SerializeField] private int _health = 300;
  
  private Coroutine _coroutine;

  private readonly int attackTriggerHash = Animator.StringToHash("Attack");

  private void Awake()
  {
    _attackRadius.OnAttack += HandleOnAttack;
  }
  
  private void HandleOnAttack(IDamageable target)
  {
    _animator.SetTrigger(attackTriggerHash);
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
  
  public void TakeDamage(int damage)
  {
    _health -= damage;
    
    if(_health <= 0)
      gameObject.SetActive(false);
  }
  public Transform GetTransform()
  {
    return transform;
  }
    
}
