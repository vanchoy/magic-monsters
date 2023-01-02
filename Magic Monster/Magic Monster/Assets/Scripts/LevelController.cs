using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LevelController : MonoBehaviour {
    public static LevelController instance;

    [SerializeField] public TextMeshProUGUI scoreText, finalScoreText, highscoreText;
    [SerializeField] Image panelImage;
    [SerializeField] GameObject nextLevelButton, restartLevelButton, gameElements, homeButton;
    [SerializeField] AudioClip winSound;
    [SerializeField] AudioSource audioSource;
    [SerializeField] public int twoStarScore, threeStarScore;

    int starNumber, score;

    Scene scene;
    Monster[] _monsters;
    Star[] _stars;

    private void Awake() {
        instance = this;
    }

    private void Start() {
        scene = SceneManager.GetActiveScene();
        panelImage.GetComponent<Image>().enabled = false;
        scoreText.GetComponent<TextMeshProUGUI>().enabled = true;
        finalScoreText.GetComponent<TextMeshProUGUI>().enabled = false;
        highscoreText.GetComponent<TextMeshProUGUI>().enabled = false;
        homeButton.SetActive(false);
        nextLevelButton.SetActive(false);
        restartLevelButton.SetActive(false);
        
        foreach (var star in _stars) {
            star.gameObject.SetActive(false);
        }  
    }

    private void OnEnable() {
        _monsters = FindObjectsOfType<Monster>();
        _stars = FindObjectsOfType<Star>();
    }

    // Update is called once per frame
    private void Update() {
        starNumber = PlayerPrefs.GetInt(scene.name + "starNumber", 0);
        
        if (MonstersAreAllDead()) {
            audioSource.PlayOneShot(winSound);
            EndGame();
        }
    }

    //checks if monsters are dead
    public bool MonstersAreAllDead() {
        foreach (var monster in _monsters) {
            if (monster.gameObject.activeSelf) {
                return false;
            }
        }
        return true;
    }

    //kills all monsters
    public void KillAllMonsters() {
        foreach (var monster in _monsters) {
            monster.gameObject.SetActive(false);
        }
    }

    //how many stars to show based on the current level score
    public void ShowStars(int n) {
        for (int i = 0; i < n; i++) {
            _stars[i].gameObject.SetActive(true);
            _stars[i].gameObject.GetComponent<SpriteRenderer>().sprite = Star.instance._activeStar;
        }
    }

    public void EndGame() {
        gameElements.SetActive(false);
        scoreText.GetComponent<TextMeshProUGUI>().enabled = false;
        panelImage.GetComponent<Image>().enabled = true;
        finalScoreText.GetComponent<TextMeshProUGUI>().enabled = true;
        highscoreText.GetComponent<TextMeshProUGUI>().enabled = true;
        homeButton.SetActive(true);
        restartLevelButton.SetActive(true);
        audioSource.PlayOneShot(winSound);
        
        score = PlayerPrefs.GetInt(scene.name + "scorePoints", 0);

        if (starNumber > 1 || score >= twoStarScore) {
            nextLevelButton.SetActive(true);
        }

        if (score < twoStarScore && score > 10) {
            ShowStars(1);

            if (starNumber < 2) {
                PlayerPrefs.SetInt(scene.name + "starNumber", 1);
            }
        }

        if (score >= twoStarScore && score < threeStarScore) {
            ShowStars(2);

            if (starNumber < 2 ) {
                PlayerPrefs.SetInt(scene.name + "starNumber", 2);
            }
        }

        if (score >= threeStarScore) {
            ShowStars(3);

            if (starNumber < 3) {
                PlayerPrefs.SetInt(scene.name + "starNumber", 3);
            }
        }   
    }
}