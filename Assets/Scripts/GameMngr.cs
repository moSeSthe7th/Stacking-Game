using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMngr : MonoBehaviour
{
    public Material groundMat;
    public Material playerMat;
    public Material gateMat;
    public Color[] groundColors;
    public Color[] groundEmissions;
    

    void Start()
    {
        DataScript.cubeCount = 0;
        
        DataScript.colors = new Color[] {Color.blue,Color.green,Color.yellow};
        ChangeGroundMatColor(DataScript.currentColorCode);
        ChangePlayerMatColor();
    }

    public void ChangeGroundMatColor(int colorCode)
    {
        groundMat.color = groundColors[colorCode];
        groundMat.SetColor("_EmissionColor", groundEmissions[colorCode]);
    }

    public void ChangePlayerMatColor()
    {
        playerMat.color = DataScript.colors[DataScript.currentColorCode];
        playerMat.SetColor("_EmissionColor", DataScript.colors[DataScript.currentColorCode]);
        gateMat.color = DataScript.colors[DataScript.currentColorCode];
        gateMat.SetColor("_EmissionColor", DataScript.colors[DataScript.currentColorCode]);
    }
}
