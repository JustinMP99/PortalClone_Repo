using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public enum Portal_Type
{
    Blue,
    Orange,
    Red,
    Green
}

public enum Portal_Side
{
    Left,
    Right
}
public class PortalScript : MonoBehaviour
{

 
    [Header ("Portal Objects")]
    public Camera PortalCamera;
    public GameObject CameraSetPos;
    public GameObject OtherPortal;

    [Header("Portal Attributes")]
    public Material PortalMaterial;
    public bool HasBeenFired = false;
    public Portal_Side portalSide;
    public Portal_Type portalType;
    public Vector3 CameraOffSet;

    [Header("Other")]
    public GameObject Player;
    public Transform PlayerTransform;
    public GameObject ReplicatedObject;
    public GameObject TouchingObject;
    private bool UpdateReplicatedObject;
    private bool CheckDot;


    //Private Variables
    private RenderTexture ViewTexture;
    private Transform PlayerCamTransform;

    float dotForTouching;
    float dotForReplicated;
    Vector3 Heading = Vector3.zero;
    Vector3 temp = Vector3.up;

    // Start is called before the first frame update
    void Start()
    {
       


    }
     

    private void Update()
    {
        
        //Check if the Object Is Behind Or In Front Of The Portal
        if(CheckDot)
        {

            Heading = this.transform.position - TouchingObject.transform.position;
            dotForTouching = Vector3.Dot(Heading, -TouchingObject.transform.forward);
            Debug.Log("The Dot Is: " + dotForTouching);

            //If The Touching Object Is In Front Of The Portal, Duplicate the object
            if (dotForTouching > 0)
            {
                
            }
            //If Touching Object Is Behind The Portal
            else if (dotForTouching < 0) 
            { 
            
                //If The Touching Object Is Let Go
                if(!TouchingObject.GetComponent<BasePickup>().GetbeingHeld())
                {

                    StopCheckingTouching();

                }

            }

            if (UpdateReplicatedObject)   
            {
                //Vector3 tempPos = this.transform.InverseTransformPoint(TouchingObject.transform.position);
                //ReplicatedObject.transform.position = new Vector3(OtherPortal.transform.position.x + tempPos.x, OtherPortal.transform.position.y + tempPos.y, OtherPortal.transform.position.z + tempPos.z);
                
                
                Heading.y = -Heading.y;
                ReplicatedObject.transform.rotation = OtherPortal.transform.rotation;
                ReplicatedObject.transform.position = OtherPortal.transform.position;
                ReplicatedObject.transform.Translate(Heading);
       
            }

        }

    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (OtherPortal != null)
        {

            #region Portal Camera Position

            //Get Players Position Relative To Other Portal
            //Transform Position From World Space To Local Space
            //Vector3 tempPos = OtherPortal.transform.InverseTransformPoint(Player.transform.position);
            Vector3 tempPos = OtherPortal.transform.InverseTransformPoint(Player.GetComponent<PlayerController>().FPSCamera.transform.position);

            //Set The Local Position Of The Camera At The Reflected Point On X And Z
            PortalCamera.transform.localPosition = new Vector3(-tempPos.x,tempPos.y, -tempPos.z);

            #endregion

            #region Portal Camera Rotation

            float yAngle = Vector3.SignedAngle(-OtherPortal.transform.forward, Player.transform.forward, Vector3.up);
            float xAngle = Player.GetComponent<PlayerController>().FPSCamera.transform.rotation.eulerAngles.x;
            //xAngle = Mathf.Clamp(xAngle, -90.0f, 90.0f);
            //xAngle = -xAngle;
            PortalCamera.transform.localRotation = Quaternion.Euler(xAngle, yAngle, 0.0f);


            ////rotate the other portals Camera Based on the angle between the player and this portal
            ////Get the angle between the player and this portal ( This angle will be signed so it knows what direction to turn)
            //float Yangle = Vector3.SignedAngle(this.transform.forward, PlayerTransform.forward, Vector3.up);
            //Debug.Log("The Angle between the Player and the " + portalSide + " Portal is " + Yangle);

            ////Get the Angle to look up
            //float Xangle = Player.GetComponent<PlayerController>().FPSCamera.transform.rotation.eulerAngles.x;
            ////Xangle = Mathf.Clamp(Xangle, -90.0f, 90.0f);
            //Xangle = -Xangle;
            //Debug.Log("The X Angle between the Player and the " + portalSide + " Portal is " + Xangle);

            ////creates a quaternion that stores an angle rotation around a specified axis ( the Y-axis in this case)
            //Quaternion PortalRotationYDiff = Quaternion.AngleAxis(Yangle, Vector3.up);

            ////create a quaternion the stores the angle rotation around a specified axis ( the X-axis in this case)
            //Quaternion PortalRotationXDiff = Quaternion.AngleAxis(Xangle, Vector3.right);

            ////Set the other portals local Camera rotation to the result of itself multiplied by the portalRotationDiff
            //OtherPortal.GetComponent<PortalScript>().PortalCamera.transform.localRotation = PortalRotationYDiff * PortalRotationXDiff * OtherPortal.GetComponent<PortalScript>().CameraSetPos.transform.localRotation;



            #endregion


            #region Create Oblique Projection Matrix

            //Plane P = new Plane(-OtherPortal.transform.forward, OtherPortal.transform.position);
            Plane P = new Plane(-this.transform.forward, this.transform.position);
            Vector4 clipPlaneWorldSpace = new Vector4(P.normal.x, P.normal.y, P.normal.z, P.distance);
            Vector4 clipPlaneToCamera = Matrix4x4.Transpose(Matrix4x4.Inverse(PortalCamera.worldToCameraMatrix)) * clipPlaneWorldSpace;

            var newMatrix = Player.GetComponent<PlayerController>().FPSCamera.GetComponent<Camera>().CalculateObliqueMatrix(clipPlaneToCamera);
            PortalCamera.projectionMatrix = newMatrix;
            
            

            #endregion

        }

        #region MY PAIN AND SUFFERING THAT RESULTED IN NOTHING


        //if (OtherPortal != null)
        //{

        //    #region CameraPosition

        //    //=================================================================================================================================================================
        //    //update Camera
        //    //reset camera position
        //    PortalCamera.transform.localPosition = Vector3.zero;
        //    //get z distance 
        //    float ZDistance = Vector3.Distance(OtherPortal.transform.position, Player.transform.position);
        //    //float XDistance = OtherPortal.transform.position.x - PlayerTransform.position.x;
        //    //float YDistance = OtherPortal.transform.position.y - PlayerTransform.position.y;
        //    float XDistance = 0.0f;
        //    float YDistance = 0.0f;

        //    //set the Right portals Camera by the result of itself - RightZDistance
        //    Vector3 Temp = Vector3.zero;
        //    //Temp.z is negative because of the portal positioning

        //    Temp.x = -(PortalCamera.transform.localPosition.x - XDistance);
        //    //Temp.y = (PortalCamera.transform.localPosition.y - YDistance);
        //    Temp.y = 0.0f;
        //    Temp.z = -(PortalCamera.transform.localPosition.z - ZDistance);
        //    PortalCamera.transform.localPosition = Temp;



        //    ///METHOD B
        //    //=================================================================================================================================================================
        //    //PortalCamera.transform.position = CameraSetPos.transform.position;
        //    //Vector3 offset = Player.transform.position - OtherPortal.transform.position;


        //    //PortalCamera.transform.Translate(offset);
        //    //=================================================================================================================================================================
        //    #endregion


        //    #region CameraRotation

        //    //float AngularDifference = Quaternion.Angle(this.transform.rotation, OtherPortal.transform.rotation);
        //    //Quaternion PortalRotationDiff = Quaternion.AngleAxis(AngularDifference, Vector3.up);
        //    //Vector3 NewCameraDir = PortalRotationDiff * Player.GetComponent<PlayerController>().FPSCamera.transform.forward;
        //    //NewCameraDir.y = -NewCameraDir.y;
        //    //PortalCamera.transform.rotation = Quaternion.LookRotation(-NewCameraDir, Vector3.up);


        //    //POSSIBLE SOLUTION


        //    //==============================================================================================================================================================================================================================
        //    // WORKS BUT LACKS X axis rotation
        //    //==============================================================================================================================================================================================================================

        //    ////rotate the other portals Camera Based on the angle between the player and this portal
        //    //float angle = Quaternion.Angle(this.transform.rotation, PlayerTransform.rotation);

        //    ////creates a quaternion that stores an angle rotation around a specified axis ( the Y-axis in this case)
        //    //Quaternion PortalRotationYDiff = Quaternion.AngleAxis(angle, Vector3.up);

        //    ////Quaternion PortalRotationXDiff = Quaternion.AngleAxis(angle, Vector3.right);

        //    ////Set the other portals local Camera rotation to the result of itself multiplied by the portalRotationDiff
        //    //OtherPortal.GetComponent<PortalScript>().PortalCamera.transform.localRotation = PortalRotationYDiff * OtherPortal.GetComponent<PortalScript>().CameraSetPos.transform.localRotation;


        //    //==============================================================================================================================================================================================================================


        //    //==============================================================================================================================================================================================================================
        //    // ----WORKING---- METHOD A
        //    //==============================================================================================================================================================================================================================

        //    //rotate the other portals Camera Based on the angle between the player and this portal
        //    //Get the angle between the player and this portal ( This angle will be signed so it knows what direction to turn)
        //    float Yangle = Vector3.SignedAngle(this.transform.forward, PlayerTransform.forward, Vector3.up);
        //    Debug.Log("The Angle between the Player and the " + portalSide + " Portal is " + Yangle);

        //    //Get the Angle to look up
        //    float Xangle = Player.GetComponent<PlayerController>().FPSCamera.transform.rotation.eulerAngles.x;
        //    //Xangle = Mathf.Clamp(Xangle, -90.0f, 90.0f);
        //    Xangle = -Xangle;
        //    Debug.Log("The X Angle between the Player and the " + portalSide + " Portal is " + Xangle);

        //    //creates a quaternion that stores an angle rotation around a specified axis ( the Y-axis in this case)
        //    Quaternion PortalRotationYDiff = Quaternion.AngleAxis(Yangle, Vector3.up);

        //    //create a quaternion the stores the angle rotation around a specified axis ( the X-axis in this case)
        //    Quaternion PortalRotationXDiff = Quaternion.AngleAxis(Xangle, Vector3.right);

        //    //Set the other portals local Camera rotation to the result of itself multiplied by the portalRotationDiff
        //    OtherPortal.GetComponent<PortalScript>().PortalCamera.transform.localRotation = PortalRotationYDiff * PortalRotationXDiff * OtherPortal.GetComponent<PortalScript>().CameraSetPos.transform.localRotation;

        //    //==============================================================================================================================================================================================================================



        //    ///METHOD B
        //    //=================================================================================================================================================================
        //    ////rotate the other portals Camera Based on the angle between the player and this portal
        //    ////Get the angle between the player and this portal ( This angle will be signed so it knows what direction to turn)
        //    //float Yangle = Vector3.SignedAngle(this.transform.forward, PlayerTransform.forward, Vector3.up);
        //    //Debug.Log("The Angle between the Player and the " + portalSide + " Portal is " + Yangle);

        //    ////Get the Angle to look up
        //    //float Xangle = Player.GetComponent<PlayerController>().FPSCamera.transform.rotation.eulerAngles.x;
        //    ////Xangle = Mathf.Clamp(Xangle, -90.0f, 90.0f);
        //    //Xangle = -Xangle;
        //    //Debug.Log("The X Angle between the Player and the " + portalSide + " Portal is " + Xangle);

        //    ////creates a quaternion that stores an angle rotation around a specified axis ( the Y-axis in this case)
        //    //Quaternion PortalRotationYDiff = Quaternion.AngleAxis(Yangle, Vector3.up);

        //    ////create a quaternion the stores the angle rotation around a specified axis ( the X-axis in this case)
        //    //Quaternion PortalRotationXDiff = Quaternion.AngleAxis(Xangle, Vector3.right);

        //    ////Set the other portals local Camera rotation to the result of itself multiplied by the portalRotationDiff
        //    //OtherPortal.GetComponent<PortalScript>().PortalCamera.transform.localRotation = OtherPortal.GetComponent<PortalScript>().CameraSetPos.transform.localRotation;
        //    //OtherPortal.GetComponent<PortalScript>().PortalCamera.transform.localRotation = OtherPortal.GetComponent<PortalScript>().PortalCamera.transform.localRotation * PortalRotationYDiff * PortalRotationXDiff;





        //    #endregion


        //}


        #endregion

    }

    public void CreateReplicatedObject(GameObject objectToReplicate)
    {
        ReplicatedObject = Instantiate(TouchingObject.gameObject);
        ReplicatedObject.GetComponent<Rigidbody>().isKinematic = false;
        ReplicatedObject.GetComponent<Rigidbody>().useGravity = true;
        ReplicatedObject.GetComponent<BasePickup>().SetbeingHeld(false);
        ReplicatedObject.tag = "OtherPortalObject";
        SetUpdateReplicatedObject(true);
    }

    public void StopCheckingTouching()
    {

        //Stop Checking Dot Product Of Touching Object
        SetCheckDotState(false);
        //Stop Updating Replicated Object
        SetUpdateReplicatedObject(false);
        //Destroy Touching
        Destroy(TouchingObject);
        //Set Touching Object To Null
        TouchingObject = null;
       
        //Set Replicate Object Tag To "HoldingObject"
        ReplicatedObject.tag = "HoldingObject";
        //Set Replicated Object To Null
        ReplicatedObject = null;
    }

    #region Getters

    public bool GetUpdateReplicatedObject()
    {

        return UpdateReplicatedObject;

    }

    public bool GetCheckDotState()
    {
        return CheckDot;
    }

    public float GetTouchingDot()
    {
        return dotForTouching;
    }

    #endregion

    #region Setters

    public void SetUpdateReplicatedObject(bool state)
    {
        UpdateReplicatedObject = state;
    }

    public void SetCheckDotState(bool state)
    {
        CheckDot = state;
    }

    #endregion

}
