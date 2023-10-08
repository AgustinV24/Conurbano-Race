using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using Fusion.Sockets;
using System;

public class SpawnNetworkPlayer : MonoBehaviour, INetworkRunnerCallbacks
{   

    [SerializeField] NetworkPlayer _playerPrefab;
    public Action<NetworkPlayer> OnConnected; 
    public Action<NetworkPlayer> OnDisconnected; 


    CharacterInputHandler _characterInputs;

    public void OnConnectedToServer(NetworkRunner runner)
    {
        if (runner.Topology == SimulationConfig.Topologies.Shared)
        {            
            runner.Spawn(_playerPrefab, Vector3.zero, Quaternion.identity, runner.LocalPlayer);            
        }
    }

    public void OnInput(NetworkRunner runner, NetworkInput input)
    {
        if (!NetworkPlayer.local) return;

        if (!_characterInputs)
        {
            _characterInputs = NetworkPlayer.local.GetComponent<CharacterInputHandler>();
        }
        else
        {
            input.Set(_characterInputs.GetNetworkInputs());
        }

        
    }

    public void OnDisconnectedFromServer(NetworkRunner runner) 
    {
        OnDisconnected(_playerPrefab.GetComponent<NetworkPlayer>());
    
    }

    #region Callbacks sin usar

    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player) { }

    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason) { }

    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token) { }

    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data) { }

    

    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken) { }

    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input) { }

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player) { }

    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ArraySegment<byte> data) { }

    public void OnSceneLoadDone(NetworkRunner runner) { }

    public void OnSceneLoadStart(NetworkRunner runner) { }

    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList) { }

    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason) { }

    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message) { }

    

    #endregion
}
