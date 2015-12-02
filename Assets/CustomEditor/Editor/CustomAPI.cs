using UnityEngine;
using UnityEditor;
using System.Collections;
using System;

public static class CustomAPI
{
    static Type[] scripts = { typeof(Enemy), typeof(Test) };
    static Type[] customs = { typeof(CustomEnemy), typeof(CustomTest) };

    public static int CVX(bool copied, Component comp)
    {
        int rBool = copied ?  1 : 0;
        EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();

        if (GUILayout.Button("C"))
        {
            rBool = UnityEditorInternal.ComponentUtility.CopyComponent(comp) ? 1 : 0;
        }
        if (GUILayout.Button("V"))
        {
            if (rBool == 1)
                UnityEditorInternal.ComponentUtility.PasteComponentValues(comp);
        }

        if (GUILayout.Button("X"))
        {
            //DestroyImmediate(comp);  (handled in CustomEditorAPI)
            rBool = -1; 
        }
        GUILayout.FlexibleSpace();
        EditorGUILayout.EndHorizontal();

        return rBool;
    }
    
    public static void ErrorGUI(GameObject obj, System.Type type)
    {

        // The GameObject obj does not contain the component type Enemy
        //  1. Forgotten - Add Script 2. Improper object passed - Remove Tag
        if (GUILayout.Button("Add script"))
        {
            obj.AddComponent(type);
        }
        if (GUILayout.Button("Remove tag"))
        {
            obj.tag = "Untagged";
        }
        EditorGUILayout.LabelField("Proper script missing");
    }
    /*
    public static void drawCustomInspector(string tag, GameObject obj)
    {
        string tagClass = "Custom" + char.ToUpper(tag[0]) + tag.Substring(1);

        for (int i = 0; i < scripts.Length; i++)
        {
            if (customs[i])
        }

        foreach (Type item in customs)
        {
            if (item == obj.GetType())
                item.OnInspector
        }
        
        

        /*  
        Possible Future Implementation -
            Pre-define the 'Custom' part like do now
            Convert String->Class So can call: "CustomTag".OnCustomInspectorGUI(obj)
                "OnCustomInspectorGUI" would be a Pre-defined method common to all Custom classes
                'obj' would be the GameObject passed into method where class type is found in component
                
        */
    //}
}