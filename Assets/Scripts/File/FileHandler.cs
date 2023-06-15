using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;
using SFB;

public class FileHandler
{
	public static string FileExplorerSave(string fileType)
	{
		return StandaloneFileBrowser.SaveFilePanel("Select directory to save file", ".", "level."+fileType, fileType);
	}

	public static string FileExplorerOpen(string fileType)
	{
		return StandaloneFileBrowser.OpenFilePanel("Select file to open", ".", fileType, false)[0];
    }
}
