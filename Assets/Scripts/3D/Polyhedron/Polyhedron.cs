using UnityEngine;
using PrimitiveCommon;

namespace PrimitiveGenerator
{
    [RequireComponent(typeof(MeshRenderer))]
    [RequireComponent(typeof(MeshFilter))]
    public class Polyhedron : PrimitiveBase
    {
        [SerializeField] SolidType _type = SolidType.Tetrahedron;
        [SerializeField, Range(Const.MIN_SCALE, Const.MAX_SCALE)] float _size = 1f;
        [SerializeField] MeshFilter _filter;
        [SerializeField] MeshRenderer _renderer;
        [SerializeField] Material _material;

        private MeshData _data;

        public void Generate(SolidType type, float size, Material material = null)
        {
            CreateMesh(type, size);
            UpdateMesh();
            if(material != null) {
                SetMaterial(material, _renderer);
            }
        }

        private IPlatonicSolid GetBase(SolidType type)
        {
            switch (type)
            {
                case SolidType.Tetrahedron:
                    return new Tetrahedron();
                case SolidType.Octahedron:
                    return new Octahedron();
                case SolidType.Dodecahedron:
                    return new Dodecahedron();
                case SolidType.Icosahedron:
                    return new Icosahedron();
                default:
                    Debug.LogWarning("[WARN] Input Invalid SolidType");
                    return new Tetrahedron(); // DEFAULT
            }
        }

        private void CreateMesh(SolidType type, float size)
        {
            IPlatonicSolid solid = GetBase(type);
            _data = PolyhedronBuilder.Build(solid, size);
        }

        private void UpdateMesh()
        {
            if (!_filter.sharedMesh)
            {
                _filter.sharedMesh = new Mesh();
            }

            _filter.sharedMesh.Clear();
            _filter.sharedMesh.vertices = _data.Vertices;
            _filter.sharedMesh.triangles = _data.Triangles;
            _filter.sharedMesh.uv = _data.Uv;
            _filter.sharedMesh.normals = _data.Normals;
        }

        private void OnDestroy()
        {
            var mat = _renderer.material;
            if (mat != null)
            {
                Destroy(mat);
            }
        }

        // private void OnValidate() {
        //     Generate(_type, _size, _material);
        // }
    }
}
