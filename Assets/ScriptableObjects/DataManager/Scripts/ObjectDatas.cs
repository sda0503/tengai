using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ObjectDatas", menuName = "Data/Datas/ObjectDatas")]
public class ObjectDatas : ScriptableObject
{
    private static ObjectDatas _i;
    public static ObjectDatas I 
    { 
        get 
        {
            if (_i == null)
            {
                _i = Resources.Load<ObjectDatas>("ObjectDatas");
            }
            return _i; 
        } 
    }

    [SerializeField] private List<ObjectData> datas = new List<ObjectData>();
    private Dictionary<int, ObjectData> _datas = new();

    private bool _isDatas = false;

    public bool HasData()
    {
        return _isDatas;
    }

    public void Init()
    {
        Debug.Log("hi");
        foreach(ObjectData data in datas)
        {
            _datas.Add(data.Name.GetHashCode(), data);
        }
        _isDatas = true;
    }

    public ObjectData GetData(string name)
    {
        if (_datas.Count == 0)
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
