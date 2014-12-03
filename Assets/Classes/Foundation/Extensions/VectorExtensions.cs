using Assets.Classes.Foundation.Classes;
using UnityEngine;

namespace Assets.Classes.Foundation.Extensions
{
    public static class VectorExtensions
    {

        public static Point2 ToPoint2(this Vector3 v)
        {
            return new Point2((int)v.x, (int)v.z);
        }

        public static Vector3 Clamp(this Vector3 vector, Vector3 min, Vector3 max)
        {
            var x = vector.x;
            x = (x < min.x) ? min.x : x;
            x = (x > max.x) ? max.x : x;

            var y = vector.y;
            y = (y < min.y) ? min.y : y;
            y = (y > max.y) ? max.y : y;

            var z = vector.z;
            z = (z < min.z) ? min.z : z;
            z = (z > max.z) ? max.z : z;

            return new Vector3(x, y, z);
        }

        public static float DistanceSquared(this Vector3 vector1, Vector3 vector2)
        {
            var x = vector1.x - vector2.x;
            var y = vector1.y - vector2.y;
            var z = vector1.z - vector2.z;

            return (((x * x) + (y * y)) + (z * z));
        }

        public static Vector3 FromVector2(this Vector3 vector, Vector2 vector2)
        {
            return new Vector3(vector2.x, vector2.y);
        }

        public static int SizeInBytes(this Vector3 v)
        {
            const int sizeOfFloat = sizeof(float);

            return sizeOfFloat * 3;
        }

        public static int SizeInBytes(this Vector2 v)
        {
            const int sizeOfFloat = sizeof(float);

            return sizeOfFloat * 2;
        }

        public static int SizeInBytes(this Vector4 v)
        {
            const int sizeOfFloat = sizeof(float);

            return sizeOfFloat * 4;
        }
    }
}