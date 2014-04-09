using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class Arena : MonoBehaviour {

    public float dropHeight;
    public int multiplier;
    public GameObject enemyPrefab;
    public static Arena instance;
    public List<Enemy> enemies;

    private PyroPad[] pads;

    private TwinStickController[] players;

	// Use this for initialization
	void Start () {
        instance = this;
        players = FindObjectsOfType<TwinStickController>();
        pads = new PyroPad[players.Length];
        players.Length.Times(i => pads[i] = new PyroPad(players[i].player));
       

        switch (Settings.instance.startingEnemies.selection)
        {
            case 0:
                multiplier = 1;
                break;
            case 1:
                multiplier = 3;
                break;
            case 2:
                multiplier = 6;
                break;
            case 3:
                multiplier = 15;
                break;
            case 4:
                multiplier = 30;
                break;
            case 5:
                multiplier = 50;
                break;
        }
	}

    public void spawn()
    {
        
        
        for (int i = 0; i < multiplier; i++)
        {
            var newEnemy = Instantiate(enemyPrefab) as GameObject;
            newEnemy.transform.position = collider.bounds.randomPoint() + Vector3.up * dropHeight;
        }

        foreach (var p in players)
        {
            if (p.dead)
            {
                p.dead = false;
                p.enabled = true;
                p.renderer.enabled = true;
                p.laserSword.SetActive(true);
            }
        }
        multiplier++;
    }

	// Update is called once per frame
	void Update () 
    {
        if (enemies.Count == 0)
        {
            spawn();
        }


        if(players.All(p => p.dead))
        {

            DeadUI.instance.canvas.enabled = true ;
            foreach (var pad in pads)
            {
                pad.Update();
                if (pad.GetButtonDown(button.Select))
                    DeadUI.instance.regret();
            }
        }
        else
        {
            DeadUI.instance.canvas.enabled = false;
        }
	}
}
