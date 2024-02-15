using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuffSlot : MonoBehaviour
{
    private Image _img;
    private TextMeshProUGUI _text;
    private Buff _buff;

    private void Awake()
    {
        _img = GetComponent<Image>();
        _text = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    public void Init(Buff buff)
    {
        _img.sprite = buff.icon;
        int turn = buff.maxTurn - buff.turn;
        _text.text = turn > 1 ? turn.ToString() : string.Empty;
        _buff = buff;
    }

    public void UpdateSlot()
    {
        int turn = _buff.maxTurn - _buff.turn;
        _text.text = turn > 0 ? turn.ToString() : string.Empty;
        if (turn == 0) Destroy(gameObject);
    }
}
