using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
 

public class UI_Manger : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _scoreText;
    [SerializeField]
    private TextMeshProUGUI _gameOverText;
    [SerializeField]
    private TextMeshProUGUI _Restart_Info;
    [SerializeField]
    private Image _lifeImage;
    [SerializeField]
    private Sprite[] _livesSprites;
    // Start is called before the first frame update
    void Start()
    {
         
        _scoreText.text= "Score: 0";
        _gameOverText.text = "";
        _Restart_Info.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
     
    public void SetScore(string text)
    {

        _scoreText.text = "Score: " + text;
      
    }

    public void UpdateLives(int lifeCount)
    {
        _lifeImage.sprite = _livesSprites[lifeCount];
    }

    public void startGameOver()
    {
        StartCoroutine(SetGameOver());


    }

    IEnumerator SetGameOver()
    {
        
        _Restart_Info.text = "Press R to Restart Level";
        while (1==1)
        {
            _gameOverText.text = "Game Over";
            yield return new WaitForSeconds(0.5f);
            _gameOverText.text = "";
            Debug.Log("Test9");
            yield return new WaitForSeconds(0.5f);
            GameObject.Find("Game_manger").GetComponent<GameManger>().SetGameStatus(true);
        }
         

    }
}
