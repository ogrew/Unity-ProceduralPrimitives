// ref: https://github.com/sanukin39/UniPlaneMeshGenerator/blob/main/Assets/UniPlaneMeshGenerator/Runtime/PlaneMeshGenerator.cs
    
using UnityEngine;

namespace PrimitiveGenerator
{
    public class GridBuilder
    {
        public static Mesh Build(float width, float height, Vector2 division, bool isBackVisible)
        {
            // UIで分割なしを0としているため
            division += new Vector2(1f, 1f);

            Vector2 size = new Vector2(width, height);
            int divx = (int)division.x;
            int divy = (int)division.y;

            Vector2 step = size / division;
            Vector3 offset = new Vector3(size.x / 2f, 0, size.y / 2f);

            int vCount = (divx + 1) * (divy + 1);
            var vertices = new Vector3[vCount];
            var uvs = new Vector2[vCount];
            int tCount = (divx * divy) * 2 * 3;
            if(isBackVisible)
            {
                tCount *= 2;
            }
            var triangles = new int[tCount];

            int tIndex = 0;
            for(int dy = 0; dy <= divy; dy++) {
                for(int dx = 0; dx <= divx; dx++) {
                    float sx = dx * step.x;
                    float sy = dy * step.y;
                    int vIndex = dy * (divx + 1) + dx;
                    var pos = Vector3.right * sx + Vector3.forward * sy;
                    vertices[vIndex] = pos - offset;
                    uvs[vIndex] = new Vector2( sx / size.x, sy / size.y );

                    if(dx == divx || dy == divy) {
                        continue;
                    }

                    int backFaceOffset = tCount / 2;

                    int tIndexP1 = tIndex + 1;
                    int tIndexP2 = tIndex + 2;
                    int tIndexP3 = tIndex + 3;
                    int tIndexP4 = tIndex + 4;
                    int tIndexP5 = tIndex + 5;

                    triangles[tIndex] = vIndex;
                    triangles[tIndexP1] = vIndex + divx + 1;
                    triangles[tIndexP2] = vIndex + divx + 2;

                    triangles[tIndexP3] = vIndex;
                    triangles[tIndexP4] = vIndex + divx + 2;
                    triangles[tIndexP5] = vIndex + 1;

                    tIndex += 6;
                }
            }

            Mesh mesh = new Mesh();
            mesh.name = "Grid";
            mesh.vertices = vertices;
            mesh.triangles = triangles;
            mesh.uv = uvs;

            mesh.RecalculateNormals();
            mesh.RecalculateTangents();
            mesh.Optimize();

            return mesh;
        }
    }
}
