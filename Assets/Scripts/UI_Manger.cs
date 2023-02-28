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
    private TextMeshProUGUI _restartInfo;
    [SerializeField]
    private Image _lifeImage;
    [SerializeField]
    private Sprite[] _livesSprites;
    [SerializeField]
    private GameObject[] _ammoGameObjects;
    // Start is called before the first frame update
    private Slider _thrustBar;
    private Image _thrustKnob;
     
    void Start()
    {
         
        _scoreText.text= "Score: " + GameValues.score;
        _gameOverText.text = "";
        _restartInfo.text = "";
        _thrustBar = GameObject.Find("ThrustBar").GetComponent<Slider>();
        _thrustKnob= GameObject.Find("Thrust Knob").GetComponent<Image>();

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
    public void UpdateAmmo(int img_Id)
    {

      
        GameObject image = _ammoGameObjects[img_Id-1];
        image.GetComponent<Image>().color = Color.black;
    }
    public void ReloadAmmo()
    {
        foreach(GameObject image in _ammoGameObjects)
        {
            image.GetComponent<Image>().color = Color.white;
        }

    }
    public float ThrustAmount()
    {
        return _thrustBar.value;
    }
    public void UpdateThrustAmount(float fuelAmt)
    {
          _thrustBar.value+= fuelAmt;     
    }
    public void ChangeKnobColor(int x, int y, int z, int t)
    {
        _thrustKnob.color= new Color (x, y, z, t);
    }

    public void startGameOver()
    {
        StartCoroutine(SetGameOver());


    }
 

    public void startWave(int waveNum)
    {
        StartCoroutine(SetWave(waveNum));
    }
    

    IEnumerator SetGameOver()
    {
        
        _restartInfo.text = "Press R to Restart Level<br>          M to go to the menu";
        while (1==1)
        {
            _gameOverText.text = "Game Over";
            yield return new WaitForSeconds(0.5f);
            _gameOverText.text = "";
            yield return new WaitForSeconds(0.5f);
            GameObject.Find("Game_manger").GetComponent<GameManger>().SetGameStatus(true);
        }
         

    }

    IEnumerator SetWave(int waveNum)
    {
        int count = 0;
        _restartInfo.text = "'Why did you blow up the asteriod?'";
        GameValues.wave = waveNum;
        while (count<2)
        {
            _gameOverText.text = "Wave " + waveNum;
            yield return new WaitForSeconds(0.5f);
            _gameOverText.text = "";
            yield return new WaitForSeconds(0.5f);
            count++;
        }
        _restartInfo.text = "";
        GameObject.Find("Spawn_Manger").GetComponent<Spawn_Manger>().SetIsEnemySpawnActive(true);

    }
}
