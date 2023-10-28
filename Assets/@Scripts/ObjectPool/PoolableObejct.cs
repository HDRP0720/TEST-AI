using UnityEngine;

public class PoolableObejct : MonoBehaviour
{
  public ObjectPool Parent;

  public virtual void OnDisable()
  {
    Parent.ReturnObjectToPool(this);
  }
}
