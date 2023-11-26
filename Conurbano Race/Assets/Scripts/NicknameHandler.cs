using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NicknameHandler : MonoBehaviour
{
    public static NicknameHandler Instance { get; private set; }

    [SerializeField] NicknameItem _nicknamePrefab;

    List<NicknameItem> _allNicknames;

    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        _allNicknames = new List<NicknameItem>();
    }

    public NicknameItem AddNickname(NetworkPlayer owner)
    {
        var newNickname = Instantiate(_nicknamePrefab, transform);

        newNickname.SetOwner(owner.transform);

        _allNicknames.Add(newNickname);

        owner.OnPlayerLeft += () =>
        {
            _allNicknames.Remove(newNickname);

            Destroy(newNickname.gameObject);
        };

        return newNickname;
    }

    private void LateUpdate()
    {
        foreach (var nick in _allNicknames)
        {
            nick.UpdatePosition();
        }
    }
}
