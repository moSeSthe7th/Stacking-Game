using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelScript : MonoBehaviour
{
    public int startColorCode;

    void Start()
    {
        DataScript.currentColorCode = startColorCode;   
    }

  
    void Update()
    {
        
    }
}
