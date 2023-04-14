using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : MonoBehaviour
{

    [SerializeField]
    private int FOV;
    [SerializeField]
    private int X_Sensitivity;
    [SerializeField]
    private int Y_Sensitivity;
    [SerializeField]
    private bool MotionBlurState;
    [SerializeField]
    private bool ViewBobState;


    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    #region Getters

    //Returns The Current FOV
    public int GetFOV()
    {
        return FOV;
    }

    //Returns The Current X Sensitivity
    public int GetXSensitivity()
    {
        return X_Sensitivity;
    }

    //Returns The Current Y Sensitivity
    public int GetYSensitivity()
    {
        return Y_Sensitivity;
    }
    
    public bool GetMotionBlurState()
    {

        return MotionBlurState;
    }

    public bool GetViewBobState()
    {
        return ViewBobState;
    }


    #endregion

    #region Setters

    public void SetFOV(int NewFOV)
    {
        FOV = NewFOV;
    }

    public void SetXSensitivity(int NewX)
    {

        X_Sensitivity = NewX;

    }

    public void SetYSensitivity(int NewY)
    {

        Y_Sensitivity = NewY;

    }

    public void SetMotionBlurState(bool state)
    {
        MotionBlurState = state;
    }

    public void SetViewBobState(bool state)
    {
        ViewBobState = state;
    }

    #endregion

}
