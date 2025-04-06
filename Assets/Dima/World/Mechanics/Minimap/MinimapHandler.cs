using ProceduralGeneration.GameObjects;
using ProceduralGeneration.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Minimap
{

    [Serializable]
    public class MoveableMinimapObject
    {
        public Transform transform;

        [Space(10), Header("Visualization")]
        public int layer;
        public Color color;
        [NonSerialized] public Vector2Int lastPosition;
        [NonSerialized] public Color lastColor = Color.black;
    }
    public class MinimapHandler : MonoBehaviour
    {
        [SerializeField] private RawImage content;
        [NonSerialized] public World world;
        public List<MoveableMinimapObject> minimapObjects = new List<MoveableMinimapObject>();

        private Texture2D map;

        void Start()
        {
            minimapObjects.Sort((x, y) => (x.layer.CompareTo(y.layer)));

            world = Database.worlds[0];

            Vector2Int size = world.Grid.Max - world.Grid.Min;

            map = new Texture2D(Mathf.Max(size.x, size.y)+1, Mathf.Max(size.x, size.y)+1);
            map.filterMode = FilterMode.Point;
            content.texture = map;

            Color[] pixels = new Color[map.width * map.height];
            for (var i = 0; i < pixels.Length; i++)
                pixels[i] = Color.black;

            map.SetPixels(pixels);

            foreach (var prop in world.GetDescedants<Prop>())
                foreach (Vector2Int position in prop.Grid)
                    map.SetPixel(position.x-world.Grid.Min.x, position.y - world.Grid.Min.y, Color.red);

            map.Apply();
        }

        private (Dictionary<Vector2Int, Color>, Dictionary<Vector2Int, Color>) GetPositions()
        {
            Dictionary<Vector2Int, Color> current = new Dictionary<Vector2Int, Color>(), last = new Dictionary<Vector2Int, Color>();

            foreach(MoveableMinimapObject minimapObject in minimapObjects)
            {
                Vector3 currentPositionV3 = minimapObject.transform.position / world.Scale;
                Vector2Int currentPosition = Vector2Int.RoundToInt(new Vector2(currentPositionV3.x, currentPositionV3.z)) - world.Grid.Min;

                if (currentPosition.x > map.width || currentPosition.x < 0 || currentPosition.y > map.height || currentPosition.y < 0)continue;
                if (current.ContainsKey(currentPosition)) current[currentPosition] = minimapObject.color;
                else current.Add(currentPosition, minimapObject.color);
                
                if (!last.ContainsKey(minimapObject.lastPosition)) last.Add(minimapObject.lastPosition, minimapObject.lastColor);

                if (minimapObject.lastPosition != currentPosition) { 
                    Color lastColor = map.GetPixel(currentPosition.x, currentPosition.y);
                    minimapObject.lastColor = lastColor == Color.black && world.Grid.Contains(currentPosition + world.Grid.Min) && minimapObject.transform.gameObject.tag != Camera.main.tag ? new Color(.5f, .5f, .5f) : lastColor;
                }

                minimapObject.lastPosition = currentPosition;
            }

            return (current, last);
        }

        private void ColorPixels()
        {
            (Dictionary<Vector2Int, Color> current, Dictionary<Vector2Int, Color> last) = GetPositions();

            List<Vector2Int> toColorBack = last.Keys.ToList();
            toColorBack.Except(current.Keys);

            foreach(Vector2Int pos in toColorBack) map.SetPixel(pos.x, pos.y, last[pos]);

            foreach (Vector2Int pos in current.Keys) map.SetPixel(pos.x, pos.y, current[pos]);
        }
        void FixedUpdate()
        {
            if (minimapObjects.Count == 0) return;

            ColorPixels();

            map.Apply();

            SetOffset();
        }

        private void SetOffset()
        {
            Vector3 currentPositionV3 = minimapObjects[minimapObjects.Count-1].transform.position / world.Scale;
            Vector2Int currentPosition = Vector2Int.RoundToInt(new Vector2(currentPositionV3.x, currentPositionV3.z))- world.Grid.Min;

            float uvX = Mathf.Clamp((currentPosition.x - ((map.width) * content.uvRect.width) / 2) / map.width, 0, 1-content.uvRect.width),
                uvY = Mathf.Clamp((currentPosition.y - ((map.height) * content.uvRect.height) / 2) / map.height, 0, 1-content.uvRect.height);

            content.uvRect = new Rect(uvX, uvY, content.uvRect.width, content.uvRect.height);
        }
    }
}

