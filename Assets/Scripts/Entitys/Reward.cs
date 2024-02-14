using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public enum RewardType
{
   Gold,
   Potion,
   Card,
   SCard,
   Relic
}

public class Reward : MonoBehaviour
{
    public Sprite[] sprites;
    Image image;
    Text text;
    public int power;
    public RewardType rewardType;

    void Awake()
    {
        image = transform.GetChild(0).GetComponent<Image>();
        text = transform.GetChild(1).GetComponent<Text>();
        power = Random.Range(10, 30);
        SwitchType(rewardType);
    }

    void SwitchType(RewardType type)
    {
        switch (type)
        {
            case RewardType.Gold:
                image.sprite = sprites[0];
                text.text = $"{power.ToString()} 골드";
                break;
            case RewardType.Potion:
                image.sprite = sprites[0];
                break;
            case RewardType.Card:
                image.sprite = sprites[1];
                text.text = "댁에 카드를 추가";
                break;
            case RewardType.SCard:
                image.sprite = sprites[2];
                text.text = "댁에 카드를 추가";
                break;
            case RewardType.Relic:
                image.sprite = sprites[0];
                break;
        }
    }
}
