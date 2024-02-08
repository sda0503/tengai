using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour
{
    [SerializeField] private Text costText;
    [SerializeField] private Text cardNameText;
    [SerializeField] private Text cardDescriptionText;
    [SerializeField] private Image cardImage;

    private Card card;
    
    public void SetCard(Card card)
    {
        this.card = card;
        InitText();
    }

    private void InitText()
    {
        costText.text = card.CardData.Cost.ToString();
        cardNameText.text = card.CardData.name;
        cardDescriptionText.text = card.CardData.description;
        cardImage.sprite = card.CardData.cardSprite;
    }
}
