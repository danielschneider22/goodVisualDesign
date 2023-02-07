using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public AK.Wwise.Event RegularHit;
    public AK.Wwise.Event WinningHit;
    public AK.Wwise.Event DoorOpen;


    public Transform gemList;
 
    void Start()
    {
        UpdateText();
    }

    private void UpdateText()
    {
        if(totalNumGems - gemsCollected <= 0)
        {
            if(SceneManager.GetActiveScene().buildIndex == 1)
            {
                exitText.text = "Press E\n To win the game!";
            } else
            {
                exitText.text = "Press E\n To go to next level!";
            }
            
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
        activeGem.GetComponent<SpriteRenderer>().enabled = true;
        activeGem.GetComponent<MoveUpAndDown>().enabled = true;
        hitsLeftText.text = activeGem.hitsLeft.ToString() + " hits left";
    }

    public void HideMiniGame()
    {
        miniGameCanvas.SetActive(false);
    }
    public void HadSuccess()
    {
        if(activeGem != null)
        {
            activeGem.hitsLeft = activeGem.hitsLeft - 1;
            hitsLeftText.text = activeGem.hitsLeft.ToString() + " hits left";


            if (activeGem.hitsLeft == 0)
            {
                WinningHit.Post(gameObject);
                gemsCollected = gemsCollected + 1;
                gemList.GetChild(gemsCollected - 1).gameObject.SetActive(true);
                UpdateText();
                miniGameCanvas.SetActive(false);
                activeGem.isDestroyed = true;
                Destroy(activeGem.gameObject);
                activeGem = null;

                if (totalNumGems <= gemsCollected)
                {
                    DoorOpen.Post(gameObject);
                    doorTop.sprite = topOpenDoor;
                    doorBottom.sprite = bottomDoor;
                    hitsLeftText.text = "8 hits left";
                }

            }
            else
            {
                RegularHit.Post(gameObject);
            }
        }
        
    }
}
