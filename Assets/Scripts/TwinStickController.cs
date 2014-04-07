using UnityEngine;
using System.Collections;
using System.Linq;

public class TwinStickController : MonoBehaviour {

    public float maxSpeed, lerpFalloff,  downForce;
   
    //regular bullet stuff
    public float bulletSpeed, reloadTime, bulletLifetime, bulletForce;
    public GameObject bulletPrefab;

    //charge shot stuff
    public float maxChargeTime, chargeShotDistance, chargeShotSize, chargeShotForceModifier;
    public GameObject chargeShotPrefab;

    public GameObject laserSword, laserSwordPrefab;

    //laser sword
    public float laserSwordDistance;
    public float laserSwordLerpTime, cutDuration, cutAngle;
    private bool swording, primed, cutAnimation;
    private Vector3 cutDir;

    //gravity well
    public float rotateRate, gravityRadius, gravityForce;


    public ScoreDisplay scoreDisplay;
    public playerIndex player;
    private PyroPad pad;
    private float reloadTimer, chargeTimer;
    private bool charging, charged;
    public bool dead;

    public int dieBulletCount;
    
    private GameObject chargeShot;


    //sound effects
    public AudioClip shoot, charge, chargeShootSound, chargedSound, pickUp, swing, move, gravityWell, repel, dieSound;
    public AudioSource moveSource;
    public float moveSpeedFrequency;
    private float moveSpeedCounter;

	// Use this for initialization
	void Start () {
        pad = new PyroPad(player);
        laserSword = Instantiate(laserSwordPrefab) as GameObject;
        scoreDisplay = FindObjectsOfType<ScoreDisplay>().Where(s => s.player == player).First();
	}
    

   
	// Update is called once per frame
	void FixedUpdate () {
        pad.Update();

        Vector3 dir = new Vector3(pad.LeftStick.x, 0, pad.LeftStick.y);
        Vector3 rightDir = new Vector3(pad.RightStick.x, 0, pad.RightStick.y);

        rigidbody.AddForce(Vector3.down * downForce);
        var torqueVector = Vector3.Cross(Vector3.up, dir);
        rigidbody.AddTorque((torqueVector * dir.magnitude * maxSpeed));

        reloadTimer -= Mathf.Min(reloadTimer, Time.fixedDeltaTime);


        //move sound
        if (moveSpeedCounter <= 0)
        {

            moveSource.pitch = rigidbody.angularVelocity.magnitude / 20;
            moveSource.PlayOneShot(move,.25f);
            
            moveSpeedCounter = moveSpeedFrequency;
        }
        moveSpeedCounter -= rigidbody.angularVelocity.magnitude * Time.fixedDeltaTime;


        //laser swording
        if (pad.LeftTrigger > .1f)
        {
            swording = true;
            if (!cutAnimation)
            {
                laserSword.transform.localScale = new Vector3(.1f, .1f, pad.RightStick.magnitude * laserSwordDistance);
                laserSword.transform.position = Vector3.Lerp(laserSword.transform.position, transform.position + rightDir * laserSwordDistance / 2, laserSwordLerpTime);

                if (rightDir != Vector3.zero)
                    laserSword.transform.forward = rightDir;
            }
            
            if (rightDir.magnitude >= .9)
            {
                cutDir = rightDir;
                primed = true;
            }
            if (primed & rightDir.magnitude <= .1f && !cutAnimation)
            {
                cutAnimation = true;
                audio.PlayOneShot(swing);
                StartCoroutine(cut(cutDir));
            }


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
            audio.PlayOneShot(shoot);
            Shoot(pad.RightStick.normalized);
            reloadTimer = reloadTime;
        }


        //gravity well
        if (pad.GetButton(button.RightShoulder))
        {
            if (!audio.isPlaying)
            {
                audio.clip = gravityWell;
                audio.Play();
            }
            transform.Rotate(Vector3.up, rotateRate * Time.fixedDeltaTime, Space.World);
            var pickups = Physics.OverlapSphere(transform.position, gravityRadius, 1 << LayerMask.NameToLayer("Pick up") | 1 << LayerMask.NameToLayer("Enemy"));
            if(pickups.Length != 0)
                foreach (Rigidbody r in pickups.Select(p => p.rigidbody))
                {
                    Vector3 toMe = transform.position - r.transform.position;
                    r.AddForce(toMe.normalized * (1 / toMe.magnitude) * gravityForce * Time.fixedDeltaTime);
                }
        }

        if (pad.GetButton(button.LeftShoulder))
        {
             if (!audio.isPlaying)
            {
                audio.clip = repel;
                audio.Play();
            }
            transform.Rotate(Vector3.up, -rotateRate * Time.fixedDeltaTime, Space.World);
            var pickups = Physics.OverlapSphere(transform.position, gravityRadius, 1 << LayerMask.NameToLayer("Pick up") | 1 << LayerMask.NameToLayer("Enemy"));
            if(pickups.Length != 0)
                foreach (Rigidbody r in pickups.Select(p => p.rigidbody))
                {
                    Vector3 toMe = transform.position - r.transform.position;
                    r.AddForce(-toMe.normalized * (1/toMe.magnitude) * gravityForce * Time.fixedDeltaTime);
                }
        
        }

        //charging
        if (pad.RightTrigger > .1f)
        {
            if (!charging)
            {
                chargeShot = Instantiate(chargeShotPrefab) as GameObject;
                audio.PlayOneShot(charge);
                Physics.IgnoreCollision(collider, chargeShot.collider);
                chargeShot.rigidbody.detectCollisions = false;
            }

            charging = true;
            float strength = (chargeTimer / maxChargeTime);
            if (strength >= 1 && !charged)
            {
                audio.PlayOneShot(chargedSound);
                charged = true;
            }
            chargeShot.transform.position = transform.position + new Vector3(rightDir.x, 1 * strength, rightDir.z) * chargeShotDistance;
            chargeShot.transform.localScale = Vector3.one * strength * chargeShotSize;
            if(rightDir != Vector3.zero)
                chargeShot.transform.forward = rightDir;
            chargeTimer += Mathf.Min(Time.fixedDeltaTime, maxChargeTime - chargeTimer);
            
        }
        else if (charging && charged)
        {
            audio.Stop();
            audio.PlayOneShot(chargeShootSound);
            chargeShoot(pad.RightStick.normalized, chargeTimer / maxChargeTime);
            chargeTimer = 0;
            charged = false;
            charging = false;
        }
        else if (charging)
        {
            Destroy(chargeShot);
            charging = false;
            charged = false;
            chargeTimer = 0;
        }        

	}


    IEnumerator cut(Vector3 cutDirection)
    {
        float cutTimer = 0;
        laserSword.transform.localScale = new Vector3(.1f, .1f, laserSwordDistance);
        while(cutTimer <= cutDuration)
        {
            float factor = cutTimer/cutDuration;
            
            var startRotation = Quaternion.AngleAxis(-cutAngle/2 + (cutAngle * factor), Vector3.up);
            Vector3 startPosition = transform.position + (startRotation * -cutDirection) * 5;
            laserSword.transform.position = startPosition;
            laserSword.transform.forward = startRotation * cutDirection;
            cutTimer += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
        cutAnimation = false;
        primed = false;
    }

    void chargeShoot(Vector2 dir, float strengh)
    {
        var force = new Vector3(dir.x, 0, dir.y) * bulletForce * (1 + (chargeShotForceModifier * strengh));
        StartCoroutine(pad.vibrateTime(1, 1, .15f));
        reloadTimer = .5f;
        chargeShot.GetComponent<ChargeBullet>().charged = true;
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
        
        
    }


    void OnCollisionEnter(Collision hit)
    {
        if (hit.gameObject.layer == LayerMask.NameToLayer("Pick up"))
        {
                      
            audio.PlayOneShot(pickUp);
            scoreDisplay.Score += hit.gameObject.GetComponent<PickUP>().value;
            Destroy(hit.gameObject);   

        }
        if (hit.gameObject.layer == LayerMask.NameToLayer("Enemy") && !dead)
        {
            for(int i = 0; i < dieBulletCount; i++)
            {
                var deadBullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity) as GameObject;
                deadBullet.rigidbody.velocity = Random.onUnitSphere * 50;
            }

            dead = true;
            audio.PlayOneShot(dieSound);
            renderer.enabled = false;
            laserSword.SetActive(false);
            this.enabled = false;

           
        }
    }
    

}
