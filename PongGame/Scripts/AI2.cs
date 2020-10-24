using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI2 : MonoBehaviour {
    // Start is called before the first frame update

    public float moveSpeed = 8.0f;
    public float topBounds = 3.75f;
    public float bottomBounds = -3.75f;
    public Vector2 startingPosition = new Vector2 (8.5f, 0.0f);
    public Vector3 newVector;
    private GameObject ball;
    private Vector3 bPosition;
    public GameObject opponent;

    void Start () {
        transform.localPosition = (Vector3) startingPosition;
        opponent = GameObject.FindGameObjectWithTag ("Player 1");
    }

    // Update is called once per frame
    void Update () {
        if (!ball) {
            ball = GameObject.FindGameObjectWithTag ("Ball");
        }
        if (towardPlayer () == true) {
            bPosition = ball.transform.localPosition;
            print ("entered");

            if (transform.localPosition.y > bottomBounds && bPosition.y < transform.localPosition.y) {
                newVector = new Vector3 (0, -moveSpeed * Time.deltaTime, 0);
                transform.localPosition += newVector;
            }
            if (transform.localPosition.y < bottomBounds && bPosition.y > transform.localPosition.y) {
                newVector = new Vector3 (0, -moveSpeed * Time.deltaTime, 0);
                transform.localPosition += newVector;
            }
        }
    }

    bool towardPlayer () {
        if (opponent.transform.position.x < transform.position.x && ball.GetComponent<Rigidbody2D> ().velocity.x > 0) { //if the opponent is to the left, and the ball is moving to the right, its true
            return true;
        } else if (opponent.transform.position.x > transform.position.x && ball.GetComponent<Rigidbody2D> ().velocity.x < 0) { //if the opponent is to the right and the ball is moving left, its true
            return true;
        } else {
            return false;
        }
    }
}