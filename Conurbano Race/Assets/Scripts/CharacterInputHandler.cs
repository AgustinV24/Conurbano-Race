using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInputHandler : MonoBehaviour
{
    NetworkInputData _inputData;
    public bool isUsingItem;
    public bool hasItem;
    public Rigidbody rb;

    private void Start()
    {
        hasItem = true;
        _inputData = new NetworkInputData();
        _inputData.hasItem = true;


    }
    void Update()
    {
        
        _inputData.zMovement = Input.GetAxis("Vertical");
        _inputData.xMovement = Input.GetAxis("Horizontal");

        //if (_inputData.zMovement != 0 && (rb.velocity.z >= 3f || rb.velocity.z <= -3f))
        //    _inputData.xMovement = Input.GetAxis("Horizontal");

        if (!isUsingItem && hasItem && Input.GetKeyDown(KeyCode.Space))
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
