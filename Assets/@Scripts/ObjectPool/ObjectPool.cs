using System.Collections.Generic;
using UnityEngine;

public class ObjectPool
{
  private PoolableObejct _prefab;
  private List<PoolableObejct> _availableObjects;
  
  // Constructor
  private ObjectPool(PoolableObejct prefab, int size)
  {
    _prefab = prefab;
    _availableObjects = new List<PoolableObejct>(size);
  }

  public static ObjectPool CreateInstance(PoolableObejct prefab, int size)
  {
    ObjectPool pool = new ObjectPool(prefab, size);
    GameObject poolObject = new GameObject(prefab.name + "Pool");
    pool.CreateObjects(poolObject.transform, size);

    return pool;
  }

  private void CreateObjects(Transform parent, int size)
  {
    for (int i = 0; i < size; i++)
    {
      PoolableObejct poolableObejct = GameObject.Instantiate(_prefab, Vector3.zero, Quaternion.identity, parent.transform);
      poolableObejct.Parent = this;
      poolableObejct.gameObject.SetActive(false);
    }
  }

  public void ReturnObjectToPool(PoolableObejct poolableObejct)
  {
    _availableObjects.Add(poolableObejct);
  }

  public PoolableObejct GetObject()
  {
    if (_availableObjects.Count > 0)
    {
      PoolableObejct instance = _availableObjects[0];
      _availableObjects.RemoveAt(0);
      
      instance.gameObject.SetActive(true);
      return instance;
    }
    else
    {
      Debug.LogError($"Could not get an object from pool {_prefab.name} Pool. Probably a configuration issue.");
      return null;
    }
  }
}
