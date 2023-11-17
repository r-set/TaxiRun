using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Header("Move Settings")]
    [SerializeField] private float _moveSpeed = 1f;
    [SerializeField] private float _rotationSpeed = 270f;
    [SerializeField] private float _minDistance = 0.1f;

    [Header("Route & Prefab")]
    [SerializeField] private LineRenderer _routeLine;
    [SerializeField] private GameObject _playerPrefab;

    private Vector3[] positions = new Vector3[6];
    private Vector3[] _pos;
    private int index = 0;

    void Start()
    {
        _pos = GetLinePointsInWorldSpace();
        _playerPrefab.transform.position = _pos[index];
    }

    void Update()
    {
        Move();
    }

    Vector3[] GetLinePointsInWorldSpace()
    {
        _routeLine.GetPositions(positions);
        return positions;
    }

    void Move()
    {
        _playerPrefab.transform.position = Vector3.MoveTowards(_playerPrefab.transform.position, _pos[index], _moveSpeed * Time.deltaTime);

        Vector3 destDirection = _pos[index] - _playerPrefab.transform.position;
        destDirection.y = 0;
        float destDistance = destDirection.magnitude;

        if (_playerPrefab.transform.position != _pos[index])
        {
            if (destDistance >= _minDistance)
            {
                Quaternion targetRotation = Quaternion.LookRotation(destDirection);
                _playerPrefab.transform.rotation = Quaternion.RotateTowards(_playerPrefab.transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
            }
        }
        else
        {
            index += 1;
        }

        if (index == _pos.Length)
        {
            index = 5;
        }
    }
}
