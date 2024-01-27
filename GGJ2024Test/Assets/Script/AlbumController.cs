using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlbumController : MonoBehaviour
{
    [SerializeField] Transform godContainer;

    [SerializeField] GameObject[] godPrefabs;

    [SerializeField] int pageCount;
    [SerializeField] int currentIndex = 0;

    [SerializeField] GameObject PrevBtn;
    [SerializeField] GameObject NextBtn;


    private void Start()
    {
        AlbumPrefab[] albumPrefabs = godContainer.GetComponentsInChildren<AlbumPrefab>(true);

        pageCount = albumPrefabs.Length;
        godPrefabs = new GameObject[pageCount];
        for (int i = 0; i < pageCount; i++)
        {
            godPrefabs[i] = albumPrefabs[i].gameObject;
        }

        ShowPage(currentIndex);
    }

    void ShowPage(int index)
    {
        for (int i = 0; i < pageCount; i++)
        {
            godPrefabs[i].SetActive(i == index);
        }

        PrevBtn.SetActive(index > 0);
        NextBtn.SetActive(index < pageCount);
    }

    public void NextBtnPressed()
    {
        if (currentIndex < pageCount-1)
        {
            currentIndex++;
        }
        else
        {
            currentIndex = pageCount-1;
        }
        ShowPage(currentIndex);
    }

    public void PrevBtnPressed()
    {
        if (currentIndex > 0)
        {
            currentIndex--;
        }
        else
        {
            currentIndex = 0;
        }
        ShowPage(currentIndex);
    }

}
