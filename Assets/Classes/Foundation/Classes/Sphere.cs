using Assets.Classes.Foundation.Extensions;
using UnityEngine;

namespace Assets.Classes.Foundation.Classes
{
    public struct Sphere : ISizeInBytes
    {
        public static Sphere One { get { return new Sphere(Vector3.zero, 1.0f); } }

        private readonly Vector3 _center;
        private readonly float _radius;

        public Vector3 Center { get { return _center; } }
        public float Radius { get { return _radius; } }

        public Sphere(Vector3 center, float radius)
        {
            _radius = radius;
            _center = center;
        }

        public int SizeInBytes()
        {
            var sizeOfVector3 = _center.SizeInBytes();
            const int sizeOfFloat = sizeof(float);

            return sizeOfVector3 + sizeOfFloat;
        }
    }
}