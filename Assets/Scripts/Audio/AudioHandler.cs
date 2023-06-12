using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHandler : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioInfoHandler audioInfoHandler;
    public SpectrumDrawer spectrumDrawer;

    public int beatInSection = 12;
    private int bpm = 100;

    private float TimePerBeat => 60.0f / bpm;
    private float SectionSize => TimePerBeat * beatInSection;
    private int CurrentSection => (int)(audioSource.time / SectionSize);


    private void Start()
    {
        ResetSpectrum();

        //Audio Info
        audioInfoHandler.UIName = audioSource.clip.name;

        audioInfoHandler.audioSourcePlayButton.onClick.AddListener(() =>
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
        });

        audioInfoHandler.audioSourceBPMInputField.onEndEdit.AddListener( (string value/*응 안 써*/) =>
        {
            bpm = audioInfoHandler.BPM;
            ResetSpectrum();
        });
    }

    private void ResetSpectrum()
    {
        Debug.Log(CurrentSection);
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
}
