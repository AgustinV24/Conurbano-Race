using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class TurnCollider : NetworkBehaviour
{
    public LapController lapController;
    public PlayerModel player;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 6)
        {
            player.kartRigidbody.Rigidbody.velocity = Vector3.zero;

            if(lapController.currentCheckpoint != 0)
            {
                player.transform.position = lapController.checkpoints[lapController.currentCheckpoint - 1].position + Vector3.up / 3;
                player.transform.rotation = lapController.checkpoints[lapController.currentCheckpoint - 1].rotation;
            }
            else
            {
                player.transform.position = lapController.checkpoints[lapController.currentCheckpoint].position + Vector3.up / 3;
                player.transform.rotation = lapController.checkpoints[lapController.currentCheckpoint].rotation;
            }
            
        }
    }
}
