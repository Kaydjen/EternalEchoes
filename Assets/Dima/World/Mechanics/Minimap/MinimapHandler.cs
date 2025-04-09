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

        public Vector2Int GetCurrentPosition(float scale = 1f) => Grid2Int.ToGridPosition(transform.position, scale);

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
            for (int i = 0; i < pixels.Length; i++) {
                if (world.Grid.Contains(new Vector2Int(i % map.height, i / map.height) + world.Grid.Min)) pixels[i] = Color.grey;
                else pixels[i] = Color.black;
            }
                
            map.SetPixels(pixels);

            foreach (var prop in world.GetDescedants<Prop>((x)=> x.Type.ToLower() == "spawner"))
            {
                for (int iPos = 0; iPos < prop.Grid.Count; iPos++)
                    map.SetPixel(prop.Grid[iPos].x - world.Grid.Min.x, prop.Grid[iPos].y - world.Grid.Min.y, Color.red);

                prop.onDestroy.AddListener(() =>
                {
                    for (int iPos = 0; iPos < prop.Grid.Count; iPos++)
                        map.SetPixel(prop.Grid[iPos].x - world.Grid.Min.x, prop.Grid[iPos].y - world.Grid.Min.y, Color.grey);
                });
            }
                

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
                    if (!last.ContainsKey(minimapObjects[iMinimapObject].lastPosition)) 
                        last.Add(minimapObjects[iMinimapObject].lastPosition, minimapObjects[iMinimapObject].lastColor);

                if (map.GetPixel(currentPosition.x, currentPosition.y) != minimapObjects[iMinimapObject].color)
                    minimapObjects[iMinimapObject].lastColor = map.GetPixel(currentPosition.x, currentPosition.y);

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
            if (mainCamera == null) return;

            Vector2Int currentPosition = Grid2Int.ToGridPosition(mainCamera.position, world.Scale) - world.Grid.Min;

            float uvX = Mathf.Clamp((currentPosition.x - ((map.width) * content.uvRect.width) / 2) / map.width, 0, 1-content.uvRect.width),
                uvY = Mathf.Clamp((currentPosition.y - ((map.height) * content.uvRect.height) / 2) / map.height, 0, 1-content.uvRect.height);

            content.uvRect = new Rect(uvX, uvY, content.uvRect.width, content.uvRect.height);
        }
    }
}

