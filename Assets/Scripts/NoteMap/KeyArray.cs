using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "keyArray", menuName = "ScriptableObjects/keyArray", order = 1)]
public class KeyArray : ScriptableObject
{
    [SerializeField] List<KeyCode> availableKeycode;

    public bool Has(KeyCode keyCode)
    {
        foreach(KeyCode availableKey in availableKeycode)
        {
            if (availableKey == keyCode)
                return true;
        }
        return false;
    }
}