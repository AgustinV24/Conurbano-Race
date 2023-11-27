using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InkItem : Item
{    

    public override void Actions(PlayerModel _player)
    {
        Debug.Log("Ink Item");
        _player.RPC_Ink();
    }
     
}
