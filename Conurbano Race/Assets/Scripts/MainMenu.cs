using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] NetworkHandler _networkHandler;    
    [SerializeField] GameObject _jLPanel;
    [SerializeField] GameObject _sPanel;
    [SerializeField] GameObject _sBPanel;
    [SerializeField] GameObject _hGPanel;    
    [SerializeField] Button _jLButton;
    [SerializeField] Button _hPButton;
    [SerializeField] Button _hGButton;    
    [SerializeField] InputField _sessionNameField;
    [SerializeField] InputField _nicknameField;    
    [SerializeField] Text _statusText;

    void Start()
    {
        _jLButton.onClick.AddListener(JoinLobbyButtonF);
        _hPButton.onClick.AddListener(ShowHostPanelButtonF);
        _hGButton.onClick.AddListener(HostGameButtonF);

        _networkHandler.OnJoinedLobby += () =>
        {
            _sPanel.SetActive(false);
            _sBPanel.SetActive(true);
        };
    }

    void JoinLobbyButtonF()
    {
        _networkHandler.JoinLobby();

        PlayerPrefs.SetString("nickname", _nicknameField.text);

        _jLPanel.SetActive(false);
        _sPanel.SetActive(true);

        _statusText.text = "Uniendose al Lobby...";
    }

    void ShowHostPanelButtonF()
    {
        _sBPanel.SetActive(false);
        _hGPanel.SetActive(true);

    }

    void HostGameButtonF()
    {
        _networkHandler.CreateGame(_sessionNameField.text, "GameScene");
    }
}
