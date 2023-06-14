using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Note
{
    public float time;
    public KeyCode keyCode;
    public NoteType noteType = NoteType.TimedKeyNote;
    public Language language = Language.English;

    public Note(float time, KeyCode keyCode)
    {
        this.time = time;
        this.keyCode = keyCode;
    }
}