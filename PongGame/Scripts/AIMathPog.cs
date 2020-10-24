using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMathPog : MonoBehaviour {
    public float speed;
    public float maxDiffInPos;
    private float targetY;
    public GameObject ballObject;
    //public BallMovement bMovement;
    public Rigidbody2D opponent;
    public BoxCollider2D topBoundary;
    public BoxCollider2D bottomBoundary;
    public Object path;
    private Rigidbody2D ball;
    private BallMovement2 bMovement;
    private float topBarrier;
    private float bottomBarrier;
    private float prevVelBall;
    private float x;
    private float y;
    private float ballVelX;
    private float ballVelY;
    private bool left;
    private bool isReset;

    // Start is called before the first frame update
    void Start () {
        if (PlayerPrefs.HasKey ("difficultySpeedAI")) {
            speed = PlayerPrefs.GetFloat ("difficultySpeedAI");
        } else {
            speed = 20;
        }
        ball = ballObject.GetComponent<Rigidbody2D> ();
        bMovement = ballObject.GetComponent<BallMovement2> ();
        topBarrier = topBoundary.offset.y - topBoundary.size.y / 2;
        bottomBarrier = bottomBoundary.offset.y + bottomBoundary.size.y / 2;
        x = ball.transform.position.x;
        y = ball.transform.position.y;
        ballVelY = ball.velocity.y;
        ballVelX = ball.velocity.x;
        left = isLeft ();
    }

    // Update is called once per frame
    void Update () {
        if (((ballVelX == 0 && ball.velocity.x != 0) || (ballVelY == 0 && ball.velocity.y != 0)) || bMovement.getReset ()) {
            ballVelX = ball.velocity.x;
            ballVelY = ball.velocity.y;
            x = ball.transform.position.x;
            y = ball.transform.position.y;
            prevVelBall = ballVelX;
            bMovement.resetReset ();
        }
    }

    void FixedUpdate () {
        if (towardPlayer () && !isReset && (bMovement.getInitialSpeed () == bMovement.getSpeed ())) {
            isReset = true;
            x = ball.transform.position.x;
            y = ball.transform.position.y;
            ballVelY = ball.velocity.y;
            ballVelX = ball.velocity.x;
        }
        if (hitPlayer ()) {
            x = ball.transform.position.x;
            y = ball.transform.position.y;
            ballVelY = ball.velocity.y;
            ballVelX = ball.velocity.x;
            isReset = false;
        }

        if (left) {
            brainsLeft ();
        } else {
            brainsRight ();
        }

        if (!towardPlayer ()) {
            targetY = 0;
        }
        move ();
    }

    float up () {
        return speed * Time.deltaTime;
    }

    float down () {
        return -1 * speed * Time.deltaTime;
    }

    bool towardPlayer () {
        if (opponent.transform.position.x < transform.position.x && ball.velocity.x > 0) { //if the opponent is to the left, and the ball is moving to the right, its true
            return true;
        } else if (opponent.transform.position.x > transform.position.x && ball.velocity.x < 0) { //if the opponent is to the right and the ball is moving left, its true
            return true;
        } else {
            return false;
        }
    }

    void move () {
        if (Mathf.Abs (targetY - transform.position.y) < maxDiffInPos) { //little bit of a space so it doesn't shake around as much
            transform.position = transform.position;
        } else if (transform.position.y + up () > topBarrier - transform.localScale.y / 2 && targetY > topBarrier - transform.localScale.y / 2) { //top bar 
            transform.position = new Vector2 (transform.position.x, topBarrier - transform.localScale.y / 2);
        } else if (transform.position.y + down () < bottomBarrier + transform.localScale.y / 2 && targetY < bottomBarrier + transform.localScale.y / 2) { // bottom bar
            transform.position = new Vector2 (transform.position.x, bottomBarrier + transform.localScale.y / 2);
        } else if (targetY + (speed * Time.deltaTime) / 2 < transform.position.y) {
            transform.position = new Vector2 (transform.position.x, transform.position.y + down ());
        } else if (targetY - (speed * Time.deltaTime) / 2 > transform.position.y) {
            transform.position = new Vector2 (transform.position.x, transform.position.y + up ());
        }
        //transform.position = new Vector2 (transform.position.x, targetY);
    }

    bool hitPlayer () {
        if ((prevVelBall < 0 && ball.velocity.x > 0) || (prevVelBall > 0 && ball.velocity.x < 0)) {
            prevVelBall = ball.velocity.x;
            return true;
        }
        return false;
    }

    bool isLeft () {
        if (transform.position.x < opponent.transform.position.x) {
            return true;
        } else {
            return false;
        }
    }

    void brainsRight () {
        if (x < transform.position.x - transform.localScale.x / 2 - ball.transform.localScale.x / 2 && x > opponent.transform.position.x + opponent.transform.localScale.x / 2 + ball.transform.localScale.x / 2) {
            //print ("test");
            float barrier = 0;
            float oldX = x;
            float oldY = y;
            if (ballVelY < 0) {
                //print ("negative y");
                barrier = bottomBarrier + ball.transform.localScale.y / 2;
            } else {
                //print ("positive y");
                barrier = topBarrier - ball.transform.localScale.y / 2;
            }
            float t = Mathf.Abs ((barrier - y) / ballVelY);
            x = t * ballVelX + x;
            y = barrier;
            if (x > transform.position.x - transform.localScale.x / 2 - ball.transform.localScale.x / 2) {
                //print ("hit right limit");
                x = transform.position.x - transform.localScale.x / 2 - ball.transform.localScale.x / 2;
                t = Mathf.Abs ((x - oldX) / ballVelX);
                y = t * ballVelY + oldY;
                targetY = y;
            } else if (x < opponent.transform.position.x + opponent.transform.localScale.x / 2 + ball.transform.localScale.x / 2) {
                //print ("hit left limit");
                x = opponent.transform.position.x + opponent.transform.localScale.x / 2 + ball.transform.localScale.x / 2;
                t = Mathf.Abs ((x - oldX) / ballVelX);
                y = t * ballVelY + oldY;
            }
            ballVelY = -1 * ballVelY;

            //if (x != 0 && y != 0) {
            //    Object.Instantiate (path, new Vector2 (x, y), ball.transform.rotation);
            //}
        }
    }

    void brainsLeft () {
        if (x > transform.position.x + transform.localScale.x / 2 + ball.transform.localScale.x / 2 && x < opponent.transform.position.x - opponent.transform.localScale.x / 2 - ball.transform.localScale.x / 2) {
            float barrier = 0;
            float oldX = x;
            float oldY = y;
            if (ballVelY < 0) {
                barrier = bottomBarrier + ball.transform.localScale.y / 2;
            } else {
                barrier = topBarrier - ball.transform.localScale.y / 2;
            }
            float t = Mathf.Abs ((barrier - y) / ballVelY);
            x = t * ballVelX + x;
            y = barrier;
            if (x < transform.position.x + transform.localScale.x / 2 + ball.transform.localScale.x / 2) {
                x = transform.position.x + transform.localScale.x / 2 + ball.transform.localScale.x / 2;
                t = Mathf.Abs ((x - oldX) / ballVelX);
                y = t * ballVelY + oldY;
                targetY = y;
            } else if (x > opponent.transform.position.x - opponent.transform.localScale.x / 2 - ball.transform.localScale.x / 2) {
                x = opponent.transform.position.x - opponent.transform.localScale.x / 2 - ball.transform.localScale.x / 2;
                t = Mathf.Abs ((x - oldX) / ballVelX);
                y = t * ballVelY + oldY;
            }
            ballVelY = -1 * ballVelY;

            //if (x != 0 && y != 0) {
            //     Object.Instantiate (path, new Vector2 (x, y), ball.transform.rotation);
            //}
        }
    }
}