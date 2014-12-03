// Adaptation of MonoGame's BoundingFrustum: http://monogame.codeplex.com

using System;
using Assets.Classes.Foundation.Extensions;
using UnityEngine;

namespace Assets.Classes.Foundation.Classes
{
    public struct Frustum : ISizeInBytes
    {
        private readonly Matrix4x4 _matrix;
        private readonly Plane[] _planes;
        public Plane[] Planes { get { return _planes; } }
        
        public Matrix4x4 Matrix { get { return _matrix; } }

        public Plane Left { get { return _planes[0]; } }
        public Plane Right { get { return _planes[1]; } }
        public Plane Bottom { get { return _planes[2]; } }
        public Plane Top { get { return _planes[3]; } }
        public Plane Near { get { return _planes[4]; } }
        public Plane Far { get { return _planes[5]; } }

        public Frustum(Camera fromCamera) : this(fromCamera.projectionMatrix * fromCamera.worldToCameraMatrix) {}

        public Frustum(Matrix4x4 matrix)
        {
            _matrix = matrix;
            _planes = GeometryUtility.CalculateFrustumPlanes(matrix);
        }

        public bool Intersects(Bounds bounds)
        {
            return GeometryUtility.TestPlanesAABB(_planes, bounds);
        }

        public bool Intersects(Rect rect)
        {
            return GeometryUtility.TestPlanesAABB(_planes, rect.ToBounds(float.MaxValue));
        }

        public bool Intersects(Rectangle rect)
        {
            return GeometryUtility.TestPlanesAABB(_planes, rect);
        }

        public bool Intersects(Sphere sphere)
        {
            throw new NotImplementedException();
        }

        public bool Intersects(Circle circle)
        {
            throw new NotImplementedException();
        }

        public bool Contains(Bounds bounds)
        {
            throw new NotImplementedException();
        }

        public bool Contains(Rect rect)
        {
            throw new NotImplementedException();
        }

        public bool Contains(Rectangle rect)
        {
            throw new NotImplementedException();
        }

        public bool Contains(Sphere sphere)
        {
            for(var i = 0; i < _planes.Length; i++)
            {
                var distance = _planes[i].GetDistanceToPoint(sphere.Center);

                if (distance < -sphere.Radius)
                    return false;

                if (distance < sphere.Radius)
                    return true;
            }

            return true;
        }

        public bool Contains(Circle circle)
        {
            for (var i = 0; i < _planes.Length; i++)
            {
                // Ignore bottom/top planes
                if (i == 2 || i == 3)
                    continue;

                var distance = _planes[i].GetDistanceToPoint(circle.Center);

                if (distance < -circle.Radius)
                    return false;

                if (distance < circle.Radius)
                    return true;
            }

            return true;
        }

        public bool Contains(Vector2 vector)
        {
            // Perform in 2 dimensions (ignore bottom/top planes)

            for (var i = 0; i < _planes.Length; i++)
            {
                // Ignore bottom/top planes
                if (i == 2 || i == 3)
                    continue;

                if (_planes[i].GetDistanceToPoint(vector) < 0)
                    return false;
            }

            return true;
        }

        public bool Contains(Vector3 vector)
        {
            for(var i = 0; i < _planes.Length; i++)
            {
                if (_planes[i].GetDistanceToPoint(vector) < 0)
                    return false;
            }

            return true;
        }

        // TODO
        public int SizeInBytes()
        {
            const int sizeOfPlane = 0;
            const int sizeOfMatrix = 0;

            return (sizeOfPlane * 6) + sizeOfMatrix;
        }
    }
}