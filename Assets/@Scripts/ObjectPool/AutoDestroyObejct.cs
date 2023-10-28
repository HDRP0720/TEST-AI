using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroyObejct : PoolableObejct
{
  public float AutoDestroyTime = 5f;

  private const string DisableMethodName = "Disable";

  public virtual void OnEnable()
  {
    CancelInvoke(DisableMethodName);
    Invoke(DisableMethodName, AutoDestroyTime);
  }

  public virtual void Disable()
  {
    gameObject.SetActive(false);
  }
}
