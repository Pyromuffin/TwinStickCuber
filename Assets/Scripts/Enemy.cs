using UnityEngine;
using System.Collections;


public class Enemy : MonoBehaviour {

    public float speed;
    public GameObject target;
    public ForceMode mode;
    public GameObject pickupPrefab;
    public int pickupCount;

	// Use this for initialization
	void Start () {
        target = FindObjectOfType<TwinStickController>().gameObject;
        Arena.instance.enemies.Add(this);
	}

    void OnCollisionEnter(Collision hit)
    {
        if (hit.gameObject.layer ==  LayerMask.NameToLayer("Bullet") )
        {

            for (int i = 0; i < pickupCount; i++)
            {
                var pickup = Instantiate(pickupPrefab, transform.position, Quaternion.identity) as GameObject;
                pickup.rigidbody.velocity = Random.insideUnitSphere;
            }

            Arena.instance.enemies.Remove(this);
            Destroy(hit.gameObject);
            Destroy(gameObject);
            

        }
         
        else if (hit.transform.tag == "Dude")
        {
            //Destroy(hit.transform.gameObject);
            //Destroy(gameObject);
        }
    }

	// Update is called once per frame
	void FixedUpdate () {
        var direction =  target.transform.position - transform.position;
        direction.Normalize();
        rigidbody.AddTorque(Vector3.Cross( Vector3.up, direction) * speed, mode);
	}
}
