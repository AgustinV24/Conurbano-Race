using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using System.Linq;

public class PlayerManager : NetworkBehaviour
{
    [SerializeField]private List<NetworkPlayer> connectedPlayers = new List<NetworkPlayer>();

    SpawnNetworkPlayer _snp;
    public Transform[] spawnPoints;
    public Transform[] checkPoints;
    [Networked] public int index { get; set; }
    void Start()
    {
        _snp = FindObjectOfType<SpawnNetworkPlayer>();

        _snp.OnConnected += OnPlayerConnected;

        _snp.manager = this;
    }


    private void OnPlayerConnected(NetworkPlayer player)
    {

        connectedPlayers.Add(player);        
    }

    
    private void OnPlayerDisconnected(NetworkPlayer player)
    {
        
        connectedPlayers.Remove(player);
    }

    
    public List<PlayerModel> GetConnectedPlayers()
    {
        var col = connectedPlayers.Select(x => x.GetComponent<PlayerModel>()).ToList();
        Debug.Log(col.Count);
        return col;
    }
}
