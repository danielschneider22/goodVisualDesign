﻿using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    public Animator transitionDeath;

    public float transitionTime = 1f;
    public float transitionTimeDeath = 1f;

    public static bool isDeathAnimation = false;

    public GameObject player;
    private Vector3 initPlayerPosition;

    public GameObject lava;
    private Vector3 initLavaPosition;
    private BeatsManager beatsManager;

    private void Awake()
    {
        // resetAnimators();
        initPlayerPosition = player.transform.position;
        initLavaPosition = lava.transform.position;
        beatsManager = GameObject.FindObjectOfType<BeatsManager>();
    }

    private void resetAnimators()
    {
        if (isDeathAnimation)
        {
            transition.enabled = false;
            transitionDeath.enabled = true;
        }
        else
        {
            transition.enabled = true;
            transitionDeath.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            LoadNextLevel();
        }
    }

    public void LoadNextLevel()
    {
        AkSoundEngine.StopPlayingID(beatsManager.musicId);
        isDeathAnimation = false;
        StartCoroutine((LoadLevel(SceneManager.GetActiveScene().buildIndex + 1)));
        
    }

    public void ReloadLevelDeath()
    {
        player.GetComponent<PlayerController>().MakeDead();

    }

    public void MakePlayerUndead()
    {
        lava.transform.position = initLavaPosition;
        player.transform.position = initPlayerPosition;
        player.GetComponent<PlayerController>().MakeUndead();
    }

    public void ShowDeathBlackScreenAnimation()
    {
        transitionDeath.SetTrigger("start");
        transitionDeath.enabled = true;

    }

    IEnumerator LoadLevel(int levelIndex, bool isDeath = false)
    {
        resetAnimators();
        if (isDeath)
        {
            transitionDeath.SetTrigger("start");
        } else
        {
            transition.SetTrigger("start");
        }
        
        yield return new WaitForSeconds(isDeath ? transitionTimeDeath : transitionTime);

        SceneManager.LoadScene(levelIndex);
    }
}
