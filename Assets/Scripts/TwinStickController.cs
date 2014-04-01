using UnityEngine;
using System.Collections;




public class TwinStickController : MonoBehaviour {

    public float maxSpeed, lerpFalloff,  downForce;
   
    //regular bullet stuff
    public float bulletSpeed, reloadTime, bulletLifetime, bulletForce;
    public GameObject bulletPrefab;

    //charge shot stuff
    public float maxChargeTime, chargeShotDistance, chargeShotSize, chargeShotForceModifier;
    public GameObject chargeShotPrefab;

    public GameObject laserSword;

    //laser sword
    public float laserSwordDistance;
    public float laserSwordLerpTime;
    private bool swording;


    public playerIndex player;
    private PyroPad pad;
    private float reloadTimer, chargeTimer;
    private bool charging;

    public int childCount;
    public float torquePerChild;

    private GameObject chargeShot;

	// Use this for initialization
	void Start () {
        pad = new PyroPad(player);
        
	}
    public void increaseChild()
    {
        childCount++;
    }

   
	// Update is called once per frame
	void FixedUpdate () {
        pad.Update();

        Vector3 dir = new Vector3(pad.LeftStick.x, 0, pad.LeftStick.y);
        Vector3 rightDir = new Vector3(pad.RightStick.x, 0, pad.RightStick.y);

        rigidbody.AddForce(Vector3.down * downForce);
        var torqueVector = Vector3.Cross(Vector3.up, dir);
        rigidbody.AddTorque(( torqueVector * dir.magnitude * maxSpeed) + (torqueVector * dir.magnitude * torquePerChild * childCount) );

        reloadTimer -= Mathf.Min(reloadTimer, Time.fixedDeltaTime);





        //laser swording
        if (pad.LeftTrigger > .1f)
        {
            laserSword.transform.localScale = new Vector3(.1f, .1f, pad.RightStick.magnitude * laserSwordDistance);
            laserSword.transform.position = Vector3.Lerp(laserSword.transform.position, transform.position + rightDir * laserSwordDistance / 2, laserSwordLerpTime);

            laserSword.transform.forward = rightDir;
            swording = true;

        }
        else
        {
            laserSword.transform.localScale = new Vector3(.1f, .1f, .1f);
            laserSword.transform.position = transform.position;
            swording = false;
        }

        //shoot
        if (pad.RightStick.magnitude > .1f && reloadTimer == 0 && !charging && !swording)
        {
            Shoot(pad.RightStick.normalized);
            reloadTimer = reloadTime;
        }



        //charging
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
            chargeShot.transform.position = transform.position + new Vector3(rightDir.x, 1 * strength, rightDir.z) * chargeShotDistance;
            chargeShot.transform.localScale = Vector3.one * strength  * chargeShotSize;
            chargeShot.transform.forward = rightDir;
            chargeTimer += Mathf.Min(Time.fixedDeltaTime, maxChargeTime - chargeTimer);
        }
        else if (charging)
        {
            chargeShoot(pad.RightStick.normalized, chargeTimer / maxChargeTime);
            chargeTimer = 0;
            charging = false;
        }

        

	}

    void chargeShoot(Vector2 dir, float strengh)
    {
        var force = new Vector3(dir.x, 0, dir.y) * bulletForce * (1 + (chargeShotForceModifier * strengh));
        StartCoroutine(pad.vibrateTime(1, 1, .15f));
        reloadTimer = .5f;
        chargeShot.rigidbody.detectCollisions = true;
        chargeShot.rigidbody.AddForce(force);
        rigidbody.AddForce(-force);
        
        Destroy(chargeShot, bulletLifetime);
    }

    void Shoot(Vector2 dir)
    {
        var bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity) as GameObject;
        StartCoroutine(pad.vibrateTime(.2f, 0, .1f));
        bullet.rigidbody.AddForce(new Vector3(dir.x, 0, dir.y) * bulletForce);
        rigidbody.AddForce(-new Vector3(dir.x, 0, dir.y) * bulletForce);
        Physics.IgnoreCollision(bullet.collider, collider);
        Destroy(bullet, bulletLifetime);
    }



}
