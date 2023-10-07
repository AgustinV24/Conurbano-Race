using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class Horn : NetworkBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!Object || !HasStateAuthority) return;

        if(other.TryGetComponent<PlayerModel>(out PlayerModel player))
        {
            player.kartRigidbody.Rigidbody.velocity = Vector3.zero;
            player.CouroutineActivator(player.MovementLimiting());
        }
    }
}
