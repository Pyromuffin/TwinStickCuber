using UnityEngine;
using System.Collections;

public class PickUP : MonoBehaviour {

    public int value;
    public AudioClip pickupSound;
    public bool katamari;
    public float speed;

	// Use this for initialization
	void Start () {
        rigidbody.velocity = Random.insideUnitSphere * speed;
        rigidbody.velocity = new Vector3(rigidbody.velocity.x, 0, rigidbody.velocity.z);
	}

   

	// Update is called once per frame
	void Update () {
	
	}
}
