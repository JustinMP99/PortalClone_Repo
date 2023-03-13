using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreenButtonManager : MonoBehaviour
{

    public GameObject UIManager;
    private StartScreenUIManager UIManagerScript;
    public GameObject StartScreenSettings;



    // Start is called before the first frame update
    void Start()
    {
        //set cursor
        Cursor.lockState = CursorLockMode.None;
        UIManagerScript = UIManager.GetComponent<StartScreenUIManager>();

        //add Listener
        UIManagerScript.FOVSlider.onValueChanged.AddListener(delegate { SetFOV(); });

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


    public void OpenSettings()
    {

        //Deactivate the start UI
        UIManagerScript.SetStartUIState(false);
        //Activate the settings UI
        UIManagerScript.SetSettingsUIState(true);

    }

    public void ReturnToStart()
    {
        //Activate the start UI
        UIManagerScript.SetStartUIState(true);
        //Deactivate the settings UI
        UIManagerScript.SetSettingsUIState(false);

    }



    public void SetFOV()
    {
        //set the fov Text
        UIManagerScript.SetFOVText();
        //Set the StartScreenValues FOV by using the GetFOVValue in the UIManagerScript
        StartScreenSettings.GetComponent<StartScreenSettings>().SetFOV(UIManagerScript.GetFOVValue());

    }


}
