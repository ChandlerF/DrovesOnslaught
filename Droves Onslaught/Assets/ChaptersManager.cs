using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaptersManager : MonoBehaviour
{
    //         Chapter Number  -  List of Levels
    //public Dictionary<int, List<GameObject>> Chapters = new Dictionary<int, List<GameObject>>();



    public List<GameObject> Levels = new List<GameObject>();

    //Need to save and load -----------------------------------------------
    public int ActiveChapter = 0;



    public void SetStars(bool forward)
    {
        int x;

        if (forward)
        {
            x = 10;
        }
        else
        {
            x = -10;
        }

        for (int i = 0; i < Levels.Count; i++)
        {
            //LevelButton Script = Levels[i].GetComponent<LevelButton>();
            
            Levels[i].GetComponent<LevelButton>().LevelNumber += x;
            Levels[i].GetComponent<LevelButton>().SetStars();

            Debug.Log(Levels[i].GetComponent<LevelButton>().LevelNumber);
        }
    }
























   /* public void Awake()
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

    }*/
}
