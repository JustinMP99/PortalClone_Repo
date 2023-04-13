using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public abstract class Manager : MonoBehaviour
{

    [SerializeField]
    protected Camera MainCamera;
    [SerializeField]
    protected GameObject UIManager;
    [SerializeField]
    protected StartScreenUIManager UIManagerScript;
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

    public void LoadSelectedlevelAsync(Levels Level)
    {
        //Disable StartMenu UI
        UIManagerScript.SetStartMenuState(false);
        //Enable LoadingScreen UI
        UIManagerScript.SetLoadingScreenUIState(true);
        //Async load level
        StartCoroutine(AsyncLoadLevel(Level));

    }

    IEnumerator AsyncLoadLevel(Levels Level)
    {
        //Set The Level Index Based On The Passed In Level
        int LevelIndex = 0;
        switch (Level)
        {
            case Levels.LEVEL_00:
                LevelIndex = 0;
                break;
            case Levels.LEVEL_01:
                LevelIndex = 1;
                break;
            case Levels.LEVEL_02:
                LevelIndex = 2;
                break;
            case Levels.LEVEL_03:
                LevelIndex = 3;
                break;
            case Levels.LEVEL_05:
                LevelIndex = 4;
                break;
            case Levels.LEVEL_06:
                LevelIndex = 5;
                break;
            default:
                break;
        }

        //Set The AsyncOperation To Load The Scene Which Is Determined By The Level Index
        AsyncOperation LoadOperation = SceneManager.LoadSceneAsync(LevelIndex);
        //While The Load Operation Is Not Done, Update Progress Bar
        while (!LoadOperation.isDone)
        {
            float Progress = Mathf.Clamp01(LoadOperation.progress / 0.9f);
            UIManagerScript.SetLoadingSliderValue(Progress);
            yield return null;
        }
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



    }



    #endregion





}
