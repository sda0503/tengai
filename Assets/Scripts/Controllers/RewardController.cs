using UnityEngine;
using UnityEngine.UI;

public class RewardController : MonoBehaviour
{
    public GameObject rewarObj;
    Button btn;
    RewardClick mapManager;

    public int iNum;
    void Awake()
    {
        if(iNum != null) iNum = 0;
        GameObject obj1 = transform.parent.gameObject;
        mapManager = obj1.GetComponent<RewardClick>();
    }

    // Start is called before the first frame update
    void Start()
    {
        MakeReward();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MakeReward()
    {
        int n =3;
        for (int i = 0; i < n; i++)
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
            else if (i == 2)
            {
                rewarObj.GetComponent<Reward>().iNum = iNum;
                rewarObj.GetComponent<Reward>().rewardType = RewardType.Relic;
            }

            var reList = Instantiate(rewarObj, new Vector3(x, y, 0), Quaternion.identity, gameObject.transform);
            if (i == 0) reList.GetComponent<Button>().onClick.AddListener(() => mapManager.AddGold());
            else if (i == 1) reList.GetComponent<Button>().onClick.AddListener(() => mapManager.AddCard());
            else if (i == 2) reList.GetComponent<Button>().onClick.AddListener(() => mapManager.AddRelics(iNum));
        }
        
    }
}
