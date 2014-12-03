using UnityEngine;

namespace Assets.Classes.Foundation.Extensions
{
    public static class ComponentExtensions
    {
        static public T GetOrAddComponent<T>(this Component child) where T : Component
        {
            return child.GetComponent<T>() ?? child.gameObject.AddComponent<T>();
        }

        static public T GetOrAddComponent<T>(this GameObject child) where T : Component
        {
            return child.GetComponent<T>() ?? child.gameObject.AddComponent<T>();
        }
    }
}
