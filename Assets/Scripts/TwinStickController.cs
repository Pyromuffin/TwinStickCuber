using UnityEngine;
using System.Collections;
using GamepadInput;



public class TwinStickController : MonoBehaviour {

    public float maxSpeed, lerpFalloff,  downForce;
   

    public float bulletSpeed, reloadTime, bulletLifetime, bulletForce;
    public GameObject bulletPrefab;


    public float maxChargeTime, chargeShotDistance, chargeShotSize, chargeShotForceModifier;
    public GameObject chargeShotPrefab;
       

    public GamePad.Index player;
    private GamepadState pad;
    private float reloadTimer, chargeTimer;
    private bool charging;

    public int childCount;
    public float torquePerChild;

    private GameObject chargeShot;

	// Use this for initialization
	void Start () {

        
	}
    public void increaseChild()
    {
        childCount++;
    }

	// Update is called once per frame
	void FixedUpdate () {
        pad = GamePad.GetState(player);

        Vector3 dir = new Vector3(pad.LeftStickAxis.x, 0, pad.LeftStickAxis.y);
        //rigidbody.AddForce(Vector3.Lerp(rigidbody.velocity, dir * maxSpeed, lerpFalloff));
        //rigidbody.AddForce(dir * maxSpeed);

        rigidbody.AddForce(Vector3.down * downForce);
        var torqueVector = Vector3.Cross(Vector3.up, dir);
        rigidbody.AddTorque(( torqueVector * dir.magnitude * maxSpeed) + (torqueVector * dir.magnitude * torquePerChild * childCount) );

        reloadTimer -= Mathf.Min(reloadTimer, Time.fixedDeltaTime);

        //shoot
        if (pad.rightStickAxis.magnitude > .1f && reloadTimer == 0 && !charging)
        {
            Shoot(pad.rightStickAxis.normalized);
            reloadTimer = reloadTime;
        }

        if (pad.RightTrigger > .1f)
        {
            if (!charging)
            {
                chargeShot = Instantiate(chargeShotPrefab) as GameObject;
                Physics.IgnoreCollision(collider, chargeShot.collider);
                chargeShot.rigidbody.detectCollisions = false;
            }

            charging = true;
            float strength = (chargeTimer / maxChargeTime);
            chargeShot.transform.position = transform.position + new Vector3(pad.rightStickAxis.x, 1 * strength, pad.rightStickAxis.y) * chargeShotDistance;
            chargeShot.transform.localScale = Vector3.one * strength  * chargeShotSize;
            chargeShot.transform.forward = new Vector3(pad.rightStickAxis.x, 0, pad.rightStickAxis.y) ;
            chargeTimer += Mathf.Min(Time.fixedDeltaTime, maxChargeTime - chargeTimer);
        }
        else if (charging)
        {
            chargeShoot(pad.rightStickAxis.normalized, chargeTimer / maxChargeTime);
            chargeTimer = 0;
            charging = false;
        }

        

	}

    void chargeShoot(Vector2 dir, float strengh)
    {
        var force = new Vector3(dir.x, 0, dir.y) * bulletForce * (1 + (chargeShotForceModifier * strengh));
        chargeShot.rigidbody.detectCollisions = true;
        chargeShot.rigidbody.AddForce(force);
        rigidbody.AddForce(-force);
        
        Destroy(chargeShot, bulletLifetime);
    }

    void Shoot(Vector2 dir)
    {
        var bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity) as GameObject;
        bullet.rigidbody.AddForce(new Vector3(dir.x, 0, dir.y) * bulletForce);
        rigidbody.AddForce(-new Vector3(dir.x, 0, dir.y) * bulletForce);
        Physics.IgnoreCollision(bullet.collider, collider);
        Destroy(bullet, bulletLifetime);
    }



}
