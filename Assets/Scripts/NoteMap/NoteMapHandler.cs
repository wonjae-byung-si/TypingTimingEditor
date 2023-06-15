using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteMapHandler : MonoBehaviour
{
	GameObject noteAsset;
	[SerializeField] Button noteMap;
	[SerializeField] List<Note> noteList;
	[SerializeField] KeyArray keyArray;

	private void Awake()
	{
		noteAsset = Resources.Load<GameObject>("Note");
		noteList = new List<Note>();
	}

	public void AddNote(float time, KeyCode keyCode)
	{
		if (!keyArray.Has(keyCode)) return;


		Note note = new Note(time, keyCode);
		noteList.Sort();
		if (noteList.BinarySearch(note) < 0)
			noteList.Add(note);
	}

	public void ResetNoteMap(float startTime, float endTime)
	{
		//Delete ui note
		foreach(Transform note in noteMap.transform)
		{
			Destroy(note.gameObject);
		}

		//Easy to use
		Rect rect = noteMap.GetComponent<RectTransform>().rect;
        float width = rect.width;
		float height = rect.height;

		
		for(int i = 0; i < noteList.Count; i++)
		{
			Note note = noteList[i];
			if(note.time >= startTime && note.time <= endTime)
			{
				GameObject noteObj = Instantiate(noteAsset, noteMap.transform);

				float x = ((note.time - startTime) / (endTime - startTime) * width) - width/2;
				float y = 0;

				noteObj.transform.localPosition = new Vector2(x, y);
			}
		}
	}
}
