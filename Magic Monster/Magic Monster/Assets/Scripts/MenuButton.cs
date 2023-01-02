using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour, IPointerClickHandler {
    [SerializeField] Image _image;
    [SerializeField] Sprite _defaultSprite, _pressedSprite;
    [SerializeField] AudioClip _compressClip, _uncompressClip;
    [SerializeField] AudioSource _audioSource;
    [SerializeField] GameObject _MenuPanel;

    bool toggleOn = false;

    void Start() {
        _MenuPanel.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData) {
        if (!toggleOn) {
            toggleOn = true;
            _audioSource.PlayOneShot(_compressClip);
            _image.sprite = _pressedSprite;
            _MenuPanel.SetActive(true);
        }
        else {
            toggleOn = false;
            _audioSource.PlayOneShot(_uncompressClip);
            _image.sprite = _defaultSprite;
            _MenuPanel.SetActive(false);
        }
    }
}