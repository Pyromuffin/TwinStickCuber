using UnityEngine;
using System.Collections;

public class PickUP : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

    void OnCollisionEnter(Collision hit)
    {
        if (hit.transform.tag == "Dude")
        {
            Destroy(rigidbody);
            transform.parent = hit.transform;
            hit.transform.SendMessageUpwards("increaseChild");
            transform.tag = "Dude";
            gameObject.layer = LayerMask.NameToLayer("Dude");
            Destroy(this);
        }
    }

	// Update is called once per frame
	void Update () {
	
	}
}
