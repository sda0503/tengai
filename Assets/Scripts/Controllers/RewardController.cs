using UnityEngine;
using UnityEngine.UI;

public class RewardController : MonoBehaviour
{
    public GameObject rewarObj;
    Button btn;
    RewardClick mapManager;

    GameObject[] list = new GameObject[3];

    public int iNum;
    void Awake()
    {
        if(iNum != null) iNum = 0;
        GameObject obj1 = transform.parent.gameObject;
        mapManager = obj1.GetComponent<RewardClick>();
        for (int i = 0; i < list.Length; i++)
        {
            float y = i * -120 + 640;
            float x = 960;
            var reList = Instantiate(rewarObj, new Vector3(x, y, 0), Quaternion.identity, gameObject.transform);
            list[i] = reList;
            list[i].SetActive(false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
       
    }

    public void Init(int num)
    {
        iNum = num;
        MakeReward();
    }

    public void MakeReward()
    {
        int n = Random.Range(2, 4);
        for (int i = 0; i < n; i++)
        {
            list[i].GetComponent<Button>().onClick = null;
            if (i == 0)
            {
                btn = list[i].GetComponent<Button>();
                btn.onClick.AddListener(mapManager.AddGold);
                list[i].GetComponent<Reward>().rewardType = RewardType.Gold;
                list[i].GetComponent<Button>().onClick.AddListener(() => mapManager.AddGold());
            }
            else if (i == 1)
            {
                list[i].GetComponent<Reward>().rewardType = RewardType.Card;
                list[i].GetComponent<Button>().onClick.AddListener(() => mapManager.AddCard());
            }
            else if (i == 2)
            {
                list[i].GetComponent<Reward>().iNum = iNum;
                list[i].GetComponent<Reward>().rewardType = RewardType.Relic;
                list[i].GetComponent<Button>().onClick.AddListener(() => mapManager.AddRelics(iNum));
            }
            list[i].SetActive(true);
            list[i].GetComponent<Reward>().power = Random.Range(10, 30);
            list[i].GetComponent<Reward>().SwitchType(list[i].GetComponent<Reward>().rewardType);
        }
    }
}
