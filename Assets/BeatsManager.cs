using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
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
    public AK.Wwise.Event AmbientMusic;
    
    public float introTimer;

    public string musicTiming;

    public AK.Wwise.Event MainMusic;
    public float mainMusicTimer;

    public UnityEvent OnBeatEvent;

    public List<PulseLightWithMusic> pulseLightWithMusicList;
    public Transform torches;
    public Transform uiTutorials;
    public Transform moveUpAndDownObjs;
    public AK.Wwise.Event ClickSound;

    public GameObject MineGameObj;
    public Transform mineGameStartPos;
    public Transform mineGameFinishPos;
    public Transform mineGameFinishPos2;
    public GameObject note;
    public GameObject note2;
    public int notesPass;
    public GameObject centerCircle;

    private float ambientSongLength = 73f;
    private float intenseSongLength = 96f;

    public bool isSuccessTime;

    public PlayerController playerController;
    private GemManager gemManager;

    public uint musicId;


    private void Start()
    {
        musicTiming = null;
        beatTempo = 60f / beatTempo;
        gemManager = GameObject.FindObjectOfType<GemManager>();
        if(SceneManager.GetActiveScene().buildIndex != 0)
        {
            StartMusic();
        }
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
            isSuccessTime = true;
        } else if (proportionComplete < .25f) {
            note2.GetComponent<Image>().color = Color.green;
            note.GetComponent<Image>().color = Color.red;
            isSuccessTime = true;
        }
        else
        {
            isSuccessTime = false;
            note.GetComponent<Image>().color = Color.red;
            note2.GetComponent<Image>().color = Color.red;
        }
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && MineGameObj.activeSelf)
        {
            playerController.MakeMining();
            if(isSuccessTime)
            {
                playerController.SuccessMine();
                gemManager.HadSuccess();
            } else
            {
                playerController.FailMine();
            }
        }
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
            if(MineGameObj.activeSelf)
            {
                PositionNotes();
            }
        }
        if (musicTiming == "playingIntro")
        {
            introTimer = introTimer + Time.deltaTime;
            if (introTimer >= 12f)
            {
                musicId =  MainMusic.Post(gameObject);
                musicTiming = "playingMain";
                totalTime = 0f;
                lastBeat = 0f;
            }
        } else if(musicTiming == "playingMain")
        {
            mainMusicTimer = mainMusicTimer + Time.deltaTime;
            if (mainMusicTimer >= ambientSongLength)
            {
                totalTime = 0f;
                mainMusicTimer = 0f;
                AkSoundEngine.StopPlayingID(musicId);
                if (SceneManager.GetActiveScene().buildIndex == 0)
                {
                    musicId = AmbientMusic.Post(gameObject);
                } else
                {
                    musicId = MainMusic.Post(gameObject);

                }
                lastBeat = 0f;
            }
        }
    }

    public void StartMusic()
    
    {
        if(SceneManager.GetActiveScene().buildIndex == 0)
        {
            ClickSound.Post(gameObject);
            OnBeatEvent.Invoke();
            musicId = AmbientMusic.Post(gameObject);
            musicTiming = "playingMain";
        } else
        {
            musicId = IntroMusic.Post(gameObject);
            musicTiming = "playingIntro";
        }
        
    }

    public void DoPulseLights()
    {
        foreach(PulseLightWithMusic child in pulseLightWithMusicList)
        {
            if(child.GetComponent<Animator>() != null)
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
