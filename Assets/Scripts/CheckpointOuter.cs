using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointOuter : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlaneController player = other.gameObject.GetComponent<PlaneController>();
            player.PlayerScore -= 100;

            gameObject.transform.parent.gameObject.GetComponent<CheckpointInner>().DestroyCheckpoint(other.gameObject, true);
        }
    }
}
