using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ObjectDatas", menuName = "Data/Datas/ObjectDatas")]
public class ObjectDatas : ScriptableObject//TODO : 플레이어 스텟
{
    [SerializeField] private ObjectDatas data;
    private static ObjectDatas _i;
    public static ObjectDatas I 
    { 
        get 
        {
            if (_i == null)
                _i = Resources.Load<ObjectDatas>("ObjectDatas");
            return _i; 
        } 
    }

    [SerializeField] private List<ObjectData> datas = new List<ObjectData>();
    private Dictionary<int, ObjectData> _datas = new();
    public void Init()
    {
        foreach(ObjectData data in datas)
        {
            _datas.Add(data.Name.GetHashCode(), data);
        }
    }

    public ObjectData GetData(string name)
    {
        if (_datas == null)
            Init();

        if(!_datas.TryGetValue(name.GetHashCode(), out var data))
        {
            Debug.Log($"Faile Load Object Data : {name}");
        }

        return data;
    }
}

[System.Serializable]
public class ObjectData
{
    public string Name;
    public CharacterBaseStat stat;
    public GameObject prefab;
}
