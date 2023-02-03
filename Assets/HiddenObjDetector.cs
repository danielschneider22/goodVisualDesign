using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HiddenObjDetector : MonoBehaviour
{
    public Transform listOfHiddenObjs;
    public TextMeshProUGUI distanceText;

    public List<Sprite> sensorSprites;
    public SpriteRenderer sensorSpriteRenderer;

    private void Update()
    {
        float minDist = 1333333330f;
        GameObject closestObj = null;
        foreach(Transform child in listOfHiddenObjs)
        {
            float distance = Vector3.Distance(transform.position, child.position);
            if(distance < minDist)
            {
                minDist = distance;
                closestObj = child.gameObject;
            }
        }
        distanceText.text = minDist.ToString() ;
        if(minDist < 10)
        {
            sensorSpriteRenderer.sprite = sensorSprites[0];
            sensorSpriteRenderer.enabled = true;
        }
        else if (minDist < 11)
        {
            sensorSpriteRenderer.sprite = sensorSprites[1];
            sensorSpriteRenderer.enabled = true;
        }
        else if (minDist < 12)
        {
            sensorSpriteRenderer.sprite = sensorSprites[2];
            sensorSpriteRenderer.enabled = true;
        }
        else if (minDist < 12.8)
        {
            sensorSpriteRenderer.sprite = sensorSprites[3];
            sensorSpriteRenderer.enabled = true;
        }
        else if (minDist < 13.5)
        {
            sensorSpriteRenderer.sprite = sensorSprites[4];
            sensorSpriteRenderer.enabled = true;
        }
        else
        {
            sensorSpriteRenderer.enabled = false;
        }
        
        Vector3 targ = closestObj.transform.position;
        targ.z = 0f;

        Vector3 objectPos = sensorSpriteRenderer.transform.position;
        targ.x = targ.x - objectPos.x;
        targ.y = targ.y - objectPos.y;

        float angle = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg;
        sensorSpriteRenderer.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
}
