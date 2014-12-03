using UnityEngine;

namespace Assets.Classes.Foundation.Classes
{
    public struct Point2 : ISizeInBytes
    {
        public static Point2 Zero = new Point2(0, 0);

        public int X;
        public int Y;

        public Point2(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Point2(Vector3 v)
        {
            X = (int)v.x;
            Y = (int)v.z;
        }

        public static Point2 operator +(Point2 value1, Point2 value2)
        {
            return new Point2(value1.X + value2.X, value1.Y + value2.Y);
        }

        public static Point2 operator +(Point2 value1, int value2)
        {
            return new Point2(value1.X + value2, value1.X + value2);
        }

        public static Point2 operator -(Point2 value1, Point2 value2)
        {
            return new Point2(value1.X - value2.X, value1.Y - value2.Y);
        }

        public static Point2 operator -(Point2 value1, int value2)
        {
            return new Point2(value1.X - value2, value1.X - value2);
        }

        public static Point2 operator -(Point2 value)
        {
            return new Point2(-value.X, -value.Y);
        }

        public static Point2 operator *(Point2 value1, Point2 value2)
        {
            return new Point2(value1.X * value2.X, value1.Y * value2.Y);
        }

        public static Point2 operator *(Point2 value1, int value2)
        {
            return new Point2(value1.X * value2, value1.Y * value2);
        }

        public static Point2 operator *(int value1, Point2 value2)
        {
            return new Point2(value1 * value2.X, value1 * value2.Y);
        }

        public static Point2 operator /(Point2 value1, Point2 value2)
        {
            return new Point2(value1.X / value2.X, value1.Y / value2.Y);
        }

        public static Point2 operator /(Point2 value1, int value2)
        {
            return new Point2(value1.X / value2, value1.Y / value2);
        }

        public static bool operator ==(Point2 value1, Point2 value2)
        {
            return value1.X == value2.X && value1.X == value2.Y;
        }

        public static bool operator !=(Point2 value1, Point2 value2)
        {
            return !(value1 == value2);
        }

        public static bool operator ==(Point2 value1, Vector2 value2)
        {
            return value1.X == value2.x ? value1.X == value2.y : false;
        }

        public static bool operator !=(Point2 value1, Vector2 value2)
        {
            return !(value1 == value2);
        }

        public static bool operator ==(Vector2 value1, Point2 value2)
        {
            return value1.x == value2.X ? value1.x == value2.Y : false;
        }

        public static bool operator !=(Vector2 value1, Point2 value2)
        {
            return !(value1 == value2);
        }

        public bool Equals(Point2 other)
        {
            return other.X == X && other.Y == Y;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (obj.GetType() != typeof(Point2))
                return false;

            return Equals((Point2)obj);
        }

        public override int GetHashCode()
        {
            return X.GetHashCode() + Y.GetHashCode();
        }

        public int SizeInBytes()
        {
            const int sizeOfInt = sizeof(int);

            return sizeOfInt * 2;
        }

        public static implicit operator Vector3(Point2 p)
        {
            return new Vector3(p.X, 0, p.Y);
        }

        public override string ToString()
        {
            return string.Format("X:{0}, Y:{1}", X, Y);
        }
    }
}