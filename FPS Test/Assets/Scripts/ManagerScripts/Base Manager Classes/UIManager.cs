using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public abstract class UIManager : MonoBehaviour
{


    [Header("Load Screen UI")]
    [SerializeField]
    protected GameObject LoadScreenUI;
    [SerializeField]
    protected Slider LoadingSlider;
    [SerializeField]
    protected Image LoadingScreenImage;

    [Header("Settings Menu UI")]
    [SerializeField]
    private GameObject SettingsMenu;

    [SerializeField]
    private GameObject SettingsMenuBackground;

    public Slider FOVSlider;
    [SerializeField]
    private TMP_Text XSensitivityText;
    [SerializeField]
    private Slider XSensitivitySlider;
    [SerializeField]
    private TMP_Text YSensitivityText;
    [SerializeField]
    private Slider YSensitivitySlider;

    [SerializeField]
    private TMP_Text FieldOfViewNumText;

    [SerializeField]
    //The parent object of the SettingsSelectorImages
    private GameObject SettingSelectors;
    [SerializeField]
    private GameObject[] SettingsSelectorImages;


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

    #region Getters

    public float GetFOVValue()
    {

        return FOVSlider.value;

    }

    public int GetXSensitivity()
    {
        return (int)XSensitivitySlider.value;
    }

    public int GetYSensitivity()
    {

        return (int)YSensitivitySlider.value;

    }

    public float GetLoadingSliderValue()
    {

        return LoadingSlider.value;

    }

    #endregion


    #region Setters
    public void SetFOVText()
    {
        FieldOfViewNumText.gameObject.SetActive(false);

        FieldOfViewNumText.GetComponent<TextMeshProUGUI>().text = GetFOVValue().ToString();
        //FieldOfViewNumText.GetComponent<TextMeshProUGUI>().text = "HELLO";

        FieldOfViewNumText.gameObject.SetActive(true);

    }

    public void SetFOVSliderValue(int value)
    {
        FOVSlider.value = value;
    }

    public void SetXText()
    {

        XSensitivityText.GetComponent<TextMeshProUGUI>().text = GetXSensitivity().ToString();


    }

    public void SetYText()
    {

        YSensitivityText.GetComponent<TextMeshProUGUI>().text = GetYSensitivity().ToString();

    }

    public void SetXSensitivity(int value)
    {

        //Slider 
        XSensitivitySlider.value = value;

        //Text
        XSensitivityText.text = value.ToString();
    }

    public void SetYSensitivity(int value)
    {
        YSensitivitySlider.value = value;
        YSensitivityText.text = value.ToString();
    }


    public void SetLoadingSliderValue(float Value)
    {

        LoadingSlider.value = Value;

    }


    #endregion


}
