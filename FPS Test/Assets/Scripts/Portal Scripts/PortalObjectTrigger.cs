using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalObjectTrigger : MonoBehaviour
{
    /// <summary>
    /// This Is the Portal The Trigger Is Attached To
    /// </summary>
    [SerializeField]
    private GameObject Portal;

    public void OnTriggerEnter(Collider other)
    {
        //Replicate The Item That Hit The Trigger
        if (Portal.GetComponent<PortalScript>().ReplicatedObject == null && other.gameObject.tag == "HoldingObject")
        {
            Portal.GetComponent<PortalScript>().ReplicatedObject = Instantiate(other.gameObject);
            Portal.GetComponent<PortalScript>().ReplicatedObject.tag = "OtherPortalObject";
            Portal.GetComponent<PortalScript>().SetUpdateReplicatedObject(true);
            Portal.GetComponent<PortalScript>().ReplicatedObject.GetComponent<Rigidbody>().isKinematic = true;
        }
        if (other.gameObject.tag == "Player" && other.gameObject.GetComponent<PlayerController>().GetIsInPortal() == false)
        {
            other.gameObject.GetComponent<PlayerController>().SetIsInPortal(true);
            other.gameObject.transform.position = Portal.GetComponent<PortalScript>().OtherPortal.transform.position;

            Quaternion tempRot = other.gameObject.GetComponent<PlayerController>().FPSCamera.transform.rotation;
            tempRot.y = tempRot.y * 180;

            other.gameObject.GetComponent<PlayerController>().FPSCamera.transform.rotation = tempRot;
        }
       
    }


    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "OtherPortalObject")
        {
            Portal.GetComponent<PortalScript>().ReplicatedObject.tag = "HoldingObject";
            Portal.GetComponent<PortalScript>().SetUpdateReplicatedObject(false);
        }
        if (other.gameObject.tag == "Player" && other.gameObject.GetComponent<PlayerController>().GetIsInPortal() == true)
        {
            //other.gameObject.GetComponent<PlayerController>().SetIsInPortal(false);
            //other.gameObject.transform.position = Portal.GetComponent<PortalScript>().OtherPortal.transform.position;
        }
    }


    //hanacue
}
