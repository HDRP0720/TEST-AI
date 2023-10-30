using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
  [SerializeField] private GameObject _followTarget;
  [SerializeField] private Camera _camera;
  [SerializeField] private Vector3 _offset;

  private void Update()
  {
    _camera.transform.position = _followTarget.transform.position + _offset;
  }
}
