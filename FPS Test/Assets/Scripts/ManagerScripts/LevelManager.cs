using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LevelManager : MonoBehaviour
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
        FPSCamera.GetComponent<Camera>().fieldOfView = PlayerData.GetComponent<PlayerData>().FOV;
    }


    // Start is called before the first frame update
    void Start()
    {
        //Find Player Data
        PlayerDataObj = GameObject.FindGameObjectWithTag("PlayerDataObj");
        //Set Player Data
        SetPlayerData(PlayerDataObj);
    }

    // Update is called once per frame
    void Update()
    {
   
    }
}
