using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

[SelectionBase]
public class Star : MonoBehaviour {
    public static Star instance;

    [SerializeField] public SpriteRenderer _inactiveStar;
    [SerializeField] public Sprite _activeStar;

    private void Awake() {
        instance = this;
    }
}
