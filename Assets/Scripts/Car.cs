using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Car : MonoBehaviour
{

    [SerializeField] private float speedEnemy = 2f;
    [SerializeField] Transform player;
    [SerializeField] private float distance;

    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        var step = speedEnemy * Time.deltaTime; 
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, step);
    }
}
