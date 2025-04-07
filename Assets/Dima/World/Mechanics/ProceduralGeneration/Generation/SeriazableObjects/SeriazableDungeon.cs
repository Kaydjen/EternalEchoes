using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace ProceduralGeneration.SeriazableObjects
{

    [Serializable]
    public class PropDungeonConfiguration : PropConfiguration
    {
        [Space(10), Header("Spawn configuration")]
        public Vector2Int offset;
        [Range(-1, 2)] public int rotation = 0;
    }

    [CreateAssetMenu(fileName = "NewDungeon", menuName = "ProceduralGeneration/Dungeon")]
    public class SeriazableDungeon : SeriazableLocation
    {
        public List<PropDungeonConfiguration> constProps = new List<PropDungeonConfiguration>();

        [Space(4)]
        [TextArea(1, 50)]

        public string layout =
            "■■□■■" + "\n" +
            "□■■■□" + "\n" +
            "□□■□□" + "\n" +
            "□■■■□" + "\n" +
            "■■□■■";
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(SeriazableDungeon))]
    public class SeriazableDungeonEditor : Editor
    {
        bool hideChecker = true;
        float scale = 1;

        GameObject visualized;
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EditorGUILayout.Space(30);

            hideChecker = EditorGUILayout.Toggle(new GUIContent("Hide"), hideChecker);
            if (!hideChecker)
            {
                scale = EditorGUILayout.FloatField(new GUIContent("Scale"), scale);

                EditorGUILayout.Space(5);

                if (GUILayout.Button(new GUIContent("Check")))
                {
                    if (visualized != null) DestroyImmediate(visualized);

                    SeriazableDungeon dungeon = serializedObject.targetObject as SeriazableDungeon;
                    if (dungeon == null) return;

                    visualized = new GameObject(target.name);

                    GameObjects.Location location = new GameObjects.Location(dungeon, null, Grid2Int.StringToGrid(dungeon.layout));

                    foreach (PropDungeonConfiguration configuration in dungeon.constProps)
                        Logic.Generator.CreateProp(location, configuration.seriazableProp, configuration.offset, configuration.rotation);

                    Logic.Renderer3D.RenderGrid(location, visualized.transform, scale);
                    Logic.Renderer3D.RenderProps(location, visualized.transform, scale);
                }
            }
            if (serializedObject.hasModifiedProperties) serializedObject.ApplyModifiedProperties();
        }
    }
#endif
}
