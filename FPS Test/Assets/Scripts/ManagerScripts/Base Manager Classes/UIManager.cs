using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public abstract class UIManager : MonoBehaviour
{


    
    
    [Header("Load Screen UI")]
    [SerializeField]
    protected GameObject LoadScreenUI;
    [SerializeField]
    protected Slider LoadingSlider;
    [SerializeField]
    protected Image LoadingScreenImage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    #region Loading Screen UI Functions

    public void SetLoadingScreenUIState(bool state)
    {
        LoadScreenUI.SetActive(state);
    }

    public void SetLoadingBarValue(float Value)
    {

        LoadingSlider.value = Value;

    }

    public float GetLoadingBarValue()
    {

        return LoadingSlider.value;

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
