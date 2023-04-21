using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelLoadingManager : MonoBehaviour
{

    [SerializeField]
    private GameObject LevelManagerOBJ;


    public IEnumerator AsyncLoadLevel(Levels Level)
    {
        //Set The Level Index Based On The Passed In Level
        int LevelIndex = 0;
        switch (Level)
        {
            case Levels.LEVEL_00:
                LevelIndex = 0;
                break;
            case Levels.LEVEL_01:
                LevelIndex = 1;
                break;
            case Levels.LEVEL_02:
                LevelIndex = 2;
                break;
            case Levels.LEVEL_03:
                LevelIndex = 3;
                break;
            case Levels.LEVEL_05:
                LevelIndex = 4;
                break;
            case Levels.LEVEL_06:
                LevelIndex = 5;
                break;
            default:
                break;
        }

        //Set The AsyncOperation To Load The Scene Which Is Determined By The Level Index
        AsyncOperation LoadOperation = SceneManager.LoadSceneAsync(LevelIndex);
        //While The Load Operation Is Not Done, Update Progress Bar
        while (!LoadOperation.isDone)
        {
            float Progress = Mathf.Clamp01(LoadOperation.progress / 0.9f);
            LevelManagerOBJ.GetComponent<Manager>().UIManagerScript.SetLoadingSliderValue(Progress);
            yield return null;
        }
        
    }

    public void SetSceneManager(GameObject levelManager)
    {

        LevelManagerOBJ = levelManager;
    }

    private void Start()
    {
        DontDestroyOnLoad(this);
    }


}
