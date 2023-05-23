using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;

public class FileHandler : MonoBehaviour
{
    private string FileExplorerSave()
    {
        return EditorUtility.SaveFilePanel("Select directory to save file", ".", "level", ".typm");
    }

    private string FileExplorerOpen()
    {
        return EditorUtility.OpenFilePanel("Select file to open", ".", ".typm");
    }

    public void Save()
    {
        string path = FileExplorerSave();
        DataFile<TestData> file = new DataFile<TestData>(path);
        file.Save();
    }

    public void Open()
    {
        string path = FileExplorerOpen();
        DataFile<TestData> file = DataFile<TestData>.From(path);

        print(file.Data.GetName());
    }
}
