using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{

    public Levels LastLevel = Levels.LEVEL_00;
    public string LevelText;

    public bool LevelOneCompleted = false;
    public bool LevelTwoCompleted = false;
    public bool LevelThreeCompleted = false;
    public bool LevelFourCompleted = false;
    public bool LevelFiveCompleted = false;

    

    public PlayerData (PlayerDataScript playdata)
    {
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
        DontDestroyOnLoad(this.gameObject);
    }
}
