using UnityEngine;
using PrimitiveCommon;

namespace PrimitiveGenerator
{
    [RequireComponent(typeof(MeshRenderer))]
    [RequireComponent(typeof(MeshFilter))]
    public class Grid : PrimitiveBase
    {
        [SerializeField, Range(Const.MIN_SCALE, Const.MAX_SCALE)] float _width = 1f;
        [SerializeField, Range(Const.MIN_SCALE, Const.MAX_SCALE)] float _height = 1f;
        [SerializeField, Range(0, 60)] int _divisionX = 15;
        [SerializeField, Range(0, 60)] int _divisionY = 15;
        [SerializeField] bool _isBackVisible = true;
        [SerializeField] MeshFilter _filter;
        [SerializeField] MeshRenderer _renderer;
        [SerializeField] Material _material;

        private Mesh _mesh;

        public void Generate(float width, float height, int divX, int divY, Material material = null)
        {
            CreateMesh(width, height, divX, divY);
            UpdateMesh();
            if(material != null) {
                SetMaterial(material, _renderer);
            }
        }

        private void CreateMesh(float width, float height,int divX, int divY)
        {
            Vector2 division = new Vector2(divX, divY);
            _mesh = GridBuilder.Build(width, height, division, _isBackVisible);
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
