using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BeatsManager : MonoBehaviour
{
    public float BeatsTimer;
    public TextMeshProUGUI randomText;
    public float beatTempo;
    public AkAmbient BGMusic;
    public Image image;
    public bool doAltColor;

    public UnityEvent OnBeatEvent;


    private void Start()
    {
        beatTempo = 60f / beatTempo;
    }

    void Update()
    {
        if (BGMusic.enabled)
        {
            BeatsTimer = BeatsTimer + Time.deltaTime;
            if (BeatsTimer >= (beatTempo * 2))
            {
                BeatsTimer = 0f;
                doAltColor = doAltColor ? false : true;
                image.color = doAltColor ? new Color32(100, 0, 0, 255) : new Color32(0, 100, 0, 255);
                image.color = doAltColor ? new Color32(100, 0, 0, 255) : new Color32(0, 100, 0, 255);
                OnBeatEvent.Invoke();
            }
            randomText.text = BeatsTimer.ToString();
        }
        
    }

    public void StartMusic()
    {
        BGMusic.enabled = true;
        doAltColor = doAltColor ? false : true;
        image.color = doAltColor ? new Color32(100, 0, 0, 255) : new Color32(0, 100, 0, 255);
        OnBeatEvent.Invoke();
    }
}
