using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(Enemy))]
public class CustomEnemy : CustomEditorAPI
{
    // ::: Enemy Specific Vars :::

    Enemy enemy;
    static int minMaxHP = 1, maxMaxHP = 100;

    // ======----======
    
    public CustomEnemy(GameObject obj)
    {
        OnCustomInspectorGUI(obj);
    }

    void OnEnable()
    {
        enemy = (Enemy)target;
    }

    /// <summary>
    /// The Custom Inspector you will see in the 'Inspector' window
    /// </summary>
    public override void OnInspectorGUI()
    {
        EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();

        enemy.maxHP = (int)EditorGUILayout.Slider((float)enemy.maxHP, minMaxHP, maxMaxHP);
        enemy.currentHP = (int)EditorGUILayout.Slider((float)enemy.currentHP, 0, enemy.maxHP);

        GUILayout.FlexibleSpace();
        EditorGUILayout.EndHorizontal();
        
    }

    /// <summary>
    /// The Custom Inspector you will see in a custom location
    /// in which this static function is called.
    /// (IE: In MultiObjectWindow.cs)
    /// </summary>
    /// <param name="obj">GameObject with the script Enemy attached</param>
    public static void OnCustomInspectorGUI(GameObject obj)
    {
        Enemy e = obj.GetComponent<Enemy>();
        // Produce error or e = null ?
        if (e == null)
        {
            CustomAPI.ErrorGUI(obj, typeof(Enemy));
        }
        else
        {

            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();

            addCVX(e);

            e.maxHP     = (int)EditorGUILayout.Knob(new Vector2(25, 25), (float)e.maxHP, minMaxHP, maxMaxHP, "maxHP", 
                                                    Color.white, Color.cyan, true, GUILayout.MaxWidth(75));
            if (e.currentHP > e.maxHP) e.currentHP = e.maxHP;
            e.currentHP = (int)EditorGUILayout.Knob(new Vector2(25, 25), (float)e.currentHP, 0, e.maxHP, "curHP", 
                                                    Color.white, Color.cyan, true, GUILayout.MaxWidth(75));


            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();
        }
        
    }
}
