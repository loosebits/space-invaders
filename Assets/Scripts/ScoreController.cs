using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour {
    Text scoreElement;
    public static ScoreController instance;
    int totalScore;

    public ScoreController() {
        instance = this;
    }

    // Start is called before the first frame update
    void Start() {
        scoreElement = GetComponent<Text>();
        totalScore = 0;
        AddScore(0);
    }

    public void AddScore(int score) {
        Debug.Log("Score of " + score + " added");
        totalScore += score;
        scoreElement.text = ("" + totalScore).PadLeft(5, '0');
    }

    // Update is called once per frame
    void Update() {

    }
}
