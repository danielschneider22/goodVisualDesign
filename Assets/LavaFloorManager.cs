using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaFloorManager : MonoBehaviour
{
    public Transform lavaFloorTransform;
    public GameObject lavaParticle;
    public Transform particleGenerationArea;

    public void Start()
    {
        for(var i = 0; i <= 50; i++ )
        {
            GameObject gameObj = Instantiate(lavaParticle, particleGenerationArea);
            gameObj.transform.position = new Vector3(gameObj.transform.position.x + Random.Range(-50.5f, 15.4f), gameObj.transform.position.y, gameObj.transform.position.z);
        }
    }
    public void ScaleUpLavaFloor()
    {
        lavaFloorTransform.transform.localScale = new Vector3(lavaFloorTransform.transform.localScale.x, lavaFloorTransform.transform.localScale.y + .07f, lavaFloorTransform.transform.localScale.z);
    }
}
