using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Classes.Foundation.Classes
{
    public struct MultiBounds
    {
        private readonly Vector3 _center;
        public Vector3 Center { get { return _center; } }

        private readonly int _levels;
        public int Levels { get { return _levels; } }

        private readonly List<Bounds> _bounds;
        public List<Bounds> Bounds { get { return _bounds; } }

        public MultiBounds(Vector3 center, params int[] sizes)
        {
            _center = center;
            _levels = sizes.Length;
            _bounds = new List<Bounds>(_levels);

            var sizesByGreater = sizes.OrderBy(x => x).ToArray();

            for (var i = 0; i < _levels; i++)
            {
                var thisSize = sizesByGreater[i];
                _bounds.Add(new Bounds(_center, new Vector3(thisSize, thisSize, thisSize)));
            }
        }

        // Returns earliest level contained
        public int Contains(Vector3 point)
        {
            return ContainsIterator(b => b.Contains(point));
        }

        public int Contains(Vector2 point)
        {
            return ContainsIterator(b => b.Contains(point));
        }

        public int Contains(Bounds bounds)
        {
            return 1;
            //return ContainsIterator(b => b.Contains(bounds));
        }

        private int ContainsIterator(Func<Bounds, bool> func)
        {
            for (var i = 0; i < Levels; i++)
                if (func(Bounds[i]))
                    return i;

            return -1;
        }
    }
}