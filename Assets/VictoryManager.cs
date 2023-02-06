using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryManager : MonoBehaviour
{
    public AK.Wwise.Event VictoryMusic;

    private void Start()
    {
        VictoryMusic.Post(gameObject);

    }
}
