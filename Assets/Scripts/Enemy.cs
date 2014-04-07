using UnityEngine;
using System.Collections;


public class Enemy : MonoBehaviour {

    public float speed;
    public GameObject target;
    public ForceMode mode;
    public GameObject pickupPrefab;
    public int pickupCount;
    public AudioClip dieSound, cutDie;
    private TwinStickController targetController;
    private TwinStickController[] targets;

	// Use this for initialization
	void Start () {
        targets = FindObjectsOfType<TwinStickController>();
        targetController = targets[Random.Range(0, targets.Length)];
        target = targetController.gameObject;
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
            AudioSource.PlayClipAtPoint(dieSound, Vector3.zero);
            Destroy(gameObject);
            

        }
        else if (hit.transform.tag == "Dude")
        {
            //Destroy(hit.transform.gameObject);
            //Destroy(gameObject);
        }
        else if (hit.transform.tag == "Laser Sword")
        {
            var m1 = new GameObject();
            var m2 = new GameObject();
            m1.transform.position = transform.position;
            m2.transform.position = transform.position;
            m1.transform.localScale = transform.localScale;
            m2.transform.localScale = transform.localScale;
            m1.transform.rotation = transform.rotation;
            m2.transform.rotation = transform.rotation;
            //m2.AddComponent<PickUP>();
            //m1.AddComponent<PickUP>();
            var mf1 = m1.AddComponent<MeshFilter>();
            var mf2 = m2.AddComponent<MeshFilter>();
            var mr1 = m1.AddComponent<MeshRenderer>();
            var mr2 = m2.AddComponent<MeshRenderer>();
            var pickup1 = m1.AddComponent<PickUP>();
            var pickup2 = m2.AddComponent<PickUP>();
            pickup1.value = 200;
            pickup2.value = 200;

            m1.layer = LayerMask.NameToLayer("Pick up");
            m2.layer = LayerMask.NameToLayer("Pick up");

           

            var p1 = transform.InverseTransformPoint(hit.contacts[0].point);
            var p2 = transform.InverseTransformPoint(hit.transform.position);
            var p3 = transform.InverseTransformPoint(hit.transform.position + hit.transform.forward);
            var plane = new Plane(p1,p2,p3);

            var meshes = MeshBisect.bisect(GetComponent<MeshFilter>().mesh, plane);

            mf1.mesh = meshes[0];
            mf2.mesh = meshes[1];

            mr1.material = renderer.material;
            mr2.material = renderer.material;

            var col1 = m1.AddComponent<MeshCollider>();
            col1.convex = true; 
            m1.AddComponent<Rigidbody>();
            m1.rigidbody.mass = .2f;
            m1.rigidbody.velocity = rigidbody.velocity;
            m1.rigidbody.angularVelocity = rigidbody.angularVelocity;

            var col2 = m2.AddComponent<MeshCollider>();
            col2.convex = true;
            m2.AddComponent<Rigidbody>();
            m2.rigidbody.mass = .2f;
            m2.rigidbody.velocity = rigidbody.velocity;
            m2.rigidbody.angularVelocity = rigidbody.angularVelocity;

            Arena.instance.enemies.Remove(this);
            
            AudioSource.PlayClipAtPoint(cutDie, Vector3.zero);
            Destroy(gameObject);
        }
    }

	// Update is called once per frame
	void FixedUpdate () {
        var direction =  target.transform.position - transform.position;
        direction.Normalize();
        rigidbody.AddTorque(Vector3.Cross( Vector3.up, direction) * speed, mode);

        if (targetController.dead)
        {
            targetController = targets[Random.Range(0, targets.Length)];
            target = targetController.gameObject;
        }

        if (transform.position.y <= -10)
        {
            Arena.instance.enemies.Remove(this);
            Destroy(gameObject);
        }
	}


}
