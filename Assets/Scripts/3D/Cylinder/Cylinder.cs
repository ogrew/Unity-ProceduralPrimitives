using UnityEngine;
using PrimitiveCommon;

namespace PrimitiveGenerator
{
    [RequireComponent(typeof(MeshRenderer))]
    [RequireComponent(typeof(MeshFilter))]
    public class Cylinder : PrimitiveBase
    {
        [SerializeField, Range(Const.MIN_SCALE, Const.MAX_SCALE)] float _topRadius = 0.5f;
        [SerializeField, Range(Const.MIN_SCALE, Const.MAX_SCALE)] float _bottomRadius = 0.5f;
        [SerializeField, Range(3, 50)] int _radialSegments = 24;
        [SerializeField, Range(3, 50)] int _heightSegments = 24;
        [SerializeField, Range(1f, 50f)] float _height = 1f;
        [SerializeField] bool _endCap = true;
        [SerializeField] MeshFilter _filter;
        [SerializeField] MeshRenderer _renderer;
        [SerializeField] Material _material;
        private Mesh _mesh;

        public void Generate(int radialSegments, int heightSegments, float topRadius, float bottomRadius, float height, Material material = null, bool endCap = true)
        {
            CreateMesh(radialSegments, heightSegments, topRadius, bottomRadius, height, endCap);
            UpdateMesh();
            if(material != null) {
                SetMaterial(material, _renderer);
            }
        }

        private void CreateMesh(int radialSegments, int heightSegments, float topRadius, float bottomRadius, float height, bool endCap)
        {
            _mesh = CylinderBuilder.Build(radialSegments, heightSegments, topRadius, bottomRadius, height, 1f, endCap);
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
