using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    //Identical ro dictionary (uses index instead of key)
    public List<List<bool>> Stars = new List<List<bool>>();


    private void Awake()
    {
        instance = this;

        DontDestroyOnLoad(this.gameObject);



        //Have to make it a proper singleton, reference camera
        //Have to initialize the lists Stars
        //Make UI look pretty for Menu and Level Select
        //have tab system or something to access other 'chapters' of levels
        //look into saving stats (maybe round of current level)
    }
}
