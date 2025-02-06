using System.Reflection;
using System.Runtime.Serialization;
using UnityEditor;
using UnityEngine;

public abstract class BaseEditor : Editor
{
    static GUIStyle ms_titleStule = null;
    ComponentAttribute m_componentAttribute = null;
    private void OnEnable()
    {
        if (m_componentAttribute == null)
            m_componentAttribute = GetComponentAttribute(target);
    }
    public override void OnInspectorGUI()
    {
        HeaderGUI(m_componentAttribute); 

        base.OnInspectorGUI();

       // GUILayout.Label(m_componentAttribute.Name);
    }
    public static void HeaderGUI(ComponentAttribute componentAttribute)
    {
        if (componentAttribute != null)
        {
            GUILayout.Space(10f);

            if (ms_titleStule == null)
            {
                ms_titleStule = new GUIStyle(GUI.skin.label);
                ms_titleStule.fontSize = 15;
                ms_titleStule.fontStyle = FontStyle.Bold;
                ms_titleStule.alignment = TextAnchor.MiddleCenter;
            }

            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();

            GUILayout.Label(componentAttribute.Name, ms_titleStule);

            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();

            GUILayout.Box(componentAttribute.Description, GUILayout.Width(Screen.width * .4f));

            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();


            GUILayout.Space(20f);
        }
    }
    public static ComponentAttribute GetComponentAttribute(UnityEngine.Object obj)
    {
        return obj.GetType().GetCustomAttribute<ComponentAttribute>() ?? new ComponentAttribute(obj.GetType().ToString());
    }
}
