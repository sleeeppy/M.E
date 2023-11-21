using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Singleton;

public class LocalizeScript : MonoBehaviour
{
    public string textKey;
    public string[] dropdownKey;

    void Start()
    {
        LocalizeChanged();
        S.LocalizeChanged += LocalizeChanged;
    }

    void OnDestroy()
    {
        S.LocalizeChanged -= LocalizeChanged;
    }

    string Localize(string key)
    {
        int keyIndex = S.Langs[0].value.FindIndex(x => x.ToLower() == key.ToLower());
        return S.Langs[S.curLangIndex].value[keyIndex];
    }

    void LocalizeChanged()
    {
        if (GetComponent<Text>() != null)
            GetComponent<Text>().text = Localize(textKey);

        else if (GetComponent<Dropdown>() != null)
        {
            Dropdown dropdown = GetComponent<Dropdown>();
            dropdown.captionText.text = Localize(dropdownKey[dropdown.value]);
            for (int i = 0; i < dropdown.options.Count; i++)
                dropdown.options[i].text = Localize(dropdownKey[i]);
        }
    }
}