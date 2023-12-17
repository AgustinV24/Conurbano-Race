using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
public class Manager : NetworkBehaviour
{
    public PlayerManager playerManager;
    [SerializeField] AudioSource _audioSource;
    [SerializeField] GameObject[] _count;
    [SerializeField] GameObject _canvas;
    bool started;
    void Update()
    {
        if (playerManager.GetConnectedPlayers().Count >= 2 && !started)
        {
           
            started = true;
            StartCoroutine(CountDown());
           
        }

    }
    IEnumerator CountDown()
    {
        yield return new WaitForSeconds(1f);
        _canvas.SetActive(false);
        _audioSource.Play();
        Debug.Log(_count.Length);
        yield return new WaitForSeconds(.5f);
        for (int i = 0; i < _count.Length; i++)
        {

            _count[i].SetActive(true);
            if (i > 0)
            {
                _count[i - 1].SetActive(false);
            }
            yield return new WaitForSeconds(1f);

        }
        foreach (var item in playerManager.GetConnectedPlayers())
        {
            item.canMove = true;
        }
        yield return new WaitForSeconds(1f);
        _count[_count.Length - 1].SetActive(false);
    }
}
