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
    protected GameObject levelLoadingManager;
    
    public StartScreenUIManager UIManagerScript;
    [SerializeField] protected GameObject GameSettingsOBJ;
    [SerializeField] protected GameSettingsScript GameSettingsScriptOBJ;
    [SerializeField] protected GameObject PlayerDataOBJ;
    [SerializeField] protected PlayerDataScript PlayerDataScriptObj;

    [SerializeField] protected SceneLoadingManager _baseSceneManager;

    [SerializeField] protected List<SceneID> ScenesToLoad;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetBaseSceneManager(SceneLoadingManager baseSceneManager)
    {
        _baseSceneManager = baseSceneManager;
    }


    /// <summary>
    /// This Function Will Be Called After The Base Scene Manager Has Finished Loading The Scenes
    /// </summary>
    public virtual void Startup()
    {
       



    }


    #region Pause Menu Functions

    public virtual void PauseGame()
    {


    }

    public virtual void ResumeGame()
    {



    }


    #endregion

    #region Settings Functions

    public virtual void OpenSettings()
    {

    }


    public virtual void CloseSettings()
    {


    }


    #endregion

    #region Loading New Scene

    public virtual void LoadSelectedlevelAsync(Levels Level)
    {
      

    }



    public IEnumerator AsyncLoadLevel(Levels Level)
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
            UIManager.GetComponent<UIManager>().SetLoadingSliderValue(Progress);
            yield return null;
        }

    }
    #endregion

    #region SaveData Functions


    //Sets The PlayerDataObj With Basic Data For The Creation Of A New Game Save File



    public void CopyPlayerDataScriptToPlayerData(PlayerData playerData, PlayerDataScript playerDataScript)
    {


        playerData.LastLevel = playerDataScript.LastLevel;
        playerData.LevelText = playerDataScript.LevelText;
        playerData.LevelOneCompleted = playerDataScript.LevelOneCompleted;
        playerData.LevelTwoCompleted = playerDataScript.LevelTwoCompleted;
        playerData.LevelThreeCompleted = playerDataScript.LevelOneCompleted;
        playerData.LevelFourCompleted = playerDataScript.LevelFourCompleted;
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
    public virtual void SetNewSettings()
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
