using UnityEngine;
using UnityEngine.UI;

public class NicknameItem : MonoBehaviour
{
    Transform _owner;

    const float Y_OFFSET = 2.5f;

    Text _myText;

    public void SetOwner(Transform owner)
    {
        _owner = owner;

        _myText = GetComponent<Text>();
    }

    public void UpdateNick(string newNickname)
    {
        _myText.text = newNickname;
    }

    public void UpdatePosition()
    {
        transform.position = _owner.position + Vector3.up * Y_OFFSET;
    }
}
