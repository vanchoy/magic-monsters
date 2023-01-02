using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadButton : MonoBehaviour {
    Scene scene;

    void Start() {
        scene = SceneManager.GetActiveScene();
    }

    public void reloadGame() {
        SceneManager.LoadScene(scene.name);
    }
}