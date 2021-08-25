using UnityEngine;
using PrimitiveGenerator;

public class PrimitiveManager : MonoBehaviour
{
    Sphere _sphere;
    Box _box;
    Polygon _polygon;
    Capsule _capsule;
    Cylinder _cylinder;
    Torus _torus;
    Polyhedron _polyhedron;
    PrimitiveGenerator.Grid _grid;

    [Header("Primitives")]
    [SerializeField] Sphere _spherePrefab;
    [SerializeField] Box _boxPrefab;
    [SerializeField] Polygon _polygonPrefab;
    [SerializeField] Capsule _capsulePrefab;
    [SerializeField] Cylinder _cylinderPrefab;
    [SerializeField] Torus _torusPrefab;
    [SerializeField] Polyhedron _polyhedronPrefab;
    [SerializeField] PrimitiveGenerator.Grid _gridPrefab;

    [Header("Material")]
    [SerializeField] Material _defaultMaterial;
    [SerializeField] Material _redMaterial;
    [SerializeField] Material _blueMaterial;
    [SerializeField] Material _yellowMaterial;

    [Header("GameObject")]
    [SerializeField] Transform _parent;

    private void Start()
    {
        _polygon = Instantiate(_polygonPrefab);
        _polygon.Generate(
            shape: 3,
            size: 2f,
            _redMaterial
        );
        _polygon.gameObject.transform.SetParent(_parent);
        _polygon.Set(position: new Vector3(0, 0, 0));

        _box = Instantiate(_boxPrefab);
        _box.Generate(
            width: 1f,
            height: 1f,
            depth: 1f,
            segments: 4, 
            _blueMaterial
        );
        _box.gameObject.transform.SetParent(_parent);
        _box.Set(position: new Vector3(2, 0, 0));

        _grid = Instantiate(_gridPrefab);
        _grid.Generate(
            width: 12f,
            height: 6f,
            divX: 12,
            divY: 12,
            _yellowMaterial
        );
        _grid.gameObject.transform.SetParent(_parent);
        _grid.Set(position: new Vector3(0, -2f, 0));

        _capsule = Instantiate(_capsulePrefab);
        _capsule.Generate(
            radius: 0.5f,
            height: 1f,
            lats: 16,
            lons: 32,
            rings: 2,
            _redMaterial
        );
        _capsule.gameObject.transform.SetParent(_parent);
        _capsule.Set(position: new Vector3(4, 0, 0));

        _cylinder = Instantiate(_cylinderPrefab);
        _cylinder.Generate(
            radialSegments: 24,
            heightSegments: 24,
            topRadius: 0.5f,
            bottomRadius: 0.5f,
            height: 2f,
            _blueMaterial
        );
        _cylinder.gameObject.transform.SetParent(_parent);
        _cylinder.Set(position: new Vector3(6, 0, 0));

        _sphere = Instantiate(_spherePrefab);
        _sphere.Generate(
            radius: 1f,
            level: 4,
            _yellowMaterial
        );
        _sphere.gameObject.transform.SetParent(_parent);
        _sphere.Set(position: new Vector3(-2, 0, 0));

        _torus = Instantiate(_torusPrefab);
        _torus.Generate(
            majorRadius: .5f,
            minorRadius: .25f,
            majorSegment: 24,
            minorSegment: 16,
            _redMaterial
        );
        _torus.gameObject.transform.SetParent(_parent);
        _torus.Set(position: new Vector3(-4, 0, 0));

        _polyhedron = Instantiate(_polyhedronPrefab);
        _polyhedron.Generate(
            type: SolidType.Icosahedron,
            size: 1f,
            _blueMaterial
        );
        _polyhedron.gameObject.transform.SetParent(_parent);
        _polyhedron.Set(position: new Vector3(-6, 0, 0));
    }
}
