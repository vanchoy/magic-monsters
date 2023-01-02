using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour {
    private static BackgroundMusic backgroundMusic;

    void Awake() {
        if (gameObject.activeSelf == true ) {
            gameObject.SetActive(true);
        }
        else {
            gameObject.SetActive(false);
        }
    }
}