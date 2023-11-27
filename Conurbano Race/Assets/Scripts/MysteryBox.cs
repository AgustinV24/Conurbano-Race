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
            if(!player.hasItem && player.HasStateAuthority)
            {
                //player.hasItem = true;
               // player._currentItemIndex = Random.Range(0, player.items.Length);
                player.RPC_Box(Random.Range(0, player.items.Length));
               // player.UpdateCanvas();

            }
           // player._currentItem = player.items[player._currentItemIndex];
            player.hasItem = true;
         //  player.UpdateCanvas();
            //Runner.Despawn(Object);
            //Destroy(gameObject);
            gameObject.SetActive(false);
        }
    }

}
