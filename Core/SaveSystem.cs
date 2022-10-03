using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem
{
    //private static int _slot;

    static string _peristent = Application.persistentDataPath;
    static DirectoryInfo dirInf = new DirectoryInfo(_peristent + "/saves");

    private static string SetPath(int saveSlot)
    {
        string _path = _peristent + "/saves/slot_" + saveSlot + ".data";
        return _path;
    }

    public static void CreateSaveFolder()
    {
        if (!dirInf.Exists)
        {
            Debug.Log("Creating subdirectory");
            dirInf.Create();
        }
    }

    public static void SaveData(PlayerData playerData, int saveSlot)
    {
        if (!dirInf.Exists)
        {
            //Make sure directory exists first;
            CreateSaveFolder();
        }

        if (dirInf.Exists)
        {
            string _path = SetPath(saveSlot);

            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(_path, FileMode.Create);

            SaveData data = new SaveData(playerData);

            formatter.Serialize(stream, data);
            stream.Close();
        }
    }

    public static SaveData LoadData(int saveSlot)
    {
        string _path = SetPath(saveSlot);

        if (File.Exists(_path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(_path, FileMode.Open);

            SaveData data = formatter.Deserialize(stream) as SaveData;
            stream.Close();

            return data;

        }
        else
        {
            // Debug.LogError("Save file not find in " + _path);
            return null;
        }
    }

    public static void DeleteData(int saveSlot)
    {
        string _path = SetPath(saveSlot);

        if (File.Exists(_path))
        {
            File.Delete(_path);
        }
        else
        {
            //Debug.LogError("Save file not find in " + _path);
            return;
        }
    }
}