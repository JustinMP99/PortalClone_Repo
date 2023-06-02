using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class StartScreenManager : Manager
{
    
    public GameObject StartScreenSettings;

    public GameObject LevelLoaderPrefab;

    private int CurrentMenuIter = 0;
    private int MaxMenuIter = 0;

  
    public enum Menu
    {
        InStartMenu,
        InSettingsMenu,
        InChooseSaveMenu,
        InSaveSelectedMenu
    }
    public Menu CurrentMenu;

    #region ActionMap Variables
    public PlayerInputActionMaps PlayerControl;

    private InputAction Start_Select_Up;
    private InputAction Start_Select_Down;
    private InputAction Start_Select;

    private InputAction Settings_Select_Up;
    private InputAction Settings_Select_Down;
    private InputAction Settings_Select;

    #endregion


    void Awake()
    {
        PlayerControl = new PlayerInputActionMaps();
    }

    private void OnEnable()
    {
        //Start_Select_Up = PlayerControl.MainMenu.Start_Select_Up;
        //Start_Select_Up.performed += MoveStartMenuSelectorUp;
        //Start_Select_Up.Enable();

        //Start_Select_Down = PlayerControl.MainMenu.Start_Select_Down;
        //Start_Select_Down.performed += MoveStartMenuSelectorDown;
        //Start_Select_Down.Enable();

        Start_Select = PlayerControl.MainMenu.Start_Select;
        Start_Select.performed += StartMenuSelect;
        Start_Select.Enable();

    }

    private void OnDisable()
    {

        //Start_Select_Up.Disable();
        //Start_Select_Down.Disable();
        Start_Select.Disable();

    }

    // Start is called before the first frame update
    void Start()
    {
        //set cursor
        Cursor.lockState = CursorLockMode.None;
     
    }

    // Update is called once per frame
    void Update()
    {

    }


    public override void Startup()
    {
        //Set This Scene To The Main Scene
        SceneManager.SetActiveScene(this.gameObject.scene);
        //Find UIManager
        UIManager = GameObject.FindGameObjectWithTag("MainMenuUIManager");
        //Set Button Functionality
        UIManager.GetComponent<StartScreenUIManager>().NewGameButton.onClick.AddListener(delegate { NewGameFunction(); }); 


        //Set Up Game Settings
        GameSettingsScriptOBJ = Save_LoadScript.LoadGameSettings();
        if (GameSettingsScriptOBJ != null)
        {
            Debug.Log("Game Settings Data Found!");
            //Copy To GameSettingsObject
            CopyGameSettingsScriptToGameSettings(GameSettingsOBJ, GameSettingsScriptOBJ);


        }
        else
        {
            Debug.Log("No Game Settings Data Found!");
            //There are No Prior Game Settings So Fill Them To Default
            Debug.Log("Creating Default Game Settings...");

            //Create GameSettings Object
            //GameSettingsOBJ = new GameObject();
            //set default values
            GameSettingsOBJ.GetComponent<GameSettings>().SetFOV(60);
            GameSettingsOBJ.GetComponent<GameSettings>().SetXSensitivity(5);
            GameSettingsOBJ.GetComponent<GameSettings>().SetYSensitivity(5);
            GameSettingsOBJ.GetComponent<GameSettings>().SetViewBobState(false);
            GameSettingsOBJ.GetComponent<GameSettings>().SetMotionBlurState(false);
            Debug.Log("Default Game Settings Have Been Created!");
            //Save Newly Created GameSettingsOBJ
            Save_LoadScript.SaveGameSettings(GameSettingsOBJ);

        }


    }



    #region Main Menu Input


    public void StartMenuSelect(InputAction.CallbackContext obj)
    {

        switch (CurrentMenuIter)
        {
            case 0:

                NewGameFunction();
                break;

            case 1:


                break;


            case 2:
                QuitGame();
                break;
            default:
                break;
        }

    }


    #endregion

    #region Menu Transition Functions

    //Load Into The First Level
    void NewGameFunction()
    {
        //create New Game player data
        SetNewGamePlayerData();
        //Set Scenes To Load
        ScenesToLoad.Add(SceneID.TestLevel);
        ScenesToLoad.Add(SceneID.GameUI);
        StartCoroutine(_baseSceneManager.SwitchScenes(ScenesToLoad));
      
    }

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

    public void ReturnToStartMenu()
    {
        UIManagerScript.SetChooseSaveMenuState(false);
        UIManagerScript.SetSettingsMenuState(false);
        UIManagerScript.SetStartMenuState(true);

        CurrentMenu = Menu.InStartMenu;
    }

    //To Choose Save
    public void GoToChooseSaveSelect()
    {
        //disable ---START---, ---SAVE SELECTED---
        UIManagerScript.SetStartMenuState(false);

        UIManagerScript.SetSelectedSaveMenu(false);

        //Enable Save Selected Menu
        UIManagerScript.SetChooseSaveMenuState(true);

        CurrentMenu = Menu.InChooseSaveMenu;

        //read save data
        for (int i = 0; i < 3; i++)
        {

            //Read the save data of each save file
            PlayerDataScript temp = new PlayerDataScript();
            temp = Save_LoadScript.ReadSaveData(i);
            Debug.Log(temp);
            if (temp == null)
            {
                temp = new PlayerDataScript();
                temp.LevelText = "No Level Data";
                UIManagerScript.AccessSaveFileUIIsLoaded(i, false);
            }
            else
            {
                
                //Update IsLoaded
                UIManagerScript.AccessSaveFileUIIsLoaded(i, true);
            }
           
            
            //Update UI
            UIManagerScript.SetSaveFileLevelText(i, temp.LevelText);
           

        }

    }

    public override void OpenSettings()
    {
        //set CurrentMenuIter
        CurrentMenuIter = 0;
        //Set MaxMenuIter
        MaxMenuIter = 5;

        //Deactivate the start UI
        UIManagerScript.SetStartMenuState(false);
        
        //Activate the settings UI
        UIManagerScript.SetSettingsMenuState(true);

        //set FOV and Sensitivity For The UI Using The Data From The Game Settings
        UIManagerScript.FOVSlider.value = GameSettingsOBJ.GetComponent<GameSettings>().GetFOV();
        UIManagerScript.SetXSensitivity(GameSettingsOBJ.GetComponent<GameSettings>().GetXSensitivity());
        UIManagerScript.SetYSensitivity(GameSettingsOBJ.GetComponent<GameSettings>().GetYSensitivity());

        CurrentMenu = Menu.InSettingsMenu;

    }

    #endregion

    #region Quit Game Function

    public void QuitGame()
    {

        Application.Quit();

    }


    #endregion

    #region User Action Functions


    #endregion


    #region Setters


    public void SetCurrentMenuIter(int SetValue)
    {

        CurrentMenuIter = SetValue;

    }


    public void SetMaxMenuIter(int SetValue)
    {

        MaxMenuIter = SetValue;

    }

    #endregion

    #region Getters


    public int GetCurrentMenuIter()
    {

        return CurrentMenuIter;

    }


    public int GetMaxMenuIter()
    {

        return MaxMenuIter;

    }

    #endregion

    public override void LoadSelectedlevelAsync(Levels Level)
    {

        //Disable StartMenu UI
        UIManagerScript.SetStartMenuState(false);
        //Enable LoadingScreen UI
        UIManagerScript.SetLoadingScreenUIState(true);
        //Async load level
        StartCoroutine(AsyncLoadLevel(Level));

    }

}
