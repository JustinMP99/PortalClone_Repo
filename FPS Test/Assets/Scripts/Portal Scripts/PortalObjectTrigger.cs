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
            Portal.GetComponent<PortalScript>().SetCheckDotState(true);
            if (other.gameObject.GetComponent<BasePickup>().GetbeingHeld())
            {
                Portal.GetComponent<PortalScript>().ReplicatedObject = Instantiate(other.gameObject);
                Portal.GetComponent<PortalScript>().ReplicatedObject.tag = "OtherPortalObject";
                Portal.GetComponent<PortalScript>().SetUpdateReplicatedObject(true);
            }
        }
    }


    public void OnTriggerExit(Collider other)
    {

        //Check Dot 

        
        //switch (other.gameObject.tag)
        //{
        //    case "HoldingObject":

        //        if (!other.gameObject.GetComponent<BasePickup>().GetbeingHeld())
        //        {
        //            //Destroy The Object
        //            Destroy(other.gameObject);
        //            Portal.GetComponent<PortalScript>().ReplicatedObject.tag = "HoldingObject";
        //            Portal.GetComponent<PortalScript>().SetUpdateReplicatedObject(false);
        //            Portal.GetComponent<PortalScript>().ReplicatedObject.GetComponent<Rigidbody>().isKinematic = false;
        //            Portal.GetComponent<PortalScript>().ReplicatedObject = null;
        //        }
        //        else if(other.gameObject.GetComponent<BasePickup>().GetbeingHeld())
        //        {
        //            Destroy(Portal.GetComponent<PortalScript>().ReplicatedObject.gameObject);
        //            Portal.GetComponent<PortalScript>().ReplicatedObject = null;
        //        }

        //        break;

        //    case "OtherPortalObject":

        //        Portal.GetComponent<PortalScript>().ReplicatedObject.tag = "HoldingObject";
        //        Portal.GetComponent<PortalScript>().SetUpdateReplicatedObject(false);
        //        Portal.GetComponent<PortalScript>().ReplicatedObject.GetComponent<Rigidbody>().isKinematic = false;
        //        Portal.GetComponent<PortalScript>().ReplicatedObject = null;    
        //        break;
        //    default:
        //        break;
        //}


        //if (other.gameObject.tag == "HoldingObject" && )
        //{
            

        //}
        //if (other.gameObject.tag == "OtherPortalObject")
        //{
        //   
        //}
    }


    //hanacue
}
