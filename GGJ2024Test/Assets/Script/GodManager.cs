using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using Unity.VisualScripting;
using DG.Tweening;

public class GodManager : MonoBehaviour
{
    public Image godHpFill;
    public Transform GodRoot;
    
    [SerializeField] private GodData data;

    private float godHp;
    private GodName godName;
    private GameObject GodGO = null;
    private GodUICtrl currentGodUICtrl;

    public void Setup(GodName _name , int defaultHp = 30)
    {
        godName = _name;
        InstantiateGodGO(godName);
        godHp = defaultHp;
        godHpFill.fillAmount = godHp/100;
    }

    public void ShowResponse(Answer answer)
    {
        if(currentGodUICtrl != null)
        {
            currentGodUICtrl.AngrySkin.SetActive(!answer.IsCorrect);
            currentGodUICtrl.AngryFeedback.SetActive(!answer.IsCorrect);
            currentGodUICtrl.HappyFeedback.SetActive(answer.IsCorrect);
            currentGodUICtrl.HappySkin.SetActive(answer.IsCorrect);
            currentGodUICtrl.DialogBubble.SetActive(true);
            currentGodUICtrl.DialogText.text = answer.Response;
        }
        OperateHp(answer.Score);
    }

    public void ResetGodUI()
    {
        if (currentGodUICtrl != null)
        {
            currentGodUICtrl.AngryFeedback.SetActive(false);
            currentGodUICtrl.HappyFeedback.SetActive(false);
            currentGodUICtrl.DialogBubble.SetActive(false);
        }
    }

    public void OperateHp(int _value)
    {
        godHp += _value;
        if(godHp < 0)
            godHp = 0;
        godHpFill.DOFillAmount(godHp / 100, 0.5f);
    }

    private void InstantiateGodGO(GodName name)
    {
        if(GodGO != null) 
        {
            Destroy(GodGO); 
            GodGO = null;
        }
        GodDataSet dataSet = data.GodDataAry.Find(s => s.GodName == name);
        if (dataSet != null) 
        {
            GodGO = Instantiate(dataSet.GodPrefab, GodRoot);
            currentGodUICtrl = GodGO.GetComponent<GodUICtrl>();
        }
    }
}
