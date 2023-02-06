using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaFloorManager : MonoBehaviour
{
    public Transform lavaFloorTransform;
    public GameObject lavaParticle;
    public Transform player;

    public void ScaleUpLavaFloor()
    {       
        float distance = Vector3.Distance(lavaFloorTransform.GetChild(0).position, player.position);
        lavaFloorTransform.transform.position = new Vector3(lavaFloorTransform.position.x, lavaFloorTransform.transform.position.y + (distance > 70 ? 10f : 1f), lavaFloorTransform.transform.position.z);
    }
}
