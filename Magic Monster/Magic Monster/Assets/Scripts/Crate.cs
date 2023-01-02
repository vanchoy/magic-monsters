using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour
{
    [SerializeField] public ParticleSystem _particleSystem;
    bool _isHit = false;

    float xMin = -10f;
    float xMax = 10f;

    // Start is called before the first frame update
    void Start() {
        if (transform.position.x > xMax || transform.position.x < xMin) {
            _isHit = true;
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update() {
        
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (!_isHit && ShouldDestroyFromOrbHit(collision)) {
            DidHit();
        }
    }

    bool ShouldDestroyFromOrbHit(Collision2D collision) {
        Orb orb = collision.gameObject.GetComponent<Orb>();

        if (orb != null) {
            _isHit = true;

            return true;
        }

        return false;
    }

    void DidHit() {
        _particleSystem.Play();
        ScoreManager.instance.AddPoint(1);
        StartCoroutine(DissapearAfterDelay()); 
    }

        IEnumerator DissapearAfterDelay() {
        yield return new WaitForSeconds(0.1f);
        gameObject.SetActive(false);
    }
}
