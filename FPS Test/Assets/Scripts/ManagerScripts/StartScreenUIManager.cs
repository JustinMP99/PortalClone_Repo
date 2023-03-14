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
    [SerializeField]
    private GameObject StartUI;
    [SerializeField]
    private GameObject SettingsUI,
    GoBackButton;
    public Slider FOVSlider;
    [SerializeField]
    private TMP_Text FieldOfViewNumText;
    [SerializeField]
    private GameObject[] StartSelectorImages;
    [SerializeField]
    private GameObject[] SettingsSelectorImages;

    // Start is called before the first frame update
    void Start()
    {

        StartSelectorImages[0].SetActive(true);
        SettingsSelectorImages[0].SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        
    }




    #region Set Active State Functions

    public void SetStartUIState(bool state)
    {

        StartUI.SetActive(state);

    }

    public void SetSettingsUIState(bool state)
    {

        SettingsUI.SetActive(state);

    }

    public void SetStartSelectorImageState(bool state, int ImageNum)
    {

        StartSelectorImages[ImageNum].SetActive(state);

    }
    public void SetSettingSelectorImageState(bool state, int ImageNum)
    {

        SettingsSelectorImages[ImageNum].SetActive(state);

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
