using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Singleton;

public class LocalizeSetting : MonoBehaviour
{
    Dropdown dropdown;
    
    void Start()
    {
        dropdown = GetComponent<Dropdown>();
        if (dropdown.options.Count != S.Langs.Count) SetLangOption();
        dropdown.onValueChanged.AddListener((d) => S.SetLangIndex(dropdown.value));

        LocalizeSettingChanged();
        S.LocalizeSettingChanged += LocalizeSettingChanged;
    }

	void OnDestroy()
	{
        S.LocalizeSettingChanged -= LocalizeSettingChanged;
    }

	void SetLangOption()
    {
        List<Dropdown.OptionData> optionDatas = new List<Dropdown.OptionData>();
        for (int i = 0; i < S.Langs.Count; i++)
            optionDatas.Add(new Dropdown.OptionData() { text = S.Langs[i].langLocalize });
        dropdown.options = optionDatas;
        print("드롭다운 업데이트 해주세요");
    }

    void LocalizeSettingChanged()
    {
        dropdown.value = S.curLangIndex;
    }
}
