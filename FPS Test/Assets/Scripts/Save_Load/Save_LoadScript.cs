using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public static class Save_LoadScript
{
  
    public static PlayerDataScript LoadGame(int SaveFile)
    {

        //Create a path and set it to temp data
        string path = Application.persistentDataPath + "/Player.Data";
        //Depending on the save file change which path the string will contain
        switch (SaveFile)
        {
            case 0:
                //set path to save file one
                path = Application.persistentDataPath + "/PlayerSaveOne.Data";

                break;
            case 1:
                //set path to save file two
                path = Application.persistentDataPath + "/PlayerSaveTwo.Data";
                break;
            case 2:
                //set path to save file three
                path = Application.persistentDataPath + "/PlayerSaveThree.Data";
                break;
            default:
                break;
        }

        //Check if the path exists
        if (File.Exists(path))
        {
            //set up a formatter
            BinaryFormatter Formatter = new BinaryFormatter();
            //set up stream
            FileStream fileStream = new FileStream(path, FileMode.Open);

            PlayerDataScript playerData = Formatter.Deserialize(fileStream) as PlayerDataScript;
            //close fileStream
            fileStream.Close();

            ///TEMP///
            return playerData;
        }
        return null;
    }

    public static PlayerDataScript ReadSaveData(int SaveFile)
    {
        //Create a path and set it to temp data
        string path = Application.persistentDataPath + "/Player.Data";
        //Depending on the save file change which path the string will contain
        switch (SaveFile)
        {
            case 0:
                //set path to save file one
                path = Application.persistentDataPath + "/PlayerSaveOne.Data";

                break;
            case 1:
                //set path to save file two
                path = Application.persistentDataPath + "/PlayerSaveTwo.Data";
                break;
            case 2:
                //set path to save file three
                path = Application.persistentDataPath + "/PlayerSaveThree.Data";
                break;
            default:
                break;
        }

        if (File.Exists(path))
        {
            Debug.Log("File path Exists!!!");
            //set up a formatter
            BinaryFormatter Formatter = new BinaryFormatter();
            //set up stream
            FileStream fileStream = new FileStream(path, FileMode.Open);

            PlayerDataScript playerData = Formatter.Deserialize(fileStream) as PlayerDataScript;
            //close fileStream
            fileStream.Close();

            ///TEMP///
            return playerData;
        
        }
        else
        {
            Debug.Log("File Path doesnt exist :(");
            PlayerDataScript playerData = null;
            return null;
        }
        
      
        //return null;

    }



}
