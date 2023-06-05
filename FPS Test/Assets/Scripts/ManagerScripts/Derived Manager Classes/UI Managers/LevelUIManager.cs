using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUIManager : UIManager
{

    public GameObject PlayerUICanvas;

    [Header("------GameUI------")]
    [SerializeField]
    private GameObject GameUI;

    [Header("------PauseUI------")]
    [SerializeField] private GameObject PauseUI;
    [SerializeField] public Button ResumeButton;
    [SerializeField] public Button OptionsButton;
    [SerializeField] public Button QuitButton;
    [SerializeField] private GameObject[] PauseSelectorImages;

    [Header("------Save File UI------")]
    [SerializeField] private GameObject SaveUI;
    [SerializeField] private GameObject[] SaveFileUIs;

    [Header("------Settings UI------")]
    [SerializeField]
    private GameObject SettingsUI;


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

    #region SettingsUI Functions

    public void SetSettingsUIState(bool state)
    {
        SettingsUI.SetActive(state);
    }

    #endregion



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
