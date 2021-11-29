using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    private static SaveData _current;
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
    }

    public LevelManager LvlManager = LevelManager.instance;
    public int TotalStarsEarned;
    
    //Dictionary from level manager of what's unlocked, the index of list is key of dict
    public List<bool> Unlocked = new List<bool>();


}

//Save = SerializationManager.Save(saveData.current);
//Load = SaveData.current = (SaveData)SerializationManager.Load(Application.persistentDataPath + "/saves/Save.save");
//SaveData.current.LvlManager.whatever +== x;