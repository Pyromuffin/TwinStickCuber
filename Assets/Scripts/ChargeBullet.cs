using UnityEngine;
using System.Collections;

public class ChargeBullet : MonoBehaviour {

    public GameObject bulletSpawnPrefab;
    public int spawnCount;
    public float bulletSpawnSpeed;
    public float life;
    private bool isQuitting;

	// Use this for initialization
	void Start () {
	
	}

    void OnApplicationQuit()
    {
        isQuitting = true;
    }

    void OnDestroy()
    {
        if (!isQuitting)
        {
            for (int i = 0; i < spawnCount; i++)
            {
                var bullet = Instantiate(bulletSpawnPrefab, transform.position, Quaternion.identity) as GameObject;
                bullet.rigidbody.velocity = Random.insideUnitSphere * bulletSpawnSpeed + rigidbody.velocity/10;
                Destroy(bullet, life);
            }
        }
       
        
    }

	// Update is called once per frame
	void Update () {
	
	}
}
