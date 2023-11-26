using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Fusion;
using System;

public class SessionItem : MonoBehaviour
{
    [SerializeField] Text _sessionNameText;
    [SerializeField] Text _playerCountText;
    [SerializeField] Button _joinButton;

    SessionInfo _currentSessionInfo;

    public event Action<SessionInfo> OnJoinedSession;

    public void SetSessionData(SessionInfo sessionInfo)
    {
        _currentSessionInfo = sessionInfo;

        _sessionNameText.text = sessionInfo.Name;

        _playerCountText.text = $"{sessionInfo.PlayerCount} / {sessionInfo.MaxPlayers}";

        _joinButton.enabled = sessionInfo.PlayerCount < sessionInfo.MaxPlayers;

        _joinButton.onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        OnJoinedSession?.Invoke(_currentSessionInfo);
    }
}
