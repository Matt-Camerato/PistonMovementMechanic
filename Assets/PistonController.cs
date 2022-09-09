using System;
using UnityEngine;

public class PistonController : MonoBehaviour
{
    public static event Action<KeyCode> SelectPiston;
    public static event Action<KeyCode> ExtendPiston;
    public static event Action<KeyCode> RetractPiston;

    private void Update()
    {
        KeyCode currentKey = KeyCodeUtil.GetCurrentKeyDown();
        SelectPiston.Invoke(currentKey); //update currently selected piston

        if (Input.GetMouseButton(0)) ExtendPiston.Invoke(currentKey); //extend pistons with LMB
        if (Input.GetMouseButton(1)) RetractPiston.Invoke(currentKey); //retract pistons with RMB
    }
}