using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMovement : MonoBehaviour
{
  [SerializeField] private Camera _camera;

  private NavMeshAgent _agent;
  private RaycastHit[] _hits = new RaycastHit[1];

  private void Awake()
  {
    _agent = GetComponent<NavMeshAgent>();
  }

  private void Update()
  {
    if (Input.GetKeyUp(KeyCode.Mouse0))
    {
      Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
      if (Physics.RaycastNonAlloc(ray, _hits) > 0)
      {
        _agent.SetDestination(_hits[0].point);
      }
    }
  }
}
