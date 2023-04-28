using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeHolderTriggerScript : MonoBehaviour
{

    public GameObject DoorToOpen;




    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "HoldingObject")
        {
            DoorToOpen.GetComponent<DoorAnimationController>().DoorIsOpening();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "HoldingObject")
        {
            DoorToOpen.GetComponent<DoorAnimationController>().DoorIsClosing();
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
