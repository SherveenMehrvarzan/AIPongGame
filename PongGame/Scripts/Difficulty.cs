using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Difficulty : MonoBehaviour {
    public float difficultySpeedAI;
    public float difficultySpeedBall;
    public float difficultySpeedPlayer;

    public void setBall (float speed) {
        difficultySpeedBall = speed;
        PlayerPrefs.SetFloat ("difficultySpeedBall", speed);
        PlayerPrefs.Save ();
    }

    public void setAI (float speed) {
        difficultySpeedAI = speed;
        PlayerPrefs.SetFloat ("difficultySpeedAI", speed);
        PlayerPrefs.Save ();
    }

    public void setPlayer (float speed) {
        difficultySpeedPlayer = speed;
        PlayerPrefs.SetFloat ("difficultySpeedPlayer", speed);
        PlayerPrefs.Save ();
    }
}