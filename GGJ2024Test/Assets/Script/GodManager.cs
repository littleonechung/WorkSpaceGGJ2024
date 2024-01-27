using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class GodManager : MonoBehaviour
{
    public Image godHpFill;
    //public Image godImage;
    public GameObject angryFeedback;
    public GameObject happyFeedback;
    [SerializeField]private GodData data;

    private int godHp;
    private GodName name = GodName.Default;
    private GameObject GodGO = null;
    
    public void Setup(GodName _name)
    {
        name = _name;
        
    }

    public void OperateHp(int _value)
    {
        godHp += godHp;
        godHpFill.fillAmount = godHp;
    }

    private void InstantiateGodGO(GodName name)
    {
        GodDataSet dataSet = data.GodDataAry.Find(s => s.GodName == name);
        if (dataSet != null) 
        {
            
        }
    }
}
