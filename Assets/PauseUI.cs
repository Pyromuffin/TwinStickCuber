using UnityEngine;
using System.Collections;

public class PauseUI : MonoBehaviour {

    public static PauseUI instance;
    public bool paused;
    private Canvas canvas;
	// Use this for initialization
	void Start () {
        instance = this;
        canvas = GetComponent<Canvas>();
        canvas.enabled = false;
	}

    public void pause()
    {
        canvas.enabled = true;
        Time.timeScale = 0;
        paused = true;  
    }

    public void unpause()
    {
       
        Time.timeScale = 1;
        canvas.enabled = false;
        paused = false;
    }


	// Update is called once per frame
	void Update () {
	
	}
}
