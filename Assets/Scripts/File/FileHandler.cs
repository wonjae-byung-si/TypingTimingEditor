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
//#if UNITY_EDITOR
		//return EditorUtility.SaveFilePanel("Select directory to save file", ".", "level", fileType);
//#else
		return StandaloneFileBrowser.SaveFilePanel("Select directory to save file", ".", "level", fileType);
//#endif
	}

	public static string FileExplorerOpen(string fileType)
	{
//#if UNITY_EDITOR
        //return EditorUtility.OpenFilePanel("Select file to open", ".", fileType);
//#else
		return StandaloneFileBrowser.OpenFilePanel("Select file to open", ".", fileType, false)[0];
//#endif
    }
}
