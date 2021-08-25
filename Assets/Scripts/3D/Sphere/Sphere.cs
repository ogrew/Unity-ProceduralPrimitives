using UnityEngine;
using PrimitiveCommon;

namespace PrimitiveGenerator
{
    [RequireComponent(typeof(MeshRenderer))]
    [RequireComponent(typeof(MeshFilter))]
    public class Sphere : PrimitiveBase
    {
        [SerializeField, Range(Const.MIN_SCALE, Const.MAX_SCALE)] float _radius = 1f;
        [SerializeField, Range(0, 6)] int _level = 3;
        [SerializeField] MeshFilter _filter;
        [SerializeField] MeshRenderer _renderer;
        [SerializeField] Material _material;

        private Mesh _mesh;

        public void Generate(float radius, int level, Material material = null)
        {
            CreateMesh(radius, level);
            UpdateMesh();
            if(material != null) {
                SetMaterial(material, _renderer);
            }
        }

        private void CreateMesh(float radius, int level)
        {
            var icosahedron = new Icosahedron();
            _mesh = SphereBuilder.Build(icosahedron, level, radius);
        }

        private void UpdateMesh()
        {
            if (!_filter.sharedMesh)
            {
                _filter.sharedMesh = new Mesh();
            }

            _filter.sharedMesh.Clear();
            _filter.sharedMesh = _mesh;
        }

        private void OnDestroy()
        {
            var mat = _renderer.material;
            if (mat != null)
            {
                Destroy(mat);
            }
        }
    }
}
