using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUIManager : UIManager
{

    public GameObject PlayerUICanvas;

    [Header("GameUI")]
    public GameObject GameUI;

    [Header("PauseUI")]
    public GameObject PauseUI;
    public GameObject[] PauseSelectorImages;

    [Header("Save File UI")]
    public GameObject SaveUI;
    public GameObject[] SaveFileUIs;



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



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
