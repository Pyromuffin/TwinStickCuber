using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    public Light light;
    public float life;
    public float intensity;
    private float lifeTimer;
    public Color color;

	// Use this for initialization
	void Start () {
        lifeTimer = life;
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
