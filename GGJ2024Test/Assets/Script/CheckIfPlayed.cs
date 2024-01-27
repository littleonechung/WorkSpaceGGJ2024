using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckIfPlayed : MonoBehaviour
{
    private bool PlayedBefore = false;
    public void Awake()
    {
        this.gameObject.SetActive(PlayedBefore);
    }
    public void IsPlayed()
    {
        PlayedBefore = true;
    }
}
