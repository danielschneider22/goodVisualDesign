using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowTextIfCloseToPlayer : MonoBehaviour
{
    public GameObject player;
    private TextMeshProUGUI text;

    private bool isShowing = true;
    private float timeElapsed;
    private float startValueColor = 0f;
    private float lerpDuration = .5f;

    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        text.color = new Color32(255, 255, 255, 0);
    }

    public void Update()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);
        if (distance < 40 && !isShowing)
        {
            isShowing = true;
            timeElapsed = 0f;
            startValueColor = 0;
        }
        else if (distance > 40 && isShowing)
        {
            isShowing = false;
            timeElapsed = 0f;
            startValueColor = 255;
        }
        if(timeElapsed < lerpDuration)
        {
            timeElapsed += Time.deltaTime;
            float valueToLerpColor = Mathf.Lerp(startValueColor, startValueColor == 255f ? 0f : 255f, timeElapsed / lerpDuration);
            text.color = new Color32(255, 255, 255, (byte)valueToLerpColor);
        }
        

    }
}
