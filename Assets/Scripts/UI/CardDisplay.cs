using UnityEngine;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour
{
    [SerializeField] private Text costText;
    [SerializeField] private Text cardNameText;
    [SerializeField] private Text cardDescriptionText;
    [SerializeField] private Image cardImage;

    private Card card;

    public int index;
    public float curveRateInHand;
    public float angle;
    public Vector3 targetPos;

    public void SetCard(Card card)
    {
        this.card = card;
        InitText();
    }

    public Card GetCard()
    {
        return card;
    }

    private void InitText()
    {
        costText.text = card.CardData.cost.ToString();
        cardNameText.text = card.CardData.name; 
        cardDescriptionText.text = $"{card.CardData.description}";
        cardImage.sprite = card.CardData.cardSprite;
    }
}
