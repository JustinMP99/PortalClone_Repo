using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Levels
{
    //LEVEL_00 is not a level it just shows that the player hasnt played any level
    LEVEL_00,
    LEVEL_01,
    LEVEL_02,
    LEVEL_03,
    LEVEL_05,
    LEVEL_06
}

[System.Serializable]
public class PlayerDataScript
{

    public Levels LastLevel = Levels.LEVEL_00;
    public string LevelText;

    public bool LevelOneCompleted = false;
    public bool LevelTwoCompleted = false;
    public bool LevelThreeCompleted = false;
    public bool LevelFourCompleted = false;
    public bool LevelFiveCompleted = false;


    public PlayerDataScript()
    { 

    }
    public PlayerDataScript(GameObject player)
    {
        LastLevel = player.GetComponent<PlayerData>().LastLevel;
        LevelText = player.GetComponent<PlayerData>().LevelText;
        LevelOneCompleted = player.GetComponent<PlayerData>().LevelOneCompleted;
        LevelTwoCompleted = player.GetComponent<PlayerData>().LevelTwoCompleted;
        LevelThreeCompleted = player.GetComponent<PlayerData>().LevelThreeCompleted;
        LevelFourCompleted = player.GetComponent<PlayerData>().LevelFourCompleted;
        LevelFiveCompleted = player.GetComponent<PlayerData>().LevelFiveCompleted;

    }

}
