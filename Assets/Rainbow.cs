using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Rainbow : MonoBehaviour {

    private Text me;
    private Color nextColor;
    public float speed;
    private float timer;
    private float previousTime;

	// Use this for initialization
	void Start () {
        me = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {

        if (timer <= 0)
        {
            nextColor = new Color(Random.value, Random.value, Random.value);
            timer = speed;
        }
        timer -= Time.realtimeSinceStartup - previousTime;
        previousTime = Time.realtimeSinceStartup;
        me.color = Color.Lerp(me.color, nextColor, 1-(timer / speed ) );
    
    }
}
