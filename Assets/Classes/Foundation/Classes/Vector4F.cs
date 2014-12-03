using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Assets.Classes.Foundation.Classes
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Vector4F : ISizeInBytes
    {
        public const float kEpsilon = 1E-05f;

        public float x;
        public float y;
        public float z;
        public float w;

        public Vector4F(float x, float y, float z, float w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        public Vector4F(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = 0f;
        }

        public Vector4F(float x, float y)
        {
            this.x = x;
            this.y = y;
            this.z = 0f;
            this.w = 0f;
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

                    case 2:
                        return z;

                    case 3:
                        return w;
                }
                throw new IndexOutOfRangeException("Invalid Vector4F index!");
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

                    case 2:
                        z = value;
                        break;

                    case 3:
                        w = value;
                        break;

                    default:
                        throw new IndexOutOfRangeException("Invalid Vector4F index!");
                }
            }
        }

        public static Vector4F Lerp(Vector4F from, Vector4F to, float t)
        {
            t = Mathf.Clamp01(t);

            return new Vector4F(from.x + ((to.x - from.x) * t), from.y + ((to.y - from.y) * t), from.z + ((to.z - from.z) * t), from.w + ((to.w - from.w) * t));
        }

        public static Vector4F MoveTowards(Vector4F current, Vector4F target, float maxDistanceDelta)
        {
            var vector = target - current;
            var magnitude = vector.magnitude;

            if ((magnitude > maxDistanceDelta) && (magnitude != 0f))
                return (current + (vector / magnitude) * maxDistanceDelta);

            return target;
        }

        public static Vector4F Scale(Vector4F a, Vector4F b)
        {
            return new Vector4F(a.x * b.x, a.y * b.y, a.z * b.z, a.w * b.w);
        }

        public void Scale(Vector4F scale)
        {
            x *= scale.x;
            y *= scale.y;
            z *= scale.z;
            w *= scale.w;
        }

        public override int GetHashCode()
        {
            return (((x.GetHashCode()
                ^ (y.GetHashCode() << 2))
                ^ (z.GetHashCode() >> 2))
                ^ (w.GetHashCode() >> 1));
        }

        public int SizeInBytes()
        {
            const int sizeOfDouble = sizeof(float);

            return (sizeOfDouble * 4);
        }

        public override bool Equals(object other)
        {
            if (!(other is Vector4F))
                return false;

            var vector = (Vector4F)other;

            return (((x.Equals(vector.x)
                && y.Equals(vector.y))
                && z.Equals(vector.z))
                && w.Equals(vector.w));
        }

        public static Vector4F Normalize(Vector4F a)
        {
            var num = Magnitude(a);

            if (num > 1E-05f)
                return a / num;

            return zero;
        }

        public void Normalize()
        {
            var num = Magnitude(this);

            if (num > 1E-05f)
                this = this / num;
            else
                this = zero;
        }

        public Vector4F normalized
        {
            get { return Normalize(this); }
        }

        public override string ToString()
        {
            object[] args = new object[] { this.x, this.y, this.z, this.w };
            return string.Format("({0:F1}, {1:F1}, {2:F1}, {3:F1})", args);
        }

        public string ToString(string format)
        {
            object[] args = new object[]
        {
                x.ToString(format), 
                y.ToString(format), 
                z.ToString(format), 
                w.ToString(format)
        };

            return string.Format("({0}, {1}, {2}, {3})", args);
        }

        public static float Dot(Vector4F a, Vector4F b)
        {
            return ((((a.x * b.x) + (a.y * b.y)) + (a.z * b.z)) + (a.w * b.w));
        }

        public static Vector4F Project(Vector4F a, Vector4F b)
        {
            return (b * Dot(a, b)) / Dot(b, b);
        }

        public static float Distance(Vector4F a, Vector4F b)
        {
            return Magnitude(a - b);
        }

        public static float Magnitude(Vector4F a)
        {
            return Mathf.Sqrt(Dot(a, a));
        }

        public float magnitude
        {
            get { return Mathf.Sqrt(Dot(this, this)); }
        }

        public static float SqrMagnitude(Vector4F a)
        {
            return Dot(a, a);
        }

        public float SqrMagnitude()
        {
            return Dot(this, this);
        }

        public float sqrMagnitude
        {
            get { return Dot(this, this); }
        }

        public static Vector4F Min(Vector4F lhs, Vector4F rhs)
        {
            return new Vector4F(Math.Min(lhs.x, rhs.x), Math.Min(lhs.y, rhs.y), Math.Min(lhs.z, rhs.z), Math.Min(lhs.w, rhs.w));
        }

        public static Vector4F Max(Vector4F lhs, Vector4F rhs)
        {
            return new Vector4F(Math.Max(lhs.x, rhs.x), Math.Max(lhs.y, rhs.y), Math.Max(lhs.z, rhs.z), Math.Max(lhs.w, rhs.w));
        }

        public static Vector4F zero
        {
            get { return new Vector4F(0, 0, 0, 0); }
        }

        public static Vector4F one
        {
            get { return new Vector4F(1, 1, 1, 1); }
        }

        public static Vector4F operator +(Vector4F a, Vector4F b)
        {
            return new Vector4F(a.x + b.x, a.y + b.y, a.z + b.z, a.w + b.w);
        }

        public static Vector4F operator -(Vector4F a, Vector4F b)
        {
            return new Vector4F(a.x - b.x, a.y - b.y, a.z - b.z, a.w - b.w);
        }

        public static Vector4F operator -(Vector4F a)
        {
            return new Vector4F(-a.x, -a.y, -a.z, -a.w);
        }

        public static Vector4F operator *(Vector4F a, float d)
        {
            return new Vector4F(a.x * d, a.y * d, a.z * d, a.w * d);
        }

        public static Vector4F operator *(float d, Vector4F a)
        {
            return new Vector4F(a.x * d, a.y * d, a.z * d, a.w * d);
        }

        public static Vector4F operator /(Vector4F a, float d)
        {
            return new Vector4F(a.x / d, a.y / d, a.z / d, a.w / d);
        }

        public static bool operator ==(Vector4F lhs, Vector4F rhs)
        {
            return (SqrMagnitude(lhs - rhs) < 9.999999E-11f);
        }

        public static bool operator !=(Vector4F lhs, Vector4F rhs)
        {
            return (SqrMagnitude(lhs - rhs) >= 9.999999E-11f);
        }

        public static implicit operator Vector4F(Vector3F v)
        {
            return new Vector4F(v.x, v.y, v.z, 0f);
        }

        public static implicit operator Vector3F(Vector4F v)
        {
            return new Vector3F(v.x, v.y, v.z);
        }

        public static implicit operator Vector4(Vector4F v)
        {
            return new Vector4(v.x, v.y, v.z, v.w);
        }

        public static implicit operator Vector4F(Vector4 v)
        {
            return new Vector4F(v.x, v.y, v.z, v.w);
        }
    }
}