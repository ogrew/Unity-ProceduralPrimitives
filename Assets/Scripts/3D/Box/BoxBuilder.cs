using UnityEngine;

namespace PrimitiveGenerator
{
    public class BoxBuilder
    {
        public static Mesh Build(float width, int step, float height, float depth, float scale) 
        {
            Vector3 Size = new Vector3(width * scale, height * scale, depth * scale);
            Vector3 Half = Size / 2f;

            var coordXY = new Vector3[] {Vector3.right, Vector3.up};
            var front = CreatePlane(
                coordXY, new Vector2(Size.x, Size.y), 
                step, new Vector3(-Half.x, -Half.y, Half.z),
                Vector3.forward);
            front.RecalculateNormals();
            front.RecalculateTangents();
            var back = CreatePlane(
                coordXY, new Vector2(Size.x, Size.y),
                step,  new Vector3(Half.x, -Half.y, -Half.z),
                Vector3.back);
            back.RecalculateNormals();
            back.RecalculateTangents();

            var coordXZ = new Vector3[] {Vector3.right, Vector3.back};
            var top = CreatePlane(
                coordXZ, new Vector2(Size.x, Size.z),
                step, new Vector3(-Half.x, Half.y, -Half.z),
                Vector3.up);
            top.RecalculateNormals();
            top.RecalculateTangents();
            var bottom = CreatePlane(
                coordXZ, new Vector2(Size.x, Size.z),
                step, new Vector3(-Half.x, -Half.y, Half.z),
                Vector3.down);
            bottom.RecalculateNormals();
            bottom.RecalculateTangents();

            var coordZY = new Vector3[] {Vector3.back, Vector3.up};
            var right = CreatePlane(
                coordZY, new Vector2(Size.z, Size.y),
                step, new Vector3(Half.x, -Half.y, -Half.z),
                Vector3.right);
            right.RecalculateNormals();
            right.RecalculateTangents();
            var left = CreatePlane(
                coordZY, new Vector2(Size.z, Size.y),
                step, new Vector3(-Half.x, -Half.y, Half.z),
                Vector3.left);
            left.RecalculateNormals();
            left.RecalculateTangents();

            var q = Quaternion.Euler(new Vector3(0, 0, 0));
            var one = Vector3.one;
            var planes = new CombineInstance[6];

            planes[0].mesh = front;
            var q0 = Quaternion.Euler(new Vector3(0, 0, 0));
            planes[0].transform = Matrix4x4.TRS(new Vector3(0, 0, -Size.z), q0, one);

            planes[1].mesh = back;
            var q1 = Quaternion.Euler(new Vector3(0, 180f, 0));
            planes[1].transform = Matrix4x4.TRS(new Vector3(Size.x, 0, 0), q1, one);

            planes[2].mesh = top;
            var q2 = Quaternion.Euler(new Vector3(180f, 0, 0));
            planes[2].transform = Matrix4x4.TRS(new Vector3(0, Size.y, -Size.z), q2, one);

            planes[3].mesh = bottom;
            var q3 = Quaternion.Euler(new Vector3(0, 0, 0));
            planes[3].transform = Matrix4x4.TRS(new Vector3(0, 0, 0), q3, one);

            planes[4].mesh = right;
            var q4 = Quaternion.Euler(new Vector3(0, 180f, 0));
            planes[4].transform = Matrix4x4.TRS(new Vector3(Size.x, 0, -Size.z), q4, one);

            planes[5].mesh = left;
            var q5 = Quaternion.Euler(new Vector3(0, 0, 0));
            planes[5].transform = Matrix4x4.TRS(new Vector3(0, 0, 0), q5, one);

            var box = new Mesh();
            box.CombineMeshes(planes);

            return box;
        }

        private static Mesh CreatePlane(Vector3[] coord, Vector2 size, int division, Vector3 offset, Vector3 normal)
        {
            Vector2 Step = size / division;

            var vertices = new Vector3[(division + 1) * (division + 1)];
            var uvs = new Vector2[vertices.Length];
            var normals = new Vector3[vertices.Length];
            var triangles = new int[division * division * 2 * 3];

            int tIndex = 0;
            for(int dy = 0; dy <= division; dy++) {
                for(int dx = 0; dx <= division; dx++) {
                    float sx = dx * Step.x;
                    float sy = dy * Step.y;
                    int vIndex = dy * (division + 1) + dx;
                    Vector3 vert = sx * coord[0] + sy * coord[1];
                    vertices[vIndex] = vert + offset;
                    normals[vIndex] = normal;
                    uvs[vIndex] = new Vector2( sx / size.x, sy / size.y );

                    if(dx == division || dy == division) {
                        continue;
                    }

                    triangles[tIndex++] = vIndex;
                    triangles[tIndex++] = vIndex + division + 1;
                    triangles[tIndex++] = vIndex + division + 2;

                    triangles[tIndex++] = vIndex;
                    triangles[tIndex++] = vIndex + division + 2;
                    triangles[tIndex++] = vIndex + 1;
   
                }
            }

            var mesh = new Mesh();
            mesh.name = "Box";
            mesh.vertices = vertices;
            mesh.normals = normals;
            mesh.triangles = triangles;
            mesh.uv = uvs;

            mesh.RecalculateTangents();
            mesh.Optimize();

            return mesh;
        }
    }
}
