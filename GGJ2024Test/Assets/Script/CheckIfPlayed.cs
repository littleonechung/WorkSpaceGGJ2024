using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckIfPlayed : MonoBehaviour
{
    public bool PlayedBefore = false;
    public void Check()
    {
        gameObject.SetActive(PlayedBefore);
    }
    public void IsPlayed()
    {
        PlayedBefore = true;
    }


}
