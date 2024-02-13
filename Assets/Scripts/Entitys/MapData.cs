using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapData : MonoBehaviour
{
    public Sprite mapIcon;
    public Sprite Complete;
    public int mapData;

    public void ClearRoom()
    {
        transform.GetChild(1).gameObject.SetActive(true);
        transform.GetChild(0).gameObject.GetComponent<Image>().sprite = Complete;
    }
}

