using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDeath : MonoBehaviour
{
    private LevelLoader levelLoader;

    private void Start()
    {
        levelLoader = GameObject.FindObjectOfType<LevelLoader>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
            levelLoader.ReloadLevelDeath();
        }
    }
}
