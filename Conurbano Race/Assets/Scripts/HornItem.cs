using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HornItem : Item
{
    public override void Actions(PlayerModel _player)
    {        
        _player.CouroutineActivator(_player.HornActivation());
    }

}
