using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChangerAreaScript : MonoBehaviour
{
    Material areaMat;
    public int colorCode;


    void Start()
    {
        Color areaMatColor = DataScript.colors[colorCode];
        areaMatColor.a = 0.2f;
        areaMat = GetComponent<Renderer>().material;
        areaMat.color = areaMatColor;
        areaMat.SetColor("_EmissionColor", DataScript.colors[colorCode]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
