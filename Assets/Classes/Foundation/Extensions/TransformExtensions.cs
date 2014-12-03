using UnityEngine;

namespace Assets.Classes.Foundation.Extensions
{
    public static class TransformExtensions
    {
        public static Vector3 WorldToLocal(this Transform t, Vector3 position)
        {
            return t.transform.position - position;
        }
    }
}