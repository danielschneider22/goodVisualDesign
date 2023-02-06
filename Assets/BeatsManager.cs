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
    public float totalTime;
    public float lastBeat;

    public AudioSource audioSource;

    public AK.Wwise.Event IntroMusic;
    public float introTimer;

    private string musicTiming;

    public AK.Wwise.Event MainMusic;
    public float mainMusicTimer;

    public UnityEvent OnBeatEvent;

    public List<PulseLightWithMusic> pulseLightWithMusicList;
    public Transform torches;
    public Transform uiTutorials;
    public Transform moveUpAndDownObjs;
    public AK.Wwise.Event ClickSound;

    public bool mineGameActive;
    public Transform mineGameStartPos;
    public Transform mineGameFinishPos;
    public Transform mineGameFinishPos2;
    public GameObject note;
    public GameObject note2;
    public int notesPass;
    public GameObject centerCircle;


    private void Start()
    {
        beatTempo = 60f / beatTempo;
        mineGameActive = true;
    }

    public bool IsCloseEnoughToBeat()
    {
        float beatTime = totalTime / (beatTempo * 2);
        return Mathf.Abs((int)beatTime - beatTime) <= .3f;
    }

    public void PositionNotes()
    {
        float proportionComplete = (totalTime - lastBeat) / (beatTempo * 2);
        float valueOnX = Mathf.Lerp(mineGameStartPos.position.x, mineGameFinishPos.transform.position.x, proportionComplete);
        note.transform.position = new Vector3(valueOnX, note.transform.position.y, note.transform.position.z);

        valueOnX = Mathf.Lerp(mineGameFinishPos.position.x, mineGameFinishPos2.transform.position.x, proportionComplete);
        note2.transform.position = new Vector3(valueOnX, note.transform.position.y, note.transform.position.z);
        

        float growthScale = proportionComplete > .95f || proportionComplete < .05 ? 1.25f :Mathf.Lerp(1f, 1.25f, Mathf.Abs(proportionComplete - .5f));
        centerCircle.transform.localScale = new Vector3(growthScale, growthScale, growthScale);


        if (proportionComplete > .75f)
        {
            note.GetComponent<Image>().color = Color.green;
            note2.GetComponent<Image>().color = Color.red;
        } else if (proportionComplete < .25f) {
            note2.GetComponent<Image>().color = Color.green;
            note.GetComponent<Image>().color = Color.red;
        }
        else
        {
            note.GetComponent<Image>().color = Color.red;
            note2.GetComponent<Image>().color = Color.red;
        }
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
                    OnBeatEvent.Invoke();
                    lastBeat = totalTime;
                }
            }
            if(mineGameActive)
            {
                PositionNotes();
            }
        }
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
            if (mainMusicTimer >= 96f)
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
        ClickSound.Post(gameObject);
        OnBeatEvent.Invoke();
        IntroMusic.Post(gameObject);
        musicTiming = "playingIntro";
    }

    public void DoPulseLights()
    {
        foreach(PulseLightWithMusic child in pulseLightWithMusicList)
        {
            child.TogglePulse();
        }
        foreach(Transform child in torches)
        {
            foreach(PulseLightWithMusic p in child.GetComponent<TorchManager>().pulseLightWithMusicList)
            {
                p.TogglePulse();
            }
        }
        foreach (Transform child in uiTutorials)
        {
            child.GetComponent<PulseLightWithMusic>().TogglePulse();
        }
    }

    public void DoMoveUpAndDown()
    {
        foreach (Transform child in moveUpAndDownObjs)
        {
            child.GetComponent<MoveUpAndDownToRhythm>().DoMoveUpOrDown();
        }
    }
}
