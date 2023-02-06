using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitManager : MonoBehaviour
{
    public LevelLoader levelLoader;
    public GemManager gemManager;
    public bool canGo;
    // Start is called before the first frame update
    void Start()
    {
        levelLoader = GameObject.FindObjectOfType<LevelLoader>();
        gemManager = GameObject.FindObjectOfType<GemManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && gemManager.totalNumGems == gemManager.gemsCollected && canGo)
        {
            levelLoader.LoadNextLevel();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player" && gemManager.totalNumGems == gemManager.gemsCollected)
        {
            canGo = true;
        }
    }
}
