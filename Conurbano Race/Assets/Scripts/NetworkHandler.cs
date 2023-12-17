using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;
using Fusion.Sockets;
using System;

public class NetworkHandler : MonoBehaviour, INetworkRunnerCallbacks
{
    [SerializeField] NetworkRunner _nRPrefab;
    NetworkRunner _currentRunner;
    public event Action OnJoinedLobby = delegate { };
    public event Action<List<SessionInfo>> OnSessionListUpdate = delegate { };
    
    

    public void JoinLobby()
    {
        if (_currentRunner) Destroy(_currentRunner.gameObject);

        _currentRunner = Instantiate(_nRPrefab);
        
        _currentRunner.AddCallbacks(this);

        var clientTask = JoinLobbyTask();

        Debug.Log(_currentRunner.Topology.ToString());
    }

    async Task JoinLobbyTask()
    {
        var result = await _currentRunner.JoinSessionLobby(SessionLobby.ClientServer, "Normal Lobby");

        if (result.Ok)
        {
            OnJoinedLobby();
        }
        else
        {
            Debug.LogError("No se pudo unir al lobby");
        }
    }

    public void CreateGame(string sessionName, string sceneGame)
    {        
        var clientTask = InitializeGame(GameMode.Host, sessionName, SceneUtility.GetBuildIndexByScenePath($"Scenes/{sceneGame}"));
    }

    public void JoinGame(SessionInfo sessionInfo)
    {
        Debug.Log(sessionInfo.Name);
       var clientTask = InitializeGame(GameMode.AutoHostOrClient, sessionInfo.Name);
    }


    async Task InitializeGame(GameMode gameMode, string sessionName, SceneRef? scene = null)
    {
        var sceneObj = _currentRunner.GetComponent<NetworkSceneManagerDefault>();

        _currentRunner.ProvideInput = true;

        var result = await _currentRunner.StartGame(new StartGameArgs()
        {
            GameMode = gameMode,
            Scene = scene,
            SessionName = sessionName,
            CustomLobbyName = "TestSession",
            SceneManager = sceneObj
        });


    }

    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
    {
        OnSessionListUpdate(sessionList);

        if (sessionList.Count > 0)
        {
            SessionInfo session = sessionList[0];

            //JoinGame(session);
        }
    }

    

    #region CALLBACKS
    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        
    }

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {
        
    }

    public void OnInput(NetworkRunner runner, NetworkInput input)
    {
        
    }

    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
    {
        
    }

    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
    {
        
    }

    public void OnConnectedToServer(NetworkRunner runner)
    {
        
    }

    public void OnDisconnectedFromServer(NetworkRunner runner)
    {
        
    }

    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token)
    {
        
    }

    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
    {
        
    }

    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
    {
        
    }

    

    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data)
    {
        
    }

    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
    {
        
    }

    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ArraySegment<byte> data)
    {
        
    }

    public void OnSceneLoadDone(NetworkRunner runner)
    {
        
    }

    public void OnSceneLoadStart(NetworkRunner runner)
    {
        
    }
    #endregion
}





