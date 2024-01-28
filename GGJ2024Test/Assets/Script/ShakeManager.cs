using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using DG.Tweening;

public class ShakeManager : MonoBehaviour
{
    public static ShakeManager Instance = null;
    private List<RectTransform> allTrans = new List<RectTransform>();   

    // Start is called before the first frame update
    private void Start()
    {
        Instance = this;
        for (int i = 0; i < transform.childCount; i++)
        {
            allTrans.Add(transform.GetChild(i).GetComponent<RectTransform>());
        }
    }

    public void Shake()
    {
        foreach(var t in allTrans)
        {
            if (t != null)
            {
                t.DOAnchorPosY(50, 0.1f).SetLoops(8, LoopType.Yoyo);
            }

        }
    }
}
