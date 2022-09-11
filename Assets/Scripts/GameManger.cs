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
        if(_GameOver && Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("Game");
        }
    }
    public void SetGameStatus(bool status)
    {
        _GameOver = status;
    }


}
