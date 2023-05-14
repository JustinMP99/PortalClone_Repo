using UnityEngine;

public class PortalObjectTrigger : MonoBehaviour
{
    /// <summary>
    /// This Is the Portal The Trigger Is Attached To
    /// </summary>
    [SerializeField]
    private GameObject Portal;
    float dotResult;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "HoldingObject")
        {
            Portal.GetComponent<PortalScript>().TouchingObject = other.gameObject;
            Physics.IgnoreLayerCollision(3, 8, true);
            //Physics.IgnoreLayerCollision(6, 8, true);
            Portal.GetComponent<PortalScript>().SetCheckDotState(true);
            Portal.GetComponent<PortalScript>().CreateReplicatedObject(other.gameObject);
            Portal.GetComponent<PortalScript>().SetUpdateReplicatedObject(true);
        }
    }


    public void OnTriggerExit(Collider other)
    {

        //If The colliders Tag is "HoldingObject"  
        if (other.gameObject.tag == "HoldingObject")
        {
            //If In Front...
            if (Portal.GetComponent<PortalScript>().GetTouchingDot() > 0)
            {
                //Delete Replicated
                Destroy(Portal.GetComponent<PortalScript>().ReplicatedObject);
                //Stop checking Dot And Updating Replicated 
                Portal.GetComponent<PortalScript>().SetCheckDotState(false);
                Portal.GetComponent<PortalScript>().SetUpdateReplicatedObject(false);
            }
            ////If In Back
            //else if (Portal.GetComponent<PortalScript>().GetTouchingDot() < 0 && !other.gameObject.GetComponent<BasePickup>().GetbeingHeld())
            //{

            //}
           
        }

    }


    //hanacue
}
