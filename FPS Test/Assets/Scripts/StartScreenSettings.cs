using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScreenSettings : MonoBehaviour
{
    [SerializeField]
    private float FOV;
    float Sensitivity;


    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region Setter Functions

    public void SetFOV(float NewFOV)
    {

        FOV = NewFOV;

    }

    #endregion

    #region Getter Functions

    public float GetFOV()
    {
        return FOV;
    }


    #endregion


}
