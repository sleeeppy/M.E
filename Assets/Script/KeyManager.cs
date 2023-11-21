using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum KeyAction { M0, M1, LEFT, RIGHT,SANGHO,Upar,Down, KEYCOUNT }

public static class KeySetting
{
    public static Dictionary<KeyAction, KeyCode> keys = new Dictionary<KeyAction, KeyCode>();

    // Check if a KeyCode is already in use
    public static bool IsKeyCodeUsed(KeyCode keyCode, KeyAction excludeAction)
    {
        foreach (var kvp in keys)
        {
            if (kvp.Key != excludeAction && kvp.Value == keyCode)
            {
                return true;
            }
        }
        return false;
    }
}

public class KeyManager : MonoBehaviour
{
    KeyCode[] defaultKeys = new KeyCode[] { KeyCode.Mouse0, KeyCode.Mouse1, KeyCode.Q, KeyCode.E,KeyCode.F,KeyCode.K,KeyCode.J   };

    // Start is called before the first frame update
    private void Start()
    {
        for (int i = 0; i < (int)KeyAction.KEYCOUNT; i++)
        {
            KeySetting.keys.Add((KeyAction)i, defaultKeys[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnGUI()
    {
        Event keyEvent = Event.current;

        if (keyEvent.isKey && IsValid(keyEvent.keyCode))
        {
            KeyAction keyAction = (KeyAction)key;

            if (!KeySetting.IsKeyCodeUsed(keyEvent.keyCode, keyAction))
            {
                KeySetting.keys[keyAction] = keyEvent.keyCode;
            }

            key = -1;
        }
    }

    int key = -1;

    public void ChangeKey(int num)
    {

        key = num;

    }


    bool IsValid(KeyCode keyCode)
    {
        if ((keyCode >= KeyCode.A && keyCode <= KeyCode.Z) || (keyCode >= KeyCode.Alpha0 && keyCode <= KeyCode.Alpha9))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}