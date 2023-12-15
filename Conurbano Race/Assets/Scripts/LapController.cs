using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class LapController : MonoBehaviour
{
    public int lapsToWin = 0;
    public Transform[] checkpoints;
    public TextMeshProUGUI text;

    [SerializeField] public int currentLap = 0; 
    public int currentCheckpoint = 0;

    [SerializeField] PlayerModel pl;

    private IEnumerator Start()
    {
        text.text = "Lap: " + (currentLap + 1) + "/3";
        yield return new WaitForSeconds(.2f);
        checkpoints = pl.PM.checkPoints;
        
    }


    public void RPC_UpdateText()
    {
        text.text = "Lap: " + (currentLap + 1) + "/3";
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
                    text.text = "Lap: " + (currentLap + 1) + "/3";

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
