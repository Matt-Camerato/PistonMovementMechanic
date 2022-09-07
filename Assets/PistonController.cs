using UnityEngine;
using System.Linq;

public class PistonController : MonoBehaviour
{
    [SerializeField] private float pistonExtendSpeed, pistonRetractSpeed;
    [SerializeField] private float pistonMinLength, pistonMaxLength;
    [SerializeField] private Color defaultPistonColor, activePistonColor;
    [SerializeField] private SpriteRenderer wPiston, aPiston, sPiston, dPiston;

    private void Update()
    {
        //reset pistons to default color (this should probably be reworked or done somewhere else)
        wPiston.color = defaultPistonColor;
        aPiston.color = defaultPistonColor;
        sPiston.color = defaultPistonColor;
        dPiston.color = defaultPistonColor;

        //if not pressing w, a, s, or d, dont do anything else
        KeyCode key = GetCurrentKeyDown();
        if (key != KeyCode.W && key != KeyCode.A && key != KeyCode.S && key != KeyCode.D) return;

        SpriteRenderer piston = PistonFromKey(key); //determine which piston to scale
        piston.color = activePistonColor; //set current piston to active color

        if (Input.GetMouseButton(0)) ScalePiston(piston, pistonExtendSpeed); //extend piston with LMB
        if (Input.GetMouseButton(1)) ScalePiston(piston, -pistonRetractSpeed); //retract piston with RMB
    }

    //extends or retracts a piston by the given amount
    private void ScalePiston(SpriteRenderer piston, float amount)
    {
        Transform pistonTransform = piston.transform;
        pistonTransform.localScale += Vector3.up * amount * Time.deltaTime; //adjust the scale of the piston

        //keep the length of the piston clamped between pistonMinLength and pistonMaxLength
        pistonTransform.localScale = new Vector3(
            pistonTransform.localScale.x,
            Mathf.Clamp(pistonTransform.localScale.y, pistonMinLength, pistonMaxLength),
            pistonTransform.localScale.z
        );
    }

    private SpriteRenderer PistonFromKey(KeyCode key)
    {
        switch (key)
        {
            case KeyCode.W: return wPiston;
            case KeyCode.A: return aPiston;
            case KeyCode.S: return sPiston;
            case KeyCode.D: return dPiston;
            default: return null; //<-this will never happen because of a previous check
        }
    }

    //ignore this gibberish (its the best possible way to get the KeyCode of the current key down since Unity never implemented that, should be in a separate script)
    private static readonly KeyCode[] keyCodes = System.Enum.GetValues(typeof(KeyCode)).Cast<KeyCode>().Where(k => ((int)k < (int)KeyCode.Mouse0)).ToArray();
    public static KeyCode GetCurrentKeyDown()
    {
        if (!Input.anyKey) return KeyCode.None;

        for (int i = 0; i < keyCodes.Length; i++)
            if (Input.GetKey(keyCodes[i])) return keyCodes[i];

        return KeyCode.None;
    }
}
