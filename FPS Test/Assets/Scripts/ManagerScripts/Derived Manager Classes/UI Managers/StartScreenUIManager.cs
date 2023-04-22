using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.UIElements;
using TMPro;



public class StartScreenUIManager : UIManager
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

        //Activate The First Selector Image
        StartSelectorImages[0].SetActive(true);

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

 

    public void AccessSaveFileUIIsLoaded(int SaveFileUI, bool state)
    {

        SaveFileUIs[SaveFileUI].SetIsLoaded(state);

    }


    #endregion

    #region Get Value Functions

  

    #endregion
}
