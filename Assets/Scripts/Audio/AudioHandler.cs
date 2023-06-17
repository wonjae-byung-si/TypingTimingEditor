using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class AudioHandler : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioInfoHandler audioInfoHandler;
    public SpectrumDrawer spectrumDrawer;
    public NoteMapHandler noteMapHandler;
    public BarHandler barHandler;
    public SaveLoadHandler saveLoadHandler;

    JsonDataFile jsonDataFile;

    private readonly int BEATINSECTION = 13;
    private int bpm = 100;

    private float TimePerBeat => 60.0f / bpm;
    private float SectionSize => TimePerBeat * (BEATINSECTION-1);
    private int CurrentSection => (int)(audioSource.time / SectionSize);

    private void Start()
    {
        ResetSpectrum();
        ResetNoteMap();

        //Audio Info
        audioInfoHandler.UIName = audioSource.clip.name;

        audioInfoHandler.audioSourcePlayButton.onClick.AddListener(ToggleAudioSource);
        audioInfoHandler.audioSourceBPMInputField.onEndEdit.AddListener(UpdateBPM);
        audioInfoHandler.audioSourceBPMInputField.onEndEdit.AddListener(ResetNoteMap);
        audioInfoHandler.audioSourceImportButton.onClick.AddListener(
            () => {
                string path = FileHandler.FileExplorerOpen("mp3");
                if(path == "") return;

                StartCoroutine(ImportAudioClip(path));
            }
        );

        // Note Info
        noteMapHandler.noteInfoHandler.deleteAllNotesButton.onClick.AddListener( () =>
        {
            noteMapHandler.DeleteAllNotes();
            ResetNoteMap();
        });

        noteMapHandler.noteInfoHandler.deleteNoteButton.onClick.AddListener( () => {
            noteMapHandler.DeleteCurrentNote();
            ResetNoteMap();
        });

        noteMapHandler.noteInfoHandler.timeInputField.onEndEdit.AddListener((string value) =>{
            ResetNoteMap();
        });
    
        //Save Load
        jsonDataFile = new JsonDataFile();

        saveLoadHandler.save.onClick.AddListener(() => {
            jsonDataFile.Save(bpm, noteMapHandler.noteList);
        });

        saveLoadHandler.load.onClick.AddListener(() => {
            NoteDB noteDB = jsonDataFile.Load();

            audioInfoHandler.BPM = noteDB.bpm;
            noteMapHandler.noteList = noteDB.data;

            UpdateUITime();
            ResetSpectrum();
            ResetNoteMap();
            UpdateBar();
        });

        saveLoadHandler.change.onClick.AddListener(jsonDataFile.ChangePath);
    }

    private void Update()
    {
        foreach (KeyCode keyCode in Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(keyCode) && noteMapHandler.keyArray.Has(keyCode))
            {
                // x * TPB = time
                // round(x) * TPB = time

                float offset = (int)Mathf.Round(audioSource.time / TimePerBeat);
                float time = Mathf.Min(offset * TimePerBeat, audioSource.clip.length);

                noteMapHandler.AddNote(time, keyCode);
                ResetNoteMap();
            }
        }

        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            audioSource.time = Mathf.Max(0, audioSource.time - TimePerBeat);
            
            UpdateUITime();
            ResetSpectrum();
            ResetNoteMap();
            UpdateBar();
        }

        else if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            if(audioSource.time + TimePerBeat > audioSource.clip.length) return;

            audioSource.time = audioSource.time + TimePerBeat;

            UpdateUITime();
            ResetSpectrum();
            ResetNoteMap();
            UpdateBar();
        }
    }

    private void ToggleAudioSource()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Pause();
        }
        else
        {
            audioSource.Play();
            audioInfoHandler.UIButtonImage = true;
            StartCoroutine("OnAudioSourcePlaying");
        }
    }

    IEnumerator OnAudioSourcePlaying()
    {
        int beforeSection = -1;
        while (audioSource.isPlaying)
        {
            if (CurrentSection != beforeSection)
            {
                ResetSpectrum();
                ResetNoteMap();
            }

            //Update UI
            UpdateUITime();
            UpdateBar();

            beforeSection = CurrentSection;
            yield return null;
        }

        //Pause
        audioInfoHandler.UIButtonImage = false;
        ResetSpectrum();
        ResetNoteMap();
    }

    private void UpdateBPM(string value/*응 안 써*/)
    {
        bpm = audioInfoHandler.BPM;
        ResetSpectrum();
        ResetNoteMap();
        UpdateBar();
    }

    private void ResetSpectrum()
    {
        spectrumDrawer.DrawAudioSpectrum(audioSource, CurrentSection * SectionSize, CurrentSection * SectionSize + SectionSize);
    }

    private void UpdateUITime()
    {
        audioInfoHandler.UITime = audioSource.time;
    }

    private void ResetNoteMap(string value/*응 안써*/=null)
    {
        noteMapHandler.ResetNoteMap(CurrentSection * SectionSize, CurrentSection * SectionSize + SectionSize);
    }


    IEnumerator ImportAudioClip(string path)
    {
        WWW request = new WWW("file:///" + path);
        yield return request;

        AudioClip audioClip = request.GetAudioClip();
        audioSource.clip = audioClip;
        audioSource.clip.name = audioClip.name;

        audioSource.time = 0f;
        audioInfoHandler.UIName = audioSource.clip.name;
        ResetSpectrum();
        ResetNoteMap();
    }

    private void UpdateBar()
    {
        barHandler.Lerp((audioSource.time - (CurrentSection * SectionSize)) / SectionSize);
    }
}
