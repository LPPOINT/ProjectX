// Note that the HSB conversion is from http://www.geekymonkey.com/Programming/CSharp/RGB2HSL_HSL2RGB.htm

using System;
using UnityEngine;

namespace Assets.Classes.Foundation.Extensions
{
    public static class ColorExtensions
    {
        private const float RGBtoXYZRatio = (float)(1.0 / 255);

        #region Temperature Colors
        static readonly Color Cold = Color.blue;
        static readonly Color Mild = new Color(0, 1, 1);
        static readonly Color Warm = Color.green;
        static readonly Color Tepid = Color.yellow;
        static readonly Color Hot = Color.red;
        #endregion

        public static Color FromRGB(this Color c, int red, int green, int blue, int alpha = 255)
        {
            return new Color(red * RGBtoXYZRatio, green * RGBtoXYZRatio, blue * RGBtoXYZRatio, alpha * RGBtoXYZRatio);
        }

        public static Color FromRange(this Color c, Color start, Color end, float ratio)
        {
            return Color.Lerp(start, end, ratio);
        }

        public static Color FromTemperature(this Color c, float value)
        {
            var result = Color.white;

            if (value <= .25f)
                result = Color.Lerp(Cold, Mild, value * 4);

            if ((value > .25f) && (value <= .5f))
                result = Color.Lerp(Warm, Mild, (.5f - value) * 4);

            if ((value > .5f) && (value <= .75f))
                result = Color.Lerp(Tepid, Warm, (.75f - value) * 4);

            if (value > .75f)
                result = Color.Lerp(Hot, Tepid, (1 - value) * 4);

            return result;
        }

        public static Color FromHSB(this Color c, float hue, float saturation, float brightness, float alpha = 1.0f)
        {
            var r = 1f;
            var g = 1f;
            var b = 1f;
            var v = brightness <= 0.5f ? 1 * (1 + saturation) : 1 + saturation - 1 * saturation;

            if(v > 0)
            {
                var m = 1 + 1 - v;
                var sv = (v - m) / v;
                hue *= 6;
                var sextant = (int)hue;
                var fract = hue - sextant;
                var vsf = v * sv * fract;
                var mid1 = m + vsf;
                var mid2 = v + vsf;

                switch(sextant)
                {
                    case 0:
                        r = v;
                        g = mid1;
                        b = m;
                        break;

                    case 1:
                        r = mid2;
                        g = v;
                        b = m;
                        break;

                    case 2:
                        r = m;
                        g = v;
                        b = mid1;
                        break;

                    case 3:
                        r = m;
                        g = mid2;
                        b = v;
                        break;

                    case 4:
                        r = mid1;
                        g = m;
                        b = v;
                        break;

                    case 5:
                        r = v;
                        g = m;
                        b = mid2;
                        break;
                }
            }

            return new Color(r, g, b, alpha);
        }

        public static Vector4 ToHSB(this Color c)
        {
            var red = c.r;
            var green = c.g;
            var blue = c.b;

            float hue;

            var v = Math.Max(red, green);
            v = Math.Max(v, blue);

            var m = Math.Min(red, green);
            m = Math.Min(m, blue);

            var brightness = (m + v) * 0.5f;
            if (brightness <= 0)
                return Vector3.zero;

            var vMinusM = v - m;

            var saturation = vMinusM;
            if (saturation > 0)
                saturation /= brightness <= 0.5f ? v + m : 2.0f - v - m;
            else
                return Vector3.zero;

            var red2 = (v - red) / vMinusM;
            var green2 = (v - green) / vMinusM;
            var blue2 = (v - blue) / vMinusM;

            if (red == v)
                hue = (green == m ? 5f + blue2 : 1f - green2);
            else if (green == v)
                hue = (blue == m ? 1f + red2 : 3f - blue2);
            else
                hue = (red == m ? 3f + green2 : 5f - red2);

            hue /= 6f;

            return new Vector4(hue, saturation, brightness, c.a);
        }
    }
}