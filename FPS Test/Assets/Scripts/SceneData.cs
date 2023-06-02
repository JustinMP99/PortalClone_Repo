using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct SceneData 
{

    [SerializeField] private SceneID _sceneID;
    [SerializeField] private bool _sceneLoaded;
    // Start is called before the first frame update

    public SceneData(SceneID sceneID, bool sceneLoaded)
    {
        _sceneID = sceneID;
        _sceneLoaded = sceneLoaded;
    }



    #region Setter

    public void SetSceneID(SceneID newSceneID)
    {
        _sceneID = newSceneID;
    }

    public void SetSceneLoaded(bool state)
    {
        _sceneLoaded = state;
    }

    #endregion

    #region Getter

    public SceneID GetSceneID()
    {
        return _sceneID;
    }

    public bool GetSceneLoaded()
    {
        return _sceneLoaded;
    }

    #endregion
}
