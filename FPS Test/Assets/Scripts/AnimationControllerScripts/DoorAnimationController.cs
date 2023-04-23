using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAnimationController : MonoBehaviour
{



    [SerializeField]
    public Animator DoorAnimator;






    public void DoorIsClosed()
    {
        DoorAnimator.SetBool("DoorIsClosing", false);
        DoorAnimator.SetBool("DoorIsClosed", true);
    }

    public void DoorIsClosing()
    {
        DoorAnimator.SetBool("DoorIsClosing", true);
        DoorAnimator.SetBool("DoorIsOpen", false);
    }

    public void DoorIsOpen()
    {
        DoorAnimator.SetBool("DoorIsOpening", false);
        DoorAnimator.SetBool("DoorIsOpen", true);
    }

    public void DoorIsOpening()
    {
        //DoorToOpen.GetComponent<Animator>().Play("OpenDoor");
        DoorAnimator.SetBool("DoorIsClosed", false);
        DoorAnimator.SetBool("DoorIsOpening", true);
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
