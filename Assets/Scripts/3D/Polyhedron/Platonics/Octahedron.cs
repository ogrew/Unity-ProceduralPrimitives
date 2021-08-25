using System.Collections.Generic;
using UnityEngine;

namespace PrimitiveGenerator
{
    public class Octahedron : IPlatonicSolid
    {
        public List<Vector3> Vertices { private set; get; }
        public List<TriangleFace> Faces { private set; get; }
        public Vector3 NorthPole { private set; get; }
        public Octahedron()
        {
            List<Vector3> vertices = new List<Vector3>();
            vertices.Add(new Vector3(1f, 0f, 0f));
            vertices.Add(new Vector3(-1f, 0f, 0f));
            vertices.Add(new Vector3(0f, 1f, 0f));
            vertices.Add(new Vector3(0f, -1f, 0f));
            vertices.Add(new Vector3(0f, 0f, 1f));
            vertices.Add(new Vector3(0f, 0f, -1f));

            List<TriangleFace> faces = new List<TriangleFace>();
            faces.Add(new TriangleFace(2, 0, 5));
            faces.Add(new TriangleFace(2, 5, 1));
            faces.Add(new TriangleFace(2, 1, 4));
            faces.Add(new TriangleFace(2, 4, 0));

            faces.Add(new TriangleFace(3, 5, 0));
            faces.Add(new TriangleFace(3, 1, 5));
            faces.Add(new TriangleFace(3, 4, 1));
            faces.Add(new TriangleFace(3, 0, 4));

            NorthPole = Vector3.up;
            Vertices = vertices;
            Faces = faces;
        }
    }

}
