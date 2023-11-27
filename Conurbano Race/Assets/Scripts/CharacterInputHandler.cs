using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInputHandler : MonoBehaviour
{
    NetworkInputData _inputData;
    public bool isUsingItem;
    [SerializeField] PlayerModel _pl;
    
    

    private void Start()
    {
        
        _inputData = new NetworkInputData();
        _inputData.hasItem = true;


    }
    void Update()
    {

            _inputData.zMovement = Input.GetAxis("Vertical");
            _inputData.xMovement = Input.GetAxis("Horizontal");


        

        if (!isUsingItem && _pl.hasItem && Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("SPACE");
            isUsingItem = true;            
        }
    }
    public NetworkInputData GetNetworkInputs()
    {
        _inputData.isUsingItem = isUsingItem;
        isUsingItem = false;

        return _inputData;
    }
    
}
