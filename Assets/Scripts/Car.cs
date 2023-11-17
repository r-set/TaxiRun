using UnityEngine;
using UnityEngine.AI;

public class Car : MonoBehaviour
{

    [SerializeField] private Transform _player;
    private float _rayDistance = 1f;

    private LayerMask _playerMask;
    private NavMeshAgent _agent;
    private bool _visiblePlayer;

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _playerMask = LayerMask.GetMask("Player");
    }

    void Update()
    {
        VesiblePlayer();

        if (!_visiblePlayer)
        {
            Move();
        }   
    }

    private void Move()
    {

    }

    private void VesiblePlayer()
    {
        _visiblePlayer = Physics.Raycast(transform.position, Vector3.forward, _rayDistance, _playerMask);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, Vector3.forward * _rayDistance);
    }
}
