using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCameraRotate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(new Vector3(0.0f, 0.05f, 0.0f));
    }
}
