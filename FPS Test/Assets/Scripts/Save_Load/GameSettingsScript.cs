using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameSettingsScript
{



 
    public int FOV;
    public int X_Sensitivity; 
    public int Y_Sensitivity;   
    public bool MotionBlurState;    
    public bool ViewBobState;


    public GameSettingsScript(GameObject gameSettings)
    {
        FOV = gameSettings.GetComponent<GameSettings>().GetFOV();
        X_Sensitivity = gameSettings.GetComponent<GameSettings>().GetXSensitivity();
        Y_Sensitivity =  gameSettings.GetComponent<GameSettings>().GetYSensitivity();


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
