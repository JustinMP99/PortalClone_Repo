using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.UIElements;
using TMPro;



public class StartScreenUIManager : MonoBehaviour
{

    [SerializeField]
    private TMP_Text GameTitle_TMP;




    #region Start Menu Variables
    [Header("START MENU VARIABLES")]

    [SerializeField]
    private GameObject StartMenu;
    [SerializeField]
    private GameObject StartMenuBackground;

    [SerializeField]
    //The parent object of the StartSelectorImages
    private GameObject StartSelectors;
    [SerializeField]
    //Array of each StarSelectorImage
    private GameObject[] StartSelectorImages;

    #endregion



    #region Settings Variables
    [Header("SETTINGS VARIABLES")]
    [SerializeField]
    private GameObject SettingsMenu,
    GoBackButton;
    [SerializeField]
    private GameObject SettingsMenuBackground;

    public Slider FOVSlider;

    [SerializeField]
    private TMP_Text FieldOfViewNumText;

    [SerializeField]
    //The parent object of the SettingsSelectorImages
    private GameObject SettingSelectors;
    [SerializeField]
    private GameObject[] SettingsSelectorImages;


    #endregion




    #region Select Save Variables
    [Header ("SELECT SAVE VARIABLES")]


    [SerializeField]
    private GameObject ChooseSaveMenu;


    [SerializeField]
    private SaveFileUIScript []SaveFileUIs;

   

    #endregion


    #region Save Selected Menu Variables
    [Header("SAVE SELECTED MENU VARIABLES")]

    [SerializeField]
    private GameObject SaveSelectedMenu;






    #endregion




    // Start is called before the first frame update
    void Start()
    {

        //check if a settings save file exists 

        //if it does, load the default values into a game object

        //if it doesnt, create the file with default values



        StartSelectorImages[0].SetActive(true);
        //SettingsSelectorImages[0].SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        
    }




    #region Main Menu Set State Functions

    public void SetStartMenuState(bool state)
    {

        StartMenu.SetActive(state);

    }

    //Sets the state of the StartSelectorImages parent object
    public void SetStartSelectorImages(bool state)
    {
        StartSelectors.SetActive(state);
    }

    //Sets the state of individual StartSelectorImages using an array index and state bool
    public void SetStartSelectorImageState(bool state, int ImageNum)
    {

        StartSelectorImages[ImageNum].SetActive(state);

    }
   

    #endregion

    #region Settings Menu Set State Functions

    public void SetSettingsMenuState(bool state)
    {

        SettingsMenu.SetActive(state);

    }


    public void SetSettingSelectorImageState(bool state, int ImageNum)
    {

        SettingsSelectorImages[ImageNum].SetActive(state);

    }



    #endregion

    #region Choose Save Menu State Functions

    public void SetChooseSaveMenuState(bool state)
    {
        ChooseSaveMenu.SetActive(state);

    }


    public void SetSaveFileLevelText(int SaveFile, string leveltext)
    {

        SaveFileUIs[SaveFile].SetLevelText(leveltext);

    }


    #endregion

    #region Save Selected Menu State Functions

    public void SetSelectedSaveMenu(bool state)
    {

        SaveSelectedMenu.SetActive(state);

    }


    #endregion


    #region Set Value Functions

    public void SetFOVText()
    {
        FieldOfViewNumText.gameObject.SetActive(false);

        FieldOfViewNumText.GetComponent<TextMeshProUGUI>().text = GetFOVValue().ToString();
        //FieldOfViewNumText.GetComponent<TextMeshProUGUI>().text = "HELLO";

        FieldOfViewNumText.gameObject.SetActive(true);

    }

    


    #endregion

    #region Get Value Functions

    public float GetFOVValue()
    {

        return FOVSlider.value;

    }

    #endregion
}
