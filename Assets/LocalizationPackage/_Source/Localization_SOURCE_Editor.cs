#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

/// <summary>
/// Main language localization source editor
/// Written by Matej Vanco in 2018. Updated in 2022.
/// https://matejvanco.com
/// </summary>
[CustomEditor(typeof(Localization_SOURCE))]
[CanEditMultipleObjects]
public class Localization_SOURCE_Editor : Editor
{
    private SerializedProperty LanguageFiles, SelectedLanguage;
    private SerializedProperty LoadLanguageOnStart;

    private SerializedProperty LocalizationSelector;

    private SerializedProperty AT_GameObjectChildsRoot;

    private SerializedProperty quickActions;

    private Localization_SOURCE l;
    private bool addKey = false;
    private bool categorySelected = false;
    private int category = 0;

    private string[] categories;

    private void OnEnable()
    {
        l = (Localization_SOURCE)target;

        LanguageFiles = serializedObject.FindProperty("languageFiles");
        SelectedLanguage = serializedObject.FindProperty("selectedLanguage");
        LoadLanguageOnStart = serializedObject.FindProperty("loadLanguageOnStart");
        LocalizationSelector = serializedObject.FindProperty("localizationSelector");
        AT_GameObjectChildsRoot = serializedObject.FindProperty("gameObjectChildsRoot");
        quickActions = serializedObject.FindProperty("quickActions");

        categories = new string[l.Categories.Count + 1];
        categories[0] = "All";
        for (int i = 0; i < l.Categories.Count; i++)
            categories[i + 1] = l.Categories[i];
    }

    public override void OnInspectorGUI()
    {
        if (target == null) return;
        serializedObject.Update();

        PS();

        PV();
        PP(LanguageFiles, "Language Files", "", true);
        PP(SelectedLanguage, "Selected Language", "Currently selected language for the localization");
        PS(5);
        PP(LoadLanguageOnStart, "Load Language On Start", "Load localization after program startup");
        PVE();

        PS(15);

        PV();
        if (PB("Add Key"))
        {
            Localization_SOURCE_Window.Init();
            addKey = true;
            l.Internal_RefreshInternalLocalization();
        }

        if(addKey)
        {
            PV();
            if(PB("X",GUILayout.Width(40)))
            {
                addKey = false;
                categorySelected = false;
                category = 0;
                return;
            }
            PLE("From Category:");
            PV();
            for (int i = 0; i < l.Categories.Count; i++)
            {
                if (PB(l.Categories[i]))
                {
                    categorySelected = true;
                    category = i;
                    return;
                }
            }
            PVE();
            if(categorySelected)
            {
                PLE("Key:");
                if (PB("Add All",GUILayout.Width(120)))
                {
                    for (int i = 0; i < Localization_SOURCE_Window.localizationElements.Count; i++)
                    {
                        if (Localization_SOURCE_Window.localizationElements[i].Category != category)
                            continue;
                        l.Internal_AddKey(Localization_SOURCE_Window.localizationElements[i].Key);
                    }
                    addKey = false;
                    categorySelected = false;
                    category = 0;
                    return;
                }
                PV();
                for (int i = 0; i < Localization_SOURCE_Window.localizationElements.Count; i++)
                {
                    if (Localization_SOURCE_Window.localizationElements[i].Category != category)
                        continue;
                    bool passed = true;
                    foreach(Localization_SOURCE.LocalizationSelector sel in l.localizationSelector)
                    {
                        if(sel.Key == Localization_SOURCE_Window.localizationElements[i].Key)
                        {
                            passed = false;
                            break;
                        } 
                    }
                    if (!passed)
                        continue;
                    if (PB(Localization_SOURCE_Window.localizationElements[i].Key))
                    {
                        l.Internal_AddKey(Localization_SOURCE_Window.localizationElements[i].Key);
                        addKey = false;
                        categorySelected = false;
                        category = 0;
                        return;
                    }
                }
                PVE();
            }
            PVE();
        }
        PVE();

        PS(15);

        PV();
        l.selectedCategory = EditorGUILayout.Popup(new GUIContent("Filter Category: "), l.selectedCategory, categories);
        PV();
        PP(quickActions, "Quick Actions", "Edit keys with quick actions", true);
        if(serializedObject.FindProperty("quickActions").isExpanded)
        {
            if(PB($"Apply To All Keys in '{categories[l.selectedCategory]}' Category"))
            {
                foreach(Localization_SOURCE.LocalizationSelector sel in l.localizationSelector)
                {
                    if (l.selectedCategory != 0 && sel.Category != l.selectedCategory - 1) continue;

                    sel.assignationType = l.quickActions.assignationType;
                    sel.AT_UseGeneralChildsRootObject = l.quickActions.useGeneralChildsRoot;
                    sel.AT_UITextComponentAllowed = l.quickActions.UITextAllowed;
                    sel.AT_TextMeshComponentAllowed = l.quickActions.TextMeshAllowed;
                    sel.AT_TextMeshProComponentAllowed = l.quickActions.TextMeshProAllowed;

                    if (l.quickActions.ClearAllPreviousTargets)
                    {
                        sel.AT_TextMeshObject = null;
                        sel.AT_UITextObject = null;
                        sel.AT_TextMeshProObject = null;
                    }

                    if (l.quickActions.SpecificUITexts.Length > 0)
                        sel.AT_UITextObject = l.quickActions.SpecificUITexts;
                    if (l.quickActions.SpecificTextMeshes.Length > 0)
                        sel.AT_TextMeshObject = l.quickActions.SpecificTextMeshes;
                    if (l.quickActions.SpecificTextProMeshes.Length > 0)
                        sel.AT_TextMeshProObject = l.quickActions.SpecificTextProMeshes;
                }
            }
        }
        PVE();
        PS();

        PV();
        PP(AT_GameObjectChildsRoot, "GameObject Childs Root", "Starting root for keys containing 'GameObjectChild' assignation type");
        if (l.localizationSelector.Count != 0) DrawPropertyList();
        PVE();
        PVE();
    }

    private void DrawPropertyList()
    {
        for (int i = 0; i < l.localizationSelector.Count; i++)
        {
            if (l.selectedCategory != 0 && l.localizationSelector[i].Category != l.selectedCategory - 1) continue;

            PV();

            SerializedProperty item = LocalizationSelector.GetArrayElementAtIndex(i);

            PH(false);
            PP(item, l.localizationSelector[i].Key);
            if (PB("X", GUILayout.Width(40)))
            {
                l.localizationSelector.RemoveAt(i);
                PVE();
                return;
            }
            PHE();

            if (!item.isExpanded)
            {
                PVE();
                continue;
            }

            Localization_SOURCE.LocalizationSelector sec = l.localizationSelector[i];

            PS(5);

            EditorGUI.indentLevel += 1;

            PH();
            PLE("Key: "+ sec.Key, true);
            string cat = categories[0];
            if (!(sec.Category >= categories.Length))
                cat = categories[sec.Category];
            PL("Category: " + cat);
            PHE();

            PS();

            PP(item.FindPropertyRelative("assignationType"), "Assignation Type");

            switch (sec.assignationType)
            {
                case Localization_SOURCE.LocalizationSelector.AssignationType.GameObjectChild:
                    PV();
                    PP(item.FindPropertyRelative("AT_FindChildByKeyName"), "Find Child By Key Name", "If enabled, the system will find the child of the selected component type [below] by the key name");
                    if (!sec.AT_FindChildByKeyName)
                        PP(item.FindPropertyRelative("AT_ChildName"), "Child Name");
                    else
                        EditorGUILayout.HelpBox("Object with name '"+sec.Key+"' should exist in the scene", MessageType.None);
                    PVE();
                    PS(3);

                    PV();
                    PP(item.FindPropertyRelative("AT_UseGeneralChildsRootObject"), "Use General Childs Root Object");
                    if(!sec.AT_UseGeneralChildsRootObject)
                        PP(item.FindPropertyRelative("AT_CustomChildsRootObject"), "Custom Childs Root Object");
                    PVE();

                    PS(3);

                    PV();
                    PP(item.FindPropertyRelative("AT_UITextComponentAllowed"), "UIText Component Allowed", "If disabled, objects with UI Text component will be ignored");
                    PP(item.FindPropertyRelative("AT_TextMeshComponentAllowed"), "TextMesh Component Allowed", "If disabled, objects with Text Mesh component will be ignored");
                    PP(item.FindPropertyRelative("AT_TextMeshProComponentAllowed"), "TextMeshPro [UGUI] Component Allowed", "If disabled, objects with Text Mesh Pro UGUI component will be ignored");

                    PVE();
                    break;

                case Localization_SOURCE.LocalizationSelector.AssignationType.LocalizationComponent:
                    PV();
                    EditorGUILayout.HelpBox("Object with Localization_KEY component should have an ID '" + sec.Key + "'", MessageType.None);
                    PVE();
                    PS(3);

                    PV();
                    PP(item.FindPropertyRelative("AT_UITextComponentAllowed"), "UIText Component Allowed", "If disabled, objects with UI Text component will be ignored");
                    PP(item.FindPropertyRelative("AT_TextMeshComponentAllowed"), "TextMesh Component Allowed", "If disabled, objects with Text Mesh component will be ignored");
                    PP(item.FindPropertyRelative("AT_TextMeshProComponentAllowed"), "TextMeshPro [UGUI] Component Allowed", "If disabled, objects with Text Mesh Pro component will be ignored");
                    PVE();
                    break;

                case Localization_SOURCE.LocalizationSelector.AssignationType.SpecificTextObjects:
                    PV();
                    EditorGUILayout.HelpBox("Assign specific text objects manually", MessageType.None);
                    PVE();
                    PS(3);
                    PP(item.FindPropertyRelative("AT_UITextObject"), "Specific UI Text", "Assign specific UI Text objects", true);
                    PP(item.FindPropertyRelative("AT_TextMeshObject"), "Specific Text Mesh", "Assign specific Text Mesh objects", true);
                    PP(item.FindPropertyRelative("AT_TextMeshProObject"), "Specific Text Mesh Pro [UGUI]", "Assign specific Text Mesh Pro UGUI objects", true);
                    break;

                case Localization_SOURCE.LocalizationSelector.AssignationType.None:
                    PV();
                    EditorGUILayout.HelpBox("The key exists in the scene, but nothing will happen. You can use this key inside the code", MessageType.None);
                    PVE();
                    break;
            }
           
            EditorGUI.indentLevel -= 1;
            PVE();
        }
    }

    #region Layout Shortcuts

    private void PL(string text)
    {
        GUILayout.Label(text);
    }

    private void PLE(string text, bool bold = false)
    {
        if (bold)
        {
            string add = "<b>";
            add += text + "</b>";
            text = add;
        }
        GUIStyle style = new GUIStyle();
        style.richText = true;
        style.normal.textColor = Color.white;
        EditorGUILayout.LabelField(text, style);
    }

    private void PS(float space = 10)
    {
        GUILayout.Space(space);
    }

    private void PP(SerializedProperty p, string Text, string ToolTip = "", bool includeChilds = false)
    {
        EditorGUILayout.PropertyField(p, new GUIContent(Text, ToolTip), includeChilds);
        serializedObject.ApplyModifiedProperties();
    }

    private void PV()
    {
        GUILayout.BeginVertical("Box");
    }

    private void PVE()
    {
        GUILayout.EndVertical();
    }

    private void PH(bool box = true)
    {
        if (box)
            GUILayout.BeginHorizontal("Box");
        else
            GUILayout.BeginHorizontal();
    }

    private void PHE()
    {
        GUILayout.EndHorizontal();
    }

    private bool PB(string mess, GUILayoutOption opt = null)
    {
        if(opt == null)     return GUILayout.Button(mess);
        else                return GUILayout.Button(mess, opt);
    }

    #endregion
}
#endif