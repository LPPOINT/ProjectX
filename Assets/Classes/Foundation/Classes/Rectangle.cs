using System;
using System.Runtime.InteropServices;
using Assets.Classes.Foundation.Extensions;
using UnityEngine;

namespace Assets.Classes.Foundation.Classes
{
    [StructLayout(LayoutKind.Sequential)]
    [Obsolete("RectI is the new Rectangle, in keeping with naming conventions")]
    public struct Rectangle : ISizeInBytes
    {
        private int _minX;
        private int _maxY;
        private int _width;
        private int _height;

        public Rectangle(Point2 center, int width, int height)
        {
            var halfWidth = width >> 1;
            var halfHeight = height >> 1;

            this._minX = center.X - halfWidth;
            this._maxY = center.Y + halfHeight;

            this._width = width;
            this._height = height;
        }

        public Rectangle(int left, int top, int width, int height)
        {
            this._minX = left;
            this._maxY = top;
            this._width = width;
            this._height = height;
        }

        public Rectangle(Rectangle source)
        {
            this._minX = source._minX;
            this._maxY = source._maxY;
            this._width = source._width;
            this._height = source._height;
        }

        public static Rectangle MinMaxRect(int left, int top, int right, int bottom)
        {
            return new Rectangle(left, top, right - left, bottom - top);
        }

        public int X
        {
            get { return this._minX; }
            set { this._minX = value; }
        }

        public int Y
        {
            get { return this._maxY; }
            set { this._maxY = value; }
        }

        public Point2 Center
        {
            get { return new Point2(CenterX, CenterY);}
        }

        public int CenterX
        {
            get { return MinX + (Width >> 1); }
        }

        public int CenterY
        {
            get { return MinY + (Height >> 1); }
        }

        public int Width
        {
            get { return this._width; }
            set { this._width = value; }
        }

        public int Height
        {
            get { return this._height; }
            set { this._height = value; }
        }

        public Vector3 Min
        {
            get { return new Vector3(MinX, 0, MaxY); }
        }

        public Vector3 Max
        {
            get { return new Vector3(MaxX, 0, MaxY); }
        }

        public int MinX
        {
            get { return this._minX; }
            set
            {
                this._minX = value;
                this._width = this.MaxX - this._minX;
            }
        }

        public int MinY
        {
            get { return (this._maxY - this._height); }
            set { this._height = this._maxY - value; }
        }

        public int MaxX
        {
            get { return (this._width + this._minX); }
            set { this._width = value - this._minX; }
        }

        public int MaxY
        {
            get { return this._maxY; }
            set
            {
                this._maxY = value;
                this._height = value - MinY;
            }
        }

        public bool Contains(int x, int y)
        {
            return ((((x >= this.MinX)
                      && (x < this.MaxX))
                     && (y >= this.MinY))
                    && (y < this.MaxY));
        }

        public bool Contains(Rectangle value)
        {
            return (((MinX <= value.MinX)
                && (value.MaxX <= MaxX))
                && ((MinY <= value.MinY)
                && (value.MaxY <= MaxY)));
        }

        public void Contains(ref Rectangle value, out bool result)
        {
            result = (((this.X <= value.X) 
                && ((value.X + value.Width) <= (this.X + this.Width))) 
                && (this.Y <= value.Y)) 
                && ((value.Y + value.Height) <= (this.Y + this.Height));
        }

        public bool Intersects(Rectangle value)
        {
            if ((MaxX < value.MinX) || (MinX > value.MaxX))
                return false;

            if ((MaxY < value.MinY) || (MinY > value.MaxY))
                return false;

            return true;

            //return (((this.MinX <= value.MaxX) && (this.MaxX >= value.MinX)) && ((this.MinY <= value.MaxY) && (this.MaxY >= value.MinY)));
        }

        public void Intersects(ref Rectangle value, out bool result)
        {
            result = (((value.X < (this.X + this.Width)) 
                && (this.X < (value.X + value.Width))) 
                && (value.Y < (this.Y + this.Height))) 
                && (this.Y < (value.Y + value.Height));
        }

        public override string ToString()
        {
            return string.Format("(left:{0}, top:{1}, width:{2}, height:{3})", X, Y, Width, Height);
        }

        public bool Contains(Vector2 point)
        {
            return ((((point.x >= this.MinX) 
                && (point.x < this.MaxX)) 
                && (point.y >= this.MinY)) 
                && (point.y < this.MaxY));
        }

        public bool Contains(Vector3 point)
        {
            return ((((point.x >= this.MinX) 
                && (point.x < this.MaxX)) 
                && (point.z >= this.MinY)) 
                && (point.z < this.MaxY));
        }

        public bool Contains(Sphere sphere)
        {
            var min = new Vector3(MinX, 0, MinY);
            var max = new Vector3(MaxX, 0, MaxY);

            var clamped = sphere.Center.Clamp(min, max);
            var distance = sphere.Center.DistanceSquared(clamped);
            var radius = sphere.Radius;

            if (distance > (radius * radius))
                return false;

            if (((((MinX + radius) <= sphere.Center.x) 
                && (sphere.Center.x <= (MaxX - radius))) 
                && (((MaxX - MinX) > radius) 
                && ((MinY + radius) <= sphere.Center.y))) 
                && (((sphere.Center.y <= (MaxY - radius)) 
                && ((MaxY - MinY) > radius)) 
                && ((MaxX - MinX) > radius)))
                return true;

            return false;
        }

        public bool Intersects(Sphere sphere)
        {
            var vector = sphere.Center.Clamp(Min, Max);
            var num = sphere.Center.DistanceSquared(vector);

            return (num <= (sphere.Radius * sphere.Radius));
        }

        public override int GetHashCode()
        {
            return (((this.X.GetHashCode() 
                ^ (this.Width.GetHashCode() << 2)) 
                ^ (this.Y.GetHashCode() >> 2)) 
                ^ (this.Height.GetHashCode() >> 1));
        }

        public int SizeInBytes()
        {
            const int sizeOfInt = sizeof(int);

            return sizeOfInt * 4;
        }

        public override bool Equals(object other)
        {
            if (!(other is Rectangle))
                return false;

            Rectangle rect = (Rectangle)other;

            return (((this.X.Equals(rect.X) && this.Y.Equals(rect.Y)) && this.Width.Equals(rect.Width)) && this.Height.Equals(rect.Height));
        }

        public static bool operator !=(Rectangle lhs, Rectangle rhs)
        {
            return ((((lhs.X != rhs.X) 
                || (lhs.Y != rhs.Y)) 
                || (lhs.Width != rhs.Width)) 
                || lhs.Height != rhs.Height);
        }

        public static bool operator ==(Rectangle lhs, Rectangle rhs)
        {
            return ((((lhs.X == rhs.X) 
                && (lhs.Y == rhs.Y)) 
                && (lhs.Width == rhs.Width)) 
                && (lhs.Height == rhs.Height));
        }

        public static implicit operator Rect(Rectangle r)
        {
            return new Rect(r.MinX, r.MaxY, r.Width, r.Height);
        }

        public static implicit operator Bounds(Rectangle r)
        {
            return new Bounds(new Vector3(r.X, 0, r.Y), new Vector3(r.Width, float.MaxValue, r.Height));
        }
    }
}