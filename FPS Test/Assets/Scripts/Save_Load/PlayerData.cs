using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{

    public int FOV = 60;
    public int X_Sensitivity = 5;
    public int Y_Sensitivity = 5;
    public Levels LastLevel = Levels.LEVEL_00;
    public string LevelText;

    public bool LevelOneCompleted = false;
    public bool LevelTwoCompleted = false;
    public bool LevelThreeCompleted = false;
    public bool LevelFourCompleted = false;
    public bool LevelFiveCompleted = false;

    

    public PlayerData (PlayerDataScript playdata)
    {
        FOV = playdata.FOV;
        X_Sensitivity = playdata.X_Sensitivity;
        Y_Sensitivity = playdata.Y_Sensitivity;
        LastLevel = playdata.LastLevel;
        LevelText = playdata.LevelText;
        LevelOneCompleted = playdata.LevelOneCompleted;
        LevelTwoCompleted = playdata.LevelTwoCompleted;
        LevelThreeCompleted = playdata.LevelThreeCompleted;
        LevelFourCompleted = playdata.LevelFourCompleted;
        LevelFiveCompleted = playdata.LevelFiveCompleted;

    }

    private void Start()
    {
        DontDestroyOnLoad(this);
    }
}
