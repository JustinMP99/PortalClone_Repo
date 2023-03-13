using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LevelManager : MonoBehaviour
{

    //Game Objects
    public GameObject Player;
    public GameObject PortalPrefab;
    public GameObject LeftPortal;
    public GameObject RightPortal;

    public Transform LeftPortalPosition;
    public Transform RightPortalPosition;

    public GameObject TestCameraPos;

    public Quaternion YRot;

    //Materials
    public Material RightPortalMaterial;
    public Material LeftPortalMaterial;

    public Material LeftViewMat;
    public Material RightViewMat;


    //Render Textures
    public RenderTexture LeftPortalRT;
    public RenderTexture RightPortalRT;

    private RenderTexture LeftViewTexture;
    private RenderTexture RightViewTexture;

    //Other
    bool OriginalMethod = true;

    // Start is called before the first frame update
    void Start()
    {

       
    }

    // Update is called once per frame
    void Update()
    {
   
    }
}
