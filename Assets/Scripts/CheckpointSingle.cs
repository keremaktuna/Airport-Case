using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointSingle : MonoBehaviour
{
    private TrackCheckpoints trackCheckpoints;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<PlaneController>(out PlaneController plane))
        {
            trackCheckpoints.PlayerThroughCheckpoint(this);
        }
    }

    public void SetCheckpoints(TrackCheckpoints trackCheckpoints)
    {
        this.trackCheckpoints = trackCheckpoints;
    }

    public void DestroyChecpoint()
    {
        Destroy(gameObject);
    }
}
