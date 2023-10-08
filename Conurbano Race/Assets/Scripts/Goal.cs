using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<PlayerModel>(out PlayerModel pl))
        {
            pl.winner = true;
            var col = pl.PM.GetConnectedPlayers().ToArray();
            foreach (var item in col)
            {
                item.RPC_End();
            }
          
            
        }

    }
}
