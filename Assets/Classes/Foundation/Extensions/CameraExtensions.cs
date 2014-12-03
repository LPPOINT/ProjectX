using System;
using Assets.Classes.Foundation.Enums;
using UnityEngine;

namespace Assets.Classes.Foundation.Extensions
{
    public static class CameraExtensions
    {
        public static Vector3 GetCameraPoint(this Camera camera, Alignment pointAlignment)
        {
            if (pointAlignment == Alignment.Middle) return camera.transform.position;

            var thisCam = camera;
            var farClip = thisCam.farClipPlane;

            var topLeftPosition = thisCam.ViewportToWorldPoint(new Vector3(0, 1, farClip));
            var topRightPosition = thisCam.ViewportToWorldPoint(new Vector3(1, 1, farClip));
            var btmRightPosition = thisCam.ViewportToWorldPoint(new Vector3(1, 0, farClip));
            var btmLeftPosition = thisCam.ViewportToWorldPoint(new Vector3(0, 0, farClip));


            if (pointAlignment == Alignment.MiddleLeft || pointAlignment == Alignment.MiddleRight ||
                pointAlignment == Alignment.TopMiddle || pointAlignment == Alignment.BottomMiddle)
            {
                var width = Math.Abs(topLeftPosition.x - topRightPosition.x);
                var height = Math.Abs(topLeftPosition.y - btmLeftPosition.y);
                var halfWidth = width/2;
                var halfHeight = height/2;

                if(pointAlignment == Alignment.MiddleLeft) return new Vector3(camera.transform.position.x - halfWidth, camera.transform.position.y);
                if (pointAlignment == Alignment.MiddleRight) return new Vector3(camera.transform.position.x + halfWidth, camera.transform.position.y);

                if (pointAlignment == Alignment.TopMiddle) return new Vector3(camera.transform.position.x, camera.transform.position.y + halfHeight);
                if (pointAlignment == Alignment.BottomMiddle) return new Vector3(camera.transform.position.x , camera.transform.position.y - halfHeight);

            }

            if (pointAlignment == Alignment.BottomLeft) return btmLeftPosition;
            if (pointAlignment == Alignment.BottomRight) return btmRightPosition;
            if (pointAlignment == Alignment.TopLeft) return topLeftPosition;
            if (pointAlignment == Alignment.TopRight) return topRightPosition;

            return camera.transform.position;

        }
    }
}
