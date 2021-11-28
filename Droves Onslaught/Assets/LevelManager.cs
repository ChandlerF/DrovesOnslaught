using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;


    public int TotalLevels;

    public int TotalStarsEarned = 0;

    public int CurrentLevel;

    //Identical ro dictionary (uses index instead of key)
    public Dictionary<int, List<bool>> Stars = new Dictionary<int, List<bool>>();


    private void Awake()
    {
        //If I'm the only one of myself in scene
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        //If I'm not the original
        else if (instance != this)
        {
                Destroy(gameObject);
        }



        //If there's no save data:              //Need the +1 because istarts at 1 for easy read-ability for dictionary
        for (int i = 1; i < TotalLevels + 1; i++)
        {
            //NullList needs to be in for() loop or else every Level script shares the same stars... weird
            List<bool> NullList = new List<bool>();
            NullList.Add(false);
            NullList.Add(false);
            NullList.Add(false);

            Stars.Add(i, NullList);
        }
        //Else: Load Save Data


        //Have to make it a proper singleton, reference camera
        //Have to initialize the lists Stars
        //Make UI look pretty for Menu and Level Select
        //have tab system or something to access other 'chapters' of levels
        //look into saving stats (maybe round of current level)
    }


    //Set stars earned in list
    public void StarsEarned(int x)
    {
        //Problem
        //If player tries to get 3rd star, x would not always = 3
        //So check if star is already true before setting it

        if (x >= 1)
        {
            Stars[CurrentLevel][0] = true;

            if (x >= 2)
            {
                Stars[CurrentLevel][1] = true;


                if (x >= 3)
                {
                    Stars[CurrentLevel][2] = true;
                }
            }
        }

        TotalStarsEarned += x;
    }
}
