using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GemManager : MonoBehaviour
{
    public int totalNumGems;
    public int gemsCollected;
    public TextMeshProUGUI exitText;
    public GameObject miniGameCanvas;
    public PlayerController playerController;
    public CollectCoin activeGem;
    public TextMeshProUGUI hitsLeftText;

    public SpriteRenderer doorTop;
    public SpriteRenderer doorBottom;
    public Sprite topOpenDoor;
    public Sprite bottomDoor;

    public Transform gemList;
 
    void Start()
    {
        UpdateText();
    }

    private void UpdateText()
    {
        if(totalNumGems - gemsCollected == 0)
        {
            exitText.text = "Press E\n To go to next level!";
        } else
        {
            exitText.text = "Requires " + (totalNumGems - gemsCollected) + " more\ngems to open";
        }
        
    }

    public void CollectGem()
    {
        gemsCollected = gemsCollected + 1;
    }
    
    public void ShowMiniGame(CollectCoin gem)
    {
        miniGameCanvas.SetActive(true);
        activeGem = gem;
    }

    public void HideMiniGame()
    {
        miniGameCanvas.SetActive(false);
    }
    public void HadSuccess()
    {
        activeGem.hitsLeft = activeGem.hitsLeft - 1;
        hitsLeftText.text = activeGem.hitsLeft.ToString() + " hits left";
        if(activeGem.hitsLeft == 0 )
        {
            gemsCollected = gemsCollected + 1;
            gemList.GetChild(gemsCollected - 1).gameObject.SetActive(true);
            UpdateText();
            miniGameCanvas.SetActive(false);
            activeGem.isDestroyed = true;
            Destroy(activeGem.gameObject);
            activeGem = null;

            if(totalNumGems == gemsCollected)
            {
                doorTop.sprite = topOpenDoor;
                doorBottom.sprite = bottomDoor;
            }
            
        }
    }
}
