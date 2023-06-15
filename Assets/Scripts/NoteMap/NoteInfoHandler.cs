using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NoteInfoHandler : Singleton<NoteInfoHandler>
{
    Note currentNote = null;

    [SerializeField] TMP_InputField timeInputField;
    [SerializeField] TMP_Dropdown typeDropdown;
    [SerializeField] TMP_Dropdown languageDropdown;
    [SerializeField] TextMeshProUGUI keycodeText;


    protected override void Awake()
    {
        base.Awake();

        timeInputField.onEndEdit.AddListener((string value) => {
            float time;

            if(!float.TryParse(value, out time))
            {
                time = currentNote.time;
            }

            currentNote.time = time;

            // Update UI
            UpdateTimeInputField();
        });

        typeDropdown.onValueChanged.AddListener((int value) => {
            if(currentNote == null) return;
            
            currentNote.noteType = (NoteType) value;

            //Update UI
            UpdateTypeDropdown();
        });

        languageDropdown.onValueChanged.AddListener((int value) => {
            if(currentNote == null) return;
            
            currentNote.language = (Language) value;

            //Update UI
            UpdateLanguageDropdown();
        });
    }

    public void ChangeCurrentNote(Note note) //Note is reference type
    {
        if(currentNote == note) return;
        currentNote = note;

        Debug.Log(note.keyCode);
        UpdateNoteUI();
    }
    
    private void UpdateNoteUI()
    {
        UpdateTimeInputField();
        UpdateTypeDropdown();
        UpdateLanguageDropdown();
        UpdateKeycode();
    }

    private void UpdateTimeInputField()
    {
        if(currentNote == null) return; //error handling

        timeInputField.text = currentNote.time.ToString();
    }

    private void UpdateTypeDropdown()
    {
        if(currentNote == null) return; //error handling

        typeDropdown.value = (int)currentNote.noteType;
    }

    private void UpdateLanguageDropdown()
    {
        if(currentNote == null) return; //error handling

        languageDropdown.value = (int)currentNote.language;
    }

    private void UpdateKeycode(){
        if(currentNote == null) return;

        keycodeText.text = currentNote.ToString();
    }
}
