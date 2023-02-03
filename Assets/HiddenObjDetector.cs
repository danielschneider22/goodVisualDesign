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
    public bool isGrowing = false;

    float timeElapsed;
    float lerpDuration = .3f;
    float startValue;
    float startValueColor;
    float endValue;
    float valueToLerp;
    float valueToLerpColor;

    float regularSensorX;

    private void Start()
    {
        regularSensorX = sensorSpriteRenderer.transform.localScale.x;
    }
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

        if (timeElapsed < lerpDuration)
        {

            valueToLerp = Mathf.Lerp(startValue, endValue, timeElapsed / lerpDuration);
            valueToLerpColor = Mathf.Lerp(startValueColor, startValueColor == 255 ? 0 : 255, timeElapsed / lerpDuration);
            timeElapsed += Time.deltaTime;
            sensorSpriteRenderer.transform.localScale = new Vector3(valueToLerp, sensorSpriteRenderer.transform.localScale.y, 1);
            sensorSpriteRenderer.color = new Color32(113, 139, 173, (byte)valueToLerpColor);
        }
    }

    public void ExpandRetractSensor()
    {
        isGrowing = !isGrowing;

        if(isGrowing)
        {
            timeElapsed = 0f;
            startValue = regularSensorX;
            endValue = regularSensorX * 1.25f;
            //sensorSpriteRenderer.color = new Color32(131, 181, 154, 255);
            sensorSpriteRenderer.color = new Color32(131, 181, 154, 0);
            startValueColor = 0;
        } else
        {
            timeElapsed = 0f;
            startValue = regularSensorX * 1.25f;
            endValue = regularSensorX;
            sensorSpriteRenderer.color = new Color32(113, 139, 173, 255);
            startValueColor = 255;
        }
        
    }
}
