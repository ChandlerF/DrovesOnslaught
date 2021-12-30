using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class ChaptersManager : MonoBehaviour
{

    public List<GameObject> Levels = new List<GameObject>();


    public int ActiveChapter = 0;



    public void SetStars(bool forward)
    {
        DOTween.KillAll();
        int x;

        if (forward)
        {
            ActiveChapter++;
            x = 10;
        }
        else
        {
            ActiveChapter--;
            x = -10;
        }

        for (int i = 0; i < Levels.Count; i++)
        {
            Levels[i].GetComponent<LevelButton>().LevelNumber += x;
            Levels[i].GetComponent<LevelButton>().SetStars();
        }
    }
}
