// Use this in multi-threaded code, so as not to piss off Unity
// There is NO reason to use this otherwise

using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Assets.Classes.Foundation.Classes
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Vector3F : ISizeInBytes
    {
        public const float kEpsilon = 1E-05f;
        public float x;
        public float y;
        public float z;

        public Vector3F(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public Vector3F(float x, float y)
        {
            this.x = x;
            this.y = y;
            this.z = 0f;
        }

        public static Vector3F Lerp(Vector3F from, Vector3F to, float t)
        {
            t = Mathf.Clamp01(t);

            return new Vector3F(from.x + ((to.x - from.x) * t), from.y + ((to.y - from.y) * t), from.z + ((to.z - from.z) * t));
        }

        public static Vector3F Slerp(Vector3F from, Vector3F to, float t)
        {
            return Vector3.Slerp(from, to, t);
        }

        public static void OrthoNormalize(ref Vector3 normal, ref Vector3 tangent)
        {
            Vector3.OrthoNormalize(ref normal, ref tangent);
        }

        public static void OrthoNormalize(ref Vector3 normal, ref Vector3 tangent, ref Vector3 binormal)
        {
            Vector3.OrthoNormalize(ref normal, ref tangent, ref binormal);
        }

        public static Vector3F MoveTowards(Vector3F current, Vector3F target, float maxDistanceDelta)
        {
            var vector = target - current;
            var magnitude = vector.magnitude;

            if ((magnitude > maxDistanceDelta) && (magnitude != 0f))
                return (current + (vector / magnitude) * maxDistanceDelta);

            return target;
        }

        public static Vector3F RotateTowards(Vector3F current, Vector3F target, float maxRadiansDelta, float maxMagnitudeDelta)
        {
            return Vector3.RotateTowards(current, target, maxRadiansDelta, maxMagnitudeDelta);
        }

        public static Vector3F SmoothDamp(Vector3F current, Vector3F target, ref Vector3F currentVelocity, float smoothTime, float maxSpeed)
        {
            float deltaTime = Time.deltaTime;
            return SmoothDamp(current, target, ref currentVelocity, smoothTime, maxSpeed, deltaTime);
        }

        public static Vector3F SmoothDamp(Vector3F current, Vector3F target, ref Vector3F currentVelocity, float smoothTime)
        {
            var deltaTime = Time.deltaTime;
            const float positiveInfinity = float.PositiveInfinity;

            return SmoothDamp(current, target, ref currentVelocity, smoothTime, positiveInfinity, deltaTime);
        }

        public static Vector3F SmoothDamp(Vector3F current, Vector3F target, ref Vector3F currentVelocity, float smoothTime, float maxSpeed, float deltaTime)
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
                }

                throw new IndexOutOfRangeException("Invalid Vector3F index!");
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
                        throw new IndexOutOfRangeException("Invalid Vector3F index!");
                }
            }
        }

        public static Vector3F Scale(Vector3F a, Vector3F b)
        {
            return new Vector3F(a.x * b.x, a.y * b.y, a.z * b.z);
        }

        public void Scale(Vector3F scale)
        {
            x *= scale.x;
            y *= scale.y;
            z *= scale.z;
        }

        public static Vector3F Cross(Vector3F lhs, Vector3F rhs)
        {
            return new Vector3F((lhs.y * rhs.z) - (lhs.z * rhs.y), (lhs.z * rhs.x) - (lhs.x * rhs.z), (lhs.x * rhs.y) - (lhs.y * rhs.x));
        }

        public override int GetHashCode()
        {
            return ((x.GetHashCode() ^ (y.GetHashCode() << 2)) ^ (z.GetHashCode() >> 2));
        }

        public int SizeInBytes()
        {
            const int sizeOfDouble = sizeof(float);

            return (sizeOfDouble * 3);
        }

        public override bool Equals(object other)
        {
            if (!(other is Vector3F))
                return false;

            Vector3F vector = (Vector3F)other;

            return ((x.Equals(vector.x) && y.Equals(vector.y)) && z.Equals(vector.z));
        }

        public static Vector3F Reflect(Vector3F inDirection, Vector3F inNormal)
        {
            return ((-2f * Dot(inNormal, inDirection)) * inNormal + inDirection);
        }

        public static Vector3F Normalize(Vector3F value)
        {
            var num = Magnitude(value);

            return value / num;
        }

        public void Normalize()
        {
            var num = Magnitude(this);

            this = this / num;
        }

        public Vector3F normalized
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

        public static float Dot(Vector3F lhs, Vector3F rhs)
        {
            return (((lhs.x * rhs.x) + (lhs.y * rhs.y)) + (lhs.z * rhs.z));
        }

        public static Vector3F Project(Vector3F vector, Vector3F onNormal)
        {
            var num = Dot(onNormal, onNormal);

            if (num < float.Epsilon)
                return zero;

            return (onNormal * Dot(vector, onNormal)) / num;
        }

        public static Vector3F Exclude(Vector3F excludeThis, Vector3F fromThat)
        {
            return (fromThat - Project(fromThat, excludeThis));
        }

        public static float Angle(Vector3F from, Vector3F to)
        {
            return (Mathf.Acos(Mathf.Clamp(Dot(from.normalized, to.normalized), -1f, 1f)) * 57.29578f);
        }

        public static float Distance(Vector3F a, Vector3F b)
        {
            var vector = new Vector3F(a.x - b.x, a.y - b.y, a.z - b.z);

            return Mathf.Sqrt(((vector.x * vector.x) + (vector.y * vector.y)) + (vector.z * vector.z));
        }

        public static Vector3F ClampMagnitude(Vector3F vector, float maxLength)
        {
            if (vector.sqrMagnitude > (maxLength * maxLength))
                return vector.normalized * maxLength;

            return vector;
        }

        public static float Magnitude(Vector3F a)
        {
            return Mathf.Sqrt(((a.x * a.x) + (a.y * a.y)) + (a.z * a.z));
        }

        public float magnitude
        {
            get { return Mathf.Sqrt(((x * x) + (y * y)) + (z * z)); }
        }

        public static float SqrMagnitude(Vector3F a)
        {
            return (((a.x * a.x) + (a.y * a.y)) + (a.z * a.z));
        }

        public float sqrMagnitude
        {
            get { return (((x * x) + (y * y)) + (z * z)); }
        }

        public static Vector3F Min(Vector3F lhs, Vector3F rhs)
        {
            return new Vector3F(Math.Min(lhs.x, rhs.x), Math.Min(lhs.y, rhs.y), Math.Min(lhs.z, rhs.z));
        }

        public static Vector3F Max(Vector3F lhs, Vector3F rhs)
        {
            return new Vector3F(Math.Max(lhs.x, rhs.x), Math.Max(lhs.y, rhs.y), Math.Max(lhs.z, rhs.z));
        }

        public static Vector3F zero
        {
            get { return new Vector3F(0, 0, 0); }
        }

        public static Vector3F one
        {
            get { return new Vector3F(1, 1, 1); }
        }

        public static Vector3F forward
        {
            get { return new Vector3F(0, 0, 1); }
        }

        public static Vector3F back
        {
            get { return new Vector3F(0, 0, -1); }
        }

        public static Vector3F up
        {
            get { return new Vector3F(0, 1, 0); }
        }

        public static Vector3F down
        {
            get { return new Vector3F(0, -1, 0); }
        }

        public static Vector3F left
        {
            get { return new Vector3F(-1, 0, 0); }
        }

        public static Vector3F right
        {
            get { return new Vector3F(1, 0, 0); }
        }

        public static Vector3F operator +(Vector3F a, Vector3F b)
        {
            return new Vector3F(a.x + b.x, a.y + b.y, a.z + b.z);
        }

        public static Vector3F operator -(Vector3F a, Vector3F b)
        {
            return new Vector3F(a.x - b.x, a.y - b.y, a.z - b.z);
        }

        public static Vector3F operator -(Vector3F a)
        {
            return new Vector3F(-a.x, -a.y, -a.z);
        }

        public static Vector3F operator *(Vector3F a, float d)
        {
            return new Vector3F(a.x * d, a.y * d, a.z * d);
        }

        public static Vector3F operator *(float d, Vector3F a)
        {
            return new Vector3F(a.x * d, a.y * d, a.z * d);
        }

        public static Vector3F operator /(Vector3F a, float d)
        {
            return new Vector3F(a.x / d, a.y / d, a.z / d);
        }

        public static bool operator ==(Vector3F lhs, Vector3F rhs)
        {
            return (SqrMagnitude(lhs - rhs) < 9.999999E-11f);
        }

        public static bool operator !=(Vector3F lhs, Vector3F rhs)
        {
            return (SqrMagnitude(lhs - rhs) >= 9.999999E-11f);
        }

        public static implicit operator Vector3(Vector3F v)
        {
            return new Vector3(v.x, v.y, v.z);
        }

        public static implicit operator Vector3F(Vector3 v)
        {
            return new Vector3F(v.x, v.y, v.z);
        }
    }
}