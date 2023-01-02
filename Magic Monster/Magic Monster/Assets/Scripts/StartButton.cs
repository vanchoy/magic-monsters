using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {
    [SerializeField] Image _image;
    [SerializeField] Sprite _defaultSprite, _pressedSprite;
    [SerializeField] AudioClip _compressClip, _uncompressClip;
    [SerializeField] AudioSource _audioSource;
    [SerializeField] string _nextLevelName;

    public void OnPointerDown(PointerEventData eventData) {
        _image.sprite = _pressedSprite;
        _audioSource.PlayOneShot(_compressClip);
    }

    public void OnPointerUp(PointerEventData eventData) {
        _image.sprite = _defaultSprite;
        _audioSource.PlayOneShot(_uncompressClip);
    }

    public void IWasClicked() {
        Debug.Log("GoToNextLevel" + _nextLevelName);
        SceneManager.LoadScene(_nextLevelName);
    }
}