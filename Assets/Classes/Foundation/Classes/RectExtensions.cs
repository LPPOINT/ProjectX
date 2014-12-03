using UnityEngine;

namespace Assets.Classes.Foundation.Classes
{
    public static class RectExtensions
    {
        public static int SizeInBytes(this Rect r)
        {
            const int sizeOfFloat = sizeof(float);

            return (sizeOfFloat * 6);
        }
    }
}