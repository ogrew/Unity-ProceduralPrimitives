using System.Collections.Generic;
using UnityEngine;

namespace PrimitiveGenerator
{
    public class Tetrahedron : IPlatonicSolid
    {
        public List<Vector3> Vertices { private set; get; }
        public List<TriangleFace> Faces { private set; get; }
        public Vector3 NorthPole { private set; get; }
        public Tetrahedron()
        {
            List<Vector3> vertices = new List<Vector3>();
            vertices.Add(new Vector3(1f, 1f, 1f));
            vertices.Add(new Vector3(1f, -1f, -1f));
            vertices.Add(new Vector3(-1f, 1f, -1f));
            vertices.Add(new Vector3(-1f, -1f, 1f));

            List<TriangleFace> faces = new List<TriangleFace>();
            faces.Add(new TriangleFace(0, 1, 2));
            faces.Add(new TriangleFace(0, 2, 3));
            faces.Add(new TriangleFace(1, 3, 2));
            faces.Add(new TriangleFace(0, 3, 1));

            NorthPole = Vector3.up;
            Vertices = vertices;
            Faces = faces;
        }
    }

}
