using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SaveFileUIScript : MonoBehaviour
{

    [SerializeField]
    private Image SaveImage;
    [SerializeField]
    private TMP_Text LevelText;
    [SerializeField]
    private Button SaveButton;
    [SerializeField]
    private bool IsLoaded = false;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetLevelText(string levelText)
    {
        LevelText.text = levelText;

    }
    public void SetSaveImage(Image saveImage)
    {
        SaveImage = saveImage;
    }

    public void SetIsLoaded(bool state)
    {

        IsLoaded = state;

    }


}
