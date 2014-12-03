using UnityEngine;

namespace Assets.Classes.Foundation.Extensions
{
    public static class BoundsExtensions
    {
        public static bool Contains(this Bounds b, Vector2 point)
        {
            var v3 = new Vector3(point.x, point.y) { y = b.center.y };

            return b.Contains(v3);
        }

        public static bool ContainsOrIntersects(this Bounds b, Bounds bounds)
        {
            return b.Contains(bounds) || b.Intersects(bounds);
        }

        public static bool Contains(this Bounds b, Bounds bounds)
        {
            var bMin = b.min;
            var bMax = b.max;
            var otherMin = bounds.min;
            var otherMax = bounds.max;

            return ((otherMin.x >= bMin.x) && (otherMax.x <= bMax.x))
                   && ((otherMin.y >= bMin.y) && (otherMax.y <= bMax.y))
                   && ((otherMin.z >= bMin.z) && (otherMax.z <= bMax.z));
        }

        public static bool Contains(this Bounds b, Rect rect)
        {
            var bMin = b.min;
            var bMax = b.max;

            return ((rect.xMin >= bMin.x) && (rect.xMax <= bMax.x)) && ((rect.yMin >= bMin.z) && (rect.yMax <= bMax.z));
        }

        public static Rect ToRect(this Bounds b)
        {
            return new Rect(b.min.x, b.max.y, b.size.x, b.size.y);
        }

        public static int SizeInBytes(this Bounds b)
        {
            var sizeOfVector3 = b.center.SizeInBytes();

            return sizeOfVector3 * 2;
        }
    }
}