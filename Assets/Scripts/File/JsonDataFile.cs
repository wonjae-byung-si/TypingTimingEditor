using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Pipes;

public class JsonDataFile
{
    public string path = "";

    public void ChangePath()
    {
        string newPath = FileHandler.FileExplorerSave("json");
        if(newPath == "") return;

        File.Create(path).Close();
        path = newPath;
    }

    public void Save(int bpm, List<Note> data)
    {

        if (path == "") {
            path = FileHandler.FileExplorerSave("json");
            if (path == "") return;
            File.Create(path).Close();
        }

        NoteDB notes = new NoteDB { bpm = bpm, data = data };
        string str = JsonUtility.ToJson(notes);

        File.WriteAllText(path, str);
    }

    public NoteDB Load()
    {
        path = FileHandler.FileExplorerOpen("json");

        string content = "";

        using (StreamReader fileStream = new StreamReader(path))
		{
            while(!fileStream.EndOfStream)
            {
                content += fileStream.ReadLine();
            }
		}

        NoteDB noteDB = JsonUtility.FromJson<NoteDB>(content);

        return noteDB;
    }
}