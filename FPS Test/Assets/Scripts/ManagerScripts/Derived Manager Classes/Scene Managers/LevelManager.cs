using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LevelManager : Manager
{

    [Header("---GAME OBJECTS---")]
    [SerializeField]
    private GameObject Player;
    [SerializeField]
    private GameObject FPSCamera;
    [SerializeField]
    private GameObject PortalPrefab;
    [SerializeField]
    private GameObject LeftPortal;
    [SerializeField]
    private GameObject RightPortal;

    [SerializeField]
    private Transform LeftPortalPosition;
    [SerializeField]
    private Transform RightPortalPosition;

    [SerializeField]
    private GameObject TestCameraPos;
    [SerializeField]
    private GameObject PlayerDataObj;

    private Quaternion YRot;

    [Header("---MATERIALS---")]

    [SerializeField]
    private Material RightPortalMaterial;
    [SerializeField]
    private Material LeftPortalMaterial;

    [SerializeField]
    private Material LeftViewMat;
    [SerializeField]
    private Material RightViewMat;


    [Header("---RENDER TEXTURES---")]
    [SerializeField]
    private RenderTexture LeftPortalRT;
    [SerializeField]
    private RenderTexture RightPortalRT;

    [SerializeField]
    private RenderTexture LeftViewTexture;
    private RenderTexture RightViewTexture;

    private int CurrentMenuIter = 0;
    private int MaxMenuIter = 0;
    private bool GameIsPaused;


    //Other
    //bool OriginalMethod = true;


    #region Player Pause Menu Functions


    public override void PauseGame()
    {

        //Set UI States
        UIManager.GetComponent<LevelUIManager>().SetGameUIState(false);
        UIManager.GetComponent<LevelUIManager>().SetPauseUIState(true);
        //Debug.Log("PauseUI is " + UIManager.GetComponent<LevelUIManager>().GetPauseUIState());

        GameIsPaused = true;
        //Debug.Log("GameIsPause value is: " + GameIsPaused);
        //set cursor
        Cursor.lockState = CursorLockMode.None;
        //Set Menu Iterators
        CurrentMenuIter = 0;
        MaxMenuIter = 5;
        //Disable PlayerMovement Map
        Player.GetComponent<PlayerController>().DisablePlayerMovementMap();
        //Enable PauseMenu Map
        Player.GetComponent<PlayerController>().EnablePauseControlMap();
        //update selector UI
        UIManager.GetComponent<LevelUIManager>().SetPauseMenuSelectorState(true, CurrentMenuIter);


    }

    public override void ResumeGame()
    {
        //Set UI States
        //Set Pause UI State
        UIManager.GetComponent<LevelUIManager>().SetPauseUIState(false);
        //Set GameUI state
        UIManager.GetComponent<LevelUIManager>().SetGameUIState(true);
        //Debug.Log("PauseUI is " + UIManager.GetComponent<LevelUIManager>().GetPauseUIState());

        GameIsPaused = false;
        //set cursor
        Cursor.lockState = CursorLockMode.Locked;
        //Disable PauseMenu Map
        Player.GetComponent<PlayerController>().DisablePauseControlMap();
        //Enable PlayerMovement Map
        Player.GetComponent<PlayerController>().EnablePlayerMovementMap();

        //reset Selector UI
        UIManager.GetComponent<LevelUIManager>().SetPauseMenuSelectorState(false, CurrentMenuIter);
    }


    public void PauseMoveUp()
    {
        if (GetCurrentMenuIter() == 0)
        {
            //set Previous selector off
            UIManager.GetComponent<LevelUIManager>().SetPauseMenuSelectorState(false, CurrentMenuIter);
            //set CurrentMenuIter Value
            SetCurrentMenuIter(GetMaxMenuIter() - 1);
            //set new Selector on
            UIManager.GetComponent<LevelUIManager>().SetPauseMenuSelectorState(true, CurrentMenuIter);
        }
        else
        {
            UIManager.GetComponent<LevelUIManager>().SetPauseMenuSelectorState(false, CurrentMenuIter);
            SetCurrentMenuIter(GetCurrentMenuIter() - 1);
            UIManager.GetComponent<LevelUIManager>().SetPauseMenuSelectorState(true, CurrentMenuIter);
        }
    }


    public void PauseMoveDown()
    {

        if (GetCurrentMenuIter() == (GetMaxMenuIter() - 1))
        {
            //set Previous selector off
            UIManager.GetComponent<LevelUIManager>().SetPauseMenuSelectorState(false, CurrentMenuIter);
            //set CurrentMenuIter Value
            SetCurrentMenuIter(0);
            //set new Selector on
            UIManager.GetComponent<LevelUIManager>().SetPauseMenuSelectorState(true, CurrentMenuIter);
        }
        else
        {
            UIManager.GetComponent<LevelUIManager>().SetPauseMenuSelectorState(false, CurrentMenuIter);
            SetCurrentMenuIter(GetCurrentMenuIter() + 1);
            UIManager.GetComponent<LevelUIManager>().SetPauseMenuSelectorState(true, CurrentMenuIter);
        }
    }

    public void PauseSelect()
    {

        switch (GetCurrentMenuIter())
        {
            //RESUME GAME
            case 0:
                ResumeGame();
                break;
            //SETTINGS MENU
            case 1:

                break;
            //Save Menu
            case 2:

                break;
            //Load Save Menu
            case 3:

                break;

            //QUIT GAME (return to menu)
            case 4:
                ToMainMenu();
                break;

            default:
                break;
        }
    }

    public void ToMainMenu()
    {

        LoadSelectedlevelAsync(0);
        DestroyGameSettingsOBJ();
    }


    #endregion


    #region Player Settings Menu Functions


    /// <summary>
    /// This Function Opens The Settings Menu
    /// </summary>
    public override void OpenSettings()
    {
        //Set Menu Iterators

        //Set Pause UI State
        UIManager.GetComponent<LevelUIManager>().SetPauseUIState(false);
        //Set Setting UI State
        UIManager.GetComponent<LevelUIManager>().SetSettingsUIState(true);

        //update Settings Data
        UIManager.GetComponent<LevelUIManager>().SetFOVSliderValue(GameSettingsOBJ.GetComponent<GameSettings>().GetFOV());
        UIManager.GetComponent<LevelUIManager>().SetFOVText();
        UIManager.GetComponent<LevelUIManager>().SetXSensitivity(GameSettingsOBJ.GetComponent<GameSettings>().GetXSensitivity());
        UIManager.GetComponent<LevelUIManager>().SetYSensitivity(GameSettingsOBJ.GetComponent<GameSettings>().GetYSensitivity());
        

    }

    /// <summary>
    /// This Function Closes The Settings Menu
    /// </summary>
    public override void CloseSettings()
    {
        //Set Setting UI State
        UIManager.GetComponent<LevelUIManager>().SetSettingsUIState(false);
        //Set Pause UI State
        UIManager.GetComponent<LevelUIManager>().SetPauseUIState(true);
    }

    public void SettingsMoveUp()
    {


    }

    public void SettingsMoveDown()
    {

    }

    public void SettingsSelect()
    {


    }

    #endregion

    #region Scene Loading


    public override void LoadSelectedlevelAsync(Levels Level)
    {
        //Disable all UI
        UIManager.GetComponent<LevelUIManager>().SetGameUIState(false);
        UIManager.GetComponent<LevelUIManager>().SetPauseUIState(false);
        UIManager.GetComponent<LevelUIManager>().SetSaveUI(false);

        //Enable Loading Screen UI
        UIManager.GetComponent<LevelUIManager>().SetLoadingScreenUIState(true);
        //Load Coroutine
        StartCoroutine(AsyncLoadLevel(Level));
    }

    #endregion

    #region Getters

    /// <summary>
    /// Gets The Value Of The GameIsPaused Variable
    /// </summary>
    /// <param name="PlayerData"></param>
    public bool GetGameIsPaused()
    {
        return GameIsPaused;
    }

    public int GetCurrentMenuIter()
    {
        return CurrentMenuIter;
    }

    public int GetMaxMenuIter()
    {
        return MaxMenuIter;
    }

    #endregion

    #region Setters
    
    public void SetCurrentMenuIter(int NewCurrent)
    {
        CurrentMenuIter = NewCurrent;
    }

    public void SetMaxMenuIter(int NewMax)
    {
        MaxMenuIter = NewMax;
    }

    void SetPlayerData(GameObject PlayerData)
    {

    }

   /// <summary>
   /// Sets Game Setting Data Like FOV and View Bob
   /// </summary>
    void SetGameSettingData()
    {
        //Set FOV
        MainCamera.fieldOfView = GameSettingsOBJ.GetComponent<GameSettings>().GetFOV();
        MainCamera.GetComponent<CameraController>().SetXYSensitivity(GameSettingsOBJ.GetComponent<GameSettings>().GetXSensitivity(), GameSettingsOBJ.GetComponent<GameSettings>().GetYSensitivity());

    }

    public override void SetNewSettings()
    {
        //Sets The Variables In The GameSetting
        //Set FOV
        GameSettingsOBJ.GetComponent<GameSettings>().SetFOV((int)UIManager.GetComponent<LevelUIManager>().GetFOVValue());
        //Get X Sensitivity
        GameSettingsOBJ.GetComponent<GameSettings>().SetXSensitivity(UIManager.GetComponent<LevelUIManager>().GetXSensitivity());
        //Get Y Sensitivity
        GameSettingsOBJ.GetComponent<GameSettings>().SetYSensitivity(UIManager.GetComponent<LevelUIManager>().GetYSensitivity());

        Save_LoadScript.SaveGameSettings(GameSettingsOBJ);

    }

    /// <summary>
    /// Sets The GameIsPaused Variable 
    /// </summary>
    /// <param name="state"></param>
    public void SetGameIsPaused(bool state)
    {
        GameIsPaused = state;
    }


    #endregion

    // Start is called before the first frame update
    void Start()
    {
        //Find Player Data
        PlayerDataObj = GameObject.FindGameObjectWithTag("PlayerDataObj");
        //Set Player Data
        SetPlayerData(PlayerDataObj);
        //Find Game Settings Object
        GameSettingsOBJ = GameObject.FindGameObjectWithTag("GameSettingsObj");
        //Set Game Settings Data
        SetGameSettingData();
       
    }

    // Update is called once per frame
    void Update()
    {
   
    }
}
