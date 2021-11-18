using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnergyScript : MonoBehaviour
{
    public int energyValue;
    public int energyMax;

    public TextMeshProUGUI display; 
    void Start()
    {
    }

    void Update()
    {
        display.text = energyValue + "/" + energyMax;


        if (energyValue > energyMax)
        {
            energyValue = energyMax;
        }
        if(energyValue < 0)
        {
            energyValue = 0;
        }
    }
}
