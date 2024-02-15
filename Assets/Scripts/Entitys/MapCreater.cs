using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;


public class MapCreater : MonoBehaviour
{
    int maxFloor = 13;

    public Sprite[] mapSpriteIcon;
    public Sprite[] mapSpriteHover;
    public GameObject mapObject;

    List<int[]> maxRooms = new List<int[]>();
    List<int> floorRooms = new List<int>();

    bool isStart = false;
    float y;

    MapManager mapManager;

    List<GameObject> beforeVec3 = new List<GameObject>();

    void Awake()
    {
        mapManager = transform.parent.parent.parent.gameObject.GetComponent<MapManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //전체 수
        for (int i = 0; i < 84 + 12; i++)
        {
            float r = Random.Range(0f, 100f);
            float[] p = { 1f, 5f, 12f, 14f, 20f, 48f };
            float cumulative = 0f;
            for (int j = 0; j < 6; j++)
            {
                cumulative += p[j];
                if (r <= cumulative)
                {
                    floorRooms.Add(j);
                    break;
                }
            }
        }

        for (int i = 0; i <= maxFloor; i++)
        {
            int setStageCount = Random.Range(3, 6);
            int[] arrayFloor = new int[7];
            maxRooms.Add(arrayFloor);

            int x = setStageCount;
            int y = setStageCount;
            //Debug.Log(y);
            for (int j = 0; j < 7; j++)
            {
                switch (i)
                {
                    case 0:
                        if (y != 0)
                        {
                            maxRooms[i][j] = 5;
                            y--;
                        }
                        else maxRooms[i][j] = 7;
                        break;
                    case 1:
                        if (y != 0)
                        {
                            if (floorRooms[i * x + j] == 3) maxRooms[i][j] = 5;
                            else maxRooms[i][j] = floorRooms[i * x + j];
                            y--;
                        }
                        else maxRooms[i][j] = 7;
                        break;
                    case 2:
                        if (y != 0)
                        {
                            if (floorRooms[i * x + j] == 3) maxRooms[i][j] = 5;
                            else maxRooms[i][j] = floorRooms[i * x + j];
                            y--;
                        }
                        else maxRooms[i][j] = 7;
                        break;

                    case 9:
                        if (y != 0)
                        {
                            maxRooms[i][j] = 0;
                            y--;
                        }
                        else maxRooms[i][j] = 7;
                        break;

                    case 13:
                        if (y != 0)
                        {
                            maxRooms[i][j] = 2;
                            y--;
                        }
                        else maxRooms[i][j] = 7;
                        break;

                    default:
                        if (y != 0)
                        {
                            maxRooms[i][j] = floorRooms[i * x + j];
                            y--;
                        }
                        else maxRooms[i][j] = 7;
                        break;
                }
            }
        }

        
        mapObject.SetActive(true);
        mapObject.GetComponent<Image>().sprite = mapSpriteHover[8];
        mapObject.transform.GetChild(0).GetComponent<Image>().sprite = mapSpriteIcon[8];
        var boss = Instantiate(mapObject, new Vector3(0, -350, 0), Quaternion.identity, gameObject.transform);
        boss.transform.localScale = new Vector3(2, 2, 1);
        boss.GetComponent<Button>().onClick.AddListener((mapManager.ClickArea));
        boss.GetComponent<MapData>().mapData = 8;
        boss.GetComponent<MapData>().index = 14;
        boss.GetComponent<MapData>().floor = 0;

        for (int i = maxFloor; i >= 0; i--)
        {
            ShuffleArray(maxRooms[i]);
            for (int j = 0; j < 7; j++)
            {
                float y = i * 200 - 3100 + Random.Range(-20f, 20f);
                float x = j * 130 - 400 + Random.Range(-20f, 20f);
                mapObject.transform.position = new Vector3(x, y, 0);
                mapObject.GetComponent<Image>().sprite = mapSpriteHover[maxRooms[i][j]];
                mapObject.transform.GetChild(0).GetComponent<Image>().sprite = mapSpriteIcon[maxRooms[i][j]];

                if (i >= 0 && maxRooms[i][j] != 7) mapObject.SetActive(true);
                else mapObject.SetActive(false);
                var mapObj = Instantiate(mapObject, new Vector3(x, y, 0), Quaternion.identity, gameObject.transform);
                mapObj.GetComponent<Button>().onClick.AddListener((mapManager.ClickArea));
                mapObj.GetComponent<MapData>().mapData = maxRooms[i][j];
                mapObj.GetComponent<MapData>().index = j;
                mapObj.GetComponent<MapData>().floor = i;
                mapObj.GetComponent<MapData>().EventNum = Random.Range(0, 3);

                //오브젝트 정보
                beforeVec3.Add(mapObj);
            }
        }


    }

    public void Update()
    {
        if (!isStart)
        {
            y += Time.deltaTime;
            transform.position += new Vector3(0, y, 0);
            if (transform.position.y >= 3400f)
            {
                isStart = true;
            }
        }
        ColorCheck();


    }


    private T[] ShuffleArray<T>(T[] array)
    {
        int random1, random2;
        T temp;

        for (int i = 0; i < array.Length; ++i)
        {
            random1 = Random.Range(0, array.Length);
            random2 = Random.Range(0, array.Length);

            temp = array[random1];
            array[random1] = array[random2];
            array[random2] = temp;
        }

        return array;
    }

    void ColorCheck()
    {
        for (int i = 8; i < 107; i++)
        {
            if (InfoSystem.instance.currentFloor == 0)
            {
                if (gameObject.transform.GetChild(i).GetComponent<MapData>().floor == InfoSystem.instance.currentFloor)
                {
                    gameObject.transform.GetChild(i).transform.GetChild(0).GetComponent<Image>().color = new Color(0, 0, 0, 1f);
                }
                else
                {
                    gameObject.transform.GetChild(i).transform.GetChild(0).GetComponent<Image>().color = new Color(0, 0, 0, 178 / 255f);
                }
            }
            else if (InfoSystem.instance.currentFloor == 14)
            {
                if (gameObject.transform.GetChild(i).GetComponent<MapData>().floor == InfoSystem.instance.currentFloor)
                {
                    gameObject.transform.GetChild(i).transform.GetChild(0).GetComponent<Image>().color = new Color(0, 0, 0, 1f);
                }
                else
                {
                    gameObject.transform.GetChild(i).transform.GetChild(0).GetComponent<Image>().color = new Color(0, 0, 0, 178 / 255f);
                }
            }
            else
            {
                if (gameObject.transform.GetChild(i).GetComponent<MapData>().floor == InfoSystem.instance.currentFloor
                    && (gameObject.transform.GetChild(i).GetComponent<MapData>().index == InfoSystem.instance.index - 1
                    || gameObject.transform.GetChild(i).GetComponent<MapData>().index == InfoSystem.instance.index
                    || gameObject.transform.GetChild(i).GetComponent<MapData>().index == InfoSystem.instance.index + 1
                    ))
                {
                    gameObject.transform.GetChild(i).transform.GetChild(0).GetComponent<Image>().color = new Color(0, 0, 0, 1f);

                }
                else
                {
                    gameObject.transform.GetChild(i).transform.GetChild(0).GetComponent<Image>().color = new Color(0, 0, 0, 178 / 255f);
                }
            }
        }


    }
}
