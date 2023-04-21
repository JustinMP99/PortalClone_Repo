using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LevelManager : Manager
{

    [Header("---GAME OBJECTS---")]
    public GameObject Player;
    public GameObject FPSCamera;
    public GameObject PortalPrefab;
    public GameObject LeftPortal;
    public GameObject RightPortal;

    public Transform LeftPortalPosition;
    public Transform RightPortalPosition;

    public GameObject TestCameraPos;
    public GameObject PlayerDataObj;

    public Quaternion YRot;

    [Header("---MATERIALS---")]
    public Material RightPortalMaterial;
    public Material LeftPortalMaterial;

    public Material LeftViewMat;
    public Material RightViewMat;


    [Header("---RENDER TEXTURES---")]
    public RenderTexture LeftPortalRT;
    public RenderTexture RightPortalRT;

    private RenderTexture LeftViewTexture;
    private RenderTexture RightViewTexture;

    //Other
    //bool OriginalMethod = true;


    //Set Player Data
    void SetPlayerData(GameObject PlayerData)
    {
        
    }

    //Set Game Data
    void SetGameSettingData()
    {
        //Set FOV
        MainCamera.fieldOfView = GameSettingsOBJ.GetComponent<GameSettings>().GetFOV();
        MainCamera.GetComponent<CameraController>().SetXYSensitivity(GameSettingsOBJ.GetComponent<GameSettings>().GetXSensitivity(), GameSettingsOBJ.GetComponent<GameSettings>().GetYSensitivity());

    }




    #region Scene Loading


    public override void LoadSelectedlevelAsync(Levels Level)
    {
        //Disable all UI
        UIManager.GetComponent<LevelUIManager>().SetGameUIState(false);
        UIManager.GetComponent<LevelUIManager>().SetPauseUIState(false);
        UIManager.GetComponent<LevelUIManager>().SetSaveUI(false);

        //Enable Loading Screen UI
        UIManager.GetComponent<LevelUIManager>().SetLoadingScreenUIState(true);
        //Load Coroutine
        StartCoroutine(AsyncLoadLevel(Level));
    }

    #endregion


    // Start is called before the first frame update
    void Start()
    {
        //Find Player Data
        PlayerDataObj = GameObject.FindGameObjectWithTag("PlayerDataObj");
        //Set Player Data
        SetPlayerData(PlayerDataObj);
        //Find Game Settings Object
        GameSettingsOBJ = GameObject.FindGameObjectWithTag("GameSettingsObj");
        //Set Game Settings Data
        SetGameSettingData();
       
    }

    // Update is called once per frame
    void Update()
    {
   
    }
}
