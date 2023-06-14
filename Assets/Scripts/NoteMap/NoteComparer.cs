using System;
using System.Collections.Generic;

public class NoteComparer : IComparer<Note>
{
    public int Compare(Note x, Note y)
    {
        return x.time.CompareTo(y.time);
    }
}
