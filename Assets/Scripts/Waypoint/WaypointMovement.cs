using UnityEngine;

public class WaypointMovement : MonoBehaviour
{
    [SerializeField] private Vector3 _destination;
    [SerializeField] private float _minDistance = 0.1f;
    [SerializeField] private float _rotationSpeed = 5f;
    [SerializeField] private float _speed = 1f;

    [HideInInspector] public bool reachedDestination;

    private void Update()
    {
        if(transform.position != _destination)
        {
            Vector3 destDirection = _destination - transform.position;
            destDirection.y = 0;
            float destDistance = destDirection.magnitude;
            if(destDistance >= _minDistance)
            {
                reachedDestination = false;
                Quaternion targetRotation = Quaternion.LookRotation(destDirection);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
                transform.Translate(Vector3.forward * _speed * Time.deltaTime);
            }
            else
            {
                reachedDestination = true;
            }
        }
    }

    public void SetDestination(Vector3 _desination)
    {
        this._destination = _desination;
        reachedDestination = false;
    }
}
