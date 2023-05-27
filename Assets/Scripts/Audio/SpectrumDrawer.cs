using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpectrumDrawer : MonoBehaviour
{
	public AudioSource audioSource = null;
	public RawImage texture = null;

	public Texture2D DrawAudioSpectrum(Color backgroundColor, Color spectrumColor)
	{
		AudioClip audio = audioSource.clip;
		int width = (int)texture.rectTransform.rect.width;
		int height = (int)texture.rectTransform.rect.height;

		Texture2D tex = new Texture2D(width, height, TextureFormat.RGBA32, false);
		float[] samples = new float[audio.samples * audio.channels];
		float[] waveform = new float[width];
		audio.GetData(samples, 0);

		int s = 0;
		for(int i = 0; i < samples.Length; i++)
		{
			s = (int)(width * ((float)i / samples.Length));
			if (waveform[s] < Mathf.Abs(samples[i]))
			{
				waveform[s] = Mathf.Abs(samples[i]);
			}
		}

		for (int x = 0; x < width; x++)
		{
			for (int y = 0; y < height; y++)
			{
				tex.SetPixel(x, y, backgroundColor);
			}
		}

		for (int x = 0; x < waveform.Length; x++)
		{
			for (int y = 0; y <= waveform[x] * ((float)height * .75f); y++)
			{
				tex.SetPixel(x, (height / 2) + y, spectrumColor);
				tex.SetPixel(x, (height / 2) - y, spectrumColor);
			}
		}
		tex.Apply();

		return tex;
	}

	private void Start()
	{
		texture.texture = DrawAudioSpectrum(Color.black, Color.white);
	}
}
