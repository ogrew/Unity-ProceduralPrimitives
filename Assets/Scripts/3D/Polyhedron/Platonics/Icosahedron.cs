using System.Collections.Generic;
using UnityEngine;

namespace PrimitiveGenerator
{
    public class Icosahedron : IPlatonicSolid
    {
        public List<Vector3> Vertices { private set; get; }
        public List<TriangleFace> Faces { private set; get; }
        public Vector3 NorthPole { private set; get; }
        public Icosahedron()
        {
            float t = (1f + Mathf.Sqrt(5f)) / 2f;
            List<Vector3> vertices = new List<Vector3>();
            vertices.Add(new Vector3(-1f, t, 0f));
            vertices.Add(new Vector3(1f, t, 0f));
            vertices.Add(new Vector3(-1f, -t, 0f));
            vertices.Add(new Vector3(1f, -t, 0f));

            vertices.Add(new Vector3(0f, -1f, t));
            vertices.Add(new Vector3(0f, 1f, t));
            vertices.Add(new Vector3(0f, -1f, -t));
            vertices.Add(new Vector3(0f, 1f, -t));

            vertices.Add(new Vector3(t, 0f, -1f));
            vertices.Add(new Vector3(t, 0f, 1f));
            vertices.Add(new Vector3(-t, 0f, -1f));
            vertices.Add(new Vector3(-t, 0f, 1f));

            List<TriangleFace> faces = new List<TriangleFace>();
            // 5 faces around point 0
            faces.Add(new TriangleFace(0, 11, 5));
            faces.Add(new TriangleFace(0, 5, 1));
            faces.Add(new TriangleFace(0, 1, 7));
            faces.Add(new TriangleFace(0, 7, 10));
            faces.Add(new TriangleFace(0, 10, 11));
            // 5 adjacent faces 
            faces.Add(new TriangleFace(1, 5, 9));
            faces.Add(new TriangleFace(5, 11, 4));
            faces.Add(new TriangleFace(11, 10, 2));
            faces.Add(new TriangleFace(10, 7, 6));
            faces.Add(new TriangleFace(7, 1, 8));
            // 5 faces around point 3
            faces.Add(new TriangleFace(3, 9, 4));
            faces.Add(new TriangleFace(3, 4, 2));
            faces.Add(new TriangleFace(3, 2, 6));
            faces.Add(new TriangleFace(3, 6, 8));
            faces.Add(new TriangleFace(3, 8, 9));
            // 5 adjacent faces 
            faces.Add(new TriangleFace(4, 9, 5));
            faces.Add(new TriangleFace(2, 4, 11));
            faces.Add(new TriangleFace(6, 2, 10));
            faces.Add(new TriangleFace(8, 6, 7));
            faces.Add(new TriangleFace(9, 8, 1));

            NorthPole = new Vector3(0f, t, 0f);
            Vertices = vertices;
            Faces = faces;
        }
    }

}
