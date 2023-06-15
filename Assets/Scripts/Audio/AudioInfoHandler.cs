using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AudioInfoHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI audioSourceName;
    [SerializeField] private TextMeshProUGUI audioSourceTime;
    public Button audioSourcePlayButton;
    public TMP_InputField audioSourceBPMInputField;
    public Button audioSourceImportButton;
    //public for add event listener

    private Sprite playingSprite;
    private Sprite pausingSprite;

    private void Awake()
    {
        // Get Sprite
        playingSprite = Resources.Load<Sprite>("Sprites/Playing");
        pausingSprite = Resources.Load<Sprite>("Sprites/Pausing");
    }

    public string UIName
    {
        set { audioSourceName.text = value; }
    }

    public float UITime
    {
        set { audioSourceTime.text = $"{(int)(value / 60f):D2}:{(int)(value % 60f):D2}"; }
    }

    public bool UIButtonImage
    {
        set
        {
            Image image = audioSourcePlayButton.GetComponent<Image>();
            if (value) image.sprite = playingSprite;
            else image.sprite = pausingSprite;
        }
    }

    public int BPM
    {
        get
        {
            int result;
            if (!int.TryParse(audioSourceBPMInputField.text, out result))
            {
                result = 100; // default value
                audioSourceBPMInputField.text = result.ToString();
            }

            return result;
        }

        set
        {
            audioSourceBPMInputField.text = value.ToString();
        }
    }
}