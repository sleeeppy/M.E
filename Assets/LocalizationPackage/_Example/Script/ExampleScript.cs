using UnityEngine;

/// <summary>
/// Example script for advanced localization of dynamic variables... Follow the code below
/// </summary>
public class ExampleScript : MonoBehaviour
{
    public Localization_SOURCE langLocalization; // Language localization indication
    public string exampleVariable = ""; // My desired final variable for translation
    public string myName = "Matt"; // My dynamic variable (can be any - int, string etc)

    private void OnEnable()
    {
        // Declare custom method when the language is refreshed
        langLocalization.Action_LanguageLoaded = () => { LanguageRefresh(); };
    }

    /// <summary>
    /// Custom method for language refresh
    /// </summary>
    private void LanguageRefresh()
    {
        // Get the translated text from the localization and replace the custom variable macro with myName
        // Notice that the VariableTest key is added in LanguageLocalization component and the Assignation Type is set to None as we don't need any automation
        exampleVariable = langLocalization.Lang_ReturnText("VariableTest").Replace("#MYNAME#", myName);
        // Debug the result!
        Debug.Log(exampleVariable);
    }
}
