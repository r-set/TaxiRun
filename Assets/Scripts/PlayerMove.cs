using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private LineRenderer routeLine;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float rotationSpeed = 270f;
    [SerializeField] private float _minDistance = 0.1f;

    private Vector3[] positions = new Vector3[6];
    private Vector3[] pos;
    private int index = 0;

    void Start()
    {
        pos = GetLinePointsInWorldSpace();
        playerPrefab.transform.position = pos[index];
    }

    void Update()
    {
        Move();
    }

    Vector3[] GetLinePointsInWorldSpace()
    {
        routeLine.GetPositions(positions);
        return positions;
    }

    void Move()
    {
        playerPrefab.transform.position = Vector3.MoveTowards(playerPrefab.transform.position, pos[index], moveSpeed * Time.deltaTime);

        Vector3 destDirection = pos[index] - playerPrefab.transform.position;
        destDirection.y = 0;
        float destDistance = destDirection.magnitude;

        if (playerPrefab.transform.position != pos[index])
        {
            if (destDistance >= _minDistance)
            {
                Quaternion targetRotation = Quaternion.LookRotation(destDirection);
                playerPrefab.transform.rotation = Quaternion.RotateTowards(playerPrefab.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
        }
        else
        {
            index += 1;
        }

        if (index == pos.Length)
        {
            index = 5;
        }
    }
}
