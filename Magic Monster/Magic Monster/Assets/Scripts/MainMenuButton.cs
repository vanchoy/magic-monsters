using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButton : MonoBehaviour {
    [SerializeField] string _mainMenuScene;
    Scene scene;

    void Start() {
        scene = SceneManager.GetActiveScene();
    }

    public void goToMain() {
        SceneManager.LoadScene(_mainMenuScene);
    }
}