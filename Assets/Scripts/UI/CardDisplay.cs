using UnityEngine;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour
{
    [SerializeField] private Text _costText;
    [SerializeField] private Text _cardNameText;
    [SerializeField] private Text _cardDescriptionText;
    [SerializeField] private Image _cardImage;

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
        if(_costText != null)
            _costText.text = card.CardData.cost.ToString();
        _cardNameText.text = card.CardData.cardName;
        _cardDescriptionText.text = card.CardData.description;
        _cardImage.sprite = card.CardData.cardSprite;
    }
}
