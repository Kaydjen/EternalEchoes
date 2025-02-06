using System.Reflection;
using UnityEditor;
using UnityEngine;

public abstract class BaseEditor : Editor
{
    private ComponentAttribute _componentAttribute = null;
    private void OnEnable()
    {
        if (_componentAttribute == null)
            _componentAttribute = GetComponentAttribute(target);
    }
    public override void OnInspectorGUI()
    {
        if (_componentAttribute != null) HeaderGUI(_componentAttribute); 

        base.OnInspectorGUI();
    }
    public static void HeaderGUI(ComponentAttribute componentAttribute)
    {
        GUILayout.Space(10f);

        GUIStyle titleStyle = new GUIStyle(GUI.skin.label)
        {
            fontSize = 15,
            fontStyle = FontStyle.Bold,
            alignment = TextAnchor.MiddleCenter
        };

        EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();

        GUILayout.Label(componentAttribute.Name, titleStyle);

        GUILayout.FlexibleSpace();
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();

        GUILayout.Box(componentAttribute.Description, GUILayout.Width(Screen.width * .4f));

        GUILayout.FlexibleSpace();
        EditorGUILayout.EndHorizontal();


        GUILayout.Space(20f);
    }
    public static ComponentAttribute GetComponentAttribute(Object obj)
    {
        return obj.GetType().GetCustomAttribute<ComponentAttribute>();
    }
}
