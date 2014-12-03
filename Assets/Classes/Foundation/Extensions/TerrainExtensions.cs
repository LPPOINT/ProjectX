using System;
using UnityEngine;

namespace Assets.Classes.Foundation.Extensions
{
    public static class TerrainExtensions
    {
        #region Getters

        public static float GetHeight(this Terrain t, int worldX, int worldY)
        {
            if (t.ToRect().Contains(worldX, worldY))
            {
                var pixel = t.WorldToPixel(worldX, worldY, t.terrainData.heightmapWidth, t.terrainData.heightmapHeight);

                return t.terrainData.GetHeight((int)pixel.x, (int)pixel.y);
            }

            return 0;
        }

        public static float GetInterpolatedHeight(this Terrain t, Vector2 worldPosition)
        {
            var clamped = t.WorldToClampedLocal(worldPosition);

            return t.terrainData.GetInterpolatedHeight(clamped.x, clamped.y);
        }

        public static Vector3 GetInterpolatedNormal(this Terrain t, Vector2 worldPosition)
        {
            var clamped = t.WorldToClampedLocal(worldPosition);

            return t.terrainData.GetInterpolatedNormal(clamped.x, clamped.y);
        }

        public static float GetSteepness(this Terrain t, Vector2 worldPosition)
        {
            var clamped = t.WorldToClampedLocal(worldPosition);

            return t.terrainData.GetSteepness(clamped.x, clamped.y);
        }

        public static float[, ,] GetAlphamaps(this Terrain t, Rect rect)
        {
            var clamped = t.WorldToPixel(rect, t.terrainData.heightmapWidth, t.terrainData.heightmapHeight);

            return t.terrainData.GetAlphamaps((int)clamped.x, (int)clamped.y, (int)clamped.width, (int)clamped.height);
        }

        public static int[,] GetDetailLayer(this Terrain t, Rect rect, int layerIndex)
        {
            var clamped = t.WorldToPixel(rect, t.terrainData.heightmapWidth, t.terrainData.heightmapHeight);

            return t.terrainData.GetDetailLayer((int)clamped.x, (int)clamped.y, (int)clamped.width, (int)clamped.height, layerIndex);
        }

        public static float[,] GetHeights(this Terrain t, Rect rect)
        {
            var clamped = t.WorldToPixel(rect, t.terrainData.heightmapWidth, t.terrainData.heightmapHeight);

            return t.terrainData.GetHeights((int)clamped.x, (int)clamped.y, (int)clamped.width, (int)clamped.height);
        }

        #endregion

        #region Setters

        public static void SetAlphamaps(this Terrain t, Rect rect, float[,] alphaData, int layerIndex)
        {
            var currentData = t.GetAlphamaps(rect);
            alphaData.CopyTo(currentData, layerIndex);

            SetAlphamaps(t, rect, currentData);
        }

        public static void SetAlphamaps(this Terrain t, Rect rect, float[, ,] alphaData)
        {
            if (rect.width != alphaData.GetLength(0) || rect.height != alphaData.GetLength(1))
                throw new ArgumentOutOfRangeException("alphaData", "Input dimensions must match input rectangle.");

            var clamped = t.WorldToPixel(rect, t.terrainData.heightmapWidth, t.terrainData.heightmapHeight);

            t.terrainData.SetAlphamaps((int)clamped.x, (int)clamped.y, alphaData);
        }

        public static void SetDetails(this Terrain t, Rect rect, int[,] detailData, int layerIndex)
        {
            if (rect.width != detailData.GetLength(0) || rect.height != detailData.GetLength(1))
                throw new ArgumentOutOfRangeException("detailData", "Input dimensions must match input rectangle.");

            var clamped = t.WorldToPixel(rect, t.terrainData.heightmapWidth, t.terrainData.heightmapHeight);

            t.terrainData.SetDetailLayer((int)clamped.x, (int)clamped.y, layerIndex, detailData);
        }

        public static void SetDetails(this Terrain t, Rect rect, int[, ,] detailData)
        {
            var layerCount = detailData.GetLength(2);

            for (var i = 0; i < layerCount; i++)
            {
                int[,] thisLayer = new int[detailData.GetLength(0), detailData.GetLength(1)];
                detailData.CopyTo(thisLayer, i);

                SetDetails(t, rect, thisLayer, i);
            }
        }

        public static void SetHeights(this Terrain t, Rect rect, float[,] heightData)
        {
            if (rect.width != heightData.GetLength(0) || rect.height != heightData.GetLength(1))
                throw new ArgumentOutOfRangeException("heightData", "Input dimensions must match input rectangle.");

            var clamped = t.WorldToPixel(rect, t.terrainData.heightmapWidth, t.terrainData.heightmapHeight);

            t.terrainData.SetHeights((int)clamped.x, (int)clamped.y, heightData);
        }

        #endregion

        #region Utils

        private static Vector2 WorldToPixel(this Terrain t, Vector2 worldPositon, int pixelWidth, int pixelHeight)
        {
            return WorldToPixel(t, (int)worldPositon.x, (int)worldPositon.y, pixelWidth, pixelHeight);
        }

        private static Vector2 WorldToPixel(this Terrain t, int worldX, int worldY, int pixelWidth, int pixelHeight)
        {
            var local = t.transform.WorldToLocal(new Vector3(worldX, 0, worldY));

            return ClipToRange(t, t.LocalToPixel(local, pixelWidth, pixelHeight), pixelWidth, pixelHeight);
        }

        private static Vector2 ClipToRange(this Terrain t, Vector3 position, int pixelWidth, int pixelHeight)
        {
            var x = position.x;
            var z = position.z;

            x = x < 0 ? 0 : x;
            x = x > pixelWidth ? pixelWidth : x;
            z = z < 0 ? 0 : z;
            z = z > pixelHeight ? pixelHeight : z;

            return new Vector2(x, z);
        }

        private static Rect WorldToPixel(this Terrain t, Rect rect, int pixelWidth, int pixelHeight)
        {
            var topLeft = WorldToPixel(t, (int)rect.x, (int)rect.y, pixelWidth, pixelHeight);
            var bottomRight = WorldToPixel(t, (int)rect.x, (int)(rect.y - rect.height), pixelWidth, pixelHeight);

            return new Rect().From2Points(topLeft, bottomRight);
        }

        public static Vector2 LocalToPixel(this Terrain t, Vector2 position, int pixelWidth, int pixelHeight)
        {
            var tSize = t.terrainData.size;
            var xRatio = pixelWidth / tSize.x;
            var yRatio = pixelHeight / tSize.y;

            return new Vector2(position.x * xRatio, position.y * yRatio);
        }

        public static Vector2 WorldToClampedLocal(this Terrain t, Vector2 position)
        {
            var local = t.transform.WorldToLocal(position);

            var x = local.x / t.terrainData.size.x;
            var y = local.y / t.terrainData.size.y;

            return new Vector2(x, y);
        }

        public static Rect WorldToClampedLocal(this Terrain t, Rect rect)
        {
            var localTopLeft = WorldToClampedLocal(t, new Vector2(rect.xMin, rect.yMax));
            var localBottomRight = WorldToClampedLocal(t, new Vector2(rect.xMax, rect.yMin));

            return new Rect().From2Points(localTopLeft, localBottomRight);
        }

        public static Rect ToRect(this Terrain t)
        {
            var s = t.terrainData.size;
            var p = t.transform.position;
            return new Rect(p.x, p.z + s.y, s.x, s.y);
        }

        #endregion
    }
}