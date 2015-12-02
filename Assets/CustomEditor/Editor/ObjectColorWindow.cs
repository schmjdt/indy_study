
using UnityEditor;
using UnityEngine;
using System;
using System.Collections.Generic;

public class ObjectColorWindow : EditorWindow
{

    string status = "Make Selections";
    bool showPosition = true;
    bool showOptions = false;
    bool togShared = false;
    bool togOne = true;

    Vector2 selectionScrollPos;

    int[] selectionIDs;    
    static string nameStr;
    

    [MenuItem("Window/Object Color's %#C")]
    static void Init()
    {
        updateSaveString();

        // get the window, show it, and give it focus
        var window = GetWindow<ObjectColorWindow>("Object Color's");
        window.Show();
        window.Focus();
    }
    

    void OnSelectionChange()
    {
        // Update class variable with currently selected id's
        selectionIDs = Selection.instanceIDs;

        // Reset additional options upon selection change
        togShared = false;
        showOptions = false;

        // Update Foldout Vars
        updateSelectionFoldout();

        // Re-Draws the Inspector Window
        Repaint();  
    }


    void OnInspectorUpdate()
    {
        // This will only get called 10 times per second.
        Repaint();
    }

    void OnGUI()
    {
        if (selectionIDs == null) selectionIDs = Selection.instanceIDs;

        GUILayout.Label(Selection.objects.Length + " Object's Selected\n");

        #region Object List

        showPosition = EditorGUILayout.Foldout(showPosition, status);
        if (selectionIDs.Length > 0 && showPosition)
        {

            selectionScrollPos = EditorGUILayout.BeginScrollView(selectionScrollPos, GUILayout.MaxHeight(100));
            
            // Addition Options for 2 or more selected objects
            if (selectionIDs.Length > 1)
            {
                showOptions = EditorGUILayout.Foldout(showOptions, "Additional Options");
                if (showOptions)
                {
                    togShared = GUILayout.Toggle(togShared, "   Edit Shared Renderer?");
                }
            }

            foreach (int i in selectionIDs)
            {
                UnityEngine.Object nO = EditorUtility.InstanceIDToObject(i);
                Renderer oR = getObjectRenderer(nO);

                // Only show objects that have a material
                if (oR != null)
                {
                    EditorGUILayout.BeginHorizontal();
                    GUILayout.Label(nO.name, GUILayout.MinWidth(100));


                    if (GUILayout.Button("Rnd"))
                    {
                        oR.material.color = new Color(UnityEngine.Random.Range(0f, 1f),
                                                      UnityEngine.Random.Range(0f, 1f),
                                                      UnityEngine.Random.Range(0f, 1f),
                                                      1);
                        if (togShared)
                        {
                            updateColors(oR.material.color);
                        }
                    }

                    Color tmpColor = oR.material.color;
                    ((GameObject)nO).GetComponent<Renderer>().material.color = EditorGUILayout.ColorField(
                                                                                oR.material.color,
                                                                                GUILayout.MaxWidth(50));
                    if (oR.material.color != tmpColor)
                    {
                        if (togShared)
                        {
                            updateColors(oR.material.color);
                        }
                    }

                    EditorGUILayout.Space();

                    bool tog = oR.enabled;
                    tog = GUILayout.Toggle(tog, "   Visible?");
                    if (oR.enabled != tog)
                    {
                        oR.enabled = tog;
                        if (togShared)
                        {
                            updateVisible(tog);
                        }
                    }
                    
                    GUILayout.FlexibleSpace();
                    EditorGUILayout.EndHorizontal();

                }

            }

            EditorGUILayout.EndScrollView();

        }

        #endregion

        #region Selection Save/Load
        // http://docs.unity3d.com/ScriptReference/EditorWindow.OnSelectionChange.html


        EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();

        GUILayout.Label("Selection: ");

        if (GUILayout.Button("Load")) { LoadLastSavedSelection(); }

        if (selectionIDs != null && selectionIDs.Length > 0)
        {
            if (GUILayout.Button("Save")) { SaveSelection(); }
            if (GUILayout.Button("None")) { Selection.instanceIDs = new int[0]; }
        }

        GUILayout.FlexibleSpace();
        EditorGUILayout.EndHorizontal();


        EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();

        GUILayout.Label("Saved Selection: " + nameStr);

        GUILayout.FlexibleSpace();
        EditorGUILayout.EndHorizontal();


        #endregion

    }

    void updateSelectionFoldout()
    {
        if (selectionIDs != null && selectionIDs.Length > 0)
        {
            showPosition = true;
            status = "Update Material's Color";
        }
        else
        {
            showPosition = false;
            status = "Make Selections";
        }
    }

    /// <summary>
    /// Get's a List of all of the Selected Object's Renderer's (if has it)
    /// </summary>
    /// <returns></returns>
    List<Renderer> getSelectedRenderers()
    {
        List<Renderer> r = new List<Renderer>();

        foreach (int i in selectionIDs)
        {
            UnityEngine.Object nO = EditorUtility.InstanceIDToObject(i);
            Renderer oR = getObjectRenderer(nO);
            if (oR != null)
                r.Add(oR);
        }
        return r;
    }

    /// <summary>
    /// Return's a Single Object's Renderer (if has it)
    /// </summary>
    /// <param name="o">An Unity Object</param>
    /// <returns>The passed Object's Renderer, if available</returns>
    Renderer getObjectRenderer(UnityEngine.Object o)
    {
        Renderer r = null;
        try
        {
            r = ((GameObject)o).GetComponent<Renderer>();
        }
        catch (InvalidCastException e)
        {
            // e is not a GameObject
        }
        return r;
    }

    #region Multiple Selection Editing

    void updateColors(Color sharedColor)
    {
        foreach (Renderer oR in getSelectedRenderers())
        {
            oR.material.color = sharedColor;
        }
    }

    void updateVisible(bool sharedTog)
    {
        foreach (Renderer oR in getSelectedRenderers())
        {
            oR.enabled = sharedTog;
        }
    }

    #endregion

    #region Selection Save/Load Methods

    void SaveSelection()
    {
        string saveStr = "";
        nameStr = "";
        foreach (int i in selectionIDs)
        {
            saveStr += i.ToString() + ";";
            nameStr += " " + EditorUtility.InstanceIDToObject(i).name + ",";
        }
        saveStr = saveStr.TrimEnd(char.Parse(";"));
        nameStr = nameStr.TrimEnd(char.Parse(","));
        EditorPrefs.SetString("SelectedIDs", saveStr);
    }

    void LoadLastSavedSelection()
    {
        int[] ids = GetSavedIDs();
        Selection.instanceIDs = ids;
    }

    
    static int[] GetSavedIDs()
    {
        string[] strIDs = EditorPrefs.GetString("SelectedIDs").Split(char.Parse(";"));
        
        List<int> ids = new List<int>();
        for (var i = 0; i < strIDs.Length; i++)
        {
            int id = int.Parse(strIDs[i]);
            try {
                GameObject.Find(EditorUtility.InstanceIDToObject(id).name);
                ids.Add(id);
            } catch (NullReferenceException e)
            {
                // e no longer exists
            }
        }

        return ids.ToArray();
    }

    static void updateSaveString()
    {
        nameStr = "";
        foreach (int i in GetSavedIDs())
        {
            nameStr += " " + EditorUtility.InstanceIDToObject(i).name + ",";
        }
        nameStr = nameStr.TrimEnd(char.Parse(","));
    }



    #endregion
}