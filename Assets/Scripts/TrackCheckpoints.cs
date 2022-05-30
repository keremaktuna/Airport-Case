using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackCheckpoints : MonoBehaviour
{
    private List<CheckpointSingle> checkpointSingleList;
    private int nextCheckpointIndex;

    private PlaneController playerPlane;

    private void Awake()
    {
        playerPlane = GameObject.FindGameObjectWithTag("Player").GetComponent<PlaneController>();

        Transform checkpointsTransform = transform.Find("Checkpoints");

        checkpointSingleList = new List<CheckpointSingle>();
        foreach (Transform checkpointSingleTransform in checkpointsTransform)
        {
            CheckpointSingle checkpointSingle = checkpointSingleTransform.GetComponent<CheckpointSingle>();

            checkpointSingle.SetCheckpoints(this);

            checkpointSingleList.Add(checkpointSingle);
        }

        nextCheckpointIndex = 0;
    }

    public void PlayerThroughCheckpoint(CheckpointSingle checkpointSingle)
    {
        if(checkpointSingleList.IndexOf(checkpointSingle) == nextCheckpointIndex)
        {
            //CorrectOrder
            nextCheckpointIndex++;
            checkpointSingle.DestroyChecpoint();
        }
        else
        {
            //WrongOrder
        }
    }
}
