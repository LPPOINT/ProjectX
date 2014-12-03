using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Assets.Classes.Foundation.Classes
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Vector2F : ISizeInBytes
    {
        public const float kEpsilon = 1E-05f;
        public float x;
        public float y;

        public Vector2F(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public float this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0:
                        return x;

                    case 1:
                        return y;
                }

                throw new IndexOutOfRangeException("Invalid Vector2D index!");
            }
            set
            {
                switch (index)
                {
                    case 0:
                        x = value;
                        break;

                    case 1:
                        y = value;
                        break;

                    default:
                        throw new IndexOutOfRangeException("Invalid Vector2D index!");
                }
            }
        }

        public static Vector2F Lerp(Vector2F from, Vector2F to, float t)
        {
            t = Mathf.Clamp01(t);

            return new Vector2F(from.x + ((to.x - from.x) * t), from.y + ((to.y - from.y) * t));
        }

        public static Vector2F MoveTowards(Vector2F current, Vector2F target, float maxDistanceDelta)
        {
            var vector = target - current;
            var magnitude = vector.magnitude;

            if ((magnitude > maxDistanceDelta) && (magnitude != 0f))
                return (current + (vector / magnitude) * maxDistanceDelta);

            return target;
        }

        public static Vector2F Scale(Vector2F a, Vector2F b)
        {
            return new Vector2F(a.x * b.x, a.y * b.y);
        }

        public void Scale(Vector2F scale)
        {
            x *= scale.x;
            y *= scale.y;
        }

        public void Normalize()
        {
            var magnitude = this.magnitude;

            if (magnitude > 1E-05f)
                this = this / magnitude;
            else
                this = zero;
        }

        public Vector2F normalized
        {
            get
            {
                var vector = new Vector2F(x, y);
                vector.Normalize();

                return vector;
            }
        }

        public override string ToString()
        {
            return string.Format("({0:F1}, {1:F1})", x, y);
        }

        public string ToString(string format)
        {
            return string.Format("({0}, {1})", x.ToString(format), y.ToString(format));
        }

        public override int GetHashCode()
        {
            return (x.GetHashCode() ^ (y.GetHashCode() << 2));
        }

        public int SizeInBytes()
        {
            const int sizeOfDouble = sizeof(float);

            return (sizeOfDouble * 2);
        }

        public override bool Equals(object other)
        {
            if (!(other is Vector2F))
                return false;

            var vector = (Vector2F)other;
            return (x.Equals(vector.x) && y.Equals(vector.y));
        }

        public static float Dot(Vector2F lhs, Vector2F rhs)
        {
            return ((lhs.x * rhs.x) + (lhs.y * rhs.y));
        }

        public float magnitude
        {
            get { return Mathf.Sqrt((x * x) + (y * y)); }
        }

        public float sqrMagnitude
        {
            get { return ((x * x) + (y * y)); }
        }

        public static float Angle(Vector2F from, Vector2F to)
        {
            return (Mathf.Acos(Mathf.Clamp(Dot(from.normalized, to.normalized), -1f, 1f)) * 57.29578f);
        }

        public static float Distance(Vector2F a, Vector2F b)
        {
            var vector = a - b;
            return vector.magnitude;
        }

        public static Vector2F ClampMagnitude(Vector2F vector, float maxLength)
        {
            if (vector.sqrMagnitude > (maxLength * maxLength))
                return vector.normalized * maxLength;

            return vector;
        }

        public static float SqrMagnitude(Vector2F a)
        {
            return ((a.x * a.x) + (a.y * a.y));
        }

        public float SqrMagnitude()
        {
            return ((this.x * this.x) + (this.y * this.y));
        }

        public static Vector2F Min(Vector2F lhs, Vector2F rhs)
        {
            return new Vector2F(Math.Min(lhs.x, rhs.x), Math.Min(lhs.y, rhs.y));
        }

        public static Vector2F Max(Vector2F lhs, Vector2F rhs)
        {
            return new Vector2F(Math.Max(lhs.x, rhs.x), Math.Max(lhs.y, rhs.y));
        }

        public static Vector2F zero
        {
            get { return new Vector2F(0, 0); }
        }

        public static Vector2F one
        {
            get { return new Vector2F(1, 1); }
        }

        public static Vector2F up
        {
            get { return new Vector2F(0, 1); }
        }

        public static Vector2F right
        {
            get { return new Vector2F(1, 0); }
        }

        public static Vector2F operator +(Vector2F a, Vector2F b)
        {
            return new Vector2F(a.x + b.x, a.y + b.y);
        }

        public static Vector2F operator -(Vector2F a, Vector2F b)
        {
            return new Vector2F(a.x - b.x, a.y - b.y);
        }

        public static Vector2F operator -(Vector2F a)
        {
            return new Vector2F(-a.x, -a.y);
        }

        public static Vector2F operator *(Vector2F a, float d)
        {
            return new Vector2F(a.x * d, a.y * d);
        }

        public static Vector2F operator *(float d, Vector2F a)
        {
            return new Vector2F(a.x * d, a.y * d);
        }

        public static Vector2F operator /(Vector2F a, float d)
        {
            return new Vector2F(a.x / d, a.y / d);
        }

        public static bool operator ==(Vector2F lhs, Vector2F rhs)
        {
            return (SqrMagnitude(lhs - rhs) < 9.999999E-11f);
        }

        public static bool operator !=(Vector2F lhs, Vector2F rhs)
        {
            return (SqrMagnitude(lhs - rhs) >= 9.999999E-11f);
        }

        public static implicit operator Vector2F(Vector3F v)
        {
            return new Vector2F(v.x, v.y);
        }

        public static implicit operator Vector3F(Vector2F v)
        {
            return new Vector3F(v.x, v.y, 0f);
        }

        public static implicit operator Vector2(Vector2F v)
        {
            return new Vector2(v.x, v.y);
        }

        public static implicit operator Vector2F(Vector2 v)
        {
            return new Vector3F(v.x, v.y);
        }
    }
}