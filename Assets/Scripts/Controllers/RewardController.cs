using UnityEngine;
using UnityEngine.UI;

public class RewardController : MonoBehaviour
{
    private RewardClick _mapManager;
    private GameObject[] _list = new GameObject[3];

    public GameObject rewarObj;
    public int iNum = 0;

    void Awake()
    {
        GameObject obj1 = transform.parent.gameObject;
        _mapManager = obj1.GetComponent<RewardClick>();
        for (int i = 0; i < _list.Length; i++)
        {
            float y = i * -120 + 640;
            float x = 960;
            var reList = Instantiate(rewarObj, new Vector3(x, y, 0), Quaternion.identity, gameObject.transform);
            _list[i] = reList;
            _list[i].SetActive(false);
        }
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
            _list[i].GetComponent<Button>().onClick.RemoveAllListeners();
            if (i == 0)
            {
                //btn = list[i].GetComponent<Button>();//
                _list[i].GetComponent<Reward>().rewardType = RewardType.Gold;
                _list[i].GetComponent<Button>().onClick.AddListener(() => _mapManager.AddGold());
            }
            else if (i == 1)
            {
                _list[i].GetComponent<Reward>().rewardType = RewardType.Card;
                _list[i].GetComponent<Button>().onClick.AddListener(() => _mapManager.AddCard());
            }
            else if (i == 2)
            {
                _list[i].GetComponent<Reward>().iNum = iNum;
                _list[i].GetComponent<Reward>().rewardType = RewardType.Relic;
                _list[i].GetComponent<Button>().onClick.AddListener(() => _mapManager.AddRelics(iNum));
            }
            _list[i].SetActive(true);
            _list[i].GetComponent<Reward>().power = Random.Range(10, 30);
            _list[i].GetComponent<Reward>().SwitchType(_list[i].GetComponent<Reward>().rewardType);
        }
    }
}
