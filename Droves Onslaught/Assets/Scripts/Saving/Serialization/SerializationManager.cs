using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;


[System.Serializable]
public static class SerializationManager
{
    public static void Save(SaveData saveData)
    {
        BinaryFormatter formatter = new BinaryFormatter;

        if (!Directory.Exists(Application.persistentDataPath + "/saves"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/saves");
        }

        string path = Application.persistentDataPath + "/saves/" + "Save" + ".save";
        FileStream stream = File.Create(path);

        x data = new x(saveData);
        
        formatter.Serialize(file, saveData);

        file.Close();
        Debug.Log("Saveddd");
        return true;
    }

    public static object Load(string path)
    {
        if (!File.Exists(path))
        {
            return null;
        }

        BinaryFormatter formatter = GetBinaryFormatter();

        FileStream file = File.Open(path, FileMode.Open);


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
    }

    public static BinaryFormatter GetBinaryFormatter()
    {
        BinaryFormatter formatter = new BinaryFormatter();

        return formatter;
    }
}
