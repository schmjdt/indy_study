using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(Test))]
public class CustomTest : CustomEditorAPI
{
    static bool copied;

    public CustomTest(GameObject obj)
    {
        OnCustomInspectorGUI(obj);
    }

    void OnEnable()
    {
    }

    /// <summary>
    /// The Custom Inspector you will see in the 'Inspector' window
    /// </summary>
    public override void OnInspectorGUI()
    {
        GUILayout.Label("Hello World");
    }

    /// <summary>
    /// The Custom Inspector you will see in a custom location
    /// in which this static function is called.
    /// (IE: In MultiObjectWindow.cs)
    /// </summary>
    /// <param name="obj">GameObject with the script Enemy attached</param>
    public static void OnCustomInspectorGUI(GameObject obj)
    {

        Test comp = obj.GetComponent<Test>();
        // Produce error or e = null ?
        if (comp == null)
        {
            CustomAPI.ErrorGUI(obj, typeof(Test));
        }
        else
        {
            addCVX(comp);
            GUILayout.Label("Goodbye World");
        }
    }
}
