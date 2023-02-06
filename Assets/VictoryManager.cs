using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryManager : MonoBehaviour
{
    public AK.Wwise.Event VictoryMusic;
    public GameObject VictoryCanvas;

    private void Start()
    {
        VictoryMusic.Post(gameObject);

    }
    public void DoVictory()
    {
        VictoryMusic.Post(gameObject);
    }
}
