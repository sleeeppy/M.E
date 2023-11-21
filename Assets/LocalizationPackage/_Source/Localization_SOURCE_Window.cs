#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using System.IO;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// Main language localization window editor.
/// Written by Matej Vanco in 2018. Updated in 2022.
/// https://matejvanco.com
/// </summary>
public class Localization_SOURCE_Window : EditorWindow 
{
    //---Input-Output Settings & Registry
    private static string locSelectedPath;
    private static readonly string locRegistryKey = "LOCATION_MANAGER_LocManagPath";
    //Main heading format [will be written on top of every language file] - you can customize it
    private static readonly string locHeadingFormat = "Localization_Manager_Source";

    //---Essential Variables - Language file, Category, Localization
    public static string locCurrentLanguage;
    //Main language localization file selected
    private static bool locManagerSelected = true;
    //Category settings
    private static int locCategorySelected = 0;
    public static List<string> locAvailableCategories = new List<string>();
    private static string locSelectedCategoryName;

    public class LocalizationElemenets
    {
        public string Key;
        [Multiline] public string Text;
        public int Category;
    }
    public static List<LocalizationElemenets> localizationElements = new List<LocalizationElemenets>();

    public static bool loc_WindowInitialized = false;
    private static bool loc_ReadySteady = false;

    /// <summary>
    /// Initialize & Refresh language localization system
    /// </summary>
    public static void Init()
    {
        if (loc_WindowInitialized)
            return;

        loc_WindowInitialized = true;

        localizationElements.Clear();
        Loc_GetLocalizationManagerPath();
    }

    [MenuItem("Window/Localization Manager")]
    public static void InitWindow()
    {
        Localization_SOURCE_Window win = new Localization_SOURCE_Window();
        win.minSize = new Vector2(400, 250);
        win.titleContent = new GUIContent("Localization Editor");
        win.Show();

        localizationElements.Clear();
        Loc_GetLocalizationManagerPath();
    }


    #region Data Managing

    /// <summary>
    /// Returns main Language Localization manager path (if possible & exists)
    /// </summary>
    public static void Loc_GetLocalizationManagerPath()
    {
        locCurrentLanguage = "";

        locSelectedPath = PlayerPrefs.GetString(locRegistryKey);
        loc_ReadySteady = !string.IsNullOrEmpty(locSelectedPath);

        if (!loc_ReadySteady) return; 

        loc_ReadySteady = true;
        locManagerSelected = true;

        Loc_RefreshContent();
    }

    private static void Loc_SelectLanguageFile()
    {
        Loc_SaveDatabase(locSelectedPath, false);

        string f = EditorUtility.OpenFilePanel("Select Language File Path", Application.dataPath, "xml");

        if (string.IsNullOrEmpty(f)) return;

        locSelectedPath = f;
        locManagerSelected = false;

        locCurrentLanguage = Path.GetFileNameWithoutExtension(locSelectedPath);

        Loc_LoadDatabase(locSelectedPath, false);
    }

    private static void Loc_CreateLanguageFile()
    {
        Loc_SaveDatabase(locSelectedPath, false);

        string f = EditorUtility.SaveFilePanel("Create Language File", Application.dataPath, "English" , "xml");

        if (string.IsNullOrEmpty(f)) return;

        File.Create(f).Dispose();

        locSelectedPath = f;
        locManagerSelected = false;

        locCurrentLanguage = Path.GetFileNameWithoutExtension(locSelectedPath);

        Loc_LoadDatabase(locSelectedPath, false);

        AssetDatabase.Refresh();
    }

    /// <summary>
    /// Refresh overall Language Localization content (current keys, language etc)
    /// </summary>
    public static void Loc_RefreshContent()
    {
        locAvailableCategories.Clear();
        locAvailableCategories.Add("Default"); //Add default category
        localizationElements.Clear();

        if (!File.Exists(locSelectedPath))
        {
            Loc_ErrorDebug("The selected file path '" + locSelectedPath + "' doesn't exist!");
            PlayerPrefs.DeleteKey(locRegistryKey);
            loc_ReadySteady = false;
            return;
        }

        string[] allLines = File.ReadAllLines(locSelectedPath);
        if (allLines.Length <= 1)
            return;

        //Getting all categories first (Categories starts with 'Category Delimiter Symbol')
        for (int i = 1; i < allLines.Length; i++)
        {
            string currentLine = allLines[i];
            if (currentLine.Length <= 1) continue;
            if (currentLine.StartsWith(Localization_SOURCE.GENERAL_DelimiterSymbol_Category))
                locAvailableCategories.Add(currentLine.Trim().Remove(0,1));
        }

        //Getting all localization keys (Keys contains 'Key Delimiter Symbol')
        int currentCategory = 0;
        for (int i = 1; i < allLines.Length; i++)
        {
            string currentLine = allLines[i];
            if (currentLine.Length <= 1) continue;

            //Skip lines that starts with category delimiter
            if (currentLine.StartsWith(Localization_SOURCE.GENERAL_DelimiterSymbol_Category))
            {
                currentCategory++;
                continue;
            }

            //Key must be longer than 1 char!
            if (!locManagerSelected && currentLine.IndexOf(Localization_SOURCE.GENERAL_DelimiterSymbol_Key) <= 1) continue;

            LocalizationElemenets locElement = new LocalizationElemenets();
            string keySrc = locManagerSelected ? currentLine : currentLine.Substring(0, currentLine.IndexOf(Localization_SOURCE.GENERAL_DelimiterSymbol_Key));
            locElement.Key = keySrc;
            if (!locManagerSelected)
            {
                string keyText = currentLine.Substring(keySrc.Length + 1, currentLine.Length - keySrc.Length - 1);
                locElement.Text = keyText.Replace(Localization_SOURCE.GENERAL_NewLineSymbol, System.Environment.NewLine);
            }
            locElement.Category = currentCategory;

            //Add proper key
            localizationElements.Add(locElement);
        }
    }

    /// <summary>
    /// Save current database
    /// </summary>
    public static void Loc_SaveDatabase(string ToPath, bool RefreshData = true)
    {
        if (!File.Exists(ToPath))
        {
            Loc_ErrorDebug("The file path " + ToPath + " doesn't exist!");
            return;
        }

        File.WriteAllText(ToPath, locHeadingFormat);

        FileStream fstream = new FileStream(ToPath, FileMode.Append);
        StreamWriter fwriter = new StreamWriter(fstream);

        fwriter.WriteLine("");

        foreach (string category in locAvailableCategories)
        {
            //Write category name first
            if(category != "Default") fwriter.WriteLine(Localization_SOURCE.GENERAL_DelimiterSymbol_Category + category);

            //Write category elements
            foreach (LocalizationElemenets locElement in localizationElements)
            {
                //Check conditions
                if (category != locAvailableCategories[locElement.Category]) continue;
                if (string.IsNullOrEmpty(locElement.Key)) continue;

                //Write key & text
                if (locElement.Key.Contains(Localization_SOURCE.GENERAL_DelimiterSymbol_Category)
                    ||
                    locElement.Key.Contains(Localization_SOURCE.GENERAL_DelimiterSymbol_Key))
                {
                    Loc_ErrorDebug("key '" + locElement.Key + "' contains Category Delimiter or Key Delimiter. Please remove these characters from the key... Saving process was terminated");
                    fwriter.Dispose();
                    fstream.Close();
                }

                if (locManagerSelected)
                    fwriter.WriteLine(locElement.Key);
                else
                {
                    StringBuilder sb = new StringBuilder(locElement.Text);
                    sb.Replace(System.Environment.NewLine, Localization_SOURCE.GENERAL_NewLineSymbol);
                    sb.Replace("\n", Localization_SOURCE.GENERAL_NewLineSymbol);
                    sb.Replace("\r", Localization_SOURCE.GENERAL_NewLineSymbol);
                    fwriter.WriteLine(locElement.Key + Localization_SOURCE.GENERAL_DelimiterSymbol_Key + sb.ToString());
                }
            }
        }

        fwriter.Dispose();
        fstream.Close();

        AssetDatabase.Refresh();

        if(RefreshData) Loc_RefreshContent();
    }

    /// <summary>
    /// Load current database into Localization_Source_Window
    /// </summary>
    public static void Loc_LoadDatabase(string FromPath, bool RefreshData = true)
    {
        if (!File.Exists(FromPath))
        {
            Loc_ErrorDebug("The file path " + FromPath + " doesn't exist!");
            return;
        }

        if (File.ReadAllLines(FromPath).Length > 1)
        {
            List<string> storedFilelines = new List<string>();
            for (int i = 1; i < File.ReadAllLines(FromPath).Length; i++)
                storedFilelines.Add(File.ReadAllLines(FromPath)[i]);

            foreach (string categories in locAvailableCategories)
            {
                foreach(LocalizationElemenets locArray in localizationElements)
                {
                    if(Loc_GetLocalizationCategory(categories) == locArray.Category)
                    {
                        foreach(string s in storedFilelines)
                        {
                            if (string.IsNullOrEmpty(s)) continue;
                            if (s.StartsWith(Localization_SOURCE.GENERAL_DelimiterSymbol_Category)) continue;

                            string Key = s.Contains(Localization_SOURCE.GENERAL_DelimiterSymbol_Key) ? s.Substring(0, s.IndexOf(Localization_SOURCE.GENERAL_DelimiterSymbol_Key)) : s;
                            if (string.IsNullOrEmpty(Key)) continue;

                            if (Key == locArray.Key)
                            {
                                if (s.Length < Key.Length + 1) continue;
                                locArray.Text = s.Substring(Key.Length + 1, s.Length - Key.Length - 1).Replace(Localization_SOURCE.GENERAL_NewLineSymbol, System.Environment.NewLine);
                                break;
                            }
                        }
                    }
                }
            }
        }
        
        if (RefreshData) Loc_RefreshContent();
    }

    #endregion


    private static int Loc_GetLocalizationCategory(string entry)
    {
        int c = 0;
        foreach(string categ in locAvailableCategories)
        {
            if (categ == entry) return c;
            c++;
        }
        return 0;
    }

    private static Vector2 guiScrollHelper;
    private void OnGUI()
    {
        EditorGUI.indentLevel++;
        PDrawSpace();

        GUILayout.BeginHorizontal();
        PDrawLabel("Localization Manager by Matej Vanco", true);
        GUILayout.FlexibleSpace();
        Color gc = GUI.color;
        GUI.color = Color.magenta;
        if (GUILayout.Button("Discord", GUILayout.Width(150)))
            Application.OpenURL("https://discord.com/invite/Ztr8ghQKqC");
        GUI.color = gc;
        GUILayout.EndHorizontal();

        PDrawSpace();

        //If Localization Manager file not set & ready
        if(!loc_ReadySteady)
        {
            GUILayout.BeginVertical("Box");

            EditorGUILayout.HelpBox("There is no Localization Manager file. To set up keys structure and language system, select or create a Localization Manager file.",MessageType.Info);
            GUILayout.BeginHorizontal("Box");
            if(GUILayout.Button("Select Localization Manager file"))
            {
                string f = EditorUtility.OpenFilePanel("Select Localization Manager file", Application.dataPath, "txt");
                if (string.IsNullOrEmpty(f)) return;
                locSelectedPath = f;
                PlayerPrefs.SetString(locRegistryKey, locSelectedPath);
                Loc_MessageDebug("All set! The Localization Manager is now ready.");
                Loc_GetLocalizationManagerPath();
                return;
            }
            if (GUILayout.Button("Create Localization Manager file"))
            {
                string f = EditorUtility.SaveFilePanel("Create Localization Manager file", Application.dataPath, "LocalizationManager", "txt");
                if (string.IsNullOrEmpty(f))
                    return;
                File.Create(f).Dispose();
                locSelectedPath = f;
                PlayerPrefs.SetString(locRegistryKey, locSelectedPath);
                Loc_MessageDebug("Great! The Localization Manager is now ready.");
                Loc_SaveDatabase(locSelectedPath, false);
                Loc_GetLocalizationManagerPath();
                return;
            }
            GUILayout.EndHorizontal();
            GUILayout.EndVertical();

            return;
        }

        // If the Localization Manager file selected & ready

        #region SECTION__UPPER

        GUILayout.BeginHorizontal("Box");
        if (GUILayout.Button("Save System"))
            Loc_SaveDatabase(locSelectedPath);
        PDrawSpace();
        if (locManagerSelected)
        {
            if (GUILayout.Button("Reset Manager Path"))
            {
                if (EditorUtility.DisplayDialog("Question", "You are about to reset the Localization Manager path... No file or folder will be removed, just the current Language Localization path from the registry. Are you sure to continue?", "Yes", "No"))
                {
                    PlayerPrefs.DeleteKey(locRegistryKey);
                    this.Close();
                    return;
                }
            }
        }
        GUILayout.EndHorizontal();

        PDrawSpace(5);

        string lang;
        if (!string.IsNullOrEmpty(locCurrentLanguage))
        {
            locManagerSelected = false;
            lang = locCurrentLanguage;
        }
        else
        {
            locManagerSelected = true;
            lang = "Language Manager";
        }

        GUILayout.BeginHorizontal("Box");
        PDrawLabel("Selected: " + lang);
        if (GUILayout.Button("Deselect Language"))
        {
            Loc_SaveDatabase(locSelectedPath,false);
            Loc_GetLocalizationManagerPath();
        }
        if (GUILayout.Button("Select Language"))
            Loc_SelectLanguageFile();
        PDrawSpace();
        if (GUILayout.Button("Create Language"))
            Loc_CreateLanguageFile();
        GUILayout.EndHorizontal();

        #endregion

        PDrawSpace(5);

        GUILayout.BeginVertical("Box");

        #region SECTION__CATEGORIES

        GUILayout.BeginHorizontal("Box");
        EditorGUIUtility.labelWidth -= 70;
        locCategorySelected = EditorGUILayout.Popup("Category:", locCategorySelected, locAvailableCategories.ToArray(), GUILayout.MaxWidth(300), GUILayout.MinWidth(150));
        EditorGUIUtility.labelWidth += 70;
        if (locManagerSelected)
        {
            PDrawSpace();
            locSelectedCategoryName = EditorGUILayout.TextField(locSelectedCategoryName);
            if (GUILayout.Button("Add Category"))
            {
                if (string.IsNullOrEmpty(locSelectedCategoryName))
                {
                    Loc_ErrorDebug("Please fill the required field! [Category Name]");
                    return;
                }
                locAvailableCategories.Add(locSelectedCategoryName);
                locSelectedCategoryName = "";
                GUI.FocusControl("Set");
                return;
            }
            if (GUILayout.Button("Remove Category") && locAvailableCategories.Count > 1)
            {
                if (EditorUtility.DisplayDialog("Question", "You are about to remove a category... Are you sure?", "Yes", "No"))
                {
                    if (string.IsNullOrEmpty(locSelectedCategoryName))
                    {
                        locAvailableCategories.RemoveAt(locAvailableCategories.Count - 1);
                        locCategorySelected = 0;
                    }
                    else
                    {
                        int cc = 0;
                        bool notfound = true;
                        foreach (string cat in locAvailableCategories)
                        {
                            if (locSelectedCategoryName == cat)
                            {
                                locAvailableCategories.RemoveAt(cc);
                                locCategorySelected = 0;
                                notfound = false;
                                break;
                            }
                            cc++;
                        }
                        if (notfound) Loc_ErrorDebug("The category couldn't be found.");
                        locSelectedCategoryName = "";
                    }
                    return;
                }
            }
        }
        GUILayout.EndHorizontal();

        #endregion

        PDrawSpace();

        #region SECTION__LOCALIZATION_ARRAY

        GUILayout.BeginHorizontal();
        PDrawLabel("Localization Keys & Texts");
        if (locManagerSelected && GUILayout.Button("+"))
            localizationElements.Add(new LocalizationElemenets() {Category = locCategorySelected });
        GUILayout.EndHorizontal();

        if (localizationElements.Count == 0)
            PDrawLabel(" - - Empty! - -");
        else
        {
            guiScrollHelper = EditorGUILayout.BeginScrollView(guiScrollHelper);

            int c = 0;
            foreach (LocalizationElemenets locA in localizationElements)
            {
                if (locA.Category >= locAvailableCategories.Count)
                {
                    locA.Category = 0;
                    break;
                }
                if (locAvailableCategories[locA.Category] != locAvailableCategories[locCategorySelected])
                    continue;

                EditorGUIUtility.labelWidth -= 100;
                EditorGUILayout.BeginHorizontal("Box");
                if (!locManagerSelected)
                {
                    EditorGUILayout.LabelField(locA.Key,GUILayout.Width(100));

                    EditorGUILayout.LabelField("Text:", GUILayout.Width(100));
                    locA.Text = EditorGUILayout.TextArea(locA.Text, GUILayout.MinWidth(100));
                }
                else
                {
                    EditorGUILayout.LabelField("Key:", GUILayout.Width(45));

                    locA.Key = EditorGUILayout.TextField(locA.Key, GUILayout.MaxWidth(100), GUILayout.MinWidth(30));
                    EditorGUILayout.LabelField("Category:", GUILayout.Width(75));
                    locA.Category = EditorGUILayout.Popup(locA.Category, locAvailableCategories.ToArray());
                    if (GUILayout.Button("-", GUILayout.Width(30)))
                    {
                        localizationElements.Remove(locA);
                        return;
                    }
                }
                
                EditorGUILayout.EndHorizontal();
                EditorGUIUtility.labelWidth += 100;
                c++;
            }
            EditorGUILayout.EndScrollView();
        }
        GUILayout.EndVertical();

        #endregion

        EditorGUI.indentLevel--;
    }


    #region LayoutShortcuts

    private void PDrawLabel(string text,bool bold = false)
    {
        if(bold)
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

    private void PDrawSpace(float space = 10)
    {
        GUILayout.Space(space);
    }

    #endregion

    private static void Loc_ErrorDebug(string msg)
    {
        EditorUtility.DisplayDialog("Error", msg, "OK");
    }

    private static void Loc_MessageDebug(string msg)
    {
        EditorUtility.DisplayDialog("Info", msg, "OK");
    }
}
#endif