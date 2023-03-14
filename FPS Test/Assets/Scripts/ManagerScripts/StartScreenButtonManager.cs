using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class StartScreenButtonManager : MonoBehaviour
{

    public GameObject UIManager;
    private StartScreenUIManager UIManagerScript;

    public GameObject StartScreenSettings;

    private int CurrentMenuIter = 0;
    private int MaxMenuIter = 0;

    private bool InStart = true;
    private bool InSettings = false;


    #region ActionMap Variables
    public PlayerInputActionMaps PlayerControl;

    private InputAction Select_Up;
    private InputAction Select_Down;
    private InputAction Select;


    #endregion



    private void OnEnable()
    {
        
    }
    private void OnDisable()
    {
        
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


    }

    // Update is called once per frame
    void Update()
    {

    }


    //Load into the first level
    public void StartButton()
    {
        //Load the first Level
        SceneManager.LoadScene("TestLevel");


    }

    public void QuitGame()
    {

        Application.Quit();

    }


    public void MoveSelectorUp()
    {
        //if CurrentMenuIter equals Zero
        if (GetCurrentMenuIter() == 0)
        {
            //set Previous selector off
            UIManager.GetComponent<UIManager>().SetSelectorImageState(false, CurrentMenuIter);
            //set CurrentMenuIter Value to the MaxMenuIter value
            SetCurrentMenuIter(GetMaxMenuIter() - 1);
            //set new Selector on
            UIManager.GetComponent<UIManager>().SetSelectorImageState(true, CurrentMenuIter);
        }
        else
        {
            //set previous selector off
            UIManager.GetComponent<UIManager>().SetSelectorImageState(false, CurrentMenuIter);
            //subtract 1 from the currentMenuIter
            SetCurrentMenuIter(GetCurrentMenuIter() - 1);
            UIManager.GetComponent<UIManager>().SetSelectorImageState(true, CurrentMenuIter);
        }


    }

    public void MoveSelectorDown()
    {

        if (GetCurrentMenuIter() == (GetMaxMenuIter() - 1))
        {
            //set Previous selector off
            UIManager.GetComponent<UIManager>().SetSelectorImageState(false, CurrentMenuIter);
            //set CurrentMenuIter Value
            SetCurrentMenuIter(0);
            //set new Selector on
            UIManager.GetComponent<UIManager>().SetSelectorImageState(true, CurrentMenuIter);
        }
        else
        {
            UIManager.GetComponent<UIManager>().SetSelectorImageState(false, CurrentMenuIter);
            SetCurrentMenuIter(GetCurrentMenuIter() + 1);
            UIManager.GetComponent<UIManager>().SetSelectorImageState(true, CurrentMenuIter);
        }
    }






    public void OpenSettings()
    {
        //set CurrentMenuIter
        CurrentMenuIter = 0;
        //Set MaxMenuIter
        MaxMenuIter = 5;

        //Deactivate the start UI
        UIManagerScript.SetStartUIState(false);
        //Activate the settings UI
        UIManagerScript.SetSettingsUIState(true);

        InStart = false;
        InSettings = true;

    }

    public void ReturnToStart()
    {
        //Activate the start UI
        UIManagerScript.SetStartUIState(true);
        //Deactivate the settings UI
        UIManagerScript.SetSettingsUIState(false);

        InStart = true;
        InSettings = false;

    }


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
