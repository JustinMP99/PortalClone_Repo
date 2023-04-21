using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//using UnityEngine.SceneManagement;

public abstract class Manager : MonoBehaviour
{

    [SerializeField]
    protected Camera MainCamera;
    [SerializeField]
    protected GameObject UIManager;
    [SerializeField]
    protected GameObject levelLoadingManager;
    //[SerializeField]
    public StartScreenUIManager UIManagerScript;
    [SerializeField]
    protected GameObject GameSettingsOBJ;
    [SerializeField]
    protected GameSettingsScript GameSettingsScriptOBJ;
    [SerializeField]
    protected GameObject PlayerDataOBJ;
    [SerializeField]
    protected PlayerDataScript PlayerDataScriptObj;




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    #region Loading New Scene

    public virtual void LoadSelectedlevelAsync(Levels Level)
    {
      

    }

    #endregion



    #region SaveData Functions


    //Sets The PlayerDataObj With Basic Data For The Creation Of A New Game Save File
    public void SetNewGamePlayerData()
    {

        //PlayerDataObj = Instantiate(PlayerDataPrefab);

        PlayerDataOBJ.GetComponent<PlayerData>().LastLevel = Levels.LEVEL_01;
        PlayerDataOBJ.GetComponent<PlayerData>().LevelText = "Level 1";
        PlayerDataOBJ.GetComponent<PlayerData>().LevelOneCompleted = false;
        PlayerDataOBJ.GetComponent<PlayerData>().LevelTwoCompleted = false;
        PlayerDataOBJ.GetComponent<PlayerData>().LevelThreeCompleted = false;
        PlayerDataOBJ.GetComponent<PlayerData>().LevelFourCompleted = false;
        PlayerDataOBJ.GetComponent<PlayerData>().LevelFiveCompleted = false;

    }


    public void CopyPlayerDataScriptToPlayerData(PlayerData playerData, PlayerDataScript playerDataScript)
    {


        playerData.LastLevel = playerDataScript.LastLevel;
        playerData.LevelText = playerDataScript.LevelText;
        playerData.LevelOneCompleted = playerDataScript.LevelOneCompleted;
        playerData.LevelTwoCompleted = playerDataScript.LevelTwoCompleted;
        playerData.LevelThreeCompleted = playerDataScript.LevelOneCompleted;
        playerData.LevelFourCompleted = playerDataScript.LevelFourCompleted;
    }


    //Load Into The First Level
    public void NewGameFunction()
    {
        //create New Game player data
        SetNewGamePlayerData();
        //Load First Level 
        LoadSelectedlevelAsync(Levels.LEVEL_01);
    }


    public void LoadGameFunction(int SaveToLoad)
    {

        //Read Save Data From Save File
        PlayerDataScriptObj = Save_LoadScript.LoadGame(SaveToLoad);
        //Copy That Data To The PlayerDataObj
        CopyPlayerDataScriptToPlayerData(PlayerDataOBJ.GetComponent<PlayerData>(), PlayerDataScriptObj);
        //Load the correct Scene
        LoadSelectedlevelAsync(PlayerDataOBJ.GetComponent<PlayerData>().LastLevel);

    }


    #endregion

    #region GameData Functions


    //Copy GameSettingsScript Object to GameSettings
    public void CopyGameSettingsScriptToGameSettings(GameObject gameSettings, GameSettingsScript gameSettingsScript)
    {

        gameSettings.GetComponent<GameSettings>().SetFOV(gameSettingsScript.FOV);
        gameSettings.GetComponent<GameSettings>().SetXSensitivity(gameSettingsScript.X_Sensitivity);
        gameSettings.GetComponent<GameSettings>().SetYSensitivity(gameSettingsScript.Y_Sensitivity);
        gameSettings.GetComponent<GameSettings>().SetViewBobState(gameSettingsScript.ViewBobState);
        gameSettings.GetComponent<GameSettings>().SetMotionBlurState(gameSettingsScript.MotionBlurState);

    }

    //Sets GameSettingsOBJ variables
    public void SetNewSettings()
    {
        //Sets The Variables In The GameSetting
        //Set FOV
        GameSettingsOBJ.GetComponent<GameSettings>().SetFOV((int)UIManagerScript.GetFOVValue());
        //Get X Sensitivity
        GameSettingsOBJ.GetComponent<GameSettings>().SetXSensitivity(UIManagerScript.GetXSensitivity());
        //Get Y Sensitivity
        GameSettingsOBJ.GetComponent<GameSettings>().SetYSensitivity(UIManagerScript.GetYSensitivity());

        Save_LoadScript.SaveGameSettings(GameSettingsOBJ);

    }



    #endregion

    #region Destroy Objects Functions
    public void DestroyGameSettingsOBJ()
    {
        //This is the only way to destroy it upon returning it to main menu
        Destroy(GameSettingsOBJ);
    }

    public void DestroyLevelLoadingManager()
    {

        Destroy(levelLoadingManager);

    }

    #endregion



}
