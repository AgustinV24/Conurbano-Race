using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
public class MysteryBoxManager : NetworkBehaviour
{
    public List<MysteryBox> mysteryBoxes;
    float timer;
    void Start()
    {

    }
    private void Update()
    {
        if (timer < 5)
        {
            timer += Time.deltaTime;
        }
        else
        {
            foreach (var item in mysteryBoxes)
            {
                item.gameObject.SetActive(true);
            }
            timer = 0;
        }
    }
    IEnumerator Renewal()
    {
       
        while (true)
        {
            Debug.Log("ocurre");
            yield return new WaitForSeconds(5f);
            
            foreach (var item in mysteryBoxes)
            {
                item.gameObject.SetActive(true);
            }
        }

    }
}
