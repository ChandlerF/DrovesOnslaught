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

        if (!Directory.Exists(Application.persistentDataPath + "/saves"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/saves");
        }

        string path = Application.persistentDataPath + "/saves.Save";
        FileStream stream = File.Create(path);

        SaveData data = new SaveData(manager);
        
        formatter.Serialize(stream, data);

        Debug.Log("Saveddd");
        
        stream.Close();
    }

    public static SaveData Load()
    {
        string path = Application.persistentDataPath + "/saves.Save";
        if (!File.Exists(path))
        {
            Debug.LogError("Save not found at: " + path);
            return null;
        }

        BinaryFormatter formatter = new BinaryFormatter();

        FileStream stream = new FileStream(path, FileMode.Open);
        
        SaveData data = formatter.Deserialize(stream) as SaveData;
        stream.Close();

        Debug.Log("Loadeddd");

        return data;
        
        
        
        
        /*
        try
        {
            object save = formatter.Deserialize(file);
            file.Close();
            Debug.Log("Loadeddd");
            return save;
        }
        catch
        {
            Debug.LogErrorFormat("Failed to load file at {0}", path);
            file.Close();
            return null;
        }
        */
    }

    public static BinaryFormatter GetBinaryFormatter()
    {
        BinaryFormatter formatter = new BinaryFormatter();

        return formatter;
    }
}




// var = condition ? trueCase : falseCase
