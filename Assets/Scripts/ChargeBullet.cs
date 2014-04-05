using UnityEngine;
using System.Collections;

public class ChargeBullet : MonoBehaviour {

    public GameObject bulletSpawnPrefab;
    public int spawnCount;
    public bool charged;
    public float bulletSpawnSpeed;
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
        if (!isQuitting && charged)
        {
            for (int i = 0; i < spawnCount; i++)
            {
                var bullet = Instantiate(bulletSpawnPrefab, transform.position, Quaternion.identity) as GameObject;
                bullet.rigidbody.velocity = Random.insideUnitSphere * bulletSpawnSpeed + rigidbody.velocity/10;
                
            }
        }
       
        
    }

	// Update is called once per frame
	void Update () {
	
	}
}
