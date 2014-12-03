using Assets.Classes.Foundation.Extensions;
using UnityEngine;

namespace Assets.Classes.Foundation.Classes
{
    public struct Circle : ISizeInBytes
    {
        public static Sphere One { get { return new Sphere(Vector2.zero, 1.0f); } }

        private readonly Vector2 _center;
        private readonly float _radius;

        public Vector2 Center { get { return _center; } }
        public float Radius { get { return _radius; } }

        public Circle(Vector2 center, float radius)
        {
            _radius = radius;
            _center = center;
        }

        public int SizeInBytes()
        {
            const int sizeOfFloat = sizeof(float);
            var sizeOfVector2 = _center.SizeInBytes();

            return sizeOfVector2 + sizeOfFloat;
        }
    }
}