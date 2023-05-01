using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePickup : MonoBehaviour
{

    [SerializeField]
    private bool beingHeld;
    [SerializeField]
    private int Weight;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    #region Getters

    public bool GetbeingHeld()
    {
        return beingHeld;
    }

    public int GetWeight()
    {
        return Weight;
    }

    #endregion

    #region Setters

    /// <summary>
    /// Sets The Value For beingHeld
    /// Used To Determine If The Pickup Is Being Held
    /// </summary>
    /// <param name="state"></param>
    public void SetbeingHeld(bool state)
    {
        beingHeld = state;
    }

    /// <summary>
    /// This Sets The Weight Of The Object
    /// </summary>
    /// <param name="newWeight"></param>
    public void SetWeight(int newWeight)
    { 
    
    }

    #endregion

}
