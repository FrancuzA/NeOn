using FMOD.Studio;
using FMODUnity;
using System.Collections.Generic;
using UnityEngine;

public class SplineFollow : MonoBehaviour
{
    public List<Transform> waypoints = new List<Transform>();
    public float speed = 3f;
    public EventReference GhostSoundRef;
    private EventInstance GhostSoundInstance;

    private int currentPointIndex = 0;

    private void OnEnable()
    {
        GhostSoundInstance = RuntimeManager.CreateInstance(GhostSoundRef);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(GhostSoundInstance, this.gameObject, this.GetComponent<Rigidbody>());
        GhostSoundInstance.start();
        GhostSoundInstance.release();
    }

    void Update()
    {
        if (waypoints.Count == 0) return;

        Transform targetPoint = waypoints[currentPointIndex];

        transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, speed * Time.deltaTime);
        transform.LookAt(targetPoint);

        if (Vector3.Distance(transform.position, targetPoint.position) < 0.1f)
        {
            currentPointIndex++;

            if (currentPointIndex >= waypoints.Count)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnDestroy()
    {
        GhostSoundInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }
}