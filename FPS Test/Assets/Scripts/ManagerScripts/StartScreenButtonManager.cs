using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

/// <summary>
//
//
//
//
//
//
//
//// </summary>








public class StartScreenButtonManager : MonoBehaviour
{
    

    public GameObject UIManager;
    private StartScreenUIManager UIManagerScript;

    public GameObject StartScreenSettings;

    public GameObject PlayerDataObj;


    private int CurrentMenuIter = 0;
    private int MaxMenuIter = 0;

    private bool InStartMenu = false;
    private bool InSettingsMenu = false;
    private bool InChooseSaveMenu = false;
    private bool InSaveSelectedMenu = false;


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
        Start_Select_Up = PlayerControl.MainMenu.Start_Select_Up;
        Start_Select_Up.performed += MoveStartMenuSelectorUp;
        Start_Select_Up.Enable();

        Start_Select_Down = PlayerControl.MainMenu.Start_Select_Down;
        Start_Select_Down.performed += MoveStartMenuSelectorDown;
        Start_Select_Down.Enable();

        Start_Select = PlayerControl.MainMenu.Start_Select;
        Start_Select.performed += StartMenuSelect;
        Start_Select.Enable();

    }


    private void OnDisable()
    {

        Start_Select_Up.Disable();
        Start_Select_Down.Disable();
        Start_Select.Disable();

    }



    // Start is called before the first frame update
    void Start()
    {
        //set cursor
        Cursor.lockState = CursorLockMode.None;
        UIManagerScript = UIManager.GetComponent<StartScreenUIManager>();

        //add Listener
        UIManagerScript.FOVSlider.onValueChanged.AddListener(delegate { SetFOV(); });

        //set CurrentMenuIter
        CurrentMenuIter = 0;
        //set MaxMenuIter
        MaxMenuIter = 3;

        UIManagerScript.SetStartMenuState(true);
        InStartMenu = true;



    }

    // Update is called once per frame
    void Update()
    {

    }


    //Load into the first level
    public void NewGameFunction()
    {
        //Load the first Level
        SceneManager.LoadScene("TestLevel");
    }

    public void QuitGame()
    {

        Application.Quit();

    }




    #region Main Menu Input

    public void MoveStartMenuSelectorUp(InputAction.CallbackContext obj)
    {
        //if CurrentMenuIter equals Zero
        if (GetCurrentMenuIter() == 0)
        {
            //set Previous selector off
            UIManager.GetComponent<StartScreenUIManager>().SetStartSelectorImageState(false, CurrentMenuIter);
            //set CurrentMenuIter Value to the MaxMenuIter value
            SetCurrentMenuIter(GetMaxMenuIter() - 1);
            //set new Selector on
            UIManager.GetComponent<StartScreenUIManager>().SetStartSelectorImageState(true, CurrentMenuIter);
        }
        else
        {
            //set previous selector off
            UIManager.GetComponent<StartScreenUIManager>().SetStartSelectorImageState(false, CurrentMenuIter);
            //subtract 1 from the currentMenuIter
            SetCurrentMenuIter(GetCurrentMenuIter() - 1);
            UIManager.GetComponent<StartScreenUIManager>().SetStartSelectorImageState(true, CurrentMenuIter);
        }


    }

    public void MoveStartMenuSelectorDown(InputAction.CallbackContext obj)
    {

        if (GetCurrentMenuIter() == (GetMaxMenuIter() - 1))
        {
            //set Previous selector off
            UIManager.GetComponent<StartScreenUIManager>().SetStartSelectorImageState(false, CurrentMenuIter);
            //set CurrentMenuIter Value
            SetCurrentMenuIter(0);
            //set new Selector on
            UIManager.GetComponent<StartScreenUIManager>().SetStartSelectorImageState(true, CurrentMenuIter);
        }
        else
        {
            UIManager.GetComponent<StartScreenUIManager>().SetStartSelectorImageState(false, CurrentMenuIter);
            SetCurrentMenuIter(GetCurrentMenuIter() + 1);
            UIManager.GetComponent<StartScreenUIManager>().SetStartSelectorImageState(true, CurrentMenuIter);
        }
    }

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

    public void StartMenuSetSelector(int MenuIter)
    {
        UIManager.GetComponent<StartScreenUIManager>().SetStartSelectorImageState(false, CurrentMenuIter);
        CurrentMenuIter = MenuIter;
        UIManager.GetComponent<StartScreenUIManager>().SetStartSelectorImageState(true, CurrentMenuIter);
    }

    #endregion

    #region Menu Transition Functions

    public void ReturnToStartMenu()
    {
        UIManagerScript.SetChooseSaveMenuState(false);
        UIManagerScript.SetStartMenuState(true);

        InChooseSaveMenu = false;
        InStartMenu = true;
    }



    //To Choose Save
    public void GoToChooseSaveSelect()
    {
        //disable ---START---, ---SAVE SELECTED---
        UIManagerScript.SetStartMenuState(false);

        UIManagerScript.SetSelectedSaveMenu(false);

        //Enable Save Selected Menu
        UIManagerScript.SetChooseSaveMenuState(true);

        InStartMenu = false;
        InSaveSelectedMenu = false;
        InChooseSaveMenu = true;
        
        ////read save data
        //for (int i = 0; i < 3; i++)
        //{

        //    //Read the save data of each save file
        //    PlayerData temp = new PlayerData( Save_LoadScript.ReadSaveData(i));
        //    Debug.Log(temp);
        //    if (temp == null)
        //    {
        //        temp.LevelText = "No Level Data";
        //    }
        //    Debug.Log(temp.LevelText);
        //    temp.LevelText = "No Level Data";
        //    //Update UI
        //    switch (i)
        //    {
        //        case 0:
        //            UIManagerScript.SetSaveOneText(temp.LevelText);
        //            break;

        //        case 1:
        //            UIManagerScript.SetSaveTwoText(temp.LevelText);
        //            break;

        //        case 2:
        //            UIManagerScript.SetSaveThreeText(temp.LevelText);
        //            break;
        //        default:
        //            break;
        //    }

        //}

    }




    //DEPRECATED
    public void OpenSettings()
    {
        //set CurrentMenuIter
        CurrentMenuIter = 0;
        //Set MaxMenuIter
        MaxMenuIter = 5;

        //Deactivate the start UI
        UIManagerScript.SetStartMenuState(false);
        //Deactivate Start Selector Images
        

        //Activate the settings UI
        UIManagerScript.SetSettingsMenuState(true);

        InStartMenu = false;
        InSettingsMenu = true;

    }


    //DEPTRECATED
    public void ReturnToStart()
    {
        //set CurrentMenuIter
        CurrentMenuIter = 0;
        //Set MaxMenuIter
        MaxMenuIter = 3;
        //Activate the start UI
        UIManagerScript.SetStartMenuState(true);
        //Deactivate the settings UI
        UIManagerScript.SetSettingsMenuState(false);

        UIManagerScript.SetStartSelectorImageState(true,  0);
        UIManagerScript.SetStartSelectorImageState(false, 1);
        UIManagerScript.SetStartSelectorImageState(false, 2);

        InStartMenu = true;
        InSettingsMenu = false;

    }


    #endregion

    #region Setters

    public void SetFOV()
    {
        //set the fov Text
        UIManagerScript.SetFOVText();
        //Set the StartScreenValues FOV by using the GetFOVValue in the UIManagerScript
        StartScreenSettings.GetComponent<StartScreenSettings>().SetFOV(UIManagerScript.GetFOVValue());

    }


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


}
