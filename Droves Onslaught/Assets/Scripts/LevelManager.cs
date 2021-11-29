using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;


    public int TotalLevels;

    public int TotalStarsEarned = 0;

    public int CurrentLevel;


    public Dictionary<int, List<bool>> Stars = new Dictionary<int, List<bool>>();

    public Dictionary<string, bool> BuildingsUnlocked = new Dictionary<string, bool>();
    [SerializeField] List<string> BuildingsNames = new List<string>();




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

        
        //SaveData.current = (SaveData)SerializationManager.Load(Application.persistentDataPath + "/saves/Save.save");  //-->-
        //TotalStarsEarned = SaveData.current.LvlManager.TotalStarsEarned;
        //Debug.Log("Load");















        //If there's no save data:              //Need the +1 because istarts at 1 for easy read-ability for dictionary
        //Do the Same for what buildings are unlocked //----
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




        for(int i = 0; i < BuildingsNames.Count; i++)
        {
            BuildingsUnlocked.Add(BuildingsNames[i], false);
        }
    }


    //Set stars earned in list
    public void StarsEarned(int x)
    {
        if (!Stars[CurrentLevel][x])
        {
            //SaveData.current.LvlManager.TotalStarsEarned += 1;    //-->-
            TotalStarsEarned += 1;

            Stars[CurrentLevel][x] = true;
        }
    }
}