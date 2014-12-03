using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Assets.Classes.Foundation.Classes
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Vector4D : ISizeInBytes
    {
        public const double kEpsilon = 1E-05f;

        public double x;
        public double y;
        public double z;
        public double w;

        public Vector4D(double x, double y, double z, double w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        public Vector4D(double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = 0f;
        }

        public Vector4D(double x, double y)
        {
            this.x = x;
            this.y = y;
            this.z = 0f;
            this.w = 0f;
        }

        public double this[int index]
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
                throw new IndexOutOfRangeException("Invalid Vector4D index!");
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
                        throw new IndexOutOfRangeException("Invalid Vector4D index!");
                }
            }
        }

        public static Vector4D Lerp(Vector4D from, Vector4D to, double t)
        {
            t = MathD.Clamp01(t);

            return new Vector4D(from.x + ((to.x - from.x) * t), from.y + ((to.y - from.y) * t), from.z + ((to.z - from.z) * t), from.w + ((to.w - from.w) * t));
        }

        public static Vector4D MoveTowards(Vector4D current, Vector4D target, double maxDistanceDelta)
        {
            var vector = target - current;
            var magnitude = vector.magnitude;

            if ((magnitude > maxDistanceDelta) && (magnitude != 0f))
                return (current + (vector / magnitude) * maxDistanceDelta);

            return target;
        }

        public static Vector4D Scale(Vector4D a, Vector4D b)
        {
            return new Vector4D(a.x * b.x, a.y * b.y, a.z * b.z, a.w * b.w);
        }

        public void Scale(Vector4D scale)
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
            const int sizeOfDouble = sizeof(double);

            return (sizeOfDouble * 4);
        }

        public override bool Equals(object other)
        {
            if (!(other is Vector4D))
                return false;

            var vector = (Vector4D)other;

            return (((x.Equals(vector.x)
                && y.Equals(vector.y))
                && z.Equals(vector.z))
                && w.Equals(vector.w));
        }

        public static Vector4D Normalize(Vector4D a)
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

        public Vector4D normalized
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

        public static double Dot(Vector4D a, Vector4D b)
        {
            return ((((a.x * b.x) + (a.y * b.y)) + (a.z * b.z)) + (a.w * b.w));
        }

        public static Vector4D Project(Vector4D a, Vector4D b)
        {
            return (b * Dot(a, b)) / Dot(b, b);
        }

        public static double Distance(Vector4D a, Vector4D b)
        {
            return Magnitude(a - b);
        }

        public static double Magnitude(Vector4D a)
        {
            return Math.Sqrt(Dot(a, a));
        }

        public double magnitude
        {
            get { return Math.Sqrt(Dot(this, this)); }
        }

        public static double SqrMagnitude(Vector4D a)
        {
            return Dot(a, a);
        }

        public double SqrMagnitude()
        {
            return Dot(this, this);
        }

        public double sqrMagnitude
        {
            get { return Dot(this, this); }
        }

        public static Vector4D Min(Vector4D lhs, Vector4D rhs)
        {
            return new Vector4D(Math.Min(lhs.x, rhs.x), Math.Min(lhs.y, rhs.y), Math.Min(lhs.z, rhs.z), Math.Min(lhs.w, rhs.w));
        }

        public static Vector4D Max(Vector4D lhs, Vector4D rhs)
        {
            return new Vector4D(Math.Max(lhs.x, rhs.x), Math.Max(lhs.y, rhs.y), Math.Max(lhs.z, rhs.z), Math.Max(lhs.w, rhs.w));
        }

        public static Vector4D zero
        {
            get { return new Vector4D(0, 0, 0, 0); }
        }

        public static Vector4D one
        {
            get { return new Vector4D(1, 1, 1, 1); }
        }

        public static Vector4D operator +(Vector4D a, Vector4D b)
        {
            return new Vector4D(a.x + b.x, a.y + b.y, a.z + b.z, a.w + b.w);
        }

        public static Vector4D operator -(Vector4D a, Vector4D b)
        {
            return new Vector4D(a.x - b.x, a.y - b.y, a.z - b.z, a.w - b.w);
        }

        public static Vector4D operator -(Vector4D a)
        {
            return new Vector4D(-a.x, -a.y, -a.z, -a.w);
        }

        public static Vector4D operator *(Vector4D a, double d)
        {
            return new Vector4D(a.x * d, a.y * d, a.z * d, a.w * d);
        }

        public static Vector4D operator *(double d, Vector4D a)
        {
            return new Vector4D(a.x * d, a.y * d, a.z * d, a.w * d);
        }

        public static Vector4D operator /(Vector4D a, double d)
        {
            return new Vector4D(a.x / d, a.y / d, a.z / d, a.w / d);
        }

        public static bool operator ==(Vector4D lhs, Vector4D rhs)
        {
            return (SqrMagnitude(lhs - rhs) < 9.999999E-11f);
        }

        public static bool operator !=(Vector4D lhs, Vector4D rhs)
        {
            return (SqrMagnitude(lhs - rhs) >= 9.999999E-11f);
        }

        public static implicit operator Vector4D(Vector3D v)
        {
            return new Vector4D(v.x, v.y, v.z, 0f);
        }

        public static implicit operator Vector3D(Vector4D v)
        {
            return new Vector3D(v.x, v.y, v.z);
        }

        public static implicit operator Vector4(Vector4D v)
        {
            return new Vector4((float)v.x, (float)v.y, (float)v.z, (float)v.w);
        }

        public static implicit operator Vector4D(Vector4 v)
        {
            return new Vector4D(v.x, v.y, v.z, v.w);
        }
    }
}