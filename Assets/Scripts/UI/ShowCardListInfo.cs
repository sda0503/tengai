using UnityEngine;
using UnityEngine.UI;

public class ShowCardListInfo : MonoBehaviour
{
    private HandManager _handManager;
    private CardManager _cardManager;

    [SerializeField] private Transform container;

    [SerializeField] private GameObject cardPrefab;

    private void Start()
    {
        _cardManager = CardManager.instance;
        _handManager = _cardManager.handManager;

        for(int i = 0; i < _cardManager.deck.Count; i++)
        {
            GameObject go = Instantiate(cardPrefab, container);
            go.transform.localScale += new Vector3(0.3f, 0.3f, 0f);
        }
        transform.SetAsLastSibling();
        gameObject.SetActive(false);
    }

    public void OnDeckButtonClick()
    {
        gameObject.SetActive(true);
        transform.SetAsLastSibling();
        GetComponentInChildren<ScrollRect>().verticalNormalizedPosition = 1;
        _handManager.enabled = false;

        if(_cardManager.deck.Count + _cardManager.garbages.Count > container.transform.childCount)
        {
            for(int i = 0; i < _cardManager.deck.Count + _cardManager.garbages.Count - container.transform.childCount; i++)
            {
                GameObject go = Instantiate(cardPrefab, container);
                go.transform.localScale += new Vector3(0.3f, 0.3f, 0f);
            }
        }

        int j;

        for(j = 0; j < _cardManager.deck.Count; j++)
        {
            Debug.Log(j);
            container.transform.GetChild(j).GetComponent<CardDisplay>().SetCard(_cardManager.deck[j]);
            container.transform.GetChild(j).gameObject.SetActive(true);
        }

        for(; j < container.transform.childCount; j++)
        {
            container.transform.GetChild(j).gameObject.SetActive(false);
        }
    }

    public void OnGarbageButtonClick()
    {
        gameObject.SetActive(true);
        _handManager.enabled = false;

        if (_cardManager.deck.Count + _cardManager.garbages.Count > container.transform.childCount)
        {
            for (int i = 0; i < _cardManager.deck.Count + _cardManager.garbages.Count - container.transform.childCount; i++)
            {
                GameObject go = Instantiate(cardPrefab, container);
                go.transform.localScale += new Vector3(0.3f, 0.3f, 0f);
            }
        }

        int j;

        for (j = 0; j < _cardManager.garbages.Count; j++)
        {
            container.transform.GetChild(j).GetComponent<CardDisplay>().SetCard(_cardManager.garbages[j]);
            container.transform.GetChild(j).gameObject.SetActive(true);
        }

        for (; j < container.transform.childCount; j++)
        {
            container.transform.GetChild(j).gameObject.SetActive(false);
        }
    }

    public void OnExtinguishCardsButtonClick()
    {
        gameObject.SetActive(true);
        _handManager.enabled = false;

        int j;

        for (j = 0; j < _cardManager.extinguishedCards.Count; j++)
        {
            container.transform.GetChild(j).GetComponent<CardDisplay>().SetCard(_cardManager.extinguishedCards[j]);
            container.transform.GetChild(j).gameObject.SetActive(true);
        }

        for (; j < container.transform.childCount; j++)
        {
            container.transform.GetChild(j).gameObject.SetActive(false);
        }
    }

    public void OnExitButtonClick()
    {
        gameObject.SetActive(false);
        _handManager.enabled = true;
    }
}
