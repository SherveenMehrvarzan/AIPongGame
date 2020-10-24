using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public float speed;
    private float topBounds = 3.75f;
    private float bottomBounds = -3.75f;
    public Vector2 startingPosition = new Vector2 (-8.5f, 0.0f);
    Rigidbody2D paddle;

    // Start is called before the first frame update
    void Start () {
        paddle = GetComponent<Rigidbody2D> ();
        transform.localPosition = (Vector2) startingPosition;
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKey (KeyCode.W)) {
            if (transform.localPosition.y >= topBounds) {
                transform.localPosition = new Vector2 (transform.localPosition.x, topBounds);
            } else {
                float y = Input.GetAxisRaw (gameObject.name) * speed * Time.deltaTime;
                transform.position = new Vector2 (transform.position.x, transform.position.y + y);
            }
        } else if (Input.GetKey (KeyCode.S)) {
            if (transform.localPosition.y <= bottomBounds) {
                transform.localPosition = new Vector2 (transform.localPosition.x, bottomBounds);
            } else {
                float y = Input.GetAxisRaw (gameObject.name) * speed * Time.deltaTime;
                transform.position = new Vector2 (transform.position.x, transform.position.y + y);
            }
        }
    }

    void OnCollisionEnter2D (Collision2D collision) {
        paddle.isKinematic = true;
    }
}