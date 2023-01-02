using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orb : MonoBehaviour {
    [SerializeField] float _launchForce = 500;
    [SerializeField] float _maxDragDistance = 5;

    Vector2 _startPosition;
    Rigidbody2D _rigitbody2D;
    SpriteRenderer _spriteRenderer;
    Color color;

    float xMin = -15f;
    float xMax = 15f;

    void Awake() {
        // saving referecnes to cache them
        _rigitbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        
    }
    // Start is called before the first frame update
    void Start() {
        color = _spriteRenderer.color;
        _startPosition = _rigitbody2D.position;

        // it is still simulated but it is only controlled by us (our code), and not moved/controlled by physics object or physics system on its own
        _rigitbody2D.isKinematic = true;
    }

    void OnMouseDown() {
        _spriteRenderer.color = color;
        
    }

    void OnMouseUp() {
        Vector2 currentPosition = _rigitbody2D.position;
        Vector2 direction = _startPosition - currentPosition;
        direction.Normalize();

        _rigitbody2D.isKinematic = false;
        _rigitbody2D.AddForce(direction * _launchForce);

        _spriteRenderer.color = Color.white;
    }

    void OnMouseDrag() {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 desiredPosition = mousePosition;

        float distance = Vector2.Distance(desiredPosition, _startPosition);
        if (distance > _maxDragDistance) {
            Vector2 direction = desiredPosition - _startPosition;
            direction.Normalize();
            desiredPosition = _startPosition + (direction * _maxDragDistance);
        }

        if (desiredPosition.x > _startPosition.x) {
            desiredPosition.x = _startPosition.x;
        }

        _rigitbody2D.position = desiredPosition;
    }

    // Update is called once per frame
    void Update() {
        if (LevelController.instance.MonstersAreAllDead()) {
            gameObject.SetActive(false);
        }
        if (transform.position.x > xMax || transform.position.x < xMin) {
            ResetAfterDelay();
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {
        StartCoroutine(ResetAfterDelay());
    }

    IEnumerator ResetAfterDelay() {
        yield return new WaitForSeconds(2);
        _rigitbody2D.position = _startPosition;
        _rigitbody2D.isKinematic = true;
        _rigitbody2D.velocity = Vector2.zero;
    }
}