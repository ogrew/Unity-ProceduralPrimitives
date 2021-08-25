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
![image](https://user-images.githubusercontent.com/21966381/130729996-ce17f055-a375-4081-a8d5-ea945b111f64.png)

## Usage

### [Sphere](https://github.com/ogrew/Unity-ProceduralPrimitives/blob/main/Assets/Scripts/3D/Sphere/SphereBuilder.cs)
![image](https://user-images.githubusercontent.com/21966381/130739406-704f4764-b695-4948-a9de-7f876f741976.png)
```cs
        _sphere.Generate(
            radius: 1f,
            level: 4,
            _redMaterial
        );
```
### [Box](https://github.com/ogrew/Unity-ProceduralPrimitives/blob/main/Assets/Scripts/3D/Box/BoxBuilder.cs)
![image](https://user-images.githubusercontent.com/21966381/130738860-27090e4b-f159-46e3-b7db-6f5736d53b49.png)
```cs
        _box.Generate(
            width: 1f,
            height: 1f,
            depth: 1f,
            segments: 4, 
            _yellowMaterial
        );
```

### [Cylinder](https://github.com/ogrew/Unity-ProceduralPrimitives/blob/main/Assets/Scripts/3D/Cylinder/CylinderBuilder.cs)
![image](https://user-images.githubusercontent.com/21966381/130739146-771e899d-d283-490c-a2ce-5d0c20217894.png)
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

### [Capsule](https://github.com/ogrew/Unity-ProceduralPrimitives/blob/main/Assets/Scripts/3D/Capsule/CapsuleBuilder.cs)
![image](https://user-images.githubusercontent.com/21966381/130738972-aa632a8d-58c2-4383-9a3f-345942016bb2.png)
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

### [Torus](https://github.com/ogrew/Unity-ProceduralPrimitives/blob/main/Assets/Scripts/3D/Torus/TorusBuilder.cs)
![image](https://user-images.githubusercontent.com/21966381/130739654-c24f1c1c-f523-4dab-9c19-7bcb18f7382a.png)
```cs
        _torus.Generate(
            majorRadius: .5f,
            minorRadius: .25f,
            majorSegment: 24,
            minorSegment: 16,
            _yellowMaterial
        );
```

### [Grid(Terrain)](https://github.com/ogrew/Unity-ProceduralPrimitives/blob/main/Assets/Scripts/3D/Grid/GridBuilder.cs)
![image](https://user-images.githubusercontent.com/21966381/130739845-5d56a748-0011-43e8-8818-6f62310c5b31.png)
```cs
        _grid.Generate(
            width: 12f,
            height: 6f,
            divX: 12,
            divY: 12,
            _blueMaterial
        );
```

### [Polygon](https://github.com/ogrew/Unity-ProceduralPrimitives/blob/main/Assets/Scripts/2D/PolygonBuilder.cs)
![image](https://user-images.githubusercontent.com/21966381/130740407-80fb2766-b1ef-44fa-8bc0-f4f185bb1635.png)
```cs
        _polygon.Generate(
            shape: 3,
            size: 2f,
            _redMaterial
        );
```

### [Polyhedron](https://github.com/ogrew/Unity-ProceduralPrimitives/blob/main/Assets/Scripts/3D/Polyhedron/PolyhedronBuilder.cs)
![image](https://user-images.githubusercontent.com/21966381/130741449-d83028f1-ba9b-4740-9a9a-f75a143113b0.png)
```cs
        _polyhedron.Generate(
            type: SolidType.Icosahedron,
            size: 1f,
            _redMaterial
        );
```

## License

MIT
