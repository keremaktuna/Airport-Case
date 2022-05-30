using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointInner : MonoBehaviour
{
    public bool isLastCheckpoint = false;

    public GameObject outerCircle;
    public GameObject successParticle;

    private void Start()
    {
        outerCircle.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlaneController player = other.gameObject.GetComponent<PlaneController>();
            player.PlayerScore += 100;

            DestroyCheckpoint(other.gameObject, false);
        }
    }

    public void DestroyCheckpoint(GameObject player, bool isOuter)
    {
        if(isLastCheckpoint)
        {
            player.GetComponent<PlaneController>().Landing();
        }

        if(!isOuter)
        {
            Instantiate(successParticle).transform.position = gameObject.transform.position;
        }

        Destroy(gameObject);
    }
}
