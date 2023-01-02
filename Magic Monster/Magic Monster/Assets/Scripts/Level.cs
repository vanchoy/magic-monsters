using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

[SelectionBase]
public class Level : MonoBehaviour, IPointerClickHandler {
    public static Level instance;

    [SerializeField] public Sprite _levelLocked, _oneStar, _twoStars, _threeStars;
    [SerializeField] public int currentLevelNumber;
    [SerializeField] public TextMeshProUGUI highScoreText, ptsText, levelName, gradeText;

    int levelStars, prevLevelStars, _prevLevelNumber, highscore;

    string levelObjName = "Level";

    private void Awake() {
        instance = this;       
    }

    // Start is called before the first frame update
    public void Start() {        
        highscore = PlayerPrefs.GetInt(levelObjName + currentLevelNumber + "highscorePoints", 0);
        levelStars = PlayerPrefs.GetInt(levelObjName + currentLevelNumber + "starNumber", 0);
        
        highScoreText.text = highscore.ToString();
        levelName.text = "#" + currentLevelNumber;

        if (currentLevelNumber <= 1) {
            _prevLevelNumber = 1;
        }
        else {
            _prevLevelNumber = currentLevelNumber - 1;
        }
    }

    // Update is called once per frame
    public void Update() {
        prevLevelStars = PlayerPrefs.GetInt(levelObjName + _prevLevelNumber + "starNumber", 0);

        if (prevLevelStars <= 1 && currentLevelNumber > 1) {
            gameObject.GetComponent<Image>().sprite = Level.instance._levelLocked;
            gradeText.text = "Locked";
            highScoreText.gameObject.SetActive(false);
            ptsText.gameObject.SetActive(false);
        }
        
        if (levelStars == 1) {
            gameObject.GetComponent<Image>().sprite = Level.instance._oneStar;
            gradeText.text = "Learning";
        }

        if (levelStars == 2) {
            gameObject.GetComponent<Image>().sprite = Level.instance._twoStars;
            gradeText.text = "Dedicated";
        }

        if (levelStars == 3) {
            gameObject.GetComponent<Image>().sprite = Level.instance._threeStars;
            gradeText.text = "Top Player";
        }
    }

    public void OnPointerClick(PointerEventData eventData) {
        // always have level one unlocked to play
        if (currentLevelNumber <= 1) {
            SceneManager.LoadScene(levelObjName + currentLevelNumber);
        }

        //unlock next level if if you have >2 stars on the previous
        if (2 <= prevLevelStars) {
            SceneManager.LoadScene(levelObjName + currentLevelNumber);
        }
        
    }


}