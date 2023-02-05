using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathAnimatorScript : MonoBehaviour
{
    public LevelLoader levelLoader;

    public void DoUndeath()
    {
        levelLoader.MakePlayerUndead();
    }
}
