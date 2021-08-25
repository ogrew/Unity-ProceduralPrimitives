# Unity-ProceduralPrimitives

Procedural builder for some basic primitives in Unity

```
- Sphere
- Box
- Cylinder
- Capsule
- Torus
- Grid(Terrain)
- Polygon(Triangle, Rectangle, Circle etc..)
- Polyhedron
```

## Screenshots
![ProceduralPrimitives](https://user-images.githubusercontent.com/21966381/130729540-6274618b-7bdd-4307-b8c7-af1184158cef.png)

## Usage

### Sphere
```cs
        _sphere.Generate(
            radius: 1f,
            level: 4,
            _yellowMaterial
        );
```
### Box
```cs
        _box.Generate(
            width: 1f,
            height: 1f,
            depth: 1f,
            segments: 4, 
            _blueMaterial
        );
```

### Cylinder
```cs
        _cylinder.Generate(
            radialSegments: 24,
            heightSegments: 24,
            topRadius: 0.5f,
            bottomRadius: 0.5f,
            height: 2f,
            _blueMaterial
        );
```

### Capsule
```cs
        _capsule.Generate(
            radius: 0.5f,
            height: 1f,
            lats: 16,
            lons: 32,
            rings: 2,
            _redMaterial
        );
```

### Torus
```cs
        _torus.Generate(
            majorRadius: .5f,
            minorRadius: .25f,
            majorSegment: 24,
            minorSegment: 16,
            _redMaterial
        );
```

### Grid(Terrain)
```cs
        _grid.Generate(
            width: 12f,
            height: 6f,
            divX: 12,
            divY: 12,
            _yellowMaterial
        );
```

### Polygon

```cs
        _polygon.Generate(
            shape: 3,
            size: 2f,
            _redMaterial
        );
```

### Polyhedron
```cs
        _polyhedron.Generate(
            type: SolidType.Icosahedron,
            size: 1f,
            _blueMaterial
        );
```

## License

MIT