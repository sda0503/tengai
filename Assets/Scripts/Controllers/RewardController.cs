using UnityEngine;
using UnityEngine.UI;

public class RewardController : MonoBehaviour
{
    public GameObject rewarObj;
    Button btn;
    RewardClick mapManager;
    void Awake()
    {
        GameObject obj1 = transform.parent.gameObject;
        mapManager = obj1.GetComponent<RewardClick>();
    }

    // Start is called before the first frame update
    void Start()
    {
        MakeReward();
    }

    public void MakeReward()
    {
        for (int i = 0; i < 3; i++)
        {
            float y = i * -120 + 640;
            float x = 960;
            if (i == 0)
            {
                btn = rewarObj.GetComponent<Button>();
                btn.onClick.AddListener(mapManager.AddGold);
                rewarObj.GetComponent<Reward>().rewardType = RewardType.Gold;
            }
            else if (i == 1) rewarObj.GetComponent<Reward>().rewardType = RewardType.Card;
            else if (i == 2) rewarObj.GetComponent<Reward>().rewardType = RewardType.SCard;
            var reList = Instantiate(rewarObj, new Vector3(x, y, 0), Quaternion.identity, gameObject.transform);
            if (i==0) reList.GetComponent<Button>().onClick.AddListener(() => mapManager.AddGold());
            else if (i == 1) reList.GetComponent<Button>().onClick.AddListener(() => mapManager.AddGold());
            else if (i == 2) reList.GetComponent<Button>().onClick.AddListener(() => mapManager.AddRelics());
            if (i == 0) reList.GetComponent<Button>().onClick.AddListener(() => mapManager.AddGold());
        }
    }
}
