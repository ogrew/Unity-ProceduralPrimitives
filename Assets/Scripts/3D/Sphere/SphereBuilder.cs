// ref : http://wiki.unity3d.com/index.php/CreateIcoSphere
using System.Collections.Generic;
using UnityEngine;

namespace PrimitiveGenerator
{
    public class SphereBuilder
    {
        // P1, P2 の中点を頂点に追加
        // 追加した頂点のインデックスを返す（後で面を追加するため）
        private static int GetMiddlePoint(int p1, int p2, ref List<Vector3> vertices, ref Dictionary<long, int> cache)
        {
            long smallerIndex = p1 < p2 ? p1 : p2;
            long greaterIndex = p1 < p2 ? p2 : p1;
            long key = (smallerIndex << 32) + greaterIndex;

            int ret;
            if (cache.TryGetValue(key, out ret))
            {
                return ret;
            }

            Vector3 point1 = vertices[p1];
            Vector3 point2 = vertices[p2];
            Vector3 middle = new Vector3(
                (point1.x + point2.x) / 2f,
                (point1.y + point2.y) / 2f,
                (point1.z + point2.z) / 2f
            );

            int index = vertices.Count;
            vertices.Add(middle);

            cache.Add(key, index);

            return index;
        }

        private static Vector3[] Normalize(Vector3[] vertices)
        {
            Vector3[] normals = new Vector3[vertices.Length];

            for (int i = 0; i < normals.Length; i++)
            {
                normals[i] = vertices[i].normalized;
            }
            return normals;
        }

        private static List<int> AddTriangles(List<TriangleFace> faces)
        {
            List<int> triangles = new List<int>();

            for (int i = 0; i < faces.Count; i++)
            {
                triangles.Add(faces[i].v1);
                triangles.Add(faces[i].v2);
                triangles.Add(faces[i].v3);
            }
            return triangles;
        }

        public static Mesh Build(IPlatonicSolid solid, int level, float scale)
        {
            List<Vector3> vertices = new List<Vector3>();
            List<int> triangles = new List<int>();
            List<TriangleFace> faces = new List<TriangleFace>();

            vertices = solid.Vertices;
            faces = solid.Faces;

            Dictionary<long, int> middlePointIndexCache = new Dictionary<long, int>();

            // subdivide the triangle faces
            for (int i = 1; i < level; i++)
            {
                List<TriangleFace> newFaces = new List<TriangleFace>();
                foreach (var t in faces)
                {
                    int p1 = GetMiddlePoint(t.v1, t.v2, ref vertices, ref middlePointIndexCache);
                    int p2 = GetMiddlePoint(t.v2, t.v3, ref vertices, ref middlePointIndexCache);
                    int p3 = GetMiddlePoint(t.v3, t.v1, ref vertices, ref middlePointIndexCache);
                    newFaces.Add(new TriangleFace(t.v1, p1, p3));
                    newFaces.Add(new TriangleFace(t.v2, p2, p1));
                    newFaces.Add(new TriangleFace(t.v3, p3, p2));
                    newFaces.Add(new TriangleFace(p1, p2, p3));
                }
                faces = newFaces;
            }

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

            Mesh mesh = new Mesh();
            mesh.name = "Sphere";
            mesh.vertices = vertices.ToArray();
            mesh.triangles = triangles.ToArray();
            mesh.normals = normals;
            mesh.uv = uv;

            mesh.RecalculateNormals();
            mesh.RecalculateTangents();
            mesh.Optimize();

            return mesh;
        }



    }
}
