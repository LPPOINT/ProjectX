using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Assets.Classes.Foundation.Classes
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Vector3D : ISizeInBytes
    {
        public const double kEpsilon = 1E-05f;
        public double x;
        public double y;
        public double z;

        public Vector3D(double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public Vector3D(double x, double y)
        {
            this.x = x;
            this.y = y;
            this.z = 0f;
        }

        public static Vector3D Lerp(Vector3D from, Vector3D to, double t)
        {
            t = MathD.Clamp01(t);

            return new Vector3D(from.x + ((to.x - from.x) * t), from.y + ((to.y - from.y) * t), from.z + ((to.z - from.z) * t));
        }

        public static Vector3D Slerp(Vector3D from, Vector3D to, double t)
        {
            return Vector3.Slerp(from, to, (float)t);
        }

        public static void OrthoNormalize(ref Vector3 normal, ref Vector3 tangent)
        {
            Vector3.OrthoNormalize(ref normal, ref tangent);
        }

        public static void OrthoNormalize(ref Vector3 normal, ref Vector3 tangent, ref Vector3 binormal)
        {
            Vector3.OrthoNormalize(ref normal, ref tangent, ref binormal);
        }

        public static Vector3D MoveTowards(Vector3D current, Vector3D target, double maxDistanceDelta)
        {
            var vector = target - current;
            var magnitude = vector.magnitude;

            if ((magnitude > maxDistanceDelta) && (magnitude != 0f))
                return (current + (vector / magnitude) * maxDistanceDelta);

            return target;
        }

        public static Vector3D RotateTowards(Vector3D current, Vector3D target, double maxRadiansDelta, double maxMagnitudeDelta)
        {
            return Vector3.RotateTowards(current, target, (float)maxRadiansDelta, (float)maxMagnitudeDelta);
        }

        public static Vector3D SmoothDamp(Vector3D current, Vector3D target, ref Vector3D currentVelocity, double smoothTime, double maxSpeed)
        {
            double deltaTime = Time.deltaTime;
            return SmoothDamp(current, target, ref currentVelocity, smoothTime, maxSpeed, deltaTime);
        }

        public static Vector3D SmoothDamp(Vector3D current, Vector3D target, ref Vector3D currentVelocity, double smoothTime)
        {
            var deltaTime = Time.deltaTime;
            const double positiveInfinity = double.PositiveInfinity;

            return SmoothDamp(current, target, ref currentVelocity, smoothTime, positiveInfinity, deltaTime);
        }

        public static Vector3D SmoothDamp(Vector3D current, Vector3D target, ref Vector3D currentVelocity, double smoothTime, double maxSpeed, double deltaTime)
        {
            smoothTime = Math.Max(0.0001f, smoothTime);

            var num = 2f / smoothTime;
            var num2 = num * deltaTime;
            var num3 = 1f / (((1f + num2) + ((0.48f * num2) * num2)) + (((0.235f * num2) * num2) * num2));

            var vector = current - target;
            var vector2 = target;

            var maxLength = maxSpeed * smoothTime;
            vector = ClampMagnitude(vector, maxLength);
            target = current - vector;

            var vector3 = (currentVelocity + (num * vector)) * deltaTime;
            currentVelocity = (currentVelocity - (num * vector3)) * num3;
            var vector4 = target + (vector + vector3) * num3;

            if (Dot(vector2 - current, vector4 - vector2) > 0f)
            {
                vector4 = vector2;
                currentVelocity = (vector4 - vector2) / deltaTime;
            }

            return vector4;
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
                }

                throw new IndexOutOfRangeException("Invalid Vector3D index!");
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

                    default:
                        throw new IndexOutOfRangeException("Invalid Vector3D index!");
                }
            }
        }

        public static Vector3D Scale(Vector3D a, Vector3D b)
        {
            return new Vector3D(a.x * b.x, a.y * b.y, a.z * b.z);
        }

        public void Scale(Vector3D scale)
        {
            x *= scale.x;
            y *= scale.y;
            z *= scale.z;
        }

        public static Vector3D Cross(Vector3D lhs, Vector3D rhs)
        {
            return new Vector3D((lhs.y * rhs.z) - (lhs.z * rhs.y), (lhs.z * rhs.x) - (lhs.x * rhs.z), (lhs.x * rhs.y) - (lhs.y * rhs.x));
        }

        public override int GetHashCode()
        {
            return ((x.GetHashCode() ^ (y.GetHashCode() << 2)) ^ (z.GetHashCode() >> 2));
        }

        public int SizeInBytes()
        {
            const int sizeOfDouble = sizeof(double);

            return (sizeOfDouble * 3);
        }

        public override bool Equals(object other)
        {
            if (!(other is Vector3D))
                return false;

            Vector3D vector = (Vector3D)other;

            return ((x.Equals(vector.x) && y.Equals(vector.y)) && z.Equals(vector.z));
        }

        public static Vector3D Reflect(Vector3D inDirection, Vector3D inNormal)
        {
            return ((-2f * Dot(inNormal, inDirection)) * inNormal + inDirection);
        }

        public static Vector3D Normalize(Vector3D value)
        {
            var num = Magnitude(value);

            return value / num;
        }

        public void Normalize()
        {
            var num = Magnitude(this);

            this = this / num;
        }

        public Vector3D normalized
        {
            get { return Normalize(this); }
        }

        public override string ToString()
        {
            return string.Format("({0:F1}, {1:F1}, {2:F1})", x, y, z);
        }

        public string ToString(string format)
        {
            return string.Format("({0}, {1}, {2})",
                x.ToString(format),
                y.ToString(format),
                z.ToString(format));
        }

        public static double Dot(Vector3D lhs, Vector3D rhs)
        {
            return (((lhs.x * rhs.x) + (lhs.y * rhs.y)) + (lhs.z * rhs.z));
        }

        public static Vector3D Project(Vector3D vector, Vector3D onNormal)
        {
            var num = Dot(onNormal, onNormal);

            if (num < double.Epsilon)
                return zero;

            return (onNormal * Dot(vector, onNormal)) / num;
        }

        public static Vector3D Exclude(Vector3D excludeThis, Vector3D fromThat)
        {
            return (fromThat - Project(fromThat, excludeThis));
        }

        public static double Angle(Vector3D from, Vector3D to)
        {
            return (Math.Acos(MathD.Clamp(Dot(from.normalized, to.normalized), -1f, 1f)) * 57.29578f);
        }

        public static double Distance(Vector3D a, Vector3D b)
        {
            var vector = new Vector3D(a.x - b.x, a.y - b.y, a.z - b.z);

            return Math.Sqrt(((vector.x * vector.x) + (vector.y * vector.y)) + (vector.z * vector.z));
        }

        public static Vector3D ClampMagnitude(Vector3D vector, double maxLength)
        {
            if (vector.sqrMagnitude > (maxLength * maxLength))
                return vector.normalized * maxLength;

            return vector;
        }

        public static double Magnitude(Vector3D a)
        {
            return Math.Sqrt(((a.x * a.x) + (a.y * a.y)) + (a.z * a.z));
        }

        public double magnitude
        {
            get { return Math.Sqrt(((x * x) + (y * y)) + (z * z)); }
        }

        public static double SqrMagnitude(Vector3D a)
        {
            return (((a.x * a.x) + (a.y * a.y)) + (a.z * a.z));
        }

        public double sqrMagnitude
        {
            get { return (((x * x) + (y * y)) + (z * z)); }
        }

        public static Vector3D Min(Vector3D lhs, Vector3D rhs)
        {
            return new Vector3D(Math.Min(lhs.x, rhs.x), Math.Min(lhs.y, rhs.y), Math.Min(lhs.z, rhs.z));
        }

        public static Vector3D Max(Vector3D lhs, Vector3D rhs)
        {
            return new Vector3D(Math.Max(lhs.x, rhs.x), Math.Max(lhs.y, rhs.y), Math.Max(lhs.z, rhs.z));
        }

        public static Vector3D zero
        {
            get { return new Vector3D(0, 0, 0); }
        }

        public static Vector3D one
        {
            get { return new Vector3D(1, 1, 1); }
        }

        public static Vector3D forward
        {
            get { return new Vector3D(0, 0, 1); }
        }

        public static Vector3D back
        {
            get { return new Vector3D(0, 0, -1); }
        }

        public static Vector3D up
        {
            get { return new Vector3D(0, 1, 0); }
        }

        public static Vector3D down
        {
            get { return new Vector3D(0, -1, 0); }
        }

        public static Vector3D left
        {
            get { return new Vector3D(-1, 0, 0); }
        }

        public static Vector3D right
        {
            get { return new Vector3D(1, 0, 0); }
        }

        public static Vector3D operator +(Vector3D a, Vector3D b)
        {
            return new Vector3D(a.x + b.x, a.y + b.y, a.z + b.z);
        }

        public static Vector3D operator -(Vector3D a, Vector3D b)
        {
            return new Vector3D(a.x - b.x, a.y - b.y, a.z - b.z);
        }

        public static Vector3D operator -(Vector3D a)
        {
            return new Vector3D(-a.x, -a.y, -a.z);
        }

        public static Vector3D operator *(Vector3D a, double d)
        {
            return new Vector3D(a.x * d, a.y * d, a.z * d);
        }

        public static Vector3D operator *(double d, Vector3D a)
        {
            return new Vector3D(a.x * d, a.y * d, a.z * d);
        }

        public static Vector3D operator /(Vector3D a, double d)
        {
            return new Vector3D(a.x / d, a.y / d, a.z / d);
        }

        public static bool operator ==(Vector3D lhs, Vector3D rhs)
        {
            return (SqrMagnitude(lhs - rhs) < 9.999999E-11f);
        }

        public static bool operator !=(Vector3D lhs, Vector3D rhs)
        {
            return (SqrMagnitude(lhs - rhs) >= 9.999999E-11f);
        }

        public static implicit operator Vector3(Vector3D v)
        {
            return new Vector3((float)v.x, (float)v.y, (float)v.z);
        }

        public static implicit operator Vector3D(Vector3 v)
        {
            return new Vector3D(v.x, v.y, v.z);
        }
    }
}