using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;

public class FileHandler : MonoBehaviour
{
    private string destination;
    public string Destination => destination;

    public void SaveFile()
    {
    }

    public string FileExplorerSave()
    {
        return EditorUtility.SaveFilePanel("Select Directory to save file", ".", "music", ".xml");
    }

    public string FileExplorerOpen()
    {
        return EditorUtility.OpenFilePanel("Select Directory to save file", ".", "music", ".yml");
    }
}
