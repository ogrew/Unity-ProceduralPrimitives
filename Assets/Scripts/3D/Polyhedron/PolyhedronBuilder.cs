using System.Collections.Generic;
using UnityEngine;

namespace PrimitiveGenerator
{
    public class PolyhedronBuilder
    {
        public static MeshData Build(IPlatonicSolid solid, float scale)
        {
            List<Vector3> vertices = new List<Vector3>();
            List<int> triangles = new List<int>();
            List<TriangleFace> faces = new List<TriangleFace>();

            vertices = solid.Vertices;
            faces = solid.Faces;

            Dictionary<int, int> vertWithWarpedU = UvModifier.FindAndFixeWarpedFaces(ref faces, ref vertices);
            Dictionary<int, float> poleVertIndicesCorrectU = UvModifier.FindAndFixPoleVertices(solid, ref faces, ref vertices);

            for (int i = 0; i < faces.Count; i++)
            {
                triangles.Add(faces[i].v1);
                triangles.Add(faces[i].v2);
                triangles.Add(faces[i].v3);
            }

            Vector2[] uv = new Vector2[vertices.Count];
            Vector3[] normals = new Vector3[vertices.Count];

            for (int i = 0; i < vertices.Count; i++)
            {
                Vector3 normal = vertices[i].normalized;
                float u = (Mathf.Atan2(normal.z, normal.x) / (2f * Mathf.PI)) + 0.5f; // remove the 0.5f
                float v = (Mathf.Asin(normal.y) / Mathf.PI) + 0.5f;

                vertices[i] = normal * scale;

                // correct uv issues
                if (poleVertIndicesCorrectU.ContainsKey(i))
                {
                    u = poleVertIndicesCorrectU[i];
                }
                if (vertWithWarpedU.ContainsValue(i))
                {
                    u += 1f;
                }

                // おまじない
                if (vertWithWarpedU.ContainsValue(i) && poleVertIndicesCorrectU.ContainsKey(i))
                {
                    u -= 0.5f;
                }

                uv[i] = new Vector2(u, v);
                normals[i] = normal;
            }

            return new MeshData(
                vertices.ToArray(),
                triangles.ToArray(),
                uv,
                normals
            );
        }
    }

}
