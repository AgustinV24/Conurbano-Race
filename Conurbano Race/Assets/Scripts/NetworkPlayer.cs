using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
public class NetworkPlayer : NetworkBehaviour
{
    public static NetworkPlayer local
    {
        get;

        private set;
    }
    public override void Spawned()
    {
        if (Object.HasInputAuthority)
        {
            local = this;
        }
    }

}
