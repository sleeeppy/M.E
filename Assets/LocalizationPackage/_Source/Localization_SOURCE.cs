using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Main language localization source component per-scene.
/// Written by Matej Vanco in 2018. Updated in 2022.
/// https://matejvanco.com
/// </summary>
[AddComponentMenu("Matej Vanco/Language Localization/Language Localization")]
public class Localization_SOURCE : MonoBehaviour 
{
    //Values are editable-----------------
    public static string GENERAL_DelimiterSymbol_Category = ">"; //This delimiter corresponds to custom categories
    public static string GENERAL_DelimiterSymbol_Key = "="; //This delimiter splits key & it's content in text
    public static string GENERAL_NewLineSymbol = "/l"; //This delimiter splits new lines
    //------------------------------------------

    public TextAsset[] languageFiles;
    public int selectedLanguage = 0;

    public bool loadLanguageOnStart = true;

    /// <summary>
    /// Use this action inside your code in case of language load
    /// </summary>
    public Action Action_LanguageLoaded;

    public List<string> Categories = new List<string>();

	[Serializable]
    public class LocalizationSelector
    {
        public enum AssignationType : int {None, GameObjectChild, LocalizationComponent, SpecificTextObjects};
        /// <summary>
        /// Assignation type helps you to automatize text translation that indicates to the specific physical or abstract object
        /// </summary>
        public AssignationType assignationType;

        //Essentials
        public string Key;
        public string Text;
        public int Category;

        //AT - GameObjectChild
        public bool AT_FindChildByKeyName = true;
        public string AT_ChildName;
        public bool AT_UseGeneralChildsRootObject = true;
        public Transform AT_CustomChildsRootObject;

        //AT - Allowed Types
        public bool AT_UITextComponentAllowed = true;
        public bool AT_TextMeshComponentAllowed = true;
        public bool AT_TextMeshProComponentAllowed = true;

        //AT - Available Objects
        public List<GameObject> AT_FoundObjects = new List<GameObject>();

        //AT - Specific_UIText
        public Text[] AT_UITextObject;
        //AT - Specific_TextMesh
        public TextMesh[] AT_TextMeshObject;
        //AT - Specific_TextMeshPro
        public TextMeshProUGUI[] AT_TextMeshProObject;
    }
    public List<LocalizationSelector> localizationSelector = new List<LocalizationSelector>();
    public Transform gameObjectChildsRoot;

    public int selectedCategory = 0;

    /// <summary>
    /// Quick actions allow user to manipulate with exist keys much faster!
    /// </summary>
    [Serializable]
    public class QuickActions
    {
        public LocalizationSelector.AssignationType assignationType;
        [Tooltip("If assignation type is set to GameObjectChild & the bool is set to True, the target text will be searched in the global Childs root object")] 
        public bool useGeneralChildsRoot = true;
        [Space]
        [Tooltip("Allow UIText component?")] public bool UITextAllowed = true;
        [Tooltip("Allow TextMesh component?")] public bool TextMeshAllowed = true;
        [Tooltip("Allow TextMeshPro component?")] public bool TextMeshProAllowed = true;
        [Space]
        [Tooltip("New specific UI Texts")] public Text[] SpecificUITexts;
        [Tooltip("New specific Text Meshes")] public TextMesh[] SpecificTextMeshes;
        [Tooltip("New specific Text Pro Meshes")] public TextMeshProUGUI[] SpecificTextProMeshes;
        [Tooltip("If enabled, the object fields above will be cleared if the QuickActions are applied to key in specific category")] public bool ClearAllPreviousTargets = true;
    }
    /// <summary>
    /// Quick actions allow user to manipulate with exist keys much faster!
    /// </summary>
    public QuickActions quickActions;

    private void Awake()
    {
        if (!Application.isPlaying) 
            return;

        Lang_RefreshKeyAssignations();
        if (loadLanguageOnStart)
            Lang_LoadLanguage(selectedLanguage);
    }

    #region INTERNAL METHODS

#if UNITY_EDITOR

    internal void Internal_RefreshInternalLocalization()
    {
        Categories.Clear();
        Categories.AddRange(Localization_SOURCE_Window.locAvailableCategories);
    }

    internal void Internal_AddKey(string KeyName)
    {
        foreach(Localization_SOURCE_Window.LocalizationElemenets a in Localization_SOURCE_Window.localizationElements)
        {
            if(a.Key == KeyName)
            {
                localizationSelector.Add(new LocalizationSelector() { Key = a.Key, Text = a.Text, Category = a.Category });
                return;
            }
        }
    }

#endif

    private string Internal_ConvertAndReturnText(LocalizationSelector lSelector, string[] lines)
    {
        if (lines.Length > 1)
        {
            List<string> storedFilelines = new List<string>();
            for (int i = 1; i < lines.Length; i++)
                storedFilelines.Add(lines[i]);

            foreach (string categories in Categories)
            {
                if (Internal_GetLocalizationCategory(categories) == lSelector.Category)
                {
                    foreach (string s in storedFilelines)
                    {
                        if (string.IsNullOrEmpty(s))
                            continue;
                        if (s.StartsWith(GENERAL_DelimiterSymbol_Category))
                            continue;
                        int del = s.IndexOf(GENERAL_DelimiterSymbol_Key);
                        if (del == 0 || del > s.Length)     continue;

                        string Key = s.Substring(0,del);

                        if (string.IsNullOrEmpty(Key))      continue;
                        if (Key == lSelector.Key)
                        {
                            if (s.Length < Key.Length + 1)
                                continue;
                            lSelector.Text = s.Substring(Key.Length + 1, s.Length - Key.Length - 1);
                            return lSelector.Text;
                        }
                    }
                }
            }
        }
        return "";
    }

    internal int Internal_GetLocalizationCategory(string entry)
    {
        int c = 0;
        foreach (string categ in Categories)
        {
            if (categ == entry) return c;
            c++;
        }
        return 0;
    }

    #endregion

    /// <summary>
    /// Refresh all resource objects by the selected options
    /// </summary>
    public void Lang_RefreshKeyAssignations()
    {
        foreach (LocalizationSelector sel in localizationSelector)
        {
            switch (sel.assignationType)
            {
                case LocalizationSelector.AssignationType.GameObjectChild:
                    string childName = sel.AT_ChildName;
                    if (sel.AT_FindChildByKeyName)
                        childName = sel.Key;

                    sel.AT_FoundObjects.Clear();
                    Transform root = sel.AT_UseGeneralChildsRootObject ? gameObjectChildsRoot : sel.AT_CustomChildsRootObject;
                    if(!root)
                    {
                        Debug.LogError("Localization: The key '" + sel.Key + "' should have been assigned to specific childs by it's key name, but the root object is empty");
                        return;
                    }

                    foreach (Transform t in root.GetComponentsInChildren<Transform>(true))
                    {
                        if (t.name != childName) continue;

                        if (sel.AT_UITextComponentAllowed && t.GetComponent<Text>())
                            sel.AT_FoundObjects.Add(t.gameObject);
                        if (sel.AT_TextMeshComponentAllowed && t.GetComponent<TextMesh>())
                            sel.AT_FoundObjects.Add(t.gameObject);
                        if (sel.AT_TextMeshProComponentAllowed && t.GetComponent<TextMeshProUGUI>())
                            sel.AT_FoundObjects.Add(t.gameObject);
                    }
                    break;

                case LocalizationSelector.AssignationType.LocalizationComponent:
                    sel.AT_FoundObjects.Clear();
                    foreach (Localization_KEY k in FindObjectsOfType<Localization_KEY>())
                    {
                        if(k.KeyID == sel.Key) sel.AT_FoundObjects.Add(k.gameObject);
                    }
                    break;
            }
            if (sel.assignationType == LocalizationSelector.AssignationType.GameObjectChild || sel.assignationType == LocalizationSelector.AssignationType.LocalizationComponent)
            {
                if (sel.AT_FoundObjects.Count == 0)
                    Debug.Log("Localization: The key '" + sel.Key + "' couldn't find any child objects");
            }
        }
    }

    /// <summary>
    ///  Load language database by the selected language index
    /// </summary>
    public void Lang_LoadLanguage(int languageIndex)
    {
        if (languageFiles.Length <= languageIndex)
        {
            Debug.LogError("Localization: The index for language selection is incorrect! Languages count: " + languageFiles.Length + ", Your index: " + languageIndex);
            return;
        }
        else if (languageFiles[languageIndex] == null)
        {
            Debug.LogError("Localization: The language that you've selected is empty!");
            return;
        }

        foreach (LocalizationSelector sel in localizationSelector)
        {
            sel.Text = Internal_ConvertAndReturnText(sel, languageFiles[languageIndex].text.Split('\n')).Replace(GENERAL_NewLineSymbol, System.Environment.NewLine);

            switch(sel.assignationType)
            {
                case LocalizationSelector.AssignationType.LocalizationComponent:
                case LocalizationSelector.AssignationType.GameObjectChild:
                    foreach (GameObject gm in sel.AT_FoundObjects)
                    {
                        if (gm.GetComponent<Text>() && sel.AT_UITextComponentAllowed)
                            gm.GetComponent<Text>().text = sel.Text;
                        else if (gm.GetComponent<TextMesh>() && sel.AT_TextMeshComponentAllowed)
                            gm.GetComponent<TextMesh>().text = sel.Text;
                        else if (gm.GetComponent<TextMeshProUGUI>() && sel.AT_TextMeshProComponentAllowed)
                            gm.GetComponent<TextMeshProUGUI>().text = sel.Text;
                    }
                    break;

                case LocalizationSelector.AssignationType.SpecificTextObjects:
                    foreach (TextMesh t in sel.AT_TextMeshObject)
                    {
                        if (t == null) continue;
                        t.text = sel.Text;
                    }
                    foreach (Text t in sel.AT_UITextObject)
                    {
                        if (t == null) continue;
                        t.text = sel.Text;
                    }
                    foreach (TextMeshProUGUI t in sel.AT_TextMeshProObject)
                    {
                        if (t == null) continue;
                        t.text = sel.Text;
                    }
                    break;
            }
        }

        Action_LanguageLoaded?.Invoke();
    }

    /// <summary>
    /// Return existing text by the specific key input in the currently selected language
    /// </summary>
    public string Lang_ReturnText(string keyInput)
    {
        foreach(LocalizationSelector l in localizationSelector)
            if (l.Key == keyInput) return l.Text;
        Debug.Log("Localization: Key '" + keyInput + "' couldn't be found");
        return "";
    }

#if UNITY_EDITOR

    /// <summary>
    /// Load language from the selected index in the editor
    /// </summary>
    [ContextMenu("Load Language")]
    public void Lang_LoadLanguage()
    {
        Lang_RefreshKeyAssignations();
        Lang_LoadLanguage(selectedLanguage);
    }

#endif
}
