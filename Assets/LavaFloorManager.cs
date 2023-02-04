using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaFloorManager : MonoBehaviour
{
    public Transform lavaFloorTransform;

    public void ScaleUpLavaFloor()
    {
        lavaFloorTransform.transform.localScale = new Vector3(lavaFloorTransform.transform.localScale.x, lavaFloorTransform.transform.localScale.y + .05f, lavaFloorTransform.transform.localScale.z);
    }
}
