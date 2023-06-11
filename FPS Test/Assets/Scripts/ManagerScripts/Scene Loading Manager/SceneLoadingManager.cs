using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadingManager : MonoBehaviour
{

    [SerializeField] private List<int> _loadedScenesId;
    [SerializeField] private int _gameUISceneID;
    [SerializeField] private int _mainMenuUISceneID;

    [SerializeField] private GameObject _baseSceneUIManager;
    [SerializeField] private GameObject _mainCamera;
    [SerializeField] private GameObject _eventManager;
    [SerializeField] private Manager _additiveSceneManager;

    [SerializeField] private bool _mainSceneLoaded;
    [SerializeField] private bool _UISceneLoaded;

    [SerializeField] List<SceneID> SceneList;

    // Start is called before the first frame update
    void Start()
    {
        
        //Enable Loading Screen
        _baseSceneUIManager.GetComponent<UIManager>().SetLoadingScreenUIState(true);
        
        //Disable This Scenes Camera
        _mainCamera.SetActive(false);
        
        //Disable This Scenes Event System
        _eventManager.SetActive(false);
        
        //Add Scenes To List
        SceneList.Add(SceneID.MainMenu);
        SceneList.Add(SceneID.MainMenuUI);

        //Start Main Loading Operation
        StartCoroutine(CompleteLoadOperation(SceneList));

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator ClearSceneList()
    {
        //Clear Scene List
        SceneList.Clear();
        Debug.Log("SceneList cleared!");
        yield return null;
    }

    public IEnumerator AddScenes(List<SceneID> newScenes)
    {
        foreach (SceneID newScene in newScenes)
        {
            SceneList.Add(newScene);
            yield return null;
        }
        //SceneList.Add(newScenes[0]);
        //SceneList.Add(newScenes[1]);
        Debug.Log("New Scenes Loaded");
        yield return null;
    }


    public IEnumerator SwitchScenes(List<SceneID> newScenes)
    {
        //Set Loading Screen UI
        _baseSceneUIManager.GetComponent<BaseSceneUIManager>().SetLoadingScreenUIState(true);
        yield return null;


        //Unload Levels
        foreach (SceneID scene in SceneList)
        {
            StartCoroutine(AsyncUnloadLevel(scene.ToString()));
            Debug.Log("Unloading Finished!");
            //yield return null;
        }

        //Clear List
        SceneList.Clear();
        Debug.Log("List Cleared");


        ////Set Scene List
        Debug.Log("Setting Scene List!");
        AddSceneToList(newScenes);
        //yield return null;

        //Load new Scenes
       StartCoroutine(CompleteLoadOperation(SceneList));
        yield return null; 
     
    }


    #region Scene Loading

    public IEnumerator AsyncLoadLevel(string scene)
    {

        //Set The AsyncOperation To Load The Correct Scene Additively Using Load Scene Additive
        AsyncOperation LoadOperation = SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);

        //While The Load Operation Is Not Done, Update Progress Bar
        while (!LoadOperation.isDone)
        {

            float Progress = Mathf.Clamp01(LoadOperation.progress / 0.9f);
            _baseSceneUIManager.GetComponent<UIManager>().SetLoadingSliderValue(Progress);
            yield return null;
        }
    }

    /// <summary>
    /// This Finishes The Scene Loading Process By Setting Variables And Calling SceneManagers Startup Function
    /// </summary>
    /// <returns></returns>
    public IEnumerator AfterScenesLoadOperations()
    {
        //Disable Load Screen
        _baseSceneUIManager.GetComponent<UIManager>().SetLoadingScreenUIState(false);       
        //Find The LevelManager Of The Loaded Scene
        _additiveSceneManager = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<Manager>();
        //Set The LevelManagers SceneLoadingManager To This
        _additiveSceneManager.GetComponent<Manager>().SetBaseSceneManager(this.GetComponent<SceneLoadingManager>());
        //Call the _additiveSceneManagers Startup Function
        _additiveSceneManager.GetComponent<Manager>().Startup();
        Debug.Log("Loading Successful");

        yield return null;
    }

    /// <summary>
    /// This Is The Entire Loading Process, It Loads All Scenes In SceneList As Well As Setting Variables
    /// </summary>
    /// <returns></returns>
    public IEnumerator CompleteLoadOperation(List<SceneID> newScene)
    {
        //Go Through The Passed In List A Load Each Scene Specified
        foreach (SceneID scene in newScene)
        {
            StartCoroutine(AsyncLoadLevel(scene.ToString()));
            //This is annoying
            //Computer needs time to load so wait half a second ( Works fine on desktop but laptop requires 0.5f)
            yield return new WaitForSeconds(0.5f);
        }

        yield return StartCoroutine(AfterScenesLoadOperations());
        //StartCoroutine(AfterScenesLoadOperations());
        //yield return null;

    }


    #endregion

    #region Scene Unloading

    public IEnumerator AsyncUnloadLevel(string scene)
    {
        //Set The AsyncOperation To Load The Correct Scene Additively Using Load Scene Additive
        AsyncOperation LoadOperation = SceneManager.UnloadSceneAsync(scene);

        //While The Load Operation Is Not Done, Update Progress Bar
        while (!LoadOperation.isDone)
        {

            float Progress = Mathf.Clamp01(LoadOperation.progress / 0.9f);
            _baseSceneUIManager.GetComponent<UIManager>().SetLoadingSliderValue(Progress);
            yield return null;
        }
    }

    public IEnumerator CompleteUnloadOperation(List<SceneID> oldScene)
    {
        foreach(SceneID scene in oldScene)
        {
            StartCoroutine(AsyncUnloadLevel(scene.ToString()));
            yield return null;
        }        
    }


    #endregion

    public void SetMainScene(SceneID newMainScene)
    {
        //SceneManager.SetActiveScene(newMainScene.ToString());
    }

    /// <summary>
    /// Takes The Scenes ID Adds It To The List Of Loaded Scenes
    /// </summary>
    public void AddSceneToList(List<SceneID> newScenes)
    {
        //_loadedScenesId.Add(sceneId);
        for (int i = 0; i < newScenes.Count; i++)
        {
            SceneList.Add(newScenes[i]);
        }
        
    }


}
