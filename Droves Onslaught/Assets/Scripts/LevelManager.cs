using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;


    public int TotalLevels;

    public int TotalChapters;

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



        InitializeStars(TotalLevels);

        InitializeUnlocked(BuildingsNames.Count);




        if (FileAlreadyCreated())
        {
            instance.LoadPlayer();
        }
        else
        {
            instance.SavePlayer();
        }
    }


    private void InitializeStars(int amount)
    {
        for (int i = 1; i < amount + 1; i++)
        {
            //NullList needs to be in for() loop or else every Level script shares the same stars... weird
            List<bool> NullList = new List<bool>();
            NullList.Add(false);
            NullList.Add(false);
            NullList.Add(false);

            Stars.Add(i, NullList);
        }
    }


    private void InitializeUnlocked(int amount)
    {

        for (int i = 0; i < amount; i++)
        {
            BuildingsUnlocked.Add(BuildingsNames[i], false);
        }
    }






    //Set stars earned in list
    public void StarsEarned(int x)
    {
        //If the bool (in dict) is false
        if (!Stars[CurrentLevel][x])
        {
            TotalStarsEarned += 1;
            
            //Dynamically Adds XP for each star, more XP higher the level
            AddXP((20 * (x + 1)) * (CurrentLevel / 8));

            Stars[CurrentLevel][x] = true;
            Debug.Log(Stars[CurrentLevel]);
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






        //get difference in scene vs save, if there is any, update the save data        //Not sure if necessary, i think it is, but I can't tell
        int UnlockedDifference = BuildingsUnlocked.Count - data.Unlocked.Count;

        BuildingsUnlocked = data.Unlocked;

        InitializeUnlocked(UnlockedDifference);


        int StarsDifference = Stars.Count - data.LevelsStars.Count;

        Stars = data.LevelsStars;

        InitializeStars(StarsDifference);






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
        PlayerXp += xp;
        
        while(PlayerXp > XpToLvlUp)
        {
            PlayerXp -= XpToLvlUp;
            PlayerLvl += 1;
            XpToLvlUp *= 2;
        }
    }
}
