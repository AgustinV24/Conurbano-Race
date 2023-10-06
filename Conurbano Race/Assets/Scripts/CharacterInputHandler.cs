using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInputHandler : MonoBehaviour
{
    NetworkInputData _inputData;
    public bool isUsingItem;
    public bool hasItem;

    private void Start()
    {
        _inputData = new NetworkInputData();
        
       
    }
    void Update()
    {
        _inputData.xMovement = Input.GetAxis("Horizontal");
        _inputData.zMovement = Input.GetAxis("Vertical");
        if(!isUsingItem && hasItem && Input.GetKeyDown(KeyCode.Space))
        {
            isUsingItem = true;
            hasItem = false;
        }
    }
    public NetworkInputData GetNetworkInputs()
    {
        _inputData.isUsingItem = isUsingItem;
        isUsingItem = false;

        _inputData.hasItem = hasItem;
        hasItem = false;
        return _inputData;
    }
}
