using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject gameOverPanel;
    public GameObject levelPassedPanel;
    public GameObject inGamePanel;

    public Text pointCounterText;

    void Start()
    {
        gameOverPanel.SetActive(false);
        levelPassedPanel.SetActive(false);
        inGamePanel.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameOver()
    {
        gameOverPanel.SetActive(true);
        levelPassedPanel.SetActive(false);
        inGamePanel.SetActive(false);
    }

    public void LevelPassed()
    {
        gameOverPanel.SetActive(false);
        levelPassedPanel.SetActive(true);
        inGamePanel.SetActive(false);
    }

    public void IncreasePointCounter()
    {

    }
}
