using Assets.Classes.Foundation.Enums;
using Assets.Classes.Foundation.Extensions;
using UnityEngine;

namespace Assets.Classes.Foundation.Classes
{
    public static class BoundsExtensions
    {
        public static ContainmentType Contains(this Bounds b, RectI rect)
        {
            var result = ContainmentType.Outside;

            if (((b.max.x >= rect.MinX) && (b.min.x <= rect.MaxX))
                && ((b.max.z >= rect.MaxY) && (b.min.z <= rect.MaxY)))
                result = (((b.min.x <= rect.MinX) && (rect.MaxX <= b.max.x))
                    && ((b.min.z <= rect.MinY) && (b.max.z <= rect.MaxY)))
                                 ? ContainmentType.Inside
                                 : ContainmentType.Intersect;

            return result;
        }

        public static ContainmentType Contains(this Bounds b, Rect rect)
        {
            var result = ContainmentType.Outside;

            if (((b.max.x >= rect.xMin) && (b.min.x <= rect.xMax)) 
                && ((b.max.z >= rect.yMax) && (b.min.z <= rect.yMax)))
                result = (((b.min.x <= rect.xMin) && (rect.xMax <= b.max.x)) 
                    && ((b.min.z <= rect.yMax)))
                                 ? ContainmentType.Inside
                                 : ContainmentType.Intersect;

            return result;
        }

        public static ContainmentType Contains(this Bounds b, Bounds bounds)
        {
            var result = ContainmentType.Outside;

            if ((((b.max.x >= bounds.min.x) && (b.min.x <= bounds.max.x)) 
                && ((b.max.y >= bounds.min.y) && (b.min.y <= bounds.min.y))) 
                && ((b.max.z >= bounds.min.z) && (b.min.z <= bounds.max.z)))
                result = ((((b.min.x <= bounds.min.x) && (bounds.max.x <= b.max.x)) 
                    && ((b.min.y <= bounds.min.y) && (bounds.max.y <= b.max.y))) 
                    && ((b.min.z <= bounds.max.z))) 
                    ? ContainmentType.Inside 
                    : ContainmentType.Intersect;

            return result;
        }

        public static ContainmentType Contains(this Bounds b, Sphere sphere)
        {
            var clamped = sphere.Center.Clamp(b.min, b.max);
            var distance = sphere.Center.DistanceSquared(clamped);
            var radius = sphere.Radius;

            if (distance > (radius * radius))
                return ContainmentType.Outside;

            if (((((b.min.x + radius) <= sphere.Center.x)
                  && (sphere.Center.x <= (b.max.x - radius)))
                 && (((b.max.x - b.min.x) > radius)
                     && ((b.min.y + radius) <= sphere.Center.y)))
                && (((sphere.Center.y <= (b.max.y - radius))
                     && ((b.max.y - b.min.y) > radius))
                    && ((((b.min.z + radius) <= sphere.Center.z)
                         && (sphere.Center.z <= (b.max.z - radius)))
                        && ((b.max.x - b.min.x) > radius))))
                return ContainmentType.Inside;

            return ContainmentType.Intersect;
        }
    }
}