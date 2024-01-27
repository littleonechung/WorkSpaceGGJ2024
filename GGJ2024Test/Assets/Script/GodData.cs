using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum GodName
{
    Default,
    Winnie,
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
