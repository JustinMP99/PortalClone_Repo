using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;




public class PlayerController : MonoBehaviour
{

    #region ActionMap Variables
    public PlayerInputActionMaps PlayerControl;
    

    //PLAYER MOVEMENT ACTIONS
    private InputAction Move;
    private InputAction Jump;
    private InputAction Interact;
    private InputAction FireLeft;
    private InputAction FireRight;
    private InputAction PauseGame;

    //PAUSE MENU ACTIONS
    private InputAction Pause_MoveUp;
    private InputAction Pause_MoveDown;
    private InputAction Pause_Select;
    private InputAction Pause_Return;
    #endregion

    #region Movement Variables

    public Rigidbody rb;
    public float Speed = 5.0f;
    //NEW INPUT SYSTEM
    Vector2 Movement;
    Vector3 Direction = Vector3.zero;

    #endregion

    #region GameObjectVariables
    private GameObject UIManager;
    public  GameObject FPSCamera;
    public GameObject PortalPrefab;

    public GameObject LeftPortal;
    public GameObject RightPortal;
    #endregion

    #region Menu Variables

    private int CurrentMenuIter = 0;
    private int MaxMenuIter = 3;
    private bool GameIsPaused = false;

    #endregion

    #region Interaction Variables

    private bool IsHolding = false;
    private GameObject HoldingObject;

    #endregion

    #region RenderTexture Variables
    public Material LeftViewMat;
    public Material RightViewMat;

    private RenderTexture LeftViewTexture;
    private RenderTexture RightViewTexture;
    #endregion


    //int layer = 10;
    public LayerMask LayerToHit;



    #region Other Functions


    public void PortalFireCheck()
    {

        if (LeftPortal.GetComponent<PortalScript>().HasBeenFired && RightPortal.GetComponent<PortalScript>().HasBeenFired || LeftPortal != null && RightPortal != null)
        {
            //update Render Texture
            //LEFT PORTAL
            //set materials
            //Left Portal
            //if the portal has not rendered...
            LeftPortal.GetComponent<PortalScript>().PortalCamera.enabled = true;
            if (LeftPortal.GetComponent<PortalScript>().PortalCamera.targetTexture != null)
            {
                //release the memory
                LeftViewTexture.Release();
            }
            LeftPortal.GetComponent<PortalScript>().OtherPortal = RightPortal;
            //Set the View Texture Resolution to the Screens
            LeftViewTexture = new RenderTexture(Screen.width, Screen.height, 0);
            //Render the Portals Camera to the View Texture
            LeftPortal.GetComponent<PortalScript>().PortalCamera.targetTexture = LeftViewTexture;
            //set the Other Portals Material Texture
            RightPortal.GetComponent<Renderer>().material = RightViewMat;
            //Set the materials main texture
            RightPortal.GetComponent<Renderer>().material.SetTexture("_MainTex", LeftViewTexture);
            //set the Materials Base Map
            RightPortal.GetComponent<Renderer>().material.SetTexture("_BaseMap", LeftViewTexture);

            //RIGHT PORTAL
            //update the material
            //Right Portal
            RightPortal.GetComponent<PortalScript>().PortalCamera.enabled = true;
            if (RightPortal.GetComponent<PortalScript>().PortalCamera.targetTexture != null)
            {
                RightViewTexture.Release();
            }
            RightPortal.GetComponent<PortalScript>().OtherPortal = LeftPortal;
            //Set the View Texture Resolution to the Screens
            RightViewTexture = new RenderTexture(Screen.width, Screen.height, 0);
            //Render the Portals Camera to the View Texture
            RightPortal.GetComponent<PortalScript>().PortalCamera.targetTexture = RightViewTexture;
            //set the Other Portals Material Texture
            LeftPortal.GetComponent<Renderer>().material = LeftViewMat;
            LeftPortal.GetComponent<Renderer>().material.SetTexture("_MainTex", RightViewTexture);
            LeftPortal.GetComponent<Renderer>().material.SetTexture("_BaseMap", RightViewTexture);


        }
        else
        {
            if (LeftPortal.GetComponent<PortalScript>().HasBeenFired)
            {
                

                if (LeftPortal.GetComponent<PortalScript>().PortalCamera.targetTexture != null)
                {
                    //release the memory
                    LeftViewTexture.Release();
                }
                LeftPortal.GetComponent<PortalScript>().OtherPortal = RightPortal;
                //Set the View Texture Resolution to the Screens
                LeftViewTexture = new RenderTexture(Screen.width, Screen.height, 0);
                //Render the Portals Camera to the View Texture
                LeftPortal.GetComponent<PortalScript>().PortalCamera.targetTexture = LeftViewTexture;
                //set the Other Portals Material Texture
                RightPortal.GetComponent<Renderer>().material = LeftViewMat;
                //Set the materials main texture
                RightPortal.GetComponent<Renderer>().material.SetTexture("_MainTex", LeftViewTexture);
                //set the Materials Base Map
                RightPortal.GetComponent<Renderer>().material.SetTexture("_BaseMap", LeftViewTexture);
            }
            if (RightPortal.GetComponent<PortalScript>().HasBeenFired)
            {
                if (RightPortal.GetComponent<PortalScript>().PortalCamera.targetTexture != null)
                {
                    RightViewTexture.Release();
                }
                RightPortal.GetComponent<PortalScript>().OtherPortal = LeftPortal;
                //Set the View Texture Resolution to the Screens
                RightViewTexture = new RenderTexture(Screen.width, Screen.height, 0);
                //Render the Portals Camera to the View Texture
                RightPortal.GetComponent<PortalScript>().PortalCamera.targetTexture = RightViewTexture;
                //set the Other Portals Material Texture
                LeftPortal.GetComponent<Renderer>().material = RightViewMat;
                LeftPortal.GetComponent<Renderer>().material.SetTexture("_MainTex", RightViewTexture);
                LeftPortal.GetComponent<Renderer>().material.SetTexture("_BaseMap", RightViewTexture);
            }
        }
    }

    #endregion


    #region PlayerMovement Functions

    public void PlayerJump(InputAction.CallbackContext obj)
    {

        

    }
    
    public void PlayerMove(Vector2 directions)
    {

        if (Direction != Vector3.zero)
        {
            //get the target angle
            //pass in X first then Y Because we want to turn from the players forward not unitys
            float Target = Mathf.Atan2(Direction.x, Direction.z) * Mathf.Rad2Deg + FPSCamera.transform.eulerAngles.y;
            //set the movement direction
            Vector3 movementDir = Quaternion.Euler(0.0f, Target, 0.0f) * Vector3.forward;

            rb.MovePosition(rb.position + (movementDir * Speed * Time.deltaTime));

              
        }
    }

    public void FireLeftPortal()
    {
        Debug.Log("BUTTON PRESSED");
        //bit shift the layermask
        int layermask = 1 << 8;
        //int layermask = 10;
        layermask = ~layermask;

        Ray ray = new Ray(FPSCamera.transform.position, FPSCamera.transform.forward);

        //raycast from the camera using the cameras forward
        //check what the raycast hit
        //if it hit a surface that can have portals then spawn a portal
        if (Physics.Raycast(ray, out RaycastHit hit, 100.0f, LayerToHit))
        {
            if (LeftPortal != null)
            {
                Destroy(LeftPortal);
            }

            //instantiate the Left Portal
            LeftPortal = Instantiate(PortalPrefab);
            FPSCamera.GetComponent<CameraController>().LeftP = LeftPortal;
            //set the gameobjects name
            LeftPortal.name = "Left_Portal";
            //Set Player Variable
            LeftPortal.GetComponent<PortalScript>().Player = this.gameObject;
            LeftPortal.GetComponent<PortalScript>().PlayerTransform = this.transform;
            //Set Portal Side
            LeftPortal.GetComponent<PortalScript>().portalSide = Portal_Side.Left;
            //set the Left Portal rotation to the opposite direction of the RaycastHits Normal vector3
            LeftPortal.transform.rotation = Quaternion.LookRotation(-hit.normal) ;
            //Set the Left Portal position using the hit information ( Takes the position hit in world space and adds an amplified normal vector to it)
            LeftPortal.transform.position = hit.point + (hit.normal * 0.01f);
            //set the Portal Camera rotation
            LeftPortal.GetComponent<PortalScript>().PortalCamera.transform.rotation = LeftPortal.GetComponent<PortalScript>().CameraSetPos.transform.rotation;

            LeftPortal.GetComponent<PortalScript>().HasBeenFired = true;
            Debug.Log("HIT");
            LeftPortal.GetComponent<PortalScript>().PortalCamera.enabled = false;
        }

        PortalFireCheck();
        
    }
    
    public void FireRightPortal()
    {
        
        //bit shift the layermask
        int layermask = 1 << 8;
        //int layermask = 10;
        layermask = ~layermask;

        Ray ray = new Ray(FPSCamera.transform.position, FPSCamera.transform.forward);

        //raycast from the camera using the cameras forward
        //check what the raycast hit
        //if it hit a surface that can have portals then spawn a portal
        if (Physics.Raycast(ray, out RaycastHit hit, 100.0f, LayerToHit))
        {
            if (RightPortal != null)
            {
                Destroy(RightPortal);
            }

            //Instantiate the right portal
            RightPortal = Instantiate(PortalPrefab);
            FPSCamera.GetComponent<CameraController>().RightP = RightPortal;
            RightPortal.name = "Right_Portal";
            //Set Player Variable
            RightPortal.GetComponent<PortalScript>().Player = this.gameObject;
            RightPortal.GetComponent<PortalScript>().PlayerTransform = this.transform;
            //set Portal Side
            RightPortal.GetComponent<PortalScript>().portalSide = Portal_Side.Right;
            //set the Right Portal rotation to the opposite direction of the RaycastHits Normal vector3
            RightPortal.transform.rotation = Quaternion.LookRotation(-hit.normal);

            RightPortal.transform.position = hit.point + (hit.normal * 0.01f);
            //Set the Portals Camera rotation equal to the right portals CameraSetPos
            RightPortal.GetComponent<PortalScript>().PortalCamera.transform.rotation = RightPortal.GetComponent<PortalScript>().CameraSetPos.transform.rotation;
            //Set that the portal has been fired
            RightPortal.GetComponent<PortalScript>().HasBeenFired = true;
            Debug.Log("HIT");
            RightPortal.GetComponent<PortalScript>().PortalCamera.enabled = false;
        }


        PortalFireCheck();
    }

    public void InteractWithObject()
    {

        if (IsHolding)
        {
            //release held object

            //unfreeze rotation
            HoldingObject.GetComponent<Rigidbody>().useGravity = true;
            HoldingObject.GetComponent<Rigidbody>().freezeRotation = false;
            HoldingObject.GetComponent<Rigidbody>().isKinematic = false;
            //set parent to null
            HoldingObject.transform.parent = null;
            //set holding object to null
            HoldingObject = null;
            
            //set is holding to false
            IsHolding = false;

        }
        else
        {
            //raycast
            //create a raycast
            Ray ray = new Ray(FPSCamera.transform.position, FPSCamera.transform.forward);
            //create a raycast hit
            RaycastHit hit ;

           //cast the ray
            Physics.Raycast(ray, out hit);

            //check what the collider hit
            switch (hit.collider.tag)
            {
                case "PickUp":

                    //Save Object
                    HoldingObject =  hit.rigidbody.gameObject;
                    //Parent the object
                    HoldingObject.transform.parent = FPSCamera.transform;               
                    //set IsHolding to True
                    IsHolding = true;

                    break;
                default:
                    break;
            }
        }
    }

    public void PauseGameFunction()
    {
        //Set UI States
        UIManager.GetComponent<LevelUIManager>().SetGameUIState(false);
        UIManager.GetComponent<LevelUIManager>().SetPauseUIState(true);
        Debug.Log("PauseUI is " + UIManager.GetComponent<LevelUIManager>().GetPauseUIState());
        //Disable Game Control
        PlayerControl.Game_Movement.Disable();
        //Enable Pause Menu
        PlayerControl.Game_PauseMenu.Enable();
        GameIsPaused = true;
        Debug.Log("GameIsPause value is: " + GameIsPaused);
        //set cursor
        Cursor.lockState = CursorLockMode.None;
        //Set Menu Iterators
        CurrentMenuIter = 0;
        MaxMenuIter = 5;
        //update selector UI
        UIManager.GetComponent<LevelUIManager>().SetPauseMenuSelectorState(true, CurrentMenuIter);

    }


    #endregion


    #region PauseMenu Functions

    public void ResumeGame()
    {
        //Set UI States
        UIManager.GetComponent<LevelUIManager>().SetGameUIState(true);
        UIManager.GetComponent<LevelUIManager>().SetPauseUIState(false) ;
        Debug.Log("PauseUI is " + UIManager.GetComponent<LevelUIManager>().GetPauseUIState());
        //Disable Pause Menu
        PlayerControl.Game_PauseMenu.Disable();
        //Enable Game Control
        PlayerControl.Game_Movement.Enable();
        GameIsPaused = false;
        //set cursor
        Cursor.lockState = CursorLockMode.Locked;
        //reset Selector UI
        UIManager.GetComponent<LevelUIManager>().SetPauseMenuSelectorState(false, CurrentMenuIter);
    }

    public void Pause_MoveUpFunction()
    {
        if (GetCurrentMenuIter() == 0)
        {
            //set Previous selector off
            UIManager.GetComponent<LevelUIManager>().SetPauseMenuSelectorState(false, CurrentMenuIter);
            //set CurrentMenuIter Value
            SetCurrentMenuIter(GetMaxMenuIter() - 1);
            //set new Selector on
            UIManager.GetComponent<LevelUIManager>().SetPauseMenuSelectorState(true, CurrentMenuIter);
        }
        else
        {
            UIManager.GetComponent<LevelUIManager>().SetPauseMenuSelectorState(false, CurrentMenuIter);
            SetCurrentMenuIter(GetCurrentMenuIter() - 1);
            UIManager.GetComponent<LevelUIManager>().SetPauseMenuSelectorState(true, CurrentMenuIter);
        }

    }

    public void Pause_MoveDownFunction()
    {
        if (GetCurrentMenuIter() == (GetMaxMenuIter() - 1))
        {
            //set Previous selector off
            UIManager.GetComponent<LevelUIManager>().SetPauseMenuSelectorState(false, CurrentMenuIter);
            //set CurrentMenuIter Value
            SetCurrentMenuIter(0);
            //set new Selector on
            UIManager.GetComponent<LevelUIManager>().SetPauseMenuSelectorState(true, CurrentMenuIter);
        }
        else
        {
            UIManager.GetComponent<LevelUIManager>().SetPauseMenuSelectorState(false, CurrentMenuIter);
            SetCurrentMenuIter(GetCurrentMenuIter() + 1);
            UIManager.GetComponent<LevelUIManager>().SetPauseMenuSelectorState(true, CurrentMenuIter);
        }


    }

    public void ReturnToMainMenu()
    {

        LoadNewSceneAsync(0);
        //SceneManager.LoadScene("MainMenu");

    }

    public void Pause_Selection()
    {

        switch (GetCurrentMenuIter())
        {
            //RESUME GAME
            case 0:
                ResumeGame();
                break;
            //SETTINGS MENU
            case 1:

                break;
            //QUIT GAME (return to menu)
            case 2:
                ReturnToMainMenu();
                break;

             
            default:
                break;
        }

    }

    public void PauseMenuSetSelector(int MenuIter)
    {
        UIManager.GetComponent<LevelUIManager>().SetPauseMenuSelectorState(false, CurrentMenuIter);
        CurrentMenuIter = MenuIter;
        UIManager.GetComponent<LevelUIManager>().SetPauseMenuSelectorState(true, CurrentMenuIter);
    }

    #endregion


    #region Getter/Setter Functions

    public void SetMaxMenuIter(int NewMax)
    {

        MaxMenuIter = NewMax;

    }

    public int GetMaxMenuIter()
    {

        return MaxMenuIter;
    }

    public void SetCurrentMenuIter(int NewCurrent)
    {

        CurrentMenuIter = NewCurrent;

    }

    public int GetCurrentMenuIter()
    {

        return CurrentMenuIter;
    }

    public void SetGameIsPaused(bool state)
    {

        GameIsPaused = state;

    }
    public bool GetGameIsPaused()
    {

        return GameIsPaused;

    }



    #endregion


    #region Holding Functions

    //FixedUpdate
    private void HoldingObjectPositionUpdate()
    {

        //Held Object Position
        if (HoldingObject.transform.localPosition != FPSCamera.GetComponent<CameraController>().HoldingPosition.transform.localPosition)
        {
            //HoldingObject.transform.localPosition = Vector3.MoveTowards(HoldingObject.transform.localPosition, FPSCamera.GetComponent<CameraController>().HoldingPosition.transform.localPosition, 20.0f * Time.deltaTime);
            Vector3 Temp = Vector3.MoveTowards(HoldingObject.transform.position, FPSCamera.GetComponent<CameraController>().HoldingPosition.transform.position, 20.0f * Time.deltaTime);
            HoldingObject.GetComponent<Rigidbody>().MovePosition(Temp);
        }

    }

    //FixedUpdate
    private void HoldingObjectUpdateRigidBody()
    {

        //set Rigidbody parameters
        HoldingObject.GetComponent<Rigidbody>().useGravity = false;
        HoldingObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        HoldingObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;


    }

    //Update
    private void HoldingObjectUpdateLookAt()
    {

        //Held Object Rotation
        HoldingObject.transform.LookAt(this.transform.position);

    }


    private void HoldingObjectRaycast()
    {

        //create a raycast
        Ray ray = new Ray(FPSCamera.transform.position, FPSCamera.transform.forward);
        //create a raycast hit
        RaycastHit hit;
        //LayerToHit = layer;
        LayerToHit = LayerMask.GetMask("Level");
        
        //cast the ray
        //Physics.Raycast(ray.origin, ray.direction, out hit,  4.0f, LayerToHit);
        if (Physics.Raycast(ray.origin, ray.direction, out hit, 3.0f, LayerToHit) || hit.distance > 1.0f)
        {
            Debug.Log("HIT LEVEL");
            FPSCamera.GetComponent<CameraController>().HoldingPosition.transform.localPosition = new Vector3(hit.collider.transform.position.x, hit.collider.transform.position.y, (hit.collider.transform.position.z - 0.1f));
            //HoldingObject.transform.position = 
        }

    }

    #endregion


    #region Load Scene Functions


    public void LoadNewSceneAsync(int Level)
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

    IEnumerator AsyncLoadLevel(int Level)
    {

        AsyncOperation LevelLoad = SceneManager.LoadSceneAsync(Level);
        while (!LevelLoad.isDone)
        {
            //update progress bar
            float Progress = Mathf.Clamp01(LevelLoad.progress / 0.9f);
            UIManager.GetComponent<UIManager>().SetLoadingBarValue(Progress);
            yield return null;
        }
    }

    #endregion



    // Start is called before the first frame update
    void Start()
    {
        UIManager = GameObject.FindGameObjectWithTag("UIManager");
        FPSCamera = GameObject.FindGameObjectWithTag("FPSCamera");

        UIManager.GetComponent<LevelUIManager>().SetGameUIState(true);
        //UIManager.GetComponent<UIManager>().SetPauseUIState(true);
    }

    void Awake()
    {
       PlayerControl = new PlayerInputActionMaps();
    }

    void OnEnable()
    {

        #region PlayerMovement


        ///MOVE///
        //set Move to the Move input from playerInputActionMap
        Move = PlayerControl.Game_Movement.Move;
        //Enable Move input map
        Move.Enable();
        //Ctx has data from the players input
        // I am setting movement to the stored Vector2 that ctx contains
        // Move performed is using this information
        Move.performed += ctx => Movement = ctx.ReadValue<Vector2>();
        Move.canceled += ctx2 => Movement = ctx2.ReadValue<Vector2>();

        ///JUMP///
        //Cache jump to variable
        Jump = PlayerControl.Game_Movement.Jump;
        //Link the performed action of Jump to the Function PlayerJump
        Jump.performed += PlayerJump;
        //Enable Jump
        Jump.Enable();


        ///FIRE LEFT PORTAL///
        FireLeft = PlayerControl.Game_Movement.FireLeftPortal;
        FireLeft.performed += ctx => FireLeftPortal();
        FireLeft.Enable();

        ///FIRE RIGHT PORTAL//
        FireRight = PlayerControl.Game_Movement.FireRightPortal;
        FireRight.performed += ctx => FireRightPortal();
        FireRight.Enable();

        ///PAUSE GAME///
        PauseGame = PlayerControl.Game_Movement.PauseGame;
        PauseGame.performed += ctx => PauseGameFunction();
        PauseGame.Enable();

        ///INTERACT///
        Interact = PlayerControl.Game_Movement.Interact;
        Interact.performed += ctx => InteractWithObject();
        Interact.Enable();


        #endregion

        #region PauseMenu

        Pause_Return = PlayerControl.Game_PauseMenu.Pause_Return;
        Pause_Return.performed += ctx => ResumeGame();
        Pause_Return.Enable();

        Pause_Select = PlayerControl.Game_PauseMenu.Pause_Select;
        Pause_Select.performed += ctx => Pause_Selection();
        Pause_Select.Enable();

        Pause_MoveUp = PlayerControl.Game_PauseMenu.Pause_MoveUp;
        Pause_MoveUp.performed += ctx => Pause_MoveUpFunction();
        Pause_MoveUp.Enable();

        Pause_MoveDown = PlayerControl.Game_PauseMenu.Pause_MoveDown;
        Pause_MoveDown.performed += ctx => Pause_MoveDownFunction();
        Pause_MoveDown.Enable();

        #endregion
    }

    private void OnDisable()
    {
        Move.Disable();
        //do this to prevent memory leaks
        //tells the input system that we arent taking input anymore
        Move.started -= ctx => Movement = ctx.ReadValue<Vector2>();
        Jump.Disable();
        FireLeft.Disable();
        FireRight.Disable();
        Pause_MoveUp.Disable();
        Pause_Return.Disable();
        Pause_Select.Disable();
        Pause_MoveDown.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        
        Direction = new Vector3(Movement.x, 0.0f, Movement.y).normalized;
        Debug.Log("Direction: " + Direction);
        if (IsHolding)
        {
            HoldingObjectUpdateLookAt();
            //HoldingObjectRaycast();
        }
        
    }

    void FixedUpdate()
    {
        //Movement
        if (Direction != Vector3.zero)
        {
            //get the target angle
            //pass in X first then Y Because we want to turn from the players forward not unitys
            float Target = Mathf.Atan2(Direction.x, Direction.z) * Mathf.Rad2Deg + FPSCamera.transform.eulerAngles.y;
            //set the movement direction
            Vector3 movementDir = Quaternion.Euler(0.0f, Target, 0.0f) * Vector3.forward;

            rb.MovePosition(rb.position + (movementDir * Speed * Time.deltaTime));
  
        }

        //ObjectHolding
        if (IsHolding)
        {

            HoldingObjectPositionUpdate();
            HoldingObjectUpdateRigidBody();           
          
        }

    }

}
