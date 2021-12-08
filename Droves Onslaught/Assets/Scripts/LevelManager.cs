using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;


    public int TotalLevels;

    public int TotalStarsEarned = 0;

    public int CurrentLevel;


    public Dictionary<int, List<bool>> Stars = new Dictionary<int, List<bool>>();

    public Dictionary<string, bool> BuildingsUnlocked = new Dictionary<string, bool>();
    [SerializeField] List<string> BuildingsNames = new List<string>();
    
    public int PlayerLvl = 0;
    public int PlayerXp = 0;
    public int XpToLvlUp = 100;




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




        if (FileAlreadyCreated())
        {
            instance.LoadPlayer();
        }
        else
        {
            instance.SavePlayer();
        }
    }


    //Set stars earned in list
    public void StarsEarned(int x)
    {
        //If the bool (in dict) is false
        if (!Stars[CurrentLevel][x])
        {
            TotalStarsEarned += 1;

            Stars[CurrentLevel][x] = true;
        }
    }





    public void SavePlayer()
    {
        SerializationManager.Save(this);


        Debug.Log("Save");
    }

    public void LoadPlayer()
    {
        SaveData data = SerializationManager.Load();

        TotalStarsEarned = data.TotalStarsEarned;

        BuildingsUnlocked = data.Unlocked;
        Stars = data.LevelsStars;
        PlayerLvl = data.PlayerLvl;
        PlayerXp = data.PlayerXp;
        XpToLvlUp = data.XpToLvlUp;


        Debug.Log("Load");
    }


    private bool FileAlreadyCreated()
    {
        if(SerializationManager.Load() != null)
        {
            return true;
        }

        return false;
    }
    
    
    
    
    
    
    public void AddXP(int xp)
    {
        //PlayerXp += xp;
        
        //while(PlayerXp > XpToLvlUp)
        //PlayerXp -= XpToLvlUp;
        //PlayerLvl += 1;
        //XpToLvlUp *= 1.5;
    }
}
