using UnityEngine;
using PrimitiveCommon;

namespace PrimitiveGenerator
{
    [RequireComponent(typeof(MeshRenderer))]
    [RequireComponent(typeof(MeshFilter))]
    public class Capsule : PrimitiveBase
    {
        [SerializeField, Range(Const.MIN_SCALE, Const.MAX_SCALE)] float _radius = 0.5f;
        [SerializeField, Range(Const.MIN_SCALE, Const.MAX_SCALE)] float _height = 1f;
        [SerializeField, Range(2, 50)] int _latitudes = 16;
        [SerializeField, Range(3, 50)] int _longitudes = 32;
        [SerializeField, Range(0, 20)] int _rings = 2;
        [SerializeField] Material _material;
        [SerializeField] MeshFilter _filter;
        [SerializeField] MeshRenderer _renderer;

        private Mesh _mesh;

        public void Generate(float radius, float height, int lats, int lons, int rings, Material material = null)
        {
            CreateMesh(radius, height, lats, lons, rings);
            UpdateMesh();
            if(material != null) {
                SetMaterial(material, _renderer);
            }
        }

        private void CreateMesh(float radius, float height, int lats, int lons, int rings)
        {
            _mesh = CapsuleBuilder.Build(height, radius, lats, lons, rings);
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
