using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;


[System.Serializable]
public static class SerializationManager
{
    public static void Save(LevelManager manager)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        if (!Directory.Exists(Application.persistentDataPath + "/Saves"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/Saves");
        }

        string path = Application.persistentDataPath + "/Saves/save1.Save";
        FileStream stream = File.Create(path);

        SaveData data = new SaveData(manager);
        
        formatter.Serialize(stream, data);

        Debug.Log("Saveddd");
        
        stream.Close();
    }

    public static SaveData Load()
    {
        string path = Application.persistentDataPath + "/Saves/save1.Save";
        if (!File.Exists(path))
        {
            Debug.LogError("Save not found at: " + path);
            return null;
        }


        BinaryFormatter formatter = new BinaryFormatter();

        FileStream stream = new FileStream(path, FileMode.Open);

        try
        {
            SaveData data = formatter.Deserialize(stream) as SaveData;
            stream.Close();

            Debug.Log("Loadeddd");
            Debug.Log(path);
            return data;
        }
        catch
        {

            Debug.LogErrorFormat("Failed to load file at {0}", path);
            stream.Close();
            return null;
        }
    }


    /*
    public static void DeleteSave()
    {
        string path = Application.persistentDataPath + "/saves.Save";

        FileStream stream = new FileStream(path, FileMode.Open);


        if (File.Exists(path))
        {
            File.Delete(path);

            stream.Close();
            Debug.Log("Deleted Save");
        }

        else
        {
            Debug.LogError("Save not found at: " + path);

            stream.Close();
        }
    }*/
}




// var = condition ? trueCase : falseCase