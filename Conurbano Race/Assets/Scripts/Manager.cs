using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
public class Manager : NetworkBehaviour
{
    public PlayerManager playerManager;
    void Update()
    {
        if (playerManager.GetConnectedPlayers().Count >= 2)
        {
            foreach (var item in playerManager.GetConnectedPlayers())
            {
                item.canMove = true;
            }
            Destroy(gameObject);
        }

    }
}
