using UnityEngine;
using System.Collections;

public class BisectTest : MonoBehaviour {

    public GameObject planeGO;
    public Material mat;

	// Use this for initialization
	void Start () {
	   
	}

    Plane planeFromTransform(GameObject go)
    {
        Vector3 localPos = transform.InverseTransformPoint(go.transform.position);
        Vector3 localUp = transform.InverseTransformDirection(go.transform.up);
        return new Plane(localUp, localPos);
    }

    void OnGUI()
    {
        if (GUILayout.Button("Do it"))
        {
            var m1 = new GameObject();
            var m2 = new GameObject();
            m1.transform.position = transform.position;
            m2.transform.position = transform.position;
            var mf1 = m1.AddComponent<MeshFilter>();
            var mf2 = m2.AddComponent<MeshFilter>();
            var mr1 = m1.AddComponent<MeshRenderer>();
            var mr2 = m2.AddComponent<MeshRenderer>();

            var meshes = MeshBisect.bisect(GetComponent<MeshFilter>().mesh, planeFromTransform(planeGO));

            mf1.mesh = meshes[0];
            mf2.mesh = meshes[1];

            

            mr1.material = mat;
            mr2.material = mat;

            Destroy(gameObject);
            
        }
    }

	// Update is called once per frame
	void Update () {
	
	}
}
