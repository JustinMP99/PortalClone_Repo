using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelLoadingManager : MonoBehaviour
{

    [SerializeField]
    private GameObject LevelManagerOBJ;


   

    public void SetSceneManager(GameObject levelManager)
    {

        LevelManagerOBJ = levelManager;
    }

    private void Start()
    {
        DontDestroyOnLoad(this);
    }


}
