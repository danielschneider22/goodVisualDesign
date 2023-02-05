using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaFloorManager : MonoBehaviour
{
    public Transform lavaFloorTransform;
    public GameObject lavaParticle;

    public void ScaleUpLavaFloor()
    {
        // lavaFloorTransform.transform.localScale = new Vector3(lavaFloorTransform.transform.localScale.x, lavaFloorTransform.transform.localScale.y + .2f, lavaFloorTransform.transform.localScale.z);
        lavaFloorTransform.transform.position = new Vector3(lavaFloorTransform.position.x, lavaFloorTransform.transform.position.y + .3f, lavaFloorTransform.transform.position.z);
    }
}
