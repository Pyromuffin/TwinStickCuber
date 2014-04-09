using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class Settings : MonoBehaviour {

    public static Settings instance;

    public Selector players, size, speed, friendlyFire, FOV, startingEnemies;
    public Button startButton, escapeFate;
    public Canvas scoreCanvas;

    public GameObject[] arenas;
    public GameObject dude1, dude2, dude3, dude4;


	// Use this for initialization
	void Start () {
        instance = this;
        startButton.onClick.AddListener(StartGame);
        escapeFate.onClick.AddListener(escape);
	}

    void StartGame(Button butt)
    {

        Instantiate(arenas[size.selection]);
        scoreCanvas.gameObject.SetActive(true);
        GetComponent<Canvas>().enabled = false;

        if (players.selection == 0)
        {
            Instantiate(dude1, new Vector3(2, 3, 2), Quaternion.identity);
        }
        if (players.selection == 1)
        {
            Instantiate(dude1, new Vector3(2, 3, 2), Quaternion.identity);
            Instantiate(dude2, new Vector3(-2, 3, 2), Quaternion.identity);
        }
        if (players.selection == 2)
        {
            Instantiate(dude1, new Vector3(2, 3, 2), Quaternion.identity);
            Instantiate(dude2, new Vector3(-2, 3, 2), Quaternion.identity);
            Instantiate(dude3, new Vector3(-2, 3, -2), Quaternion.identity);
        }
        if (players.selection == 3)
        {
            Instantiate(dude1, new Vector3(2, 3, 2), Quaternion.identity);
            Instantiate(dude2, new Vector3(-2, 3, 2), Quaternion.identity);
            Instantiate(dude3, new Vector3(-2, 3, -2), Quaternion.identity);
            Instantiate(dude4, new Vector3(2, 3, -2), Quaternion.identity);
        }


        switch (friendlyFire.selection)
        {
            case 0:
                break;
            case 1:
                Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Bullet"),  LayerMask.NameToLayer("Dude"), false);
                Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Bullet"), LayerMask.NameToLayer("Enemy"), true);
                break;
            case 2:
                Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Bullet"), LayerMask.NameToLayer("Dude"), false);
                break;
        }

    }


    void escape(Button butt)
    {
        Application.Quit();
    }

	// Update is called once per frame
	void Update () {
	
	}
}
