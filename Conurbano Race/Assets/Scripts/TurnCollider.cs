using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnCollider : MonoBehaviour
{
    public LapController lapController;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 6)
        {
            transform.position = lapController.checkpoints[lapController.currentCheckpoint].position;
            transform.rotation = lapController.checkpoints[lapController.currentCheckpoint].rotation;
        }
    }
}
