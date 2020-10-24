using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallMovement : MonoBehaviour {
    public float speed;
    public float xIncrement = 0.5f;
    public float increment;
    public Text ScoreText1;
    public Text ScoreText2;
    private int Score1 = 0;
    private int Score2 = 0;
    private float initialSpeed;
    private bool reset;
    private string collidor;
    private List<Vector2> choices;
    Rigidbody2D body;
    public Vector2 direction;

    // Start is called before the first frame update
    void Start () {
        body = GetComponent<Rigidbody2D> ();
        choices = new List<Vector2> ();
        choices.Add (new Vector2 (0.5f, -1));
        choices.Add (new Vector2 (-1, -1));
        choices.Add (new Vector2 (-1, 1));
        choices.Add (new Vector2 (-1, 0.5f));
        choices.Add (new Vector2 (0.5f, 1));
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
        } else if (collision.gameObject.CompareTag ("Player")) {
            direction.x = -direction.x;
            speed = Convert.ToSingle (45f * initialSpeed / ((45f - initialSpeed) * Math.Exp (-increment * xIncrement) + initialSpeed));
            xIncrement += 0.1f;
        } else if (collision.gameObject.CompareTag ("GoalRight")) {
            GetComponent<Rigidbody2D> ().position = Vector2.zero;
            Score1++;
            speed = 3f;
            xIncrement = 0.1f;
            reset = true;
            int i = UnityEngine.Random.Range (0, choices.Count);
            direction = choices[i];
        } else if (collision.gameObject.CompareTag ("GoalLeft")) {
            GetComponent<Rigidbody2D> ().position = Vector2.zero;
            Score2++;
            speed = 3f;
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

    public float getSpeed () {
        return speed;
    }

    public Vector2 getDirection () {
        return direction;
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