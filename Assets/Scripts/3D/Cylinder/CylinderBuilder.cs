// ref: https://github.com/doukasd/Unity-Components/blob/master/ProceduralCylinder/Assets/Scripts/Procedural/ProceduralCylinder.cs
using UnityEngine;

namespace PrimitiveGenerator
{
    public class CylinderBuilder
    {
        private const int MIN_RADIAL_SEGMENTS = 1;
        private const int MIN_HEIGHT_SEGMENTS = 3;
        public static Mesh Build(
            int radialSegments, int heightSegments,
            float topRadius, float bottomRadius,
            float height, float scale, bool endCap)
        {
            if (radialSegments < MIN_RADIAL_SEGMENTS) radialSegments = MIN_RADIAL_SEGMENTS;
            if (heightSegments < MIN_HEIGHT_SEGMENTS) heightSegments = MIN_HEIGHT_SEGMENTS;

            topRadius *= scale;
            bottomRadius *= scale;
            height *= scale;

            // how many vertices we need
            int vertCols = radialSegments + 1; // +1 for welding
            int vertRows = heightSegments + 1;
            int numVerts = vertCols * vertRows;
            int numUVs = numVerts;
            int numSlideTris = radialSegments * heightSegments * 2;
            int numCapTris = radialSegments - 2;
            // 3 places in the array for each tri
            int trisArrayLength = endCap ? (numSlideTris + numCapTris * 2) * 3 : numSlideTris * 2 * 3;

            Vector3[] vertices = new Vector3[numVerts];
            Vector2[] uvs = new Vector2[numUVs];
            int[] triangles = new int[trisArrayLength];

            float heightStep = height / heightSegments;
            float angleStep = 2 * Mathf.PI / radialSegments;
            float uvStepH = 1.0f / radialSegments;
            float uvStepV = 1.0f / heightSegments;

            // A. draw tube
            for (int row = 0; row < vertRows; row++)
            {
                for (int col = 0; col < vertCols; col++)
                {
                    float angle = col * angleStep;

                    float ratio = (float)(row) / vertRows;
                    float radius = mix(topRadius, bottomRadius, ratio);

                    // first and last vertex of each row at same spot
                    if (col == vertCols - 1)
                    {
                        angle = 0;
                    }

                    int index = row * vertCols + col;
                    // pos curent vertex
                    vertices[index] = new Vector3(
                        radius * Mathf.Cos(angle),
                        row * heightStep - height/2,
                        radius * Mathf.Sin(angle)
                    );

                    uvs[index] = new Vector2(
                        col * uvStepH, row * uvStepV
                    );


                    if (row == 0 || col >= vertCols - 1)
                    {
                        continue;
                    }

                    int index2 = ((row - 1) * radialSegments * 6 + col * 6);

                    if(endCap) {
                        index2 += numCapTris * 3;
                    }

                    triangles[index2 + 0] = row * vertCols + col;
                    triangles[index2 + 1] = row * vertCols + col + 1;
                    triangles[index2 + 2] = (row - 1) * vertCols + col;

                    triangles[index2 + 3] = (row - 1) * vertCols + col;
                    triangles[index2 + 4] = row * vertCols + col + 1;
                    triangles[index2 + 5] = (row - 1) * vertCols + col + 1;
                }
            }

            // B. draw caps
            if(endCap)
            {
                bool isLeftSide = true;
                int leftIndex = 0;
                int rightIndex = 0;
                int middleIndex = 0;
                int topCapVertexOffset = numVerts - vertCols;

                for (int n = 0; n < numCapTris; n++)
                {
                    int bottomCapBaseIndex = n * 3;
                    int topCapBaseIndex = (numCapTris + numSlideTris + n) * 3;

                    if (n == 0)
                    {
                        middleIndex = 0;
                        leftIndex = 1;
                        rightIndex = vertCols - 2;
                        isLeftSide = true;
                    }
                    else if (isLeftSide)
                    {
                        middleIndex = rightIndex;
                        rightIndex--;
                    }
                    else
                    {
                        middleIndex = leftIndex;
                        leftIndex++;
                    }

                    isLeftSide = !isLeftSide;

                    // assign bottom tris
                    triangles[bottomCapBaseIndex + 0] = rightIndex;
                    triangles[bottomCapBaseIndex + 1] = middleIndex;
                    triangles[bottomCapBaseIndex + 2] = leftIndex;
                    // assign top tris
                    triangles[topCapBaseIndex + 0] = topCapVertexOffset + leftIndex;
                    triangles[topCapBaseIndex + 1] = topCapVertexOffset + middleIndex;
                    triangles[topCapBaseIndex + 2] = topCapVertexOffset + rightIndex;
                }
            }
            // else
            // {
            //     for (int row = 0; row < vertRows; row++)
            //     {
            //         for (int col = 0; col < vertCols; col++)
            //         {
            //             if (row == 0 || col >= vertCols - 1)
            //             {
            //                 continue;
            //             }

            //             int index = ((row - 1) * radialSegments * 6 + col * 6) + (trisArrayLength/2);

            //             triangles[index + 0] = row * vertCols + col;
            //             triangles[index + 1] = (row - 1) * vertCols + col;
            //             triangles[index + 2] = row * vertCols + col + 1;

            //             triangles[index + 3] = (row - 1) * vertCols + col;
            //             triangles[index + 4] = (row - 1) * vertCols + col + 1;
            //             triangles[index + 5] = row * vertCols + col + 1;
            //         }
            //     } 
            // }

            Mesh mesh = new Mesh();
            mesh.name = "Cylinder";
            mesh.vertices = vertices;
            mesh.uv = uvs;
            mesh.triangles = triangles;

            mesh.RecalculateNormals();
            mesh.RecalculateBounds();

            return mesh;
        }

        private static float mix(float x, float y, float a)
        {
            return x * (1f - a) + y * a;
        }
    }
}
