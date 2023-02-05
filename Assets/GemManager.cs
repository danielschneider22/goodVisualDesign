using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GemManager : MonoBehaviour
{
    public int totalNumGems;
    public int gemsCollected;
    public TextMeshProUGUI exitText;
    // Start is called before the first frame update
    void Start()
    {
        UpdateText();
    }

    private void UpdateText()
    {
        exitText.text = "Requires " + (totalNumGems - gemsCollected) + " more\ngems to open";
    }

    public void CollectGem()
    {
        gemsCollected = gemsCollected + 1;
    }
}
