using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineSound : MonoBehaviour
{
    [SerializeField] PlayerModel player;
    float timer;
    [SerializeField] AudioSource audioD;
    AudioSource audioE;
    void Start()
    {
        audioE = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        

    }



}
