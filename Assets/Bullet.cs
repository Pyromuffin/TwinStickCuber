using UnityEngine;
using System.Collections;
using System.Linq;

public class Bullet : MonoBehaviour {

    public Light light;
    public float life;
    public float intensity;
    private float lifeTimer;
    public Color color;
    public playerIndex player;

	// Use this for initialization
	void Start () {
        lifeTimer = life;
        var players = FindObjectsOfType<TwinStickController>();
        Physics.IgnoreCollision(collider, players.Where(p => p.player == player).First().collider);
	}
	
	// Update is called once per frame
	void Update () {
        lifeTimer -= Time.deltaTime;
        light.intensity = (lifeTimer / life) * intensity;
        renderer.material.SetColor("_Color", new Color(color.r, color.g, color.b, lifeTimer/life));
        if (lifeTimer <= 0)
            Destroy(gameObject);
	}
}
