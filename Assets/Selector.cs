using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Selector : MonoBehaviour {

    public Button left, right;
    public Text label, selectionText;
    public string[] flavors;
    public int selection = 0;





	// Use this for initialization
	void Start () {
        left.onClick.AddListener(leftClick);
        right.onClick.AddListener(rightClick);
	}

    float mod(float  a, float b)
    {
        return a - b * Mathf.FloorToInt(a / b);
    }
     

    void leftClick(Button butt)
    {
        selection = (int)mod((selection - 1), flavors.Length);
        
        selectionText.text = flavors[selection];
    }

    void rightClick(Button butt)
    {
        selection = (selection + 1) % flavors.Length;
        selectionText.text = flavors[selection];
    }


}
