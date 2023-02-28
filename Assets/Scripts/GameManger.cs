using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManger : MonoBehaviour
{
    // Start is called before the first frame update
    private bool _gameOver = false;
    void Start()
    {
         
    }

    // Update is called once per frame
    void Update()
    {
        if(_gameOver)
        {
            //could be rewritten to be more elegant
            if(Input.GetKeyDown(KeyCode.R))
            {
                LoadWave(1);
            }

            if (Input.GetKeyDown(KeyCode.M))
            {
                SceneManager.LoadScene("Main_Menu");
            }
                 
        }
    }
    public void SetGameStatus(bool status)
    {
        _gameOver = status;
    }
     
    public void LoadGame()
    {
        SceneManager.LoadScene("Game"); 
    }
    public void ExitGameProgram()
    {
        Application.Quit();
    }
    public void PauseGameProgram()
    {
        Time.timeScale = 0;
    }
    public void ResumeGameProgram()
    {
        Time.timeScale = 1;
    }
    public void LoadWave(int waveNumber)
    {
        GameValues.wave = waveNumber;
       // Debug.Log("LoadWave func "+GameValues.wave);
        LoadGame();
 

    }

}
