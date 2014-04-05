using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreDisplay : MonoBehaviour {

    public int player;


    public int Score
    {
        get
        {
            return score; 
        }
        set
        {
            score = value;
            scoreText.text = "Score: " + score.ToString();
        }
    }
    private int score;

    public Text scoreText;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
