using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent), typeof(AgentLinkMover))]
public class PlayerMovement : MonoBehaviour
{
  [SerializeField] private Camera _camera;
  [SerializeField] private Animator _animator;

  private NavMeshAgent _agent;
  private AgentLinkMover _agentLinkMover;
  private RaycastHit[] _hits = new RaycastHit[1];

  private readonly int isMovingBoolHash = Animator.StringToHash("IsMoving");
  private readonly int jumpTriggerHash = Animator.StringToHash("Jump");
  private readonly int landTriggerHash = Animator.StringToHash("Land");

  private void Awake()
  {
    _agent = GetComponent<NavMeshAgent>();
    _agentLinkMover = GetComponent<AgentLinkMover>();

    _agentLinkMover.OnLinkStart += HandleOnLinkStart;
    _agentLinkMover.OnLinkEnd += HandleOnLinkEnd;
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
    
    _animator.SetBool(isMovingBoolHash, _agent.velocity.magnitude > 0.01f);
  }

  private void HandleOnLinkStart()
  {
    _animator.SetTrigger(jumpTriggerHash);
  }
  private void HandleOnLinkEnd()
  {
    _animator.SetTrigger(landTriggerHash);
  }
}
