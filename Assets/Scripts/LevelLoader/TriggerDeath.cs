using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDeath : MonoBehaviour
{
    public LevelLoader levelLoader;

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Q))
        {
            levelLoader.ReloadLevelDeath();
        }
    }

    //When the Primitive collides with the walls, it will reverse direction
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
            levelLoader.ReloadLevelDeath();
        }
    }
}
