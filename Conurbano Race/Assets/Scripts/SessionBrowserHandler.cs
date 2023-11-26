using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Fusion;

public class SessionBrowserHandler : MonoBehaviour
{
    [SerializeField] NetworkHandler _networkRunner;

    [SerializeField] Text _statusText;

    [SerializeField] SessionItem _sessionPrefab;

    [SerializeField] VerticalLayoutGroup _verticalLayoutGroup;

    private void OnEnable()
    {
        _networkRunner.OnSessionListUpdate += ReceiveSessionList;
    }

    private void OnDisable()
    {
        _networkRunner.OnSessionListUpdate -= ReceiveSessionList;
    }

    void ClearSessionList()
    {
        foreach (GameObject child in _verticalLayoutGroup.transform)
        {
            Destroy(child);
        }

        _statusText.gameObject.SetActive(false);
    }

    void ReceiveSessionList(List<SessionInfo> allSessions)
    {
        ClearSessionList();
        Debug.Log("SE CREO");
        if (allSessions.Count == 0)
        {
            NoSessionFound();
            return;
        }

        foreach (var session in allSessions)
        {
            AddNewSession(session);
            
        }
    }

    void NoSessionFound()
    {
        _statusText.text = "No se encontro sesión";
        _statusText.gameObject.SetActive(true);
    }

    void AddNewSession(SessionInfo sessionInfo)
    {
        //Instanciamos el prefab
        var newSessionItem = Instantiate(_sessionPrefab, _verticalLayoutGroup.transform);

        //Le pasamos los datos del SessionInfo para que la represente visualmente
        newSessionItem.SetSessionData(sessionInfo);
        newSessionItem.OnJoinedSession += JoinSelectedSession;
    }

    void JoinSelectedSession(SessionInfo session)
    {
        _networkRunner.JoinGame(session);
    }
}
