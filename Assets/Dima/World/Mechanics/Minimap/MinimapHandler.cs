using ProceduralGeneration.GameObjects;
using ProceduralGeneration.Logic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

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

        static public Vector2Int ToGridPosition(Vector3 position, float scale = 1f)
        {
            if (scale <= 0f) return Vector2Int.zero;

            Vector3 positionV3 = position / scale;
            return Vector2Int.RoundToInt(new Vector2(positionV3.x, positionV3.z));
        }

        public Vector2Int GetCurrentPosition(float scale = 1f) => ToGridPosition(transform.position, scale);

        public MoveableMinimapObject(Transform transform, int layer = 0, Color color = default)
        {
            this.transform = transform;
            this.layer = layer;
            this.color = color;
        }
    }
    public class MinimapHandler : MonoBehaviour
    {
        [SerializeField] private RawImage content;
        [SerializeField] private List<MoveableMinimapObject> serializedMinimapObjects;
        [NonSerialized] public World world;
        
        static private List<MoveableMinimapObject> minimapObjects = new List<MoveableMinimapObject>();
        [SerializeField] private Transform mainCamera; 

        private Texture2D map;

        static public void Add(MoveableMinimapObject minimapObject)
        {
            if (minimapObjects.Contains(minimapObject)) return;
            minimapObjects.Add(minimapObject);
        }

        static public void Sort() => minimapObjects.Sort((x, y) => x.layer.CompareTo(y.layer));

        private void Start() { StartCoroutine(Init()); }
        IEnumerator Init()
        {
            yield return new WaitWhile(() =>
            {
                world = Database.GetWorld(0);

                return world == null;
            });

            Vector2Int size = world.Grid.Max - world.Grid.Min;

            map = new Texture2D(Mathf.Max(size.x, size.y) + 1, Mathf.Max(size.x, size.y) + 1);
            map.filterMode = FilterMode.Point;
            content.texture = map;

            Color[] pixels = new Color[map.width * map.height];
            for (int i = 0; i < pixels.Length; i++) pixels[i] = Color.black;
                
            map.SetPixels(pixels);

            for (int iPosition = 0; iPosition < world.Grid.Count; iPosition++)
                map.SetPixel(world.Grid[iPosition].x - world.Grid.Min.x, world.Grid[iPosition].y - world.Grid.Min.y, Color.grey);

            foreach (var prop in world.GetDescedants<Prop>())
                foreach (Vector2Int position in prop.Grid)
                    map.SetPixel(position.x - world.Grid.Min.x, position.y - world.Grid.Min.y, Color.red);

            minimapObjects = minimapObjects.Union(serializedMinimapObjects).ToList();
            Sort();

            map.Apply();
        }

        private (Dictionary<Vector2Int, Color>, Dictionary<Vector2Int, Color>) GetPositions()
        {
            Dictionary<Vector2Int, Color> current = new Dictionary<Vector2Int, Color>(), last = new Dictionary<Vector2Int, Color>();

            for (int iMinimapObject = 0; iMinimapObject < minimapObjects.Count; iMinimapObject++)
            {
                if (minimapObjects[iMinimapObject].transform.gameObject == null)
                {
                    if (minimapObjects.Contains(minimapObjects[iMinimapObject]))
                    {
                        minimapObjects.Remove(minimapObjects[iMinimapObject]);
                        iMinimapObject--;
                        continue;
                    }
                }

                Vector2Int currentPosition = minimapObjects[iMinimapObject].GetCurrentPosition(world.Scale) - world.Grid.Min;

                if (!(currentPosition.x > map.width || currentPosition.x < 0 || currentPosition.y > map.height || currentPosition.y < 0))
                {
                    if (current.ContainsKey(currentPosition)) current[currentPosition] = minimapObjects[iMinimapObject].color;
                    else current.Add(currentPosition, minimapObjects[iMinimapObject].color);
                }

                if (minimapObjects[iMinimapObject].lastPosition != currentPosition)
                {
                    if (!last.ContainsKey(minimapObjects[iMinimapObject].lastPosition)) last.Add(minimapObjects[iMinimapObject].lastPosition, minimapObjects[iMinimapObject].lastColor);

                    Color lastColor = map.GetPixel(currentPosition.x, currentPosition.y);
                    minimapObjects[iMinimapObject].lastColor = lastColor;
                }

                minimapObjects[iMinimapObject].lastPosition = currentPosition;
            }

            return (current, last);
        }

        private void ColorPixels()
        {
            (Dictionary<Vector2Int, Color> current, Dictionary<Vector2Int, Color> last) = GetPositions();

            List<Vector2Int> toColor = current.Keys.ToList(), toColorBack = last.Keys.Except(toColor).ToList();
                

            for (int iPos = 0; iPos < toColorBack.Count; iPos++) map.SetPixel(toColorBack[iPos].x, toColorBack[iPos].y, last[toColorBack[iPos]]);

            for (int iPos = 0; iPos < toColor.Count; iPos++)
                map.SetPixel(toColor[iPos].x, toColor[iPos].y, current[toColor[iPos]]);


            map.Apply();
        }
        void FixedUpdate()
        {
            if (world == null) return;

            if (minimapObjects.Count != 0) ColorPixels();

            SetOffset();
        }

        private void SetOffset()
        {
            Vector2Int currentPosition = MoveableMinimapObject.ToGridPosition(mainCamera.position, world.Scale) - world.Grid.Min;

            float uvX = Mathf.Clamp((currentPosition.x - ((map.width) * content.uvRect.width) / 2) / map.width, 0, 1-content.uvRect.width),
                uvY = Mathf.Clamp((currentPosition.y - ((map.height) * content.uvRect.height) / 2) / map.height, 0, 1-content.uvRect.height);

            content.uvRect = new Rect(uvX, uvY, content.uvRect.width, content.uvRect.height);
        }
    }
}

