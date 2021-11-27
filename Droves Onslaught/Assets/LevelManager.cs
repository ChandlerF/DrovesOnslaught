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
    public List<List<bool>> Stars = new List<List<bool>>();


    private void Awake()
    {
        instance = this;

        DontDestroyOnLoad(this.gameObject);

        //If there's no save data:
        for(int i = 0; i < TotalLevels; i++)
        {
            Stars[i] = new List<bool>();

            for (int y = 0; y < 3; y++)
            {
                Stars[i][y] = false;
            }
        }
        //Else: Load Save Data








         void StarsEarned(int x)
        {
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
        }











        //Have to make it a proper singleton, reference camera
        //Have to initialize the lists Stars
        //Make UI look pretty for Menu and Level Select
        //have tab system or something to access other 'chapters' of levels
        //look into saving stats (maybe round of current level)
    }
}
