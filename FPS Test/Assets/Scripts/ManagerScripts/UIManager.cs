using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{

    public GameObject PlayerUICanvas;

    [Header("GameUI")]
    public GameObject GameUI;

    [Header("PauseUI")]
    public GameObject PauseUI;
    public GameObject [] PauseSelectorImages;

    [Header("Save File UI")]
    public GameObject SaveUI;
    public GameObject[] SaveFileUIs;
    
    
    [Header("Load Screen UI")]
    [SerializeField]
    private GameObject LoadScreenUI;
    [SerializeField]
    private Slider LoadingBar;
    [SerializeField]
    private Image LoadingScreenImage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


  

    #region GameUI Functions

    public void SetGameUIState(bool state)
    {

        GameUI.SetActive(state);

    }

    #endregion

    #region PauseUI Functions

    public void SetPauseUIState(bool state)
    {

        PauseUI.SetActive(state);
      
    }

    public bool GetPauseUIState()
    {
        return PauseUI.activeSelf;
    }

    public void SetPauseMenuSelectorState(bool state, int ImageNum)
    {

        PauseSelectorImages[ImageNum].SetActive(state);
        
    }

    #endregion

    #region SaveUI Functions
   

    public void SetSaveUI(bool state)
    {

        SaveUI.SetActive(state);
    }

    #endregion


    #region Loading Screen UI Functions

    public void SetLoadingScreenUIState(bool state)
    {
        LoadScreenUI.SetActive(state);
    }

    public void SetLoadingBarValue(float Value)
    {

        LoadingBar.value = Value;

    }

    public float GetLoadingBarValue()
    {

        return LoadingBar.value;

    }

    public void SetLoadingImage(Image NewLoadingImage)
    {

        LoadingScreenImage.sprite = NewLoadingImage.sprite;

    }

    #endregion




    #region UI Transition Function

    public void GoToSettings()
    {


    }

    public void GoToSave()
    {


    }


    //Goes from save select state to pause state
    public void ReturnToPause()
    {
        //Disable SaveUI

        //Disable SettingsUI

        //Disable GameUI

        //Enable PauseUI
        
    }

    #endregion

}
