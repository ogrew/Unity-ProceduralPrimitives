using UnityEngine;
using PrimitiveCommon;

namespace PrimitiveGenerator
{
    [RequireComponent(typeof(MeshRenderer))]
    [RequireComponent(typeof(MeshFilter))]
    public class Box : PrimitiveBase
    {
        [SerializeField, Range(Const.MIN_SCALE, Const.MAX_SCALE)] float _width = 1f;
        [SerializeField, Range(Const.MIN_SCALE, Const.MAX_SCALE)] float _height = 1f;
        [SerializeField, Range(Const.MIN_SCALE, Const.MAX_SCALE)] float _depth = 1f;
        [SerializeField, Range(1, 40)] int _segments = 1;
        [SerializeField] Material _material;
        [SerializeField] MeshFilter _filter;
        [SerializeField] MeshRenderer _renderer;

        private Mesh _mesh;

        public void Generate(float width, float height, float depth, int segments, Material material = null)
        {
            CreateMesh(width, height, depth, segments);
            UpdateMesh();
            if(material != null) {
                SetMaterial(material, _renderer);
            }
        }

        private void CreateMesh(float width, float height, float depth, int segments)
        {
            _mesh = BoxBuilder.Build(width, segments, height, depth, 1f);
        }

        private void UpdateMesh()
        {
            if (!_filter?.sharedMesh)
            {
                _filter.sharedMesh = new Mesh();
            }

            _filter.sharedMesh.Clear();
            _filter.sharedMesh = _mesh;
        }

        private void OnDestroy()
        {
            var instance = _renderer.material;
            if (instance != null)
            {
                Destroy(instance);
            }
        }
    }
}
