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
    public SpriteRenderer image;
    public bool doAltColor;
    public float totalTime;
    public float lastBeat;

    public AudioSource audioSource;

    public AK.Wwise.Event IntroMusic;
    public float introTimer;

    private string musicTiming;

    public AK.Wwise.Event MainMusic;
    public float mainMusicTimer;

    public UnityEvent OnBeatEvent;


    private void Start()
    {
        beatTempo = 60f / beatTempo;
    }

    void Update()
    {
        if (musicTiming != null)
        {
            totalTime = totalTime + Time.deltaTime;
            if(totalTime - lastBeat > (beatTempo * .9f))
            {
                float beatTime = totalTime / (beatTempo * 2);
                if (Mathf.Abs((int)beatTime - beatTime) <= .05f)
                {
                    BeatsTimer = 0f;
                    doAltColor = doAltColor ? false : true;
                    image.color = doAltColor ? new Color32(33, 158, 188, 255) : new Color32(35, 160, 190, 255);
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
                image.color = doAltColor ? new Color32(100, 0, 0, 255) : new Color32(33, 158, 188, 255);
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
        if (musicTiming == "playingIntro")
        {
            introTimer = introTimer + Time.deltaTime;
            if (introTimer >= 12f)
            {
                MainMusic.Post(gameObject);
                musicTiming = "playingMain";
                totalTime = 0f;
                lastBeat = 0f;
            }
        } else if(musicTiming == "playingMain")
        {
            mainMusicTimer = mainMusicTimer + Time.deltaTime;
            if (mainMusicTimer >= 36f)
            {
                totalTime = 0f;
                mainMusicTimer = 0f;
                MainMusic.Post(gameObject);
                lastBeat = 0f;
            }
        }
    }

    public void StartMusic()
    {
        // BGMusic.enabled = true;
        doAltColor = doAltColor ? false : true;
        image.color = doAltColor ? new Color32(33, 158, 188, 255) : new Color32(35, 160, 190, 255);
        OnBeatEvent.Invoke();
        IntroMusic.Post(gameObject);
        musicTiming = "playingIntro";
    }
}
