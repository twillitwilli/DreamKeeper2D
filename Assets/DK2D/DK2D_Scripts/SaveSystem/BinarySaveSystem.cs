using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class BinarySaveSystem
{
    public static void SaveData(BinarySaveData saveData)
    {
        // Create a new binary formatter
        BinaryFormatter formatter = new BinaryFormatter();

        // Get the name of the path, the file name
        string fileName = Application.persistentDataPath + "/" + "PlayerBinarySaveData";

        // Create a filestream, this allows you to read and write from a file, you pass in the filename and
        // either open the file if it exists or create the file if it doesnt exist
        FileStream filestream = new FileStream(fileName, FileMode.OpenOrCreate);

        // Now the formatter can be ran to serialize the data by passing in the filestream, then the data being saved
        formatter.Serialize(filestream, saveData);

        // Finally after the formatter is done, it is always important to then close the filestream
        filestream.Close();
    }

    public static BinarySaveData LoadData()
    {
        // When loading a file the first thing you must always do, is check to make sure the file exists
        string fileName = Application.persistentDataPath + "/" + "PlayerBinarySaveData";

        if (File.Exists(fileName))
        {
            // If the file exists, then you can start by creating a new formatter
            BinaryFormatter formatter = new BinaryFormatter();

            // Then create a file stream for opening the the file by passing in the file name
            FileStream fileStream = new FileStream(fileName, FileMode.Open);

            // Now I can deserialize the data so it is readable again
            BinarySaveData loadedData = formatter.Deserialize(fileStream) as BinarySaveData;

            // Same as before it is always important to close the file stream after you are done using it
            fileStream.Close();

            // Finally I can just return loaded data
            return loadedData;
        }

        else return null; // File Not Found
    }

    public static void DeleteFileSave()
    {
        string fileName = Application.persistentDataPath + "/" + "PlayerBinarySaveData";

        if (File.Exists(fileName))
            File.Delete(fileName);

        else { Debug.Log("No File Found"); }
    }

    public static bool SaveFileCheck()
    {
        string fileName = Application.persistentDataPath + "/" + "PlayerBinarySaveData";

        if (File.Exists(fileName))
            return true;

        return false;
    }
}