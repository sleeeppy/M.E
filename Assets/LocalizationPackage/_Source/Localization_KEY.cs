using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Use 'localization key' as an indicator to the localized text.
/// Written by Matej Vanco in 2018. Updated in 2022.
/// https://matejvanco.com
/// </summary>
[AddComponentMenu("Matej Vanco/Language Localization/Localization Key")]
public class Localization_KEY : MonoBehaviour
{
    [Space]
    public string KeyID;
    /// <summary>
    /// If enabled, the script will automatically search for Localization_SOURCE and translate itself on Awake function
    /// </summary>
    public bool TranslateAutomatically = false;

    public void Awake()
    {
        if (!TranslateAutomatically) return;
        Localization_SOURCE ls = FindObjectOfType<Localization_SOURCE>();
        if (!ls) return;
        if (GetComponent<Text>())
            GetComponent<Text>().text = ls.Lang_ReturnText(KeyID);
        else if (GetComponent<TextMeshProUGUI>())
            GetComponent<TextMeshProUGUI>().text = ls.Lang_ReturnText(KeyID);
        else if (GetComponent<TextMesh>())
            GetComponent<TextMesh>().text = ls.Lang_ReturnText(KeyID);
    }
}
