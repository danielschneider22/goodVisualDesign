using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryManager : MonoBehaviour
{
    public AK.Wwise.Event VictoryMusic;
    public GameObject VictoryCanvas;
    public LavaFloorManager lavaFloorManager;
    public BeatsManager beatsManager;

    public void DoVictory()
    {
        VictoryMusic.Post(gameObject);
        lavaFloorManager.stopLava = true;
        AkSoundEngine.StopPlayingID(beatsManager.musicId);
        VictoryCanvas.SetActive(true);
    }
}
