using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class MysteryBox : NetworkBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        enabled = false;
    }  

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerModel>(out PlayerModel player))
        {
            if(!player.hasItem)
            {
                player.hasItem = true;
                player._currentItem = player.items[Random.Range(0, player.items.Length - 1)];
            }

            
            Runner.Despawn(Object);
        }
    }

}
