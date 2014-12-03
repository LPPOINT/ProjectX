using System;
using UnityEngine;

namespace Assets.Classes.Foundation.Extensions
{
    public static class RectTransformExtensions
    {
        public static float GetWorldWidth(this RectTransform transform)
        {
            var corners = new Vector3[4];
            transform.GetWorldCorners(corners);
            var h = Math.Abs(corners[1].y - corners[2].y);
            return h;
        }

        public static float GetWorldHeight(this RectTransform transform)
        {
            var corners = new Vector3[4];
            transform.GetWorldCorners(corners);
            var h = Math.Abs(corners[0].y - corners[1].y);
            return h;
        }
    }
}
