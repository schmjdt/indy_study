using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(ObjectCustom))]
public class ObjectCustomInspector : Editor {

    SerializedProperty serProp;


    void OnEnable()
    {
        
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        serProp = serializedObject.GetIterator();
        serProp.NextVisible(true);

        do
        {
            switch (serProp.name)
            {
                case "publicFloatMin":
                    createBorderTitle("Float Manipulation:");               // Start of 'float' group, create title

                    EditorGUILayout.PropertyField(serProp);                 // Display default float field
                    serProp.floatValue = Mathf.Max(0, serProp.floatValue);  // Don't allow value below 0
                    break;
                case "publicFloat":
                    EditorGUILayout.Slider(serProp, 0, 1f);                 // Turn float field into slider

                    createBorder();                                         // End of 'float' group, create border
                    break;
                default:
                    EditorGUILayout.PropertyField(serProp);                 // Display properties default field
                    break;
            }
        } while (serProp.NextVisible(false));


        serializedObject.ApplyModifiedProperties();
    }


    void createBorder()
    {
        EditorGUILayout.Separator();
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        GUILayout.Label("______________________________");
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
        createSpace();
    }

    void createBorderTitle(string s)
    {
        createSpace();
        GUILayout.Label(s);
    }

    void createSpace()
    {
        EditorGUILayout.Space();
        EditorGUILayout.Space();
    }


}
