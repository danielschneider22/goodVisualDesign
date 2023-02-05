using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AntennaPickUpManager : MonoBehaviour
{
    public GameObject playerAntenna;
    public GameObject playerSensor;
    public TextMeshProUGUI antennaExplantion;
    public GameObject antennaLight;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
            playerAntenna.SetActive(true);
            antennaExplantion.text = "Use the sensor to\nfind gems in the walls";
            antennaLight.SetActive(false);
            playerSensor.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
