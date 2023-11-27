using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HornItem : Item
{
    public override void Actions(PlayerModel _player)
    {
        Debug.Log("Horn ITem");
        _player.CouroutineActivator(_player.HornActivation());
    }

}
