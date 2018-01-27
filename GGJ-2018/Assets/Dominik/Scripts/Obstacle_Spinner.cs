using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle_Spinner : Obstacle 
{

    private bool invert;
    private int currentWaypoint;
    [Space(10)]
    public List<Transform> waypoints = new List<Transform>();
    [Space(10)]
    public float moveSpeed;
    public float rotateSpeed;

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypoint].position, Time.deltaTime * moveSpeed);
        transform.Rotate(new Vector3(0, 0, -(Time.deltaTime * rotateSpeed)));

        if (Vector3.Distance(transform.position, waypoints[currentWaypoint].position) < 0.1f)
        {
            if (currentWaypoint == waypoints.Count - 1)
            {
                invert = true;
            }
            else if (currentWaypoint == 0 && invert)
            {
                invert = false;
            }

            if (invert)
            {
                currentWaypoint--;
            }
            else
            {
                currentWaypoint++;
            }
        }
    }
}
