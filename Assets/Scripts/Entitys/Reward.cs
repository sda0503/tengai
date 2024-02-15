using System;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

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
    public int iNum;
    public int power;
    public RewardType rewardType;

    void Awake()
    {
        image = transform.GetChild(0).GetComponent<Image>();
        text = transform.GetChild(1).GetComponent<Text>();
        power = Random.Range(10, 30);
        SwitchType(rewardType);
    }

    public void SwitchType(RewardType type)
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
                switch (iNum)
                {
                    case 0:
                        image.sprite = sprites[3];
                        text.text = "불타는 혈액";
                        break;
                    case 1:
                        image.sprite = sprites[4];
                        text.text = "매끄러운 돌";
                        break;
                    case 2:
                        image.sprite = sprites[5];
                        text.text = "딸기";
                        break;
                    case 3:
                        image.sprite = sprites[6];
                        text.text = "금강저";
                        break;
                }
                break;
        }
    }
}
