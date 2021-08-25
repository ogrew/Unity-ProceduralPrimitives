using System.Collections.Generic;
using UnityEngine;

namespace PrimitiveGenerator
{
    public interface IPlatonicSolid
    {
        List<Vector3> Vertices { get; }
        List<TriangleFace> Faces { get; }
        Vector3 NorthPole { get; }
    }

}
