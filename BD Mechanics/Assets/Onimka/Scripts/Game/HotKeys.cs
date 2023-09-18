using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HotKeys
{

    public static KeyCode SelectItems => _selectItems;
    private static KeyCode _selectItems = KeyCode.LeftAlt;

    public static KeyCode LeftMouse => _leftMouse;
    private static KeyCode _leftMouse = KeyCode.Mouse0;

    public static KeyCode RightMouse => _rightMouse;
    private static KeyCode _rightMouse = KeyCode.Mouse1;

    public static KeyCode CenterMouse => _centerMouse;
    private static KeyCode _centerMouse = KeyCode.Mouse2;

}
