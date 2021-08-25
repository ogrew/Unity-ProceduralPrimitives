using UnityEngine;
using PrimitiveCommon;

namespace PrimitiveGenerator
{
    [RequireComponent(typeof(MeshRenderer))]
    [RequireComponent(typeof(MeshFilter))]
    public class Torus : PrimitiveBase
    {
        [SerializeField, Range(Const.MIN_SCALE, Const.MAX_SCALE)] float _majorRadius = .5f;
        [SerializeField, Range(Const.MIN_SCALE, Const.MAX_SCALE)] float _minorRadius = .25f;
        [SerializeField, Range(3, 50)] int _majorSegment = 24;
        [SerializeField, Range(3, 50)] int _minorSegment = 18;
        [SerializeField] MeshFilter _filter;
        [SerializeField] MeshRenderer _renderer;
        [SerializeField] Material _material;

        private Mesh _mesh;

        public void Generate(float majorRadius, float minorRadius, int majorSegment, int minorSegment, Material material = null)
        {
            CreateMesh(majorRadius, minorRadius, majorSegment, minorSegment);
            UpdateMesh();
            if(material != null) {
                SetMaterial(material, _renderer);
            }
        }

        private void CreateMesh(float majorRadius, float minorRadius, int majorSegment, int minorSegment)
        {
            _mesh = TorusBuilder.Build(majorRadius, minorRadius, majorSegment, minorSegment);
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
            var mat = _renderer.material;
            if (mat != null)
            {
                Destroy(mat);
            }
        }
    }
}
