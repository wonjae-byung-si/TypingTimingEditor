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

    private readonly int BEATINSECTION = 12;
    private int bpm = 100;

    private float TimePerBeat => 60.0f / bpm;
    private float SectionSize => TimePerBeat * BEATINSECTION;
    private int CurrentSection => (int)(audioSource.time / SectionSize);

    private void Start()
    {
        ResetSpectrum();

        //Audio Info
        audioInfoHandler.UIName = audioSource.clip.name;

        audioInfoHandler.audioSourcePlayButton.onClick.AddListener(ToggleAudioSource);
        audioInfoHandler.audioSourceBPMInputField.onEndEdit.AddListener(ChangeBPM);
        audioInfoHandler.audioSourceBPMInputField.onEndEdit.AddListener(ResetNoteMap);
        audioInfoHandler.audioSourceScrollBar.onValueChanged.AddListener((float value) =>
        {
            audioSource.time = audioSource.clip.length * value;
            ResetSpectrum();
        });
    }

    private void Update()
    {
        foreach (KeyCode keyCode in Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(keyCode))
            {
                noteMapHandler.AddNote(audioSource.time, keyCode);
                ResetNoteMap(null);
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

    private void ChangeBPM(string value/*응 안 써*/)
    {
        bpm = audioInfoHandler.BPM;
        ResetSpectrum();
    }

    private void ResetSpectrum()
    {
        spectrumDrawer.DrawAudioSpectrum(audioSource, CurrentSection * SectionSize, CurrentSection * SectionSize + SectionSize);
    }

    IEnumerator OnAudioSourcePlaying()
    {
        int beforeSection = -1;
        while (audioSource.isPlaying)
        {
            if(CurrentSection != beforeSection)
                ResetSpectrum();

            audioInfoHandler.UITime = audioSource.time;
            beforeSection = CurrentSection;
            yield return null;
        }

        //Pause
        audioInfoHandler.UIButtonImage = false;
        ResetSpectrum();
    }

    private void ResetNoteMap(string value/*응 안써*/)
    {
        noteMapHandler.ResetNoteMap(CurrentSection * SectionSize, CurrentSection * SectionSize + SectionSize);
    }
}
