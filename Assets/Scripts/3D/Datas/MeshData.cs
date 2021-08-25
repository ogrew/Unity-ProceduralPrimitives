using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshData {
		public Vector3[] Vertices { private set; get; }
		public int[] Triangles { private set; get; }
		public Vector2[] Uv { private set; get; }
		public Vector3[] Normals {private set; get; }
		public MeshData(Vector3[] vertices, int[] triangles, Vector2[] uv, Vector3[] normals) {
			Vertices = vertices;
			Triangles = triangles;
			Uv = uv;
			Normals = normals;
        }
	}
