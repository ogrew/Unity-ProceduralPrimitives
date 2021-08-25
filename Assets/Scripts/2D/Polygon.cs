using UnityEngine;
using PrimitiveCommon;

namespace PrimitiveGenerator
{
    [RequireComponent(typeof(MeshRenderer))]
    [RequireComponent(typeof(MeshFilter))]
    public class Polygon :  PrimitiveBase
    {
        [SerializeField, Range(Const.MIN_SCALE, Const.MAX_SCALE)] float _size = 1f;
        [SerializeField, Range(3, 50)] int _shape = 3;
        [SerializeField] MeshFilter _filter;
        [SerializeField] MeshRenderer _renderer;
        [SerializeField] Material _material;

        private Mesh _mesh;

        public void Generate(int shape, float size, Material material = null)
        {
            CreateMesh(shape, size);
            UpdateMesh();
            if(material != null) {
                SetMaterial(material, _renderer);
            }
        }

        private void CreateMesh(int shape, float size)
        {
            float radius = size / 2f;
            _mesh = PolygonBuilder.Build(shape, radius);
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