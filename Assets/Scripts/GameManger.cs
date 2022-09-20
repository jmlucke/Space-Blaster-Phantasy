using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManger : MonoBehaviour
{
    // Start is called before the first frame update
    private bool _GameOver = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(_GameOver)
        {
            //could be rewritten to be more elegant
            if(Input.GetKeyDown(KeyCode.R))
            {
                LoadGame();
            }

            if (Input.GetKeyDown(KeyCode.M))
            {
                SceneManager.LoadScene("Main_Menu");
            }
                 
        }
    }
    public void SetGameStatus(bool status)
    {
        _GameOver = status;
    }
     
    public void LoadGame()
    {
        SceneManager.LoadScene("Game");
    }
    public void ExitGameProgram()
    {
        Application.Quit();
    }

}
