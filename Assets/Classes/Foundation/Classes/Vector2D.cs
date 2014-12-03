using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Assets.Classes.Foundation.Classes
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Vector2D : ISizeInBytes
    {
        public const double kEpsilon = 1E-05f;
        public double x;
        public double y;

        public Vector2D(double x, double y)
        {
            this.x = x;
            this.y = y;
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

        public static Vector2D Lerp(Vector2D from, Vector2D to, double t)
        {
            t = MathD.Clamp01(t);

            return new Vector2D(from.x + ((to.x - from.x) * t), from.y + ((to.y - from.y) * t));
        }

        public static Vector2D MoveTowards(Vector2D current, Vector2D target, double maxDistanceDelta)
        {
            var vector = target - current;
            var magnitude = vector.magnitude;

            if ((magnitude > maxDistanceDelta) && (magnitude != 0f))
                return (current + (vector / magnitude) * maxDistanceDelta);

            return target;
        }

        public static Vector2D Scale(Vector2D a, Vector2D b)
        {
            return new Vector2D(a.x * b.x, a.y * b.y);
        }

        public void Scale(Vector2D scale)
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

        public Vector2D normalized
        {
            get
            {
                var vector = new Vector2D(x, y);
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
            const int sizeOfDouble = sizeof(double);

            return (sizeOfDouble * 2);
        }

        public override bool Equals(object other)
        {
            if (!(other is Vector2D))
                return false;

            var vector = (Vector2D)other;
            return (x.Equals(vector.x) && y.Equals(vector.y));
        }

        public static double Dot(Vector2D lhs, Vector2D rhs)
        {
            return ((lhs.x * rhs.x) + (lhs.y * rhs.y));
        }

        public double magnitude
        {
            get { return Math.Sqrt((x * x) + (y * y)); }
        }

        public double sqrMagnitude
        {
            get { return ((x * x) + (y * y)); }
        }

        public static double Angle(Vector2D from, Vector2D to)
        {
            return (Math.Acos(MathD.Clamp(Dot(from.normalized, to.normalized), -1f, 1f)) * 57.29578f);
        }

        public static double Distance(Vector2D a, Vector2D b)
        {
            var vector = a - b;
            return vector.magnitude;
        }

        public static Vector2D ClampMagnitude(Vector2D vector, double maxLength)
        {
            if (vector.sqrMagnitude > (maxLength * maxLength))
                return vector.normalized * maxLength;

            return vector;
        }

        public static double SqrMagnitude(Vector2D a)
        {
            return ((a.x * a.x) + (a.y * a.y));
        }

        public double SqrMagnitude()
        {
            return ((this.x * this.x) + (this.y * this.y));
        }

        public static Vector2D Min(Vector2D lhs, Vector2D rhs)
        {
            return new Vector2D(Math.Min(lhs.x, rhs.x), Math.Min(lhs.y, rhs.y));
        }

        public static Vector2D Max(Vector2D lhs, Vector2D rhs)
        {
            return new Vector2D(Math.Max(lhs.x, rhs.x), Math.Max(lhs.y, rhs.y));
        }

        public static Vector2D zero
        {
            get { return new Vector2D(0, 0); }
        }

        public static Vector2D one
        {
            get { return new Vector2D(1, 1); }
        }

        public static Vector2D up
        {
            get { return new Vector2D(0, 1); }
        }

        public static Vector2D right
        {
            get { return new Vector2D(1, 0); }
        }

        public static Vector2D operator +(Vector2D a, Vector2D b)
        {
            return new Vector2D(a.x + b.x, a.y + b.y);
        }

        public static Vector2D operator -(Vector2D a, Vector2D b)
        {
            return new Vector2D(a.x - b.x, a.y - b.y);
        }

        public static Vector2D operator -(Vector2D a)
        {
            return new Vector2D(-a.x, -a.y);
        }

        public static Vector2D operator *(Vector2D a, double d)
        {
            return new Vector2D(a.x * d, a.y * d);
        }

        public static Vector2D operator *(double d, Vector2D a)
        {
            return new Vector2D(a.x * d, a.y * d);
        }

        public static Vector2D operator /(Vector2D a, double d)
        {
            return new Vector2D(a.x / d, a.y / d);
        }

        public static bool operator ==(Vector2D lhs, Vector2D rhs)
        {
            return (SqrMagnitude(lhs - rhs) < 9.999999E-11f);
        }

        public static bool operator !=(Vector2D lhs, Vector2D rhs)
        {
            return (SqrMagnitude(lhs - rhs) >= 9.999999E-11f);
        }

        public static implicit operator Vector2D(Vector3D v)
        {
            return new Vector2D(v.x, v.y);
        }

        public static implicit operator Vector3D(Vector2D v)
        {
            return new Vector3D(v.x, v.y, 0f);
        }

        public static implicit operator Vector2(Vector2D v)
        {
            return new Vector2((float)v.x, (float)v.y);
        }

        public static implicit operator Vector2D(Vector2 v)
        {
            return new Vector3D(v.x, v.y);
        }
    }
}