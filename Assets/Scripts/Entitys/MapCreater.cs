using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;
using Random = UnityEngine.Random;


public class MapCreater : MonoBehaviour
{
    int maxFloor = 13;
    int curFloor = 0;

    public Sprite[] mapSpriteIcon;

    public GameObject mapObject;

    List<int> maxRooms = new List<int>();
    List<int> floorRooms = new List<int>();

    int maxCount = 0;

    // Start is called before the first frame update
    void Start()
    {

        //Ãþ ¸¶´Ù °¹¼ö ³Ö±â
        for (int i = 0; i <= maxFloor; i++)
        {
            int setStageCount = Random.Range(3, 6);
            maxCount += setStageCount;
            maxRooms.Add(setStageCount);
        }

        for(int i=0; i< maxCount; i++)
        {
            float r = Random.Range(0f, 100f);
            float[] p = { 1f, 5f, 12f, 16f, 22f, 45f};
            float cumulative = 0f;
            for (int j =0; j<6; j++)
            {
                cumulative += p[j];
                if(r <= cumulative)
                {
                    floorRooms.Add(j);
                    break;
                }
            }
        }
        floorRooms.Add(0);
        floorRooms.Add(0);
        
        //Ãþ¸¶´Ù »ý¼º
        for (int i = 0; i <= maxFloor; i++)
        {
            int y =  i * 100;
            // ¼ÂÆÃ
            for(int j=0; j< 7; j++) 
            {
                int x = j * 100 + 560;
                mapObject.GetComponent<Image>().sprite = mapSpriteIcon[MapSetting(i)];
                if(maxRooms[i] > 0)
                {
                    Instantiate(mapObject, new Vector3(x, y, 0), Quaternion.identity, gameObject.transform);
                    maxRooms[i]--;
                }
            }
        }
    }

    int MapSetting(int num)
    {
        int rand = Random.Range(0, floorRooms.Count);
        int x = 0;
        x = floorRooms[rand];
        floorRooms.Remove(rand);
        switch (num)
        {
            case 0:
                x = 5;
                break;
            case 1:
                break;
            case 2: break;
            case 3: break;
            case 4: break;
            case 5: break;
            case 6: break;
            case 7: break;
            case 8: break;
            case 9:
                x = 0;
                break;
            case 10: break;
            case 11: break;
            case 12: break;
            case 13: break;
        }

        return x;
    }
}
