using System.Collections.Generic;
using UnityEngine;

namespace PrimitiveGenerator
{
    public class PolygonBuilder
    {
        public static Mesh Build(int shape, float radius)
        {
            if(shape < 3) {
                shape = 3;
            }
            var vertices = new List<Vector3>();
            var uvs = new List<Vector2>();
            var triangles = new List<int>();

            var center = Vector3.zero;
            vertices.Add(center);
            uvs.Add(new Vector2(0.5f, 0.5f));

            float stepAngle = 2.0f * Mathf.PI / shape;

            float x, y, s, t;
            for (int i = 0; i < shape; i++)
            {
                float angle = i * stepAngle;
                x = radius * Mathf.Cos(angle);
                y = radius * Mathf.Sin(angle);
                vertices.Add(new Vector3(x, y, 0));

                s = 0.5f + Mathf.Cos(angle) / 2f;
                t = 0.5f + Mathf.Sin(angle) / 2f;
                uvs.Add(new Vector2(s, t));
            }

            for (int i = 0; i < shape-1; i++)
            {
                triangles.Add(0);
                triangles.Add(i + 2);
                triangles.Add(i + 1);
            }
            triangles.Add(0);
            triangles.Add(1);
            triangles.Add(shape);

            Mesh mesh = new Mesh();
            mesh.name = "Polygon";
            mesh.vertices = vertices.ToArray();
            mesh.triangles = triangles.ToArray();
            mesh.uv = uvs.ToArray();

            mesh.RecalculateNormals();
            mesh.RecalculateTangents();
            mesh.Optimize();

            return mesh;
        }
    }
}
