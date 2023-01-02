using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class Monster : MonoBehaviour {
    [SerializeField] Sprite _deadSprite;
    [SerializeField] ParticleSystem _particleSystem;
    [SerializeField] AudioClip _audioFile;
    [SerializeField] AudioSource _audioSource;
    
    bool _hasDied = false;

    float xMin = -10f;
    float xMax = 10f;
    //float yMin = -4.3f;
    //float yMax = 4.3f;

    void Update() {
        if (transform.position.x > xMax || transform.position.x < xMin) {
            _hasDied = true;
            gameObject.SetActive(false);
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (!_hasDied) {
            if (ShouldDieFromCollision(collision)) {
                Die(); 
            }
            if (GotHit(collision)) {
                Hit();
            }
        }

    }

    bool ShouldDieFromCollision(Collision2D collision) {
        Orb orb = collision.gameObject.GetComponent<Orb>();

        if (orb != null) {
            _hasDied = true;
            ScoreManager.instance.AddPoint(1);
            return true;
        }

        if (collision.contacts[0].normal.y < -0.5 || collision.contacts[0].normal.x < -0.5 || collision.contacts[0].normal.x > 1) {
            _hasDied = true;
            return true;
        }

        return false;
    }

    bool GotHit(Collision2D collision) {
        if (collision.contacts[0].normal.y < 0 || collision.contacts[0].normal.x < -0.1 || collision.contacts[0].normal.y > 1 || collision.contacts[0].normal.x > 0.5) {
            return true;
        }
        return false;
    }

    void Hit() {
        ScoreManager.instance.AddPoint(1);
    }

    void Die() {
        ScoreManager.instance.AddPoint(3);
        GetComponent<SpriteRenderer>().sprite = _deadSprite;
        _particleSystem.Play(); //it will cause the particle system to play when the monster dies
        StartCoroutine(DissapearAfterDelay()); //the monster will dissapear 1 second after it dies
        _audioSource.PlayOneShot(_audioFile); // play fall (disappear) sound
    }

    IEnumerator DissapearAfterDelay() {
        yield return new WaitForSeconds(2);
        gameObject.SetActive(false);
    }
}