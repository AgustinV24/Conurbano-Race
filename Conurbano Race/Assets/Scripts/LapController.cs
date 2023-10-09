using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LapController : MonoBehaviour
{
    public int lapsToWin = 2;
    public Transform[] checkpoints; 

    [SerializeField] private int currentLap = 0; 
    public int currentCheckpoint = 0;

    [SerializeField] PlayerModel pl;

    private void Start()
    {
        checkpoints = pl.PM.checkPoints;
    }

    void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.layer == 11)
        {
            
            if (other.transform == checkpoints[currentCheckpoint])
            {
                
                currentCheckpoint++;

                
                if (currentCheckpoint >= checkpoints.Length)
                {
                    currentLap++;

                    
                    if (currentLap >= lapsToWin)
                    {
                        pl.winner = true;
                        var col = pl.PM.GetConnectedPlayers().ToArray();
                        foreach (var item in col)
                        {
                            item.RPC_End();
                        }
                    }

                    
                    currentCheckpoint = 0;
                }
            }
            else
            {
                
            }
        }
    }

    
}
