using UnityEngine;
using System.Collections;

using System.Collections.Generic;

public class Arena : MonoBehaviour {

    public float dropHeight;
    public int multiplier;
    public GameObject enemyPrefab;
    public static Arena instance;
    public List<Enemy> enemies;

	// Use this for initialization
	void Start () {
        instance = this;
        
	}

    public void spawn()
    {
        multiplier++;
        
        for (int i = 0; i < multiplier; i++)
        {
            var newEnemy = Instantiate(enemyPrefab) as GameObject;
            newEnemy.transform.position = collider.bounds.randomPoint() + Vector3.up * dropHeight;
        }
    }

	// Update is called once per frame
	void Update () 
    {
        if (enemies.Count == 0)
        {
            spawn();
        }

	}
}
