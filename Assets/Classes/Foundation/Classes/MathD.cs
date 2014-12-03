using System.Runtime.InteropServices;

namespace Assets.Classes.Foundation.Classes
{
    [StructLayout(LayoutKind.Sequential)]
    public struct MathD
    {
        public static double Clamp(double value, double min, double max)
        {
            if (value < min)
            {
                value = min;
                return value;
            }

            if (value > max)
                value = max;

            return value;
        }

        public static double Clamp01(double value)
        {
            if (value < 0d)
                return 0d;

            if (value > 1d)
                return 1d;

            return value;
        }
    }
}