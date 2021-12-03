using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    /*private static SaveData _current;
    public static SaveData current
    {
        get
        {
            if(_current == null)
            {
                _current = new SaveData();
            }

            return _current;
        }
        set
        {
            if (value != null)
            {
                _current = value;
            }
        }
    }*/

    public int TotalStarsEarned;

    //Dictionary from level manager of what's unlocked, the index of list is key of dict

    //Dict of upgrades unlocked
    public Dictionary<string, bool> Unlocked = new Dictionary<string, bool>();                           //public List<bool> Unlocked = new List<bool>();      //Use bool[] or int[] if needed

    public Dictionary<int, List<bool>> LevelsStars = new Dictionary<int, List<bool>>();



    public SaveData (LevelManager Manager)
    {
        TotalStarsEarned = Manager.TotalStarsEarned;
        Unlocked = Manager.BuildingsUnlocked;
        LevelsStars = Manager.Stars;
    }

}







//Save = SerializationManager.Save(saveData.current);
//Load = SaveData.current = (SaveData)SerializationManager.Load(Application.persistentDataPath + "/saves/Save.save");
//SaveData.current.LvlManager.whatever +== x;
