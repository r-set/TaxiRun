using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    public Waypoint previosWaypoint;
    public Waypoint nextWaypoint;
    [Range(0f, 5f)] public float width = 0.6f;
    public List<Waypoint> branches = new List<Waypoint>();

    [Range(0f, 1f)] public float branchRatio = 0.5f;

    public Vector3 GetPosition()
    {
        Vector3 minBounds = transform.position + transform.right * width / 2f;
        Vector3 maxBounds = transform.position - transform.forward * width / 2f;
        return Vector3.Lerp(minBounds, maxBounds, Random.Range(0f, 1f));
    }
}
