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
    public float totalTime;
    public float lastBeat;

    public AudioSource audioSource;

    public AK.Wwise.Event SomeSound;

    public UnityEvent OnBeatEvent;


    private void Start()
    {
        beatTempo = 60f / beatTempo;
    }

    void Update()
    {
        if (BGMusic.enabled)
        {
            totalTime = totalTime + Time.deltaTime;
            if(totalTime - lastBeat > (beatTempo * .9f))
            {
                float beatTime = totalTime / (beatTempo * 2);
                if (Mathf.Abs((int)beatTime - beatTime) <= .005f)
                {
                    BeatsTimer = 0f;
                    doAltColor = doAltColor ? false : true;
                    image.color = doAltColor ? new Color32(100, 0, 0, 255) : new Color32(0, 100, 0, 255);
                    OnBeatEvent.Invoke();
                    lastBeat = totalTime;
                }
            }
            

            /* BeatsTimer = BeatsTimer + Time.deltaTime;
            if (BeatsTimer >= (beatTempo * 2))
            {
                BeatsTimer = 0f;
                doAltColor = doAltColor ? false : true;
                image.color = doAltColor ? new Color32(100, 0, 0, 255) : new Color32(0, 100, 0, 255);
                image.color = doAltColor ? new Color32(100, 0, 0, 255) : new Color32(0, 100, 0, 255);
                OnBeatEvent.Invoke();
            }*/
            // randomText.text = BeatsTimer.ToString();
        }
        // if(totalTime > 5f)
        // {
            // BGMusic.Stop(0);
            // BGMusic.gameObject.SetActive(false);
            // audioSource.Stop();
            // totalTime = 0f;
        // }
        /*else if(lastBeat > 0f && !BGMusic.gameObject.activeSelf)
        {
            BGMusic.gameObject.SetActive(true);
        }*/
        
    }

    public void StartMusic()
    {
        BGMusic.enabled = true;
        doAltColor = doAltColor ? false : true;
        image.color = doAltColor ? new Color32(100, 0, 0, 255) : new Color32(0, 100, 0, 255);
        OnBeatEvent.Invoke();
    }
}
