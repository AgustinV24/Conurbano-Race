using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;
public class NetworkHandler : MonoBehaviour
{
    NetworkRunner _runner;
    void Start()
    {
        _runner = GetComponent<NetworkRunner>();
        var clientTask = InitializeGame(GameMode.Shared, SceneManager.GetActiveScene().buildIndex);
    }

    Task InitializeGame(GameMode gameMode, SceneRef scene)
    {
        var sceneObj = _runner.GetComponent<NetworkSceneManagerDefault>();
        _runner.ProvideInput = true;
        return _runner.StartGame(new StartGameArgs
        {
            GameMode = gameMode,
            Scene = scene,
            SessionName = "TestSession",
            SceneManager = sceneObj

        }
        );
    }

}
