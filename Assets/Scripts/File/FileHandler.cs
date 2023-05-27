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
}
