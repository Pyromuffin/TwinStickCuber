using UnityEngine;
using System.Collections;

public class DeadUI : MonoBehaviour {

    public static DeadUI instance;
    public Canvas canvas;
	// Use this for initialization
	void Awake () {
        instance = this;
        
        canvas = GetComponent<Canvas>();
        canvas.enabled = false; 
	}

    public void regret()
    {
        Application.LoadLevel(0);
    }

	// Update is called once per frame
	void Update () {
	
	}
}
