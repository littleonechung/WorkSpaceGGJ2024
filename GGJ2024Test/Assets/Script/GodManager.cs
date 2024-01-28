using System;
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

    public Dictionary<GodName, float> godHPDic = new Dictionary<GodName, float>();
    private float godHp;
    private GodName godName;
    private GameObject GodGO = null;
    private GodUICtrl currentGodUICtrl;

    public void Init()
    {
        godHPDic.Clear();
        
        GodName[] enumValues = (GodName[])Enum.GetValues(typeof(GodName));
        foreach (GodName godName in enumValues)
        {
            if (godName != GodName.Default)
            {
                godHPDic[godName] = 0;
            }
        }
    }

    public bool GetResult()
    {
        float total = 0;
        foreach (int value in godHPDic.Values)
        {
            total += value;
        }

        return total / godHPDic.Values.Count >= 50;
    }

    public void Setup(GodName _name)
    {
        godName = _name;
        InstantiateGodGO(godName);
        godHPDic[godName] = 0;
        godHpFill.fillAmount = godHPDic[godName]/100;
    }

    public void ShowResponse(Answer answer)
    {
        if(currentGodUICtrl != null)
        {
            currentGodUICtrl.AngrySkin.SetActive(!answer.IsCorrect);
            currentGodUICtrl.AngryFeedback.SetActive(!answer.IsCorrect);
            currentGodUICtrl.HappyFeedback.SetActive(answer.IsCorrect);
            currentGodUICtrl.HappySkin.SetActive(answer.IsCorrect);
            if (!answer.IsCorrect)
                ShowAngryAnime();
            if (!string.IsNullOrEmpty(answer.Response))
            {
                currentGodUICtrl.DialogBubble.SetActive(true);
                currentGodUICtrl.DialogText.text = answer.Response;
            }
            if (answer.IsCorrect)
                SoundManager.Instance.PlaySound(AudioName.correct);
            else
                SoundManager.Instance.PlaySound(AudioName.incorrect);
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
        godHPDic[godName] += _value;
        if(godHPDic[godName] < 0)
            godHPDic[godName] = 0;
        godHpFill.DOFillAmount(godHPDic[godName] / 100, 0.5f);
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

    private void ShowAngryAnime()
    {
        RectTransform angryTrans = currentGodUICtrl.AngryFeedback.GetComponent<RectTransform>();
        if (angryTrans != null)
        {
            angryTrans.DOScale(4f, 0.15f).SetLoops(2,LoopType.Yoyo).SetEase(Ease.InOutBack);
        }
        ShakeManager.Instance.Shake();
    }
}
