using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.UI;

public class GodManager : MonoBehaviour
{
    public Image godHpFill;
    public Image godImage;
    public Sprite[] godSprite;

    private int godHp;
    private string godId;

    public void Setup(string _godId)
    {
        godId = _godId;

    }

    public void OperateHp(int _value)
    {
        godHp += godHp;
        godHpFill.fillAmount = godHp;
    }

    private void ChangeGodImage()
    {
       
    }
}
