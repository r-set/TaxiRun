using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointNavigation : MonoBehaviour
{

    private WaypointMovement controller;
    public Waypoint currentWaypoint;

    private void Awake()
    {
        controller = GetComponent<WaypointMovement>();
    }

    void Start()
    {
        controller.SetDestination(currentWaypoint.GetPosition());
    }

    void Update()
    {
        if (controller._reachedDestination)
        {

            bool shouldBranch = false;

            if(currentWaypoint.branches != null && currentWaypoint.branches.Count > 0)
            {
                shouldBranch = Random.Range(0f, 1f) <= currentWaypoint.branchRatio;
            }

            if (shouldBranch)
            {
                currentWaypoint = currentWaypoint.branches[Random.Range(0, currentWaypoint.branches.Count - 1)];
            }
            else
            {
                currentWaypoint = currentWaypoint.nextWaypoint;
            }

            currentWaypoint = currentWaypoint.nextWaypoint;
            controller.SetDestination(currentWaypoint.GetPosition());
        }
    }
}
