// ref: https://blog.applibot.co.jp/2017/08/17/tutorial-for-unity-3d-2/
using System.Collections.Generic;
using UnityEngine;

namespace PrimitiveGenerator
{
    public class TorusBuilder
    {
        public static Mesh Build(float radius, float innerRadius, int seg1, int seg2)
        {
            var vertices = new List<Vector3>();
            var triangles = new List<int>();
            var normals = new List<Vector3>();
            var uvs = new List<Vector2>();

            for(int i = 0; i <= seg2; i++) {
                float phi = Mathf.PI * 2f * i / seg2;
                float r = Mathf.Cos(phi) * innerRadius;
                float y = Mathf.Sin(phi) * innerRadius;

                for (int j = 0; j <= seg1; j++) {
                    float theta = Mathf.PI * 2f * j / seg1;
                    float x = Mathf.Cos(theta) * (radius + r);
                    float z = Mathf.Sin(theta) * (radius + r);

                    vertices.Add(new Vector3(x, y, z));
                    normals.Add(new Vector3(r * Mathf.Cos(theta), y, r * Mathf.Sin(theta)));

                    uvs.Add(new Vector2(
                        (float)j / (float)seg1,
                        (float)i / (float)seg2
                    ));
                }
            }

            for(int i = 0; i < seg1; i++) {
                for(int j = 0; j < seg2; j++) {
                    int tri = (seg1 + 1) * j + i;

                    triangles.Add(tri);
                    triangles.Add(tri + seg1 + 2);
                    triangles.Add(tri + 1);

                    triangles.Add(tri);
                    triangles.Add(tri + seg1 + 1);
                    triangles.Add(tri + seg1 + 2);
                }
            }

            Mesh mesh = new Mesh();
            mesh.name = "Torus";
            mesh.vertices = vertices.ToArray();
            mesh.normals = normals.ToArray();
            mesh.uv = uvs.ToArray();
            mesh.triangles = triangles.ToArray();

            mesh.Optimize();
            return mesh;
        }
    }
}
