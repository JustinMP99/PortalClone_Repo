using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{

    public GameObject PlayerUICanvas;

    public GameObject GameUI;
    public GameObject PauseUI;

    public GameObject [] PauseSelectorImages;
        
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

    public void SetPauseMenuSelectorState(bool state, int ImageNum)
    {

        PauseSelectorImages[ImageNum].SetActive(state);

    }

    #endregion

}
