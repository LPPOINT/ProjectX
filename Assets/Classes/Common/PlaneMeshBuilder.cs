using UnityEngine;

namespace Assets.Classes.Common
{
    public static class PlaneMeshBuilder
    {
        public static Mesh CreateMesh(float width, float height)
        {
            Mesh m = new Mesh();
            m.name = "ScriptedMesh";
            m.vertices = new Vector3[] {
         new Vector3(-width, -height, 0.01f),
         new Vector3(width, -height, 0.01f),
         new Vector3(width, height, 0.01f),
         new Vector3(-width, height, 0.01f)
     };
            m.uv = new Vector2[] {
         new Vector2 (0, 0),
         new Vector2 (0, 1),
         new Vector2(1, 1),
         new Vector2 (1, 0)
     };
            m.triangles = new int[] { 0, 1, 2, 0, 2, 3 };
            m.RecalculateNormals();

            return m;
        }
        public static GameObject CreatePlane(float width, float height, Color color)
        {
            var plane = new GameObject("Plane");
            var meshFilter = (MeshFilter)plane.AddComponent(typeof(MeshFilter));
            meshFilter.mesh = CreateMesh(width, height);
            var renderer = plane.AddComponent(typeof(MeshRenderer)) as MeshRenderer;
            renderer.material.shader = Shader.Find("Particles/Additive");
            var tex = new Texture2D(1, 1);
            tex.SetPixel(0, 0, color);
            tex.Apply();
            renderer.material.mainTexture = tex;
            renderer.material.color = Color.green;
            return plane;
        }
    }
}
