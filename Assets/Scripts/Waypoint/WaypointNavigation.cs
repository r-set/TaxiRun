using UnityEngine;

public class WaypointNavigation : MonoBehaviour
{

    private WaypointMovement _controller;
    public Waypoint currentWaypoint;

    private void Awake()
    {
        _controller = GetComponent<WaypointMovement>();
    }

    void Start()
    {
        _controller.SetDestination(currentWaypoint.GetPosition());
    }

    void Update()
    {
        if (_controller.reachedDestination)
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
            _controller.SetDestination(currentWaypoint.GetPosition());
        }
    }
}
