using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour {
    public static ScoreManager instance;

    [SerializeField] public float timeRemaining;
    [SerializeField] public bool timerIsRunning = false;

    TextMeshProUGUI scoreText, finalScoreText, highScoreText;
    Scene scene;
    int score = 0;
    int highscore = 0;
    int seconds; 

    private void Awake() {
        instance = this;
    }

    // Start is called before the first frame update
    private void Start() {
        scene = SceneManager.GetActiveScene();
        finalScoreText = LevelController.instance.finalScoreText;
        highScoreText = LevelController.instance.highscoreText;
        scoreText = LevelController.instance.scoreText;
    
        scoreText.text = scene.name + ": " + score.ToString() + seconds.ToString();
        highscore = PlayerPrefs.GetInt(scene.name + "highscorePoints", 0);
        
        
        timerIsRunning = true;
    }

    void Update() {
        seconds = Mathf.FloorToInt(timeRemaining); //convert time float to int
        finalScoreText.text = "Score: " + score.ToString() + " | Time: " + seconds + "s";
        highScoreText.text = "Highscore: " + highscore.ToString();
        scoreText.text = scene.name + ": " + score.ToString() + " | " + seconds.ToString() + "s";

        PlayerPrefs.SetInt(scene.name + "scorePoints", score);  //update score; update shared value

        if (LevelController.instance.MonstersAreAllDead()) {
            timerIsRunning = false;
        }

        if (timerIsRunning) {
            if (timeRemaining >= 1) {
                timeRemaining -= Time.deltaTime;
            }

            else {
                timeRemaining = 0;
                timerIsRunning = false;
            }
        }
        else {
            LevelController.instance.KillAllMonsters();
        }
    }

    public void AddPoint(int n) {
        score += n;

        if (highscore < score) {
            PlayerPrefs.SetInt(scene.name + "highscorePoints", score);
        } 
        else {
            PlayerPrefs.SetInt(scene.name + "scorePoints", score); 
        }
    }
}