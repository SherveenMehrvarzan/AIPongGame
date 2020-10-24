using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;
public class PlayerMovement2 : MonoBehaviour {
    public float speed;
    public BoxCollider2D topBoundary;
    public BoxCollider2D bottomBoundary;
    private float topBounds;
    private float bottomBounds;
    public Vector2 startingPosition = new Vector2 (-8.5f, 0.0f);
    Rigidbody2D paddle;

    // Start is called before the first frame update
    void Start () {
        if (PlayerPrefs.HasKey ("difficultySpeedPlayer")) {
            speed = PlayerPrefs.GetFloat ("difficultySpeedPlayer");
        } else {
            speed = 20;
        }
        paddle = GetComponent<Rigidbody2D> ();
        transform.localPosition = (Vector2) startingPosition;
        topBounds = topBoundary.offset.y - topBoundary.size.y / 2;
        bottomBounds = bottomBoundary.offset.y + bottomBoundary.size.y / 2;
    }

    // Update is called once per frame
    void Update () {
        if (gameObject.name == "Players 1") {
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
        } else {
            if (Input.GetKey (KeyCode.UpArrow)) {
                if (transform.localPosition.y >= topBounds) {
                    transform.localPosition = new Vector2 (transform.localPosition.x, topBounds);
                } else {
                    float y = Input.GetAxisRaw (gameObject.name) * speed * Time.deltaTime;
                    transform.position = new Vector2 (transform.position.x, transform.position.y + y);
                }
            } else if (Input.GetKey (KeyCode.DownArrow)) {
                if (transform.localPosition.y <= bottomBounds) {
                    transform.localPosition = new Vector2 (transform.localPosition.x, bottomBounds);
                } else {
                    float y = Input.GetAxisRaw (gameObject.name) * speed * Time.deltaTime;
                    transform.position = new Vector2 (transform.position.x, transform.position.y + y);
                }
            }
        }
    }
}