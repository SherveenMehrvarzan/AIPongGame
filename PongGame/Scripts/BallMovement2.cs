using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallMovement2 : MonoBehaviour {
    public float speed;
    public AudioSource source;
    public float maxSpeed;
    public float xIncrement = 0.5f;
    public float increment;
    public Text ScoreText1;
    public Text ScoreText2;
    public Rigidbody2D leftPlayer;
    public Rigidbody2D rightPlayer;
    private int Score1 = 0;
    private int Score2 = 0;
    private float initialSpeed;
    private bool reset;
    private string collidor;
    private List<Vector2> choices;
    private float oldSpeed;
    Rigidbody2D body;
    public Vector2 direction;

    // Start is called before the first frame update
    void Start () {
        if (PlayerPrefs.HasKey ("difficultySpeedBall")) {
            maxSpeed = PlayerPrefs.GetFloat ("difficultySpeedBall");
        } else {
            maxSpeed = 25;
        }
        oldSpeed = speed;
        body = GetComponent<Rigidbody2D> ();
        choices = new List<Vector2> ();
        choices.Add (new Vector2 (0.5f, -1));
        choices.Add (new Vector2 (-1, -1));
        choices.Add (new Vector2 (-1, 1));
        choices.Add (new Vector2 (-1, 0.5f));
        choices.Add (new Vector2 (0.8f, 1));
        int i = UnityEngine.Random.Range (0, choices.Count);
        direction = choices[i];
        initialSpeed = speed;
        reset = false;
        if (ScoreText1 != null && ScoreText2 != null) {
            ScoreText1.text = Score1.ToString ();
            ScoreText2.text = Score2.ToString ();
        }
    }

    // Update is called once per frame
    void Update () { }

    void FixedUpdate () {
        body.velocity = direction * speed;

    }

    void OnCollisionEnter2D (Collision2D collision) {
        if (collision.gameObject.CompareTag ("Wall")) {
            direction.y = -direction.y;
        } else if (collision.gameObject.CompareTag ("Player") || collision.gameObject.CompareTag ("Player 1")) {
            if (transform.position.x < leftPlayer.transform.position.x + leftPlayer.transform.localScale.x / 2 || transform.position.x > rightPlayer.transform.position.x - rightPlayer.transform.localScale.x / 2) {
                direction.y = -direction.y;
            } else {
                direction.x = -direction.x;
            }
            source.Play ();
            speed = Convert.ToSingle (maxSpeed * initialSpeed / ((maxSpeed - initialSpeed) * Math.Exp (-increment * xIncrement) + initialSpeed));
            xIncrement += 0.1f;
        } else if (collision.gameObject.CompareTag ("GoalRight")) {
            GetComponent<Rigidbody2D> ().position = Vector2.zero;
            Score1++;
            speed = oldSpeed;
            xIncrement = 0.1f;
            reset = true;
            int i = UnityEngine.Random.Range (0, choices.Count);
            direction = choices[i];
        } else if (collision.gameObject.CompareTag ("GoalLeft")) {
            GetComponent<Rigidbody2D> ().position = Vector2.zero;
            Score2++;
            speed = oldSpeed;
            xIncrement = 0.1f;
            reset = true;
            int i = UnityEngine.Random.Range (0, choices.Count);
            direction = choices[i];
        }

        if (collision.gameObject != null) {
            collidor = collision.gameObject.name;
        }

        if (ScoreText1 != null && ScoreText2 != null) {
            ScoreText1.text = Score1.ToString ();
            ScoreText2.text = Score2.ToString ();
        }

    }

    public void updateSpeed (float newSpeed) {
        speed = newSpeed;
    }

    public float getInitialSpeed () {
        return initialSpeed;
    }

    public float getSpeed () {
        return speed;
    }

    public Vector2 getDirection () {
        return direction;
    }

    public Vector2 getPosition () {
        return transform.position;
    }

    public void setDirection (Vector2 dir) {
        direction = dir;
    }

    public bool getReset () {
        return reset;
    }

    public void resetReset () {
        reset = false;
    }

    public string getCollidor () {
        return collidor;
    }
}