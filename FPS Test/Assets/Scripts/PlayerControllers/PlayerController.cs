using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;


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
    private InputAction Pause;

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
    //[SerializeField]
    //private GameObject UIManager;
    [SerializeField]
    private GameObject levelManager;

    public GameObject FPSCamera;
    public GameObject CameraSetPosition;



    public GameObject PortalPrefab;
    public GameObject LeftPortal;
    public GameObject RightPortal;
    #endregion

    #region Menu Variables

    [SerializeField]
    private int CurrentMenuIter = 0;
    [SerializeField]
    private int MaxMenuIter = 3;
    private bool GameIsPaused = false;

    #endregion

    #region Interaction Variables

    private bool IsHolding = false;
    private GameObject HoldingObject;
    private float _distanceTraveled;
    private float _totalDistance;
    private float _currentAmountTraveled;
    private float _startTime;
    private Vector3 _holdForceMult = new Vector3(5.0f, 5.0f, 5.0f);


    #endregion

    #region RenderTexture Variables
    public Material LeftViewMat;
    public Material RightViewMat;

    private RenderTexture LeftViewTexture;
    private RenderTexture RightViewTexture;
    #endregion

    private bool IsInPortal;

    //int layer = 10;
    public LayerMask LayerToHit;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (IsHolding)
        {
            HoldingObjectUpdateLookAt();
        }

    }

    void FixedUpdate()
    {

        PlayerMove(Movement);

        //ObjectHolding
        if (IsHolding)
        {

            HoldingObjectPositionUpdate();
            HoldingObjectUpdateRigidBody();

        }

    }

    void Awake()
    {
        PlayerControl = new PlayerInputActionMaps();
    }

    void OnEnable()
    {

        PlayerControl.Game_Movement.Enable();
        PlayerControl.Game_PauseMenu.Disable();

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
        Pause = PlayerControl.Game_Movement.PauseGame;
        Pause.performed += ctx => PauseGame();
        Pause.Enable();

        ///INTERACT///
        Interact = PlayerControl.Game_Movement.Interact;
        Interact.performed += ctx => InteractFunction();
        Interact.Enable();


        #endregion

        #region PauseMenu

        Pause_Return = PlayerControl.Game_PauseMenu.Pause_Return;
        Pause_Return.performed += ctx => ResumeGame();
        Pause_Return.Enable();

        //Pause_Select = PlayerControl.Game_PauseMenu.Pause_Select;
        //Pause_Select.performed += ctx => Pause_Selection();
        //Pause_Select.Enable();

        //Pause_MoveUp = PlayerControl.Game_PauseMenu.Pause_MoveUp;
        //Pause_MoveUp.performed += ctx => Pause_MoveUpFunction();
        //Pause_MoveUp.Enable();

        //Pause_MoveDown = PlayerControl.Game_PauseMenu.Pause_MoveDown;
        //Pause_MoveDown.performed += ctx => Pause_MoveDownFunction();
        //Pause_MoveDown.Enable();

        #endregion

    }

    private void OnDisable()
    {

        //do this to prevent memory leaks
        //tells the input system that we arent taking input anymore
        #region Movement Map Disable

        Move.performed -= ctx => Movement = ctx.ReadValue<Vector2>(); ;
        Move.Disable();

        Jump.performed -= PlayerJump;
        Jump.Disable();

        FireLeft.performed -= ctx => FireLeftPortal();
        FireLeft.Disable();

        FireRight.performed -= ctx => FireRightPortal();
        FireRight.Disable();

        Interact.performed -= ctx => InteractFunction();
        Interact.Disable();

        #endregion
        //Movement Input Maps

        #region Pause Map Disable

        //Pause Input Maps        
        //Pause.performed -= ctx => PauseGame();
        //Pause.Disable();

        //Pause_Return.performed -= ctx => ResumeGame();
        //Pause_Return.Disable();

        //Pause_Select.performed -= ctx => Pause_Selection();
        //Pause_Select.Disable();

        //Pause_MoveUp.performed -= ctx => Pause_MoveUpFunction();
        //Pause_MoveUp.Disable();

        //Pause_MoveDown.performed -= ctx => Pause_MoveDownFunction();
        //Pause_MoveDown.Disable();

        #endregion


        PlayerControl.Game_Movement.Disable();
        PlayerControl.Game_PauseMenu.Disable();
        
    }

    #region Other Functions

    public void PortalFireCheck()
    {


        if (LeftPortal != null && RightPortal != null)
        {
            //update Render Texture
            //LEFT PORTAL
            //set materials
            //Left Portal
            //if the portal has not rendered...
            LeftPortal.GetComponent<PortalScript>().PortalCamera.enabled = true;
            LeftPortal.GetComponent<PortalScript>().PortalCamera.transform.position = LeftPortal.GetComponent<PortalScript>().CameraSetPos.transform.position;
            if (LeftPortal.GetComponent<PortalScript>().PortalCamera.targetTexture != null)
            {
                //release the memory
                LeftViewTexture.Release();
            }
            LeftPortal.GetComponent<PortalScript>().OtherPortal = RightPortal;
            //Set the View Texture Resolution to the Screens
            LeftViewTexture = new RenderTexture(Screen.width, Screen.height, 10);
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
            RightPortal.GetComponent<PortalScript>().PortalCamera.transform.position = RightPortal.GetComponent<PortalScript>().CameraSetPos.transform.position;
            if (RightPortal.GetComponent<PortalScript>().PortalCamera.targetTexture != null)
            {
                RightViewTexture.Release();
            }
            RightPortal.GetComponent<PortalScript>().OtherPortal = LeftPortal;
            //Set the View Texture Resolution to the Screens
            RightViewTexture = new RenderTexture(Screen.width, Screen.height, 10);
            //Render the Portals Camera to the View Texture
            RightPortal.GetComponent<PortalScript>().PortalCamera.targetTexture = RightViewTexture;
            //set the Other Portals Material Texture
            LeftPortal.GetComponent<Renderer>().material = LeftViewMat;
            LeftPortal.GetComponent<Renderer>().material.SetTexture("_MainTex", RightViewTexture);
            LeftPortal.GetComponent<Renderer>().material.SetTexture("_BaseMap", RightViewTexture);

        }
        else
        {
            if (LeftPortal != null)
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
                ////set the Other Portals Material Texture
                //RightPortal.GetComponent<Renderer>().material = LeftViewMat;
                ////Set the materials main texture
                //RightPortal.GetComponent<Renderer>().material.SetTexture("_MainTex", LeftViewTexture);
                ////set the Materials Base Map
                //RightPortal.GetComponent<Renderer>().material.SetTexture("_BaseMap", LeftViewTexture);
            }
            if (RightPortal != null)
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
                //LeftPortal.GetComponent<Renderer>().material = RightViewMat;
                //LeftPortal.GetComponent<Renderer>().material.SetTexture("_MainTex", RightViewTexture);
                //LeftPortal.GetComponent<Renderer>().material.SetTexture("_BaseMap", RightViewTexture);
            }
        }


        //if (LeftPortal.GetComponent<PortalScript>().HasBeenFired && RightPortal.GetComponent<PortalScript>().HasBeenFired || LeftPortal != null && RightPortal != null)
        //{
        //    //update Render Texture
        //    //LEFT PORTAL
        //    //set materials
        //    //Left Portal
        //    //if the portal has not rendered...
        //    LeftPortal.GetComponent<PortalScript>().PortalCamera.enabled = true;
        //    LeftPortal.GetComponent<PortalScript>().PortalCamera.transform.position = LeftPortal.GetComponent<PortalScript>().CameraSetPos.transform.position;
        //    if (LeftPortal.GetComponent<PortalScript>().PortalCamera.targetTexture != null)
        //    {
        //        //release the memory
        //        LeftViewTexture.Release();
        //    }
        //    LeftPortal.GetComponent<PortalScript>().OtherPortal = RightPortal;
        //    //Set the View Texture Resolution to the Screens
        //    LeftViewTexture = new RenderTexture(Screen.width, Screen.height, 10);
        //    //Render the Portals Camera to the View Texture
        //    LeftPortal.GetComponent<PortalScript>().PortalCamera.targetTexture = LeftViewTexture;
        //    //set the Other Portals Material Texture
        //    RightPortal.GetComponent<Renderer>().material = RightViewMat;
        //    //Set the materials main texture
        //    RightPortal.GetComponent<Renderer>().material.SetTexture("_MainTex", LeftViewTexture);
        //    //set the Materials Base Map
        //    RightPortal.GetComponent<Renderer>().material.SetTexture("_BaseMap", LeftViewTexture);

        //    //RIGHT PORTAL
        //    //update the material
        //    //Right Portal
        //    RightPortal.GetComponent<PortalScript>().PortalCamera.enabled = true;
        //    RightPortal.GetComponent<PortalScript>().PortalCamera.transform.position = RightPortal.GetComponent<PortalScript>().CameraSetPos.transform.position;
        //    if (RightPortal.GetComponent<PortalScript>().PortalCamera.targetTexture != null)
        //    {
        //        RightViewTexture.Release();
        //    }
        //    RightPortal.GetComponent<PortalScript>().OtherPortal = LeftPortal;
        //    //Set the View Texture Resolution to the Screens
        //    RightViewTexture = new RenderTexture(Screen.width, Screen.height, 10);
        //    //Render the Portals Camera to the View Texture
        //    RightPortal.GetComponent<PortalScript>().PortalCamera.targetTexture = RightViewTexture;
        //    //set the Other Portals Material Texture
        //    LeftPortal.GetComponent<Renderer>().material = LeftViewMat;
        //    LeftPortal.GetComponent<Renderer>().material.SetTexture("_MainTex", RightViewTexture);
        //    LeftPortal.GetComponent<Renderer>().material.SetTexture("_BaseMap", RightViewTexture);


        //}

    }

    #endregion

    #region PlayerMovement Functions

    public void PlayerJump(InputAction.CallbackContext obj)
    {

        rb.AddForce(Vector3.up * 5.0f, ForceMode.Impulse);

    }

    public void PlayerMove(Vector2 directions)
    {
        //Direction = new Vector3(directions.x, 0.0f, directions.y).normalized;
        Direction.x = directions.x;
        Direction.y = 0.0f;
        Direction.z = directions.y;
        Direction = Direction.normalized;
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

        if (!IsHolding)
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
                LeftPortal.transform.rotation = Quaternion.LookRotation(-hit.normal);
                //Set the Left Portal position using the hit information ( Takes the position hit in world space and adds an amplified normal vector to it)
                LeftPortal.transform.position = hit.point + (hit.normal * 0.01f);
                
                //set the Portal Camera rotation
                LeftPortal.GetComponent<PortalScript>().PortalCamera.transform.rotation = LeftPortal.GetComponent<PortalScript>().CameraSetPos.transform.rotation;

                LeftPortal.GetComponent<PortalScript>().HasBeenFired = true;

                LeftPortal.GetComponent<PortalScript>().PortalCamera.enabled = false;
            }

            PortalFireCheck();

        }

    }

    public void FireRightPortal()
    {
        if (!IsHolding)
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
                RightPortal.GetComponent<PortalScript>().PortalCamera.enabled = false;
            }


            PortalFireCheck();
        }

    }

    public void InteractFunction()
    {

        if (IsHolding)
        {
            //release held object

            //unfreeze rotation
            HoldingObject.GetComponent<Rigidbody>().useGravity = true;
            HoldingObject.GetComponent<Rigidbody>().freezeRotation = false;
            HoldingObject.GetComponent<Rigidbody>().isKinematic = false;
            //Set 
            HoldingObject.GetComponent<BasePickup>().SetbeingHeld(false);
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
            RaycastHit hit;

            //cast the ray
            Physics.Raycast(ray, out hit);

            //check what the collider hit
            switch (hit.collider.tag)
            {
                case "HoldingObject":

                    //Save Object
                    HoldingObject = hit.rigidbody.gameObject;
                    //HoldingObject.GetComponent<Rigidbody>().isKinematic = true;
                    //Parent the object
                    //HoldingObject.transform.parent = FPSCamera.transform;
                    //Set That The Object Is Being Held
                    HoldingObject.GetComponent<BasePickup>().SetbeingHeld(true);
                    //set IsHolding to True
                    IsHolding = true;

                    //set StartTime
                    _startTime = Time.time;
                    //Get Total Distance
                    _totalDistance = Vector3.Distance(HoldingObject.transform.position, FPSCamera.GetComponent<CameraController>().HoldingPosition.transform.position);

                    break;
                default:
                    break;
            }
        }
    }

    public void PauseGame()
    {

        //Call LevelManager Pause Function
        levelManager.GetComponent<LevelManager>().PauseGame();

    }

    #endregion

    #region PauseMenu Functions

    public void ResumeGame()
    {
        // Call The LevelManagers ResumeGame Function
        levelManager.GetComponent<LevelManager>().ResumeGame();
    }

    public void Pause_MoveUpFunction()
    {
        levelManager.GetComponent<LevelManager>().PauseMoveUp();

    }

    public void Pause_MoveDownFunction()
    {

        levelManager.GetComponent<LevelManager>().PauseMoveDown();

    }

    public void ReturnToMainMenu()
    {
        levelManager.GetComponent<LevelManager>().ToMainMenu();
    }

    public void Pause_Selection()
    {

        levelManager.GetComponent<LevelManager>().PauseSelect();

    }

    //public void PauseMenuSetSelector(int MenuIter)
    //{
    //    UIManager.GetComponent<LevelUIManager>().SetPauseMenuSelectorState(false, CurrentMenuIter);
    //    CurrentMenuIter = MenuIter;
    //    UIManager.GetComponent<LevelUIManager>().SetPauseMenuSelectorState(true, CurrentMenuIter);
    //}

    #endregion

    #region SettingsMenu Functions

    public void OpenSettings()
    {
        levelManager.GetComponent<LevelManager>().OpenOptions();

    }
    public void CloseSettings()
    {

        levelManager.GetComponent<LevelManager>().CloseSettings();
    }

    public void ApplySettings()
    {
        levelManager.GetComponent<LevelManager>().SetNewSettings();
    }

    public void MoveSettingsSelectorUp()
    {
        levelManager.GetComponent<LevelManager>().SettingsMoveUp();
    }

    public void MoveSettingsSelectorDown()
    {

        levelManager.GetComponent<LevelManager>().SettingsMoveDown();

    }

    public void SelectSettingsOption()
    {



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

    public bool GetIsInPortal()
    {
        return IsInPortal;
    }

    public void SetIsInPortal(bool state)
    {
        IsInPortal = state;
    }


    #endregion

    #region Holding Functions

    //FixedUpdate
    private void HoldingObjectPositionUpdate()
    {

        //Held Object Position
        if (HoldingObject.transform.localPosition != FPSCamera.GetComponent<CameraController>().HoldingPosition.transform.localPosition)
        {

            Vector3 directionToHoldPOS = (FPSCamera.GetComponent<CameraController>().HoldingPosition.transform.position - HoldingObject.transform.position).normalized;
            Vector3 holdForce;
            holdForce.x = directionToHoldPOS.x * _holdForceMult.x;
            holdForce.y = directionToHoldPOS.y * _holdForceMult.y;
            holdForce.z = directionToHoldPOS.z * _holdForceMult.z;

            float distanceToHold = Vector3.Distance(FPSCamera.GetComponent<CameraController>().HoldingPosition.transform.position, HoldingObject.transform.position);
            distanceToHold = Mathf.Clamp(distanceToHold, 0.0f, 1.0f);
            holdForce *= distanceToHold;

            HoldingObject.GetComponent<Rigidbody>().AddForce(holdForce, ForceMode.VelocityChange);

            //_distanceTraveled = (Time.time - _startTime) * 0.5f;

            //_currentAmountTraveled = _distanceTraveled / _totalDistance;

            //Vector3 Temp = Vector3.Lerp(HoldingObject.transform.position, FPSCamera.GetComponent<CameraController>().HoldingPosition.transform.position, _currentAmountTraveled);
            //HoldingObject.GetComponent<Rigidbody>().MovePosition(Temp);



            //HoldingObject.transform.localPosition = Vector3.MoveTowards(HoldingObject.transform.localPosition, FPSCamera.GetComponent<CameraController>().HoldingPosition.transform.localPosition, 20.0f * Time.deltaTime);
            //Vector3 Temp = Vector3.MoveTowards( 20.0f * Time.deltaTime);
            //HoldingObject.GetComponent<Rigidbody>().MovePosition(Temp);
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
        Vector3 lookAt = this.transform.position;
        //This Allows The Object To Move Up And Down Without Rotating Up And Down
        lookAt.y = HoldingObject.transform.position.y ;
        HoldingObject.transform.LookAt(lookAt, Vector3.up);
       
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

    #region Enable/Disable Control Maps

    public void EnablePlayerMovementMap()
    {
        PlayerControl.Game_Movement.Enable();
    }
    public void DisablePlayerMovementMap()
    {
        PlayerControl.Game_Movement.Disable();
    }


    public void EnablePauseControlMap()
    {
        PlayerControl.Game_PauseMenu.Enable();
    }

    public void DisablePauseControlMap()
    {
        PlayerControl.Game_PauseMenu.Disable();

    }





    #endregion

}
