
using UnityEditor;
using UnityEngine;
using System;
using System.Collections.Generic;

public class MultiObjectWindow : EditorWindow
{
    /* *** 
        DEFAULT TAGS:
            Untagged, Respawn, Finish, EditorOnly, MainCamera, Player, GameController  [Total: 7]

            enemy, test_tag_1, test_tag_2, test_tag_3

       Custom Editor Scripts:
            Name Scheme Chosen and Followed (IE: CustomXxxx.cs => Editor script)
                Where 'Xxxx' is the custom tag name in 'xxxx' or 'Xxxx' format

                

       ***
     */

    // ComboBox vars 
    enum eLayers { PLAYER, ENEMY };
    eLayers layer;

    string[] layers = { "PLAYER", "ENEMY" };
    string[] layersCustom;
    int layerChoice = 0;
    // --------------

    float knobVal = 10f;

    Vector2 selectionScrollPos;


    [MenuItem("Tools/Multi-Object Edit")]
    static void Init()
    {
        // get the window, show it, and give it focus
        var window = GetWindow<MultiObjectWindow>("Multi-Object Edit");
        window.Show();
        window.Focus();

    }
    
    void OnInspectorUpdate()
    {
        // This will only get called 10 times per second.
        Repaint();
    }

    void OnGUI()
    {
        layersCustom = tagsCustom(tags()); // Get updated list of custom tags

        // FIRST TWO ComboBox (popup) are Here for Example Purposes ONLY
        EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();

        GUILayout.Label("ComboBox via ENUM");
        layer = (eLayers)EditorGUILayout.EnumPopup(layer);              // ComboBox via ENUM

        GUILayout.FlexibleSpace();
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();

        GUILayout.Label("ComboBox via STRING[]");
        layer = (eLayers)EditorGUILayout.Popup((int)layer, layers);     // ComboBox via STRING[]
        
        GUILayout.FlexibleSpace();
        EditorGUILayout.EndHorizontal();
        // -------------------------------------------------------------

        EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();

        GUILayout.Label("ComboBox via custom tags");
        layerChoice = EditorGUILayout.Popup(layerChoice, layersCustom);     // ComboBox via STRING[]/Tags

        GUILayout.FlexibleSpace();
        EditorGUILayout.EndHorizontal();
        
        drawOptions(layersCustom[layerChoice]); // Draw custom elements depending on selected tag
    }

    #region Draw Inspector
    
    void drawOptions(string tag)
    {
        GameObject[] gO = GameObject.FindGameObjectsWithTag(tag);

        EditorGUILayout.LabelField("Objects with tag: " + tag);
        foreach (GameObject item in gO)
        {
            EditorGUILayout.BeginHorizontal();

            //EditorGUILayout.LabelField(item.name);
            if (GUILayout.Button(item.name, GUILayout.MaxWidth(150)))
            {
                int[] id = { item.GetInstanceID() };
                Selection.instanceIDs = id;
            }

            drawCustomInspector(tag, item);
            //CustomAPI.drawCustomInspector(tag, item);

            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();

        }
    }


    void drawCustomInspector(string tag, GameObject obj)
    {
        string tagClass = "Custom" + char.ToUpper(tag[0]) + tag.Substring(1);


        switch (tagClass)
        {
            case "CustomEnemy":
                CustomEnemy.OnCustomInspectorGUI(obj);
                break;
            case "CustomTest":
                CustomTest.OnCustomInspectorGUI(obj);
                break;

        }

        /*  
        Possible Future Implementation -
            Pre-define the 'Custom' part like do now
            Convert String->Class So can call: "CustomTag".OnCustomInspectorGUI(obj)
                "OnCustomInspectorGUI" would be a Pre-defined method common to all Custom classes
                'obj' would be the GameObject passed into method where class type is found in component

        Error Checking -
            When pass obj to OnCustomInspectorGUI and try to 'find' component, it won't be found
                This means 1. Improper Tag on Object, 2. Script missing on Object
        */
    }

    #endregion


    #region Retrieve Tags

    string tags()
    {
        string[] tagList = UnityEditorInternal.InternalEditorUtility.tags;
        string tag = "";

        if (tagList.Length > 7)
        {
            for (int i = 7; i < tagList.Length; i++)
            {
                tag += tagList[i] + ",";
            }
        }

        return tag;
    }

    string[] tagsCustom(string tags)
    {
        return tags.Split(',');
    }

    #endregion



    /*
    void drawOptions(eLayers eL)
    {
        selectionScrollPos = EditorGUILayout.BeginScrollView(selectionScrollPos, GUILayout.MaxHeight(100));

        switch (eL)
        {
            case eLayers.PLAYER:
                drawPlayers();
                break;
            case eLayers.ENEMY:
                drawEnemys();
                break;
        }

        EditorGUILayout.EndScrollView();
    }


    void drawPlayers()
    {
        GameObject[] gO = GameObject.FindGameObjectsWithTag("Player");
        Player[] players = new Player[gO.Length];

        for (int i = 0; i < gO.Length; i++)
        {
            players[i] = gO[i].GetComponent<Player>();
        }

        foreach (Player item in players)
        {
            EditorGUILayout.LabelField(item.name);
        }

    }

    void drawEnemys()
    {
        GameObject[] gO = GameObject.FindGameObjectsWithTag("enemy");
        Enemy[] players = new Enemy[gO.Length];

        for (int i = 0; i < gO.Length; i++)
        {
            players[i] = gO[i].GetComponent<Enemy>();
        }

        foreach (Enemy item in players)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(item.name);

            item.maxHP = (int)EditorGUILayout.Knob(new Vector2(25, 25), (float)item.maxHP, 0, 100, "maxHP", Color.white, Color.cyan, true);
            if (item.currentHP > item.maxHP) item.currentHP = item.maxHP;
            item.currentHP = (int)EditorGUILayout.Knob(new Vector2(25, 25), (float)item.currentHP, 0, item.maxHP, "curHP", Color.white, Color.cyan, true);

            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();
            // LOAD ENEMY's CUSTOM EDITOR HERE?
        }
    }
    */
}
