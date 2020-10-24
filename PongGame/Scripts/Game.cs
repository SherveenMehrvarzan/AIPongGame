using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour {
    // Start is called before the first frame update
    void Start () {

    }

    // Update is called once per frame
    void FixedUpdate () {
        if (Input.GetKey (KeyCode.Escape)) {
            SceneManager.LoadScene ("Beginning");
        }
    }
}