using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class Horn : NetworkBehaviour
{
    public PlayerModel playerM;
    public SphereCollider sC;
    public NetworkRigidbody rb;
    
    // Start is called before the first frame update
    void Start()
    {
        enabled = false;
    }

    public override void FixedUpdateNetwork()
    {
        rb.Rigidbody.MovePosition(playerM.kartRigidbody.Rigidbody.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hehehehehe");
        if (!Object || !Object.HasStateAuthority) return;

        if (other.TryGetComponent<PlayerModel>(out PlayerModel player))
        {
            if(player != playerM)
            player.RPC_Stun();                    
            
        }
    }
}
