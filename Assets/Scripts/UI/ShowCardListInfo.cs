using System.Collections.Generic;
using UnityEngine;

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
        this.gameObject.SetActive(false);
    }

    public void OnDeckButtonClick()
    {
        this.gameObject.SetActive(true);
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
        this.gameObject.SetActive(true);
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

    public void OnExitButtonClick()
    {
        this.gameObject.SetActive(false);
        _handManager.enabled = true;
    }
}
