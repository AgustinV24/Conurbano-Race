using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using System;
public class NetworkPlayer : NetworkBehaviour
{
    public static NetworkPlayer local
    {
        get;

        private set;
    }

    NicknameItem _myNickname;

    public event Action OnPlayerLeft = delegate { };

    [Networked(OnChanged = nameof(OnNicknameChanged))]
    string Nickname { get; set; }

    public override void Spawned()
    {
        _myNickname = NicknameHandler.Instance.AddNickname(this);

        if (Object.HasInputAuthority)
        {
            local = this;

            var nickname = PlayerPrefs.GetString("nickname");

            RPC_SetNickname(nickname);
        }
    }

    [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
    void RPC_SetNickname(string newNick)
    {        
        Nickname = newNick;
    }

    static void OnNicknameChanged(Changed<NetworkPlayer> changed)
    {
        var behaviour = changed.Behaviour;

        behaviour._myNickname.UpdateNick(behaviour.Nickname);
    }

    public override void Despawned(NetworkRunner runner, bool hasState)
    {
        OnPlayerLeft();
        Runner.Shutdown();
        Application.Quit();
       
    }
    


}
