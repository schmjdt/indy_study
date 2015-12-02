using UnityEngine;
using UnityEditor;
using System.Collections;

public class CustomEditorAPI : Editor
{
    protected SerializedProperty serProp;

    protected static bool copied;
    protected static int valCVX;

    public CustomEditorAPI()
    {
        OnCustomInspectorGUI(null);
    }

    void OnEnable()
    {
        // Anything want to start-up with ALL custom inspector
    }

    /// <summary>
    /// The Custom Inspector you will see in the 'Inspector' window
    /// </summary>
    public override void OnInspectorGUI()
    {
        // Override this function in the child
    }


    /// <summary>
    /// The Custom Inspector you will see in a custom location
    /// in which this static function is called.
    /// (IE: In MultiObjectWindow.cs)
    /// </summary>
    /// <param name="obj">GameObject with a particular Component attached</param>
    public static void OnCustomInspectorGUI(GameObject obj)
    {
        // Create this function in the child
    }

    /// <summary>
    /// Implement this method in your custom editor script too add these features:
    /// Copy - Paste - Cut Script Buttons
    /// </summary>
    /// <param name="comp"></param>
    protected static void addCVX(Component comp)
    {
        valCVX = CustomAPI.CVX(copied, comp);

        if (valCVX < 0)         DestroyImmediate(comp);
        else if (valCVX > 0)    copied = true;
        else                    copied = false;
    }
}
