using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JsonDataFile
{
    public string path = "";

    public void ChangePath()
    {
        string newPath = FileHandler.FileExplorerSave("json");
        if(newPath == "") return;

        path = newPath;
    }

    public void Save(int bpm, List<Note> data)
    {

        FileStream fileStream;
        if (path == "") {
            path = FileHandler.FileExplorerSave("json");
            fileStream = File.Create(path);
            fileStream.Close();
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