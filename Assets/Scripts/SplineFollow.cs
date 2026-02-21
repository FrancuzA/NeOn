using UnityEngine;

public class SplineFollow : MonoBehaviour
{
    public Transform[] waypoints;
    public float speed = 3f;
    public bool loop = true;

    private int currentPointIndex = 0;

    void Update()
    {
        if (waypoints.Length == 0) return;

        Transform targetPoint = waypoints[currentPointIndex];

        transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, speed * Time.deltaTime);
        transform.LookAt(targetPoint);

        if (Vector3.Distance(transform.position, targetPoint.position) < 0.1f)
        {
            currentPointIndex++;

            if (currentPointIndex >= waypoints.Length)
            {
                if (loop) currentPointIndex = 0;
                else enabled = false;
            }
        }
    }
}