using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{

    private Transform FPSCameraTransform;
    [SerializeField]
    private GameObject Player;
    [SerializeField]
    private GameObject CameraSetPosition;
    [SerializeField]
    private GameObject levelManager;

    public GameObject HoldingPosition;

    public GameObject LeftP = null;
    public GameObject RightP = null;

    //Input
    public PlayerInputActionMaps InputMap;
    private InputAction Look;

    public Vector2 MousePosition = Vector2.zero;
    //public Vector2 MouseInput = Vector2.zero;
    public Vector3 RotationValues;

    //public float Sensitivity = 100.0f;
    public float XRotation = 0.0f;
    public float Xrot = 0.0f;
    public float YRotation = 0.0f;


    [SerializeField] private Vector2 Sensitivity;
    private float XSensitivity = 4.0f;
    private float YSensitivity = 4.0f;

    public void Awake()
    {
        InputMap = new PlayerInputActionMaps();
        Sensitivity.x = XSensitivity;
        Sensitivity.y = YSensitivity;
    }

    public void OnEnable()
    {
        //set Look input action to Look Action in the player movement action map
        Look = InputMap.Game_Movement.Look;
        Look.Enable();

        //capture mouse position in MousePosition vector
        Look.performed += ctx => MousePosition = ctx.ReadValue<Vector2>() * Sensitivity;
        Look.canceled += ctx2 => MousePosition = ctx2.ReadValue<Vector2>();
    }

    public void OnDisable()
    {
        Look.Disable();
    }

    /// <summary>
    /// Updates The Camera Position And Rotation
    /// </summary>
    public void UpdateCamera()
    {

        //Create rotation Value
        XRotation -= MousePosition.y * Time.deltaTime * Sensitivity.x;
        XRotation = Mathf.Clamp(XRotation, -80.0f, 80.0f);
        YRotation += MousePosition.x * Time.deltaTime * Sensitivity.y;

        //Apply Rotation Value
        //this.transform.localRotation = Quaternion.Euler(XRotation, -YRotation, 0.0f);
        this.transform.localEulerAngles = new Vector3(XRotation, YRotation, 0.0f);  

        //rotate player   
        Player.transform.localRotation = Quaternion.Euler(0.0f, YRotation, 0.0f);

        //update cameras transform to follow player
        this.transform.position = CameraSetPosition.transform.position;

    }


    public void SetXYSensitivity(float X, float Y)
    {

        Sensitivity.x = X;
        Sensitivity.y = Y;

    }

    // Start is called before the first frame update
    void Start()
    {
        //set cursor
        Cursor.lockState = CursorLockMode.Locked;
     
    }

    // Update is called once per frame
    void Update()
    {
        if (!levelManager.GetComponent<LevelManager>().GetGameIsPaused())
        {
            UpdateCamera();
        }
        
    }


    public void SetCameraFOV(int newFOV)
    {
        this.GetComponent<Camera>().fieldOfView = (float)newFOV; 
    }

}
