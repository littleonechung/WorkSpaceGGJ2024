using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum GodName
{
    Default = 0,
    Tutorial = 1,
    Kindness = 2,
    Loki = 3,
    CatGod = 4,
    SalaryMan = 5,
    Winnie = 6,
    EndGod = 7,
}

public enum GodFeeling
{
    Happy,
    Angry,
}

[System.Serializable]
public class GodDataSet
{
    public GodName GodName;
    public GameObject GodPrefab;
}

[CreateAssetMenu(fileName = "GodData",menuName ="GodData/CreateData",order =1)]
public class GodData : ScriptableObject
{
    public List<GodDataSet> GodDataAry;
}
