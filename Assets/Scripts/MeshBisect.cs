using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class MeshBisect 
{

    public static Mesh[] bisect(Mesh mesh, Plane plane)
    {

        Mesh otherMesh = new Mesh();
        otherMesh.vertices = mesh.vertices;
        otherMesh.triangles = mesh.triangles;
        otherMesh.normals = mesh.normals;
        otherMesh.uv = mesh.uv;
        otherMesh.tangents = otherMesh.tangents;


        var vertices = mesh.vertices;
        var normals = mesh.normals;

        for (int i = 0; i < mesh.vertexCount; i ++)
        {
            //on the positive side
            if (plane.GetSide(vertices[i]))
            {
                //flatten?!?!
                float distance = plane.GetDistanceToPoint(vertices[i]);
                vertices[i] = vertices[i] - (plane.normal * Mathf.Abs(distance));
                normals[i] = -plane.normal;
            }
           
        }

        mesh.vertices = vertices;
        mesh.normals = normals;

        vertices = otherMesh.vertices;
        normals = otherMesh.normals;

        for (int i = 0; i < mesh.vertexCount; i++)
        {
            if (!plane.GetSide(vertices[i]))
            {
                //flatten?!?!
                float distance = plane.GetDistanceToPoint(vertices[i]);
                vertices[i] = vertices[i] + (plane.normal * Mathf.Abs(distance));
                normals[i] = -plane.normal;
            }

        }

        otherMesh.vertices = vertices;
        otherMesh.normals = normals;

        mesh.Optimize();
        otherMesh.Optimize();
        mesh.RecalculateBounds();
        otherMesh.RecalculateBounds();
        

        var meshes = new Mesh[2];
        meshes[0] = mesh;
        meshes[1] = otherMesh;
        
        return meshes;
    }


    public static Plane planeFromTransform(GameObject go, GameObject relativeTo)
    {
        Vector3 localPos = relativeTo.transform.InverseTransformPoint(go.transform.position);
        Vector3 localUp = relativeTo.transform.InverseTransformDirection(go.transform.up);
        return new Plane(localUp, localPos);
    }


}
