using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    [SerializeField] private GameObject iconObj;
    [SerializeField] private GameObject backGroundObj;
    [SerializeField] private TextMeshProUGUI hpText;
    private TextMeshProUGUI defText;
    private Slider _slider;

    [SerializeField] private GameObject _buffSlotPrefab;
    [SerializeField] private Transform _buffPivot;
    private List<BuffSlot> _buffSlots = new();

    private void Awake()
    {
        defText = iconObj.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        _slider = transform.GetChild(0).GetComponent<Slider>();
    }

    public void UpdateHPBar(int hp, int maxHP, int def)
    {
        hpText.text = $"{hp} / {maxHP}";
        defText.text = $"{def}";

        _slider.value = hp / (float)maxHP;

        if (iconObj.activeSelf)
        {
            if (def == 0)
            {
                iconObj.SetActive(false);
                backGroundObj.SetActive(false);
            }
        }
        else
        {
            if (def > 0)
            {
                iconObj.SetActive(true);
                backGroundObj.SetActive(true);
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
