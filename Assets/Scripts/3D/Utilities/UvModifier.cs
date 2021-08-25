using System.Collections.Generic;
using UnityEngine;

namespace PrimitiveGenerator
{
    public class UvModifier
    {
        public static Dictionary<int, int> FindAndFixeWarpedFaces(ref List<TriangleFace> faces, ref List<Vector3> vertices)
        {
            List<TriangleFace> warpedFaces = new List<TriangleFace>();
            Dictionary<int, int> checkedVert = new Dictionary<int, int>();

            // find warped faces
            foreach (TriangleFace face in faces)
            {
                Vector3 coord1 = GetUvCoords(vertices[face.v1]);
                Vector3 coord2 = GetUvCoords(vertices[face.v2]);
                Vector3 coord3 = GetUvCoords(vertices[face.v3]);

                Vector3 texNormal = Vector3.Cross(coord2 - coord1, coord3 - coord1);
                if (texNormal.z > 0)
                {
                    warpedFaces.Add(face);
                }
            }

            // fix warped faces
            foreach (TriangleFace wFace in warpedFaces)
            {
                float xCoord1 = GetUvCoords(vertices[wFace.v1]).x;
                float xCoord2 = GetUvCoords(vertices[wFace.v2]).x;
                float xCoord3 = GetUvCoords(vertices[wFace.v3]).x;

                if (xCoord1 < 0.25f)
                {
                    int newV1 = wFace.v1;
                    if (!checkedVert.TryGetValue(wFace.v1, out newV1))
                    {
                        vertices.Add(vertices[wFace.v1]);
                        checkedVert[wFace.v1] = vertices.Count - 1;
                        newV1 = vertices.Count - 1;
                    }
                    wFace.SetV1(newV1);
                }
                if (xCoord2 < 0.25f)
                {
                    int newV2 = wFace.v2;
                    if (!checkedVert.TryGetValue(wFace.v2, out newV2))
                    {
                        vertices.Add(vertices[wFace.v2]);
                        checkedVert[wFace.v2] = vertices.Count - 1;
                        newV2 = vertices.Count - 1;
                    }
                    wFace.SetV2(newV2);
                }
                if (xCoord3 < 0.25f)
                {
                    int newV3 = wFace.v3;
                    if (!checkedVert.TryGetValue(wFace.v3, out newV3))
                    {
                        vertices.Add(vertices[wFace.v3]);
                        checkedVert[wFace.v3] = vertices.Count - 1;
                        newV3 = vertices.Count - 1;
                    }
                    wFace.SetV3(newV3);
                }
            }
            return checkedVert;
        }

        public static Dictionary<int, float> FindAndFixPoleVertices(IPlatonicSolid solid, ref List<TriangleFace> faces, ref List<Vector3> vertices)
        {
            Vector3 north = solid.NorthPole;
            Vector3 south = -solid.NorthPole;

            List<int> poleVerticeInd = new List<int>();
            Dictionary<int, float> poleVertIndicesCorrectU = new Dictionary<int, float>();

            foreach (TriangleFace face in faces)
            {
                if (vertices[face.v1] == north || vertices[face.v1] == south)
                {
                    if (!poleVerticeInd.Contains(face.v1))
                    {
                        poleVerticeInd.Add(face.v1);
                    }
                    else
                    {
                        vertices.Add(vertices[face.v1] == north ? north : south);
                        face.SetV1(vertices.Count - 1);
                    }
                    float xCoordB = GetUvCoords(vertices[face.v2]).x;
                    float xCoordC = GetUvCoords(vertices[face.v3]).x;
                    float correctedU = (xCoordB + xCoordC) / 2f + 0.5f; // I am not sure why it is needed but it seems needed...

                    poleVertIndicesCorrectU[face.v1] = correctedU;
                }
            }

            return poleVertIndicesCorrectU;
        }

        private static Vector2 GetUvCoords(Vector3 vertice)
        {
            Vector3 vertCoord = vertice.normalized;
            float u = (Mathf.Atan2(vertCoord.z, vertCoord.x) / (2f * Mathf.PI));
            float v = (Mathf.Asin(vertCoord.y) / Mathf.PI) + 0.5f;

            return new Vector2(u, v);
        }
    }

}
