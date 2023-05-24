using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePickup : MonoBehaviour
{

    [SerializeField] private bool _beingHeld;
    [SerializeField] private int _weight;


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
        return _beingHeld;
    }

    public int GetWeight()
    {
        return _weight;
    }

    #endregion

    #region Setters

    /// <summary>
    /// Sets The Value For _beingHeld
    /// Used To Determine If The Pickup Is Being Held
    /// </summary>
    /// <param name="state"></param>
    public void SetbeingHeld(bool state)
    {
        _beingHeld = state;
    }

    /// <summary>
    /// This Sets The _weight Of The Object
    /// </summary>
    /// <param name="newWeight"></param>
    public void SetWeight(int newWeight)
    { 
    
    }

    #endregion

}
