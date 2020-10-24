using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour {
    public float speed;
    public float trackSpeed;
    public GameObject ball;
    public GameObject AIBall;
    public GameObject opponent;
    private float targetY;
    public Rigidbody2D ballBody;
    public Rigidbody2D AIBody;
    public BallMovement AIMovement;
    public BallMovement bMovement;
    public Collision2D ballCollide;
    private bool follow;
    // Start is called before the first frame update
    void Start () {
        targetY = 0;
        follow = false;
        logicStart ();
    }

    // Update is called once per frame
    void Update () {
        if (follow) {
            AIBody.position = ballBody.position;
            AIMovement.setDirection (bMovement.getDirection ());
            AIMovement.updateSpeed (bMovement.getSpeed ());
            targetY = 0;
        }

        //checks which side they are on and then resets ball if its been hit by opponent (regular ball)
        if ((opponent.transform.position.x < transform.position.x && ballBody.position.x <= opponent.transform.position.x) ||
            (opponent.transform.position.x > transform.position.x && ballBody.position.x >= opponent.transform.position.x)) {
            resetBall ();
            logicStart ();
            follow = false;
        }

        //checks which side the opponent is on and if the ball is heading towards the opponent, moves them back to center
        if ((opponent.transform.position.x < transform.position.x && AIBody.velocity.x < 0) ||
            (opponent.transform.position.x > transform.position.x && AIBody.velocity.x > 0)) {
            targetY = 0;

        }

        //checks which side they are on, then sets target value to wherever the ball is when it crosses them and resets it
        if (!follow && ((opponent.transform.position.x < transform.position.x && AIBody.position.x >= transform.position.x - 0.5) ||
                (opponent.transform.position.x > transform.position.x && AIBody.position.x <= transform.position.x + 0.5))) { //MAKE THIS 1.2 NUMBER RELATED TO SCALE OF BOTH PLAYER AND BALL
            targetY = AIBody.position.y;
            resetBall ();
            logicStart ();
        }

        if (Mathf.Abs (targetY - transform.position.y) < 0.01) { //little bit of a space so it doesn't shake around as much
            transform.position = transform.position;
        } else if (targetY < transform.position.y) {
            transform.position = new Vector2 (transform.position.x, transform.position.y + down ());
        } else if (targetY > transform.position.y) {
            transform.position = new Vector2 (transform.position.x, transform.position.y + up ());
        }

        if (bMovement.getReset () || AIMovement.getReset ()) { //if either ball gets reset, moves the ai ball back on top of the other ball
            bMovement.resetReset ();
            AIMovement.resetReset ();
            follow = false;
            resetBall ();
            logicStart ();
        }
    }

    float up () {
        return speed * Time.deltaTime;
    }

    float down () {
        return -1 * speed * Time.deltaTime;
    }

    void logicStart () {
        AIMovement.updateSpeed (bMovement.getSpeed () + trackSpeed);
    }

    void resetBall () {
        AIMovement.updateSpeed (bMovement.getSpeed ());
        AIMovement.setDirection (bMovement.getDirection ());
        AIBody.position = ballBody.position;
    }
}