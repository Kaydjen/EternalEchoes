using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace ProceduralGeneration.SeriazableObjects
{
    [CreateAssetMenu(fileName = "NewProp", menuName = "ProceduralGeneration/Prop")]
    public class SeriazableProp : ScriptableObject
    {
        public string type = "Unknown";

        [Space(5), Header("Visualization")]
        public GameObject gameObject;
        public Vector2 pivot = new Vector2(2, 2);

        [TextArea(1, int.MaxValue)]

        public string hitbox =
            "□□■□□" + "\n" +
            "□■■■□" + "\n" +
            "■■■■■" + "\n" +
            "□■■■□" + "\n" +
            "□□■□□";
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(SeriazableProp))]
    public class SeriazablePropEditor : Editor
    {
        bool hideChecker = true;
        float scale;
        GameObject tilePrefab;
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EditorGUILayout.Space(30);


            hideChecker = EditorGUILayout.Toggle(new GUIContent("Hide"), hideChecker);
            if (!hideChecker) {

                scale = EditorGUILayout.FloatField(new GUIContent("Scale"), scale);

                EditorGUILayout.Space(5);
                GUILayout.Label(new GUIContent("Visualization"));

                tilePrefab = EditorGUILayout.ObjectField(new GUIContent("Tile Prefab"), tilePrefab, typeof(GameObject), true) as GameObject;

                EditorGUILayout.Space(5);

                if (GUILayout.Button(new GUIContent("Check hitbox")))
                {
                    if (tilePrefab == null) {
                        Debug.LogWarning("Tile Prefab property have to be serialized");
                        return;
                    }

                    SeriazableProp prop = serializedObject.targetObject as SeriazableProp;

                    if (prop == null) return;

                    Transform parent = new GameObject(target.name).transform;
    
                    Logic.Renderer3D.RenderGrid(Grid2Int.StringToGrid(prop.hitbox, Vector2Int.zero), tilePrefab, parent, scale);
                    Logic.Renderer3D.Create(prop.gameObject, parent, new Vector3(prop.pivot.x, 0, prop.pivot.y)*scale);
                }
            }
            if (serializedObject.hasModifiedProperties) serializedObject.ApplyModifiedProperties();
        }
    }
#endif
}
