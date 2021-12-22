using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaptersManager : MonoBehaviour
{

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
            Levels[i].GetComponent<LevelButton>().LevelNumber += x;
            Levels[i].GetComponent<LevelButton>().SetStars();
        }
    }
}
