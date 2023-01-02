using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MuteButton : MonoBehaviour {
    public void MuteToggle(bool muted) {
        if (muted) {
            AudioListener.volume = 0;
        }
        else {
            AudioListener.volume = 1;
        }
    }
}