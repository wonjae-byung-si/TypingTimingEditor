using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    delegate void OnKeyPress(KeyCode key);

    List<OnKeyPress> onKeyPress;


    private void Start()
    {
        onKeyPress = new List<OnKeyPress>();
    }

    void Update()
    {
        Event e = Event.current;
        if (e.isKey)
        {
            foreach(OnKeyPress func in onKeyPress)
            {
                func?.Invoke(e.keyCode);
            }
        }
    }
}
