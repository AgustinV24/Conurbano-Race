using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoost : Item
{
    

    public override void Actions(PlayerModel _player)
    {
        //Debug.Log("USO item");
        _player._inputData.isBeingBoosted = true;
        _player.kartRigidbody.Rigidbody.AddForce(_player.transform.forward * 400, ForceMode.Impulse);
    }

}
