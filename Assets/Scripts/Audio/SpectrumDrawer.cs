using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpectrumDrawer : MonoBehaviour
{
	public AudioSource audioSource = null;
	private RawImage rawImage = null;
	private float[] sample;

	[SerializeField] private Color backgroundColor;
	[SerializeField] private Color spectrumColor;

    private void Awake()
    {
        rawImage = GetComponent<RawImage>();

		sample = new float[audioSource.clip.samples * audioSource.clip.channels];
		audioSource.clip.GetData(sample, 0);
    }

    private void Start()
    {
        DrawAudioSpectrum(.0f, audioSource.clip.length);
    }

    private float[] GetSpectrum(float startTime, float endTime)
	{
		if (startTime > endTime)
			throw new Exception("startTime is bigger than endTime");

		// Easy to use
		AudioClip audio = audioSource.clip;
        int width = (int)rawImage.rectTransform.rect.width;
        int height = (int)rawImage.rectTransform.rect.height;

		//Section
		int sampleSize = audio.samples * audio.channels;
		int startIndex = (int)(startTime / audio.length * sampleSize); //이상
		int endIndex = (int)(endTime / audio.length * sampleSize); //초과 

		int size = endIndex - startIndex;

        float[] waveform = new float[width];
        int s = 0;
        for (int i = 0; i < size; i++)
        {
            s = (int)(width * ((float)i / size));

            if (waveform[s] < Mathf.Abs(sample[i+startIndex])) // Max value
                waveform[s] = Mathf.Abs(sample[i+startIndex]);
        }

		return waveform;
    }

	public void DrawAudioSpectrum(float startTime, float endTime)
	{

        int width = (int)rawImage.rectTransform.rect.width;
        int height = (int)rawImage.rectTransform.rect.height;

        Texture2D tex = new Texture2D(width, height, TextureFormat.RGBA32, false);
        tex.filterMode = FilterMode.Point;

        float[] waveform = GetSpectrum(startTime, endTime);

		// Fill Background
		for (int x = 0; x < width; x++)
			for (int y = 0; y < height; y++)
                tex.SetPixel(x, y, backgroundColor);

		// Draw Spectrum
		for (int x = 0; x < waveform.Length; x++)
			for (int y = 0; y <= waveform[x] * height; y++)
                tex.SetPixel(x, y, spectrumColor);

        tex.Apply();


        rawImage.texture = tex;
	}
}
