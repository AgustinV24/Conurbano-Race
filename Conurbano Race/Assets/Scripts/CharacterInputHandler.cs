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
        hasItem = true;
        _inputData = new NetworkInputData();
        _inputData.hasItem = true;


    }
    void Update()
    {
        _inputData.xMovement = Input.GetAxis("Horizontal");
        _inputData.zMovement = Input.GetAxis("Vertical");
        if(!isUsingItem && hasItem && Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("toco space");
            isUsingItem = true;
            hasItem = false;
            StartCoroutine(BoolChange());
        }
    }
    public NetworkInputData GetNetworkInputs()
    {
        _inputData.isUsingItem = isUsingItem;
        _inputData.hasItem = hasItem;

        return _inputData;
    }
    IEnumerator BoolChange()
    {
        yield return new WaitForFixedUpdate();
        yield return new WaitForEndOfFrame();
        isUsingItem = false;
    }
}
