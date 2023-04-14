using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public static class Save_LoadScript
{
    #region PlayerSaveData

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
            PlayerDataScript TempPlayerData = null;
            return TempPlayerData;
        }


        //return null;

    }

    public static void SaveGameData(GameObject Player, int SaveFile)
    {

        //create a temp PlayerDataScript
        PlayerDataScript TempPlayerData = new PlayerDataScript(Player);
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

        //Write to Binary file

        //create Formatter
        BinaryFormatter Formatter = new BinaryFormatter();
        //create Stream
        FileStream fileStream = new FileStream(path, FileMode.Open);
        //serialize file
        Formatter.Serialize(fileStream, TempPlayerData);
        //close Stream
        fileStream.Close();
    }
    #endregion

    #region GameSettingsSaveData

    //Function For Loading Game Settings
    public static GameSettingsScript LoadGameSettings()
    {
        //Create a path and set it to temp data
        string path = "C:/Users/justi/Downloads/SideProjects/Unity Projects/FPSProject/PortalCloneRepo/PortalClone_Repo/FPS Test/Assets/GameData" + "/GameSettingsFile.Data";
        //Check if the path exists
        if (File.Exists(path))
        {
            //set up a formatter
            BinaryFormatter Formatter = new BinaryFormatter();
            //set up stream
            FileStream fileStream = new FileStream(path, FileMode.Open);

            GameSettingsScript CurrentGameSettings = Formatter.Deserialize(fileStream) as GameSettingsScript;
            //close fileStream
            fileStream.Close();

            ///TEMP///
            return CurrentGameSettings;
        }
        return null;
    }


    //Function For Saving Game Settings
    public static void SaveGameSettings(GameObject gameSettings)
    {
        //Create A Temp GameSettingsScript Obj
        GameSettingsScript TempGameSettings = new GameSettingsScript(gameSettings);
        //Set The Path For The Game Settings Save File
        string path = "C:/Users/justi/Downloads/SideProjects/Unity Projects/FPSProject/PortalCloneRepo/PortalClone_Repo/FPS Test/Assets/GameData" + "/GameSettingsFile.Data";

        BinaryFormatter Formatter = new BinaryFormatter();
        //create Stream
        FileStream fileStream = new FileStream(path, FileMode.Create);
        //serialize file
        Formatter.Serialize(fileStream, TempGameSettings);
        //close Stream
        fileStream.Close();


    }

    #endregion


}
