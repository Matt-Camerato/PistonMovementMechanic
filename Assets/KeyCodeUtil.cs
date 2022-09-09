using System.Linq;
using UnityEngine;

public static class KeyCodeUtil
{
    //this is best possible way to get the KeyCode of the current key down using the old input system since Unity never implemented that :)
    private static readonly KeyCode[] keyCodes = System.Enum.GetValues(typeof(KeyCode)).Cast<KeyCode>().Where(k => ((int)k < (int)KeyCode.Mouse0)).ToArray();
    public static KeyCode GetCurrentKeyDown()
    {
        if (!Input.anyKey) return KeyCode.None;

        for (int i = 0; i < keyCodes.Length; i++)
            if (Input.GetKey(keyCodes[i])) return keyCodes[i];

        return KeyCode.None;
    }
}
