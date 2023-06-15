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
    }

    private void Update()
    {
        foreach (KeyCode keyCode in Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(keyCode))
            {
                // x * TPB = time
                // round(x) * TPB = time

                float offset = (int)Mathf.Round(audioSource.time / TimePerBeat);
                float time = Mathf.Min(offset * TimePerBeat, audioSource.clip.length);
                Debug.Log(audioSource.time + " " + time); //TODO: Remove later


                noteMapHandler.AddNote(time, keyCode);
                ResetNoteMap();
            }
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

            UpdateUITime();
            beforeSection = CurrentSection;
            yield return null;
        }

        //Pause
        audioInfoHandler.UIButtonImage = false;
        ResetSpectrum();
    }

    private void UpdateBPM(string value/*응 안 써*/)
    {
        bpm = audioInfoHandler.BPM;
        ResetSpectrum();
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
        ResetSpectrum();
        ResetNoteMap();
    }
}
