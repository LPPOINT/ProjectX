using UnityEngine;

namespace Assets.Classes.Foundation.Extensions
{
    public static class RectExtensions
    {
        public static Rect From2Points(this Rect r, Vector2 topLeft, Vector2 bottomRight)
        {
            return new Rect(topLeft.x, topLeft.y, bottomRight.x - topLeft.x, bottomRight.y - topLeft.y);
        }

        public static Vector3 Center(this Rect r)
        {
            return new Vector3(r.xMax - (r.width * .5f), 0, r.yMax - (r.height * .5f));
        }

        public static bool Contains(this Rect r, Rect rect)
        {
            return ((((r.x <= rect.x) && ((rect.x + rect.width) <= (r.x + r.width))) && (rect.y < r.y)) && ((rect.y - rect.height) >= (r.y - r.height)));
        }

        public static bool Contains(this Rect r, int x, int y)
        {
            return r.Contains(new Vector2(x, y));
        }

        public static bool Intersects(this Rect r, Rect rect)
        {
            return ((((rect.x < (r.x + r.width)) && (r.x < (rect.x + rect.width))) && (rect.y > (r.y - r.height))) && (r.y > (rect.y - rect.height)));
        }

        public static bool ContainsOrIntersects(this Rect r, Rect rect)
        {
            return Contains(r, rect) || Intersects(r, rect);
        }

        public static Bounds ToBounds(this Rect r, float y = 0)
        {
            return new Bounds(r.Center(), new Vector3(r.width, y, r.height));
        }
    }
}