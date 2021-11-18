using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpawnAllies : MonoBehaviour
{
    public GameObject[] spawnAlly;
    public GameObject particlePivot;
    public Image currentImage;
    public TextMeshProUGUI text;
    public TextMeshProUGUI numPrice;
    public AudioClip audioSpawn;
    AudioSource audioSource;

    private AimScript _AimScript;
    public int numAlly;

    EnergyScript energyScript;

    void Start()
    {
        _AimScript = GetComponent<AimScript>();
        energyScript = FindObjectOfType<EnergyScript>();

        audioSource = GetComponent<AudioSource>();
        text.text = spawnAlly[numAlly].GetComponent<HealthSystem>().nameObj;
        numPrice.text = spawnAlly[numAlly].GetComponent<AllyData>().numToSpawn + "";
        currentImage.sprite = spawnAlly[numAlly].GetComponent<HealthSystem>().imageToy;
    }


    private void Update()
    {

        //if (numAlly >= 0 && numAlly <= spawnAlly.Length - 1)
        //{
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (numAlly == 2)
            {
                numAlly = 0;
                text.text = spawnAlly[numAlly].GetComponent<HealthSystem>().nameObj;
                numPrice.text = spawnAlly[numAlly].GetComponent<AllyData>().numToSpawn + "";
                currentImage.sprite = spawnAlly[numAlly].GetComponent<HealthSystem>().imageToy;


            }
            else
            {
                numAlly++;
                text.text = spawnAlly[numAlly].GetComponent<HealthSystem>().nameObj;
                numPrice.text = spawnAlly[numAlly].GetComponent<AllyData>().numToSpawn + "";
                currentImage.sprite = spawnAlly[numAlly].GetComponent<HealthSystem>().imageToy;

            }


        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (numAlly == 0)
            {
                numAlly = 2;
                text.text = spawnAlly[numAlly].GetComponent<HealthSystem>().nameObj;
                numPrice.text = spawnAlly[numAlly].GetComponent<AllyData>().numToSpawn + "";
                currentImage.sprite = spawnAlly[numAlly].GetComponent<HealthSystem>().imageToy;
            }
            else
            {
                numAlly--;
                text.text = spawnAlly[numAlly].GetComponent<HealthSystem>().nameObj;
                numPrice.text = spawnAlly[numAlly].GetComponent<AllyData>().numToSpawn + "";
                currentImage.sprite = spawnAlly[numAlly].GetComponent<HealthSystem>().imageToy;
            }



        }
        //}
        //else if(numAlly > spawnAlly.Length - 1)
        //{
        //    numAlly = 0;
        //}   
        //else if(numAlly < 0)
        //{
        //    numAlly = spawnAlly.Length - 1;
        //}


    }

    void FixedUpdate()
    {
        if(_AimScript.canSpawn)
        {
            particlePivot.SetActive(true);
            particlePivot.transform.position = _AimScript.spawnPos;

            if (Input.GetMouseButtonUp(0))
            {
                if (spawnAlly[numAlly].GetComponent<AllyData>().numToSpawn <= energyScript.energyValue)
                {
                    int remove = spawnAlly[numAlly].GetComponent<AllyData>().numToSpawn;

                    Instantiate(spawnAlly[numAlly], _AimScript.spawnPos, Quaternion.identity);
                    audioSource.PlayOneShot(audioSpawn);
                    energyScript.energyValue -= remove;
                    _AimScript.canSpawn = false;
                }
                else
                {
                    print("sem Energia");
                }
               
            }
        }
        else
        {
            particlePivot.SetActive(false);
        }
    }
}
