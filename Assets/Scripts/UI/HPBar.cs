using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    [SerializeField] private GameObject _iconObj;
    [SerializeField] private GameObject _backGroundObj;
    [SerializeField] private TextMeshProUGUI _hpText;
    private TextMeshProUGUI _defText;
    private Slider _slider;

    [SerializeField] private GameObject _buffSlotPrefab;
    [SerializeField] private Transform _buffPivot;
    private List<BuffSlot> _buffSlots = new();

    private void Awake()
    {
        _defText = _iconObj.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        _slider = transform.GetChild(0).GetComponent<Slider>();
    }

    public void UpdateHPBar(int hp, int maxHP, int def)
    {
        _hpText.text = $"{hp} / {maxHP}";
        _defText.text = $"{def}";

        _slider.value = hp / (float)maxHP;

        if (_iconObj.activeSelf)
        {
            if (def == 0)
            {
                _iconObj.SetActive(false);
                _backGroundObj.SetActive(false);
            }
        }
        else
        {
            if (def > 0)
            {
                _iconObj.SetActive(true);
                _backGroundObj.SetActive(true);
            }
        }
    }

    public void CreateBuffSlot(Buff buff)
    {
        _buffSlots.Add(Instantiate(_buffSlotPrefab, _buffPivot).GetComponent<BuffSlot>());
        _buffSlots[^1].Init(buff);
    }

    public void UpdateBuffSlots()
    {
        foreach(var slot in _buffSlots)
        {
            slot.UpdateSlot();
        }
    }
}
