using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaptersManager : MonoBehaviour
{
    //         Chapter Number  -  List of Levels
    public Dictionary<int, List<GameObject>> Chapters = new Dictionary<int, List<GameObject>>();
    
    //Need to save -----------------------------------------------
    public int ActiveChapter = 0;


    public void Awake()
    {
        for(int i = 0; i < LevelManager.instance.TotalChapters; i++)
        {
            List<GameObject> Levels = new List<GameObject>();
            for (int z = 0; z < transform.childCount; z++)
            {
                int index = z + (i * 10);
                Debug.Log(index);
                Levels.Add(transform.GetChild(index).gameObject);
            }

            Chapters.Add(i, Levels);
        }

        Debug.Log(Chapters[0].Count);
        Debug.Log(Chapters[1].Count);

        for (int i = 0; i < Chapters[0].Count; i++)
        {
            Chapters[1][i].SetActive(true);
            Chapters[0][i].SetActive(false);
        }

    }
}
