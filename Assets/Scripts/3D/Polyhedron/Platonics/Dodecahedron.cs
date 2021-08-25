using System.Collections.Generic;
using UnityEngine;

namespace PrimitiveGenerator
{
    public class Dodecahedron : IPlatonicSolid
    {
        public List<Vector3> Vertices { private set; get; }
        public List<TriangleFace> Faces { private set; get; }
        public Vector3 NorthPole { private set; get; }
        public Dodecahedron()
        {
            float t = (1f + Mathf.Sqrt(5f)) / 2f;
            List<Vector3> vertices = new List<Vector3>();
            // center cube coordinates
            vertices.Add(new Vector3(1f, 1f, 1f));          //0
            vertices.Add(new Vector3(-1f, -1f, -1f));       //1
            vertices.Add(new Vector3(-1f, 1f, 1f));         //2
            vertices.Add(new Vector3(1f, -1f, 1f));         //3
            vertices.Add(new Vector3(1f, 1f, -1f));         //4
            vertices.Add(new Vector3(1f, -1f, -1f));        //5
            vertices.Add(new Vector3(-1f, 1f, -1f));        //6
            vertices.Add(new Vector3(-1f, -1f, 1f));        //7
                                                            // yz plane coordinates
            vertices.Add(new Vector3(0f, 1f / t, t));   //8
            vertices.Add(new Vector3(0f, 1f / t, -t));  //9
            vertices.Add(new Vector3(0f, -1f / t, t));  //10
            vertices.Add(new Vector3(0f, -1f / t, -t)); //11
                                                        // xy plnae coordinates
            vertices.Add(new Vector3(1f / t, t, 0f));       //12
            vertices.Add(new Vector3(-1f / t, t, 0f));  //13
            vertices.Add(new Vector3(1f / t, -t, 0f));  //14
            vertices.Add(new Vector3(-1f / t, -t, 0f)); //15
                                                        // xz planet coordinates
            vertices.Add(new Vector3(t, 0f, 1f / t));       //16
            vertices.Add(new Vector3(-t, 0f, 1f / t));  //17
            vertices.Add(new Vector3(t, 0f, -1f / t));  //18
            vertices.Add(new Vector3(-t, 0f, -1f / t)); //19

            //vertex at the center of each face
            Vector3 centerFace1 = (vertices[12] + vertices[4] + vertices[9] + vertices[6] + vertices[13]) / 5f;
            Vector3 centerFace2 = (vertices[12] + vertices[13] + vertices[2] + vertices[8] + vertices[0]) / 5f;
            Vector3 centerFace3 = (vertices[12] + vertices[0] + vertices[16] + vertices[18] + vertices[4]) / 5f;
            Vector3 centerFace4 = (vertices[9] + vertices[4] + vertices[18] + vertices[5] + vertices[11]) / 5f;
            Vector3 centerFace5 = (vertices[6] + vertices[9] + vertices[11] + vertices[1] + vertices[19]) / 5f;
            Vector3 centerFace6 = (vertices[13] + vertices[6] + vertices[19] + vertices[17] + vertices[2]) / 5f;

            Vector3 centerFace7 = (vertices[17] + vertices[19] + vertices[1] + vertices[15] + vertices[7]) / 5f;
            Vector3 centerFace8 = (vertices[14] + vertices[15] + vertices[1] + vertices[11] + vertices[5]) / 5f;
            Vector3 centerFace9 = (vertices[14] + vertices[5] + vertices[18] + vertices[16] + vertices[3]) / 5f;
            Vector3 centerFace10 = (vertices[3] + vertices[16] + vertices[0] + vertices[8] + vertices[10]) / 5f;
            Vector3 centerFace11 = (vertices[10] + vertices[8] + vertices[2] + vertices[17] + vertices[7]) / 5f;
            Vector3 centerFace12 = (vertices[15] + vertices[14] + vertices[3] + vertices[10] + vertices[7]) / 5f;

            vertices.Add(centerFace1);      //20
            vertices.Add(centerFace2);      //21
            vertices.Add(centerFace3);      //22
            vertices.Add(centerFace4);      //23
            vertices.Add(centerFace5);      //24
            vertices.Add(centerFace6);      //25
            vertices.Add(centerFace7);      //26
            vertices.Add(centerFace8);      //27
            vertices.Add(centerFace9);      //28
            vertices.Add(centerFace10);     //29
            vertices.Add(centerFace11);     //30
            vertices.Add(centerFace12);     //31

            List<TriangleFace> faces = new List<TriangleFace>();
            // face 1
            faces.Add(new TriangleFace(20, 13, 12));
            faces.Add(new TriangleFace(20, 12, 4));
            faces.Add(new TriangleFace(20, 4, 9));
            faces.Add(new TriangleFace(20, 9, 6));
            faces.Add(new TriangleFace(20, 6, 13));
            // face 2
            faces.Add(new TriangleFace(21, 13, 2));
            faces.Add(new TriangleFace(21, 2, 8));
            faces.Add(new TriangleFace(21, 8, 0));
            faces.Add(new TriangleFace(21, 0, 12));
            faces.Add(new TriangleFace(21, 12, 13));
            // face 3
            faces.Add(new TriangleFace(22, 12, 0));
            faces.Add(new TriangleFace(22, 0, 16));
            faces.Add(new TriangleFace(22, 16, 18));
            faces.Add(new TriangleFace(22, 18, 4));
            faces.Add(new TriangleFace(22, 4, 12));
            // face 4
            faces.Add(new TriangleFace(23, 4, 18));
            faces.Add(new TriangleFace(23, 18, 5));
            faces.Add(new TriangleFace(23, 5, 11));
            faces.Add(new TriangleFace(23, 11, 9));
            faces.Add(new TriangleFace(23, 9, 4));
            // face 5
            faces.Add(new TriangleFace(24, 9, 11));
            faces.Add(new TriangleFace(24, 11, 1));
            faces.Add(new TriangleFace(24, 1, 19));
            faces.Add(new TriangleFace(24, 19, 6));
            faces.Add(new TriangleFace(24, 6, 9));
            // face 6
            faces.Add(new TriangleFace(25, 6, 19));
            faces.Add(new TriangleFace(25, 19, 17));
            faces.Add(new TriangleFace(25, 17, 2));
            faces.Add(new TriangleFace(25, 2, 13));
            faces.Add(new TriangleFace(25, 13, 6));
            // face 7
            faces.Add(new TriangleFace(26, 7, 17));
            faces.Add(new TriangleFace(26, 17, 19));
            faces.Add(new TriangleFace(26, 19, 1));
            faces.Add(new TriangleFace(26, 1, 15));
            faces.Add(new TriangleFace(26, 15, 7));
            // face 8
            faces.Add(new TriangleFace(27, 15, 1));
            faces.Add(new TriangleFace(27, 1, 11));
            faces.Add(new TriangleFace(27, 11, 5));
            faces.Add(new TriangleFace(27, 5, 14));
            faces.Add(new TriangleFace(27, 14, 15));
            // face 9
            faces.Add(new TriangleFace(28, 14, 5));
            faces.Add(new TriangleFace(28, 5, 18));
            faces.Add(new TriangleFace(28, 18, 16));
            faces.Add(new TriangleFace(28, 16, 3));
            faces.Add(new TriangleFace(28, 3, 14));
            // face 10
            faces.Add(new TriangleFace(29, 3, 16));
            faces.Add(new TriangleFace(29, 16, 0));
            faces.Add(new TriangleFace(29, 0, 8));
            faces.Add(new TriangleFace(29, 8, 10));
            faces.Add(new TriangleFace(29, 10, 3));
            // face 11
            faces.Add(new TriangleFace(30, 10, 8));
            faces.Add(new TriangleFace(30, 8, 2));
            faces.Add(new TriangleFace(30, 2, 17));
            faces.Add(new TriangleFace(30, 17, 7));
            faces.Add(new TriangleFace(30, 7, 10));
            // face 12
            faces.Add(new TriangleFace(31, 15, 14));
            faces.Add(new TriangleFace(31, 14, 3));
            faces.Add(new TriangleFace(31, 3, 10));
            faces.Add(new TriangleFace(31, 10, 7));
            faces.Add(new TriangleFace(31, 7, 15));

            NorthPole = Vector3.up;
            Vertices = vertices;
            Faces = faces;
        }
    }

}
