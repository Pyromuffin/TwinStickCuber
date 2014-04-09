using UnityEngine;
using System.Collections;

public class TheWorstWiggle : MonoBehaviour {

    public float strength, speed;
    private Vector3 start;
    private float timer;
    private float previousTime;

	// Use this for initialization
	void Start () {
        start = transform.position;
	}
	
	// Update is called once per frame
	void Update () {

        if (timer <= 0)
        {
            transform.position = start + Random.insideUnitSphere * strength;
            timer = speed;
        }
        timer -=  Time.realtimeSinceStartup -previousTime;
        previousTime = Time.realtimeSinceStartup;
    }
}
