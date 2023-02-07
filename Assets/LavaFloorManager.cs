using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaFloorManager : MonoBehaviour
{
    public Transform lavaFloorTransform;
    public GameObject lavaParticle;
    public Transform player;
    public bool stopLava;

    public void ScaleUpLavaFloor()
    {       
        if(stopLava)
        {
            return;
        }
        float distance = Vector3.Distance(lavaFloorTransform.GetChild(0).position, player.position);
        lavaFloorTransform.transform.position = new Vector3(lavaFloorTransform.position.x, lavaFloorTransform.transform.position.y + (distance > 70 ? 10f : 1.25f), lavaFloorTransform.transform.position.z);
    }
}
