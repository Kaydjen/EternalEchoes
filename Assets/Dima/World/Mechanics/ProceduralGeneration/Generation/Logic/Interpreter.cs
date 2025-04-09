using System.Collections.Generic;
using UnityEngine;
using ProceduralGeneration.GameObjects;
using ProceduralGeneration.SeriazableObjects;
using System.Linq;
using System;
using UnityEngine.UIElements;

namespace ProceduralGeneration.Logic
{
    public class Interpreter
    {

        public static string Reverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
        static public List<string> RotateGrid(string grid, int rotation = 0)
        {
            List<string> gridList = grid.Replace("\r", "").Split('\n').ToList();

            int maxSize = gridList[0].Length;
            for (int iStr = 0; iStr < gridList.Count; iStr++)
            {
                if (gridList[iStr].Length > maxSize) maxSize = gridList[iStr].Length;
            }

            if (rotation == 2)
                for (int j = 0; j < gridList.Count; j++) 
                    for (int i = 0; i < maxSize - gridList[j].Length; i++) gridList[j] += '□';
            else if (Mathf.Abs(rotation) == 1)
            {
                List<string> rotated = new List<string>();

                for (int x = 0; x < maxSize; x++)
                {
                    string str = "";
                    for (int y = gridList.Count - 1; y >= 0; y--)
                    {
                        try { str += gridList[y][x]; }
                        catch { str += '□'; }
                    }

                    rotated.Add(str);
                }

                gridList = rotated;
            }

            if (rotation == 1 || rotation == 2)
            {
                gridList.Reverse();

                for (int i = 0; i < gridList.Count; i++) {
                    gridList[i] = Reverse(gridList[i]);
                }
            }

            return gridList;
        }

        static public Vector2 RotatePivot(string grid, Vector2 pivot, int rotation = 0)
        {
            List<string> gridArray = grid.Replace("\r", "").Split('\n').ToList();

            int maxSize = gridArray[0].Length;
            foreach (string str in gridArray)
            {
                if (str.Length > maxSize) maxSize = str.Length;
            }

            if (rotation != -1 && rotation != 0)
            {
                pivot.y = gridArray.Count - pivot.y - 1;
            }
            if (rotation != 1 && rotation != 0)
            {
                pivot.x = maxSize - pivot.x - 1;
                
            }

            return pivot;
        }
        static public float DeepestCoordinate(GameObject gameObject)
        {
            float deepestCoordinate = gameObject.transform.position.y - 1f;
            Renderer[] renderers = gameObject.GetComponentsInChildren<UnityEngine.Renderer>();

            for (int iRenderers = 0; iRenderers < renderers.Length; iRenderers++)
                if (deepestCoordinate > renderers[iRenderers].bounds.min.y) deepestCoordinate = renderers[iRenderers].bounds.min.y;

            return deepestCoordinate;
        }
        static public Vector2 RotateDirection2D(Vector2 direction, float angle)
        {
            float rad = angle * Mathf.Deg2Rad;
            float cos = Mathf.Cos(rad);
            float sin = Mathf.Sin(rad);
            return new Vector2(direction.x * cos - direction.y * sin, direction.x * sin + direction.y * cos);
        }
        static public Vector3 SizeToScale(GameObject gameObject, Vector3 size)
        {
            if (gameObject == null || size == Vector3.zero) return Vector3.zero;
            UnityEngine.Renderer renderer = gameObject.GetComponent<UnityEngine.Renderer>();
            if (renderer == null) return Vector3.zero;

            Vector3 scale = gameObject.transform.localScale, absoluteSize = renderer.bounds.size;

            return new Vector3(scale.x * size.x / absoluteSize.x, scale.y * size.y / absoluteSize.y, scale.z * size.z / absoluteSize.z);
        }

        static public Vector3 CentralPosition(GameObject gameObject)
        {
            UnityEngine.Renderer[] renderers = gameObject.GetComponentsInChildren<UnityEngine.Renderer>();

            if (renderers.Length == 0) return Vector3.zero;

            Vector3 min = renderers[0].bounds.min, max = renderers[0].bounds.max;

            for (int iRenderers = 1; iRenderers < renderers.Length; iRenderers++)
            {
                min = Vector3.Min(min, renderers[iRenderers].bounds.min);
                max = Vector3.Max(max, renderers[iRenderers].bounds.max);
            }

            return (min + max) / 2;
        }
        static public World CreateWorld(in SeriazableWorld world) => new World(world);
    }
}
