using System.Runtime.InteropServices;
using UnityEngine;

namespace Assets.Classes.Foundation.Classes
{
    [StructLayout(LayoutKind.Sequential)]
    public struct RectD : ISizeInBytes
    {
        private double m_XMin;
        private double m_YMin;
        private double m_Width;
        private double m_Height;

        public RectD(double left, double top, double width, double height)
        {
            m_XMin = left;
            m_YMin = top;
            m_Width = width;
            m_Height = height;
        }

        public RectD(RectD source)
        {
            m_XMin = source.m_XMin;
            m_YMin = source.m_YMin;
            m_Width = source.m_Width;
            m_Height = source.m_Height;
        }

        public static RectD MinMaxRect(double left, double top, double right, double bottom)
        {
            return new RectD(left, top, right - left, bottom - top);
        }

        public double x
        {
            get { return m_XMin; }
            set { m_XMin = value; }
        }

        public double y
        {
            get { return m_YMin; }
            set { m_YMin = value; }
        }

        public double width
        {
            get { return m_Width; }
            set { m_Width = value; }
        }

        public double height
        {
            get { return m_Height; }
            set { m_Height = value; }
        }

        public double xMin
        {
            get { return this.m_XMin; }
            set
            {
                var xMax = this.xMax;
                m_XMin = value;
                m_Width = xMax - m_XMin;
            }
        }

        public double yMin
        {
            get { return m_YMin; }
            set
            {
                var yMax = this.yMax;
                m_YMin = value;
                m_Height = yMax - m_YMin;
            }
        }

        public double xMax
        {
            get { return (m_Width + m_XMin); }
            set { m_Width = value - m_XMin; }
        }

        public double yMax
        {
            get { return (m_Height + m_YMin); }
            set { m_Height = value - m_YMin; }
        }

        public override string ToString()
        {
            object[] args = new object[]
        {
                x, 
                y, 
                width, 
                height
        };

            return string.Format("(left:{0:F2}, top:{1:F2}, width:{2:F2}, height:{3:F2})", args);
        }

        public string ToString(string format)
        {
            object[] args = new object[]
        {
                x.ToString(format), 
                y.ToString(format), 
                width.ToString(format), 
                height.ToString(format)
        };

            return string.Format("(left:{0}, top:{1}, width:{2}, height:{3})", args);
        }

        public bool Contains(Vector2 point)
        {
            return ((((point.x >= xMin)
                && (point.x < xMax))
                && (point.y >= yMin))
                && (point.y < yMax));
        }

        public bool Contains(Vector3 point)
        {
            return ((((point.x >= xMin)
                && (point.x < xMax))
                && (point.y >= yMin))
                && (point.y < yMax));
        }

        public bool Contains(Vector2D point)
        {
            return ((((point.x >= xMin)
                && (point.x < xMax))
                && (point.y >= yMin))
                && (point.y < yMax));
        }

        public bool Contains(Vector3D point)
        {
            return ((((point.x >= xMin)
                && (point.x < xMax))
                && (point.y >= yMin))
                && (point.y < yMax));
        }

        public override int GetHashCode()
        {
            return (((x.GetHashCode()
                ^ (width.GetHashCode() << 2))
                ^ (y.GetHashCode() >> 2))
                ^ (height.GetHashCode() >> 1));
        }

        public int SizeInBytes()
        {
            const int sizeOfDouble = sizeof(double);

            return (sizeOfDouble * 4);
        }

        public override bool Equals(object other)
        {
            if (!(other is RectD))
                return false;

            var rect = (RectD)other;

            return (((x.Equals(rect.x)
                && y.Equals(rect.y))
                && width.Equals(rect.width))
                && height.Equals(rect.height));
        }

        public static bool operator !=(RectD lhs, RectD rhs)
        {
            return ((((lhs.x != rhs.x)
                || (lhs.y != rhs.y))
                || (lhs.width != rhs.width))
                || lhs.height != rhs.height);
        }

        public static bool operator ==(RectD lhs, RectD rhs)
        {
            return ((((lhs.x == rhs.x)
                      && (lhs.y == rhs.y))
                     && (lhs.width == rhs.width))
                    && (lhs.height == rhs.height));
        }
    }
}