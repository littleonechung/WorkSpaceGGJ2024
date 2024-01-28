using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultPanel : MonoBehaviour
{
    [Serializable]
    public struct GodValue
    {
        public GodName name;
        public Image hpImg;
    }

    [SerializeField]
    List<GodValue> GodValueList = new List<GodValue>();

    public void ChangeHP(GodName name, float hp)
    {
        foreach (GodValue godValue in GodValueList)
        {
            if (godValue.name == name)
            {
                godValue.hpImg.fillAmount = hp / 100;
            }
        }
    }

}
