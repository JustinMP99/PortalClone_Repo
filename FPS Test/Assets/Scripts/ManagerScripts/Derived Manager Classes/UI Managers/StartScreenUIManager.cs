using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.UIElements;
using TMPro;
using Unity.VisualScripting.Dependencies.NCalc;
using System;
using Unity.VisualScripting;

public class StartScreenUIManager : UIManager
{

    [SerializeField]
    private TMP_Text GameTitle_TMP;

    #region Start Menu Variables
    [Header("START MENU VARIABLES")]
    [SerializeField] private GameObject UI;
    [SerializeField] private GameObject StartMenu;
    [SerializeField] private GameObject StartMenuBackground;
    
    //Main Menu Buttons
    public Button NewGameButton;
    public Button LoadGameButton;
    public Button OptionsButton;
    public Button QuitButton;

    //Options Menu Buttons
    public Button ApplyButton;
    public Button GoBackButton;


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

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    #region Set Button Functions

    public void SetNewGameButtonFunction()
    {

    }



    #endregion

    #region Main Menu Set State Functions

   


    public void SetStartMenuState(bool state)
    {

        StartMenu.SetActive(state);

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
