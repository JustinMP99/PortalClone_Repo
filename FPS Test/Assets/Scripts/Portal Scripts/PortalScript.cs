using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

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
            //If The Object Is Behind The Portal, Spawn The Duplicate Object
            if (dotForTouching < 0)
            {
                //Spawn Duplicate 

                //Set CheckDot To False

                //Set UpdateDuplicateObject To True

            }

        }


        if (UpdateReplicatedObject) 
        {

            //If Dot Product Is Greater Than 0, The Object Is In Front Or Facing The Same Direction
            //If Dot Product Is 0, Then The Object Is Perpendicular
            //If Dot Product Is Less Than 0, Then The Object Is Behind Or Facing The Opposite Direction
            
            Heading.y = -Heading.y;
          
            //Set The Duplicated Object To The OtherPortals Position
           
            //Replicated Object Transform
            ReplicatedObject.transform.position = OtherPortal.transform.position;
            ReplicatedObject.transform.Translate(Heading);

            //Replicated Object Rotation
            
            ReplicatedObject.transform.rotation = OtherPortal.transform.localRotation; 


        }

    }

    // Update is called once per frame
    void LateUpdate()
    {

        if (OtherPortal != null)
        {

            #region CameraPosition

            //=================================================================================================================================================================
            //update Camera
            //reset camera position
            //PortalCamera.transform.localPosition = Vector3.zero;
            ////get z distance 
            //float ZDistance = Vector3.Distance(OtherPortal.transform.position, Player.transform.position);
            ////float XDistance = OtherPortal.transform.position.x - PlayerTransform.position.x;
            ////float YDistance = OtherPortal.transform.position.y - PlayerTransform.position.y;
            //float XDistance = 0.0f;
            //float YDistance = 0.0f;

            ////set the Right portals Camera by the result of itself - RightZDistance
            //Vector3 Temp = Vector3.zero;
            ////Temp.z is negative because of the portal positioning

            //Temp.x = -(PortalCamera.transform.localPosition.x - XDistance);
            ////Temp.y = (PortalCamera.transform.localPosition.y - YDistance);
            //Temp.y = 0.0f;
            //Temp.z = -(PortalCamera.transform.localPosition.z - ZDistance);
            //PortalCamera.transform.localPosition = Temp;



            ///
            //=================================================================================================================================================================
            PortalCamera.transform.position = CameraSetPos.transform.position;
            Vector3 offset = Player.transform.position - OtherPortal.transform.position;


            PortalCamera.transform.Translate(offset);
            //=================================================================================================================================================================
            #endregion


            #region CameraRotation

            //float AngularDifference = Quaternion.Angle(this.transform.rotation, OtherPortal.transform.rotation);
            //Quaternion PortalRotationDiff = Quaternion.AngleAxis(AngularDifference, Vector3.up);
            //Vector3 NewCameraDir = PortalRotationDiff * Player.GetComponent<PlayerController>().FPSCamera.transform.forward;
            //NewCameraDir.y = -NewCameraDir.y;
            //PortalCamera.transform.rotation = Quaternion.LookRotation(-NewCameraDir, Vector3.up);


            //POSSIBLE SOLUTION


            //==============================================================================================================================================================================================================================
            // WORKS BUT LACKS X axis rotation
            //==============================================================================================================================================================================================================================

            ////rotate the other portals Camera Based on the angle between the player and this portal
            //float angle = Quaternion.Angle(this.transform.rotation, PlayerTransform.rotation);

            ////creates a quaternion that stores an angle rotation around a specified axis ( the Y-axis in this case)
            //Quaternion PortalRotationYDiff = Quaternion.AngleAxis(angle, Vector3.up);

            ////Quaternion PortalRotationXDiff = Quaternion.AngleAxis(angle, Vector3.right);

            ////Set the other portals local Camera rotation to the result of itself multiplied by the portalRotationDiff
            //OtherPortal.GetComponent<PortalScript>().PortalCamera.transform.localRotation = PortalRotationYDiff * OtherPortal.GetComponent<PortalScript>().CameraSetPos.transform.localRotation;


            //==============================================================================================================================================================================================================================


            //==============================================================================================================================================================================================================================
            // ----WORKING----
            //==============================================================================================================================================================================================================================

            //rotate the other portals Camera Based on the angle between the player and this portal
            //Get the angle between the player and this portal ( This angle will be signed so it knows what direction to turn)
            float Yangle = Vector3.SignedAngle(this.transform.forward, PlayerTransform.forward, Vector3.up);
            Debug.Log("The Angle between the Player and the " + portalSide + " Portal is " + Yangle);

            //Get the Angle to look up
            float Xangle = Player.GetComponent<PlayerController>().FPSCamera.transform.rotation.eulerAngles.x;
            //Xangle = Mathf.Clamp(Xangle, -90.0f, 90.0f);
            Xangle = -Xangle;
            Debug.Log("The X Angle between the Player and the " + portalSide + " Portal is " + Xangle);

            //creates a quaternion that stores an angle rotation around a specified axis ( the Y-axis in this case)
            Quaternion PortalRotationYDiff = Quaternion.AngleAxis(Yangle, Vector3.up);

            //create a quaternion the stores the angle rotation around a specified axis ( the X-axis in this case)
            Quaternion PortalRotationXDiff = Quaternion.AngleAxis(Xangle, Vector3.right);

            //Set the other portals local Camera rotation to the result of itself multiplied by the portalRotationDiff
            OtherPortal.GetComponent<PortalScript>().PortalCamera.transform.localRotation = PortalRotationYDiff * PortalRotationXDiff * OtherPortal.GetComponent<PortalScript>().CameraSetPos.transform.localRotation;

            //==============================================================================================================================================================================================================================




            #endregion


        }
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
