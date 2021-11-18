using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RoundSystem : MonoBehaviour
{
    public AudioClip clipSom;
    public GameObject[] enemy;
    public GameObject[] ally;
    public GameObject[] Rounds;
    public GameObject[] Rounds2;
    public GameObject[] Rounds3;
    public string[] textRounds;
    public Transform[] posToSpawn;

    public TextMeshProUGUI display;
    public Animator anim;

    public bool inicia;
    public int level;
    public GameObject vitoria;
    public GameObject derrota;
    public GameObject some;

    private int num;
    private bool inGame;
    private bool oneTime;
    EnergyScript energyScript;
    FirstPersonController firstPersonController;
    AudioSource audioSource;
    void Start()
    {
        energyScript = FindObjectOfType<EnergyScript>();
        firstPersonController = FindObjectOfType<FirstPersonController>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        enemy = GameObject.FindGameObjectsWithTag("Enemy");
        ally = GameObject.FindGameObjectsWithTag("Ally");

        if(Input.GetKeyUp(KeyCode.F))
        {
            if (inGame == false)
            {
                inicia = true;
                level++;
                if(level < 3) audioSource.PlayOneShot(clipSom);
                anim.SetTrigger("InicioTrigger");
            }
        }

        if(enemy.Length == 0)
        {
            if (!oneTime)
            {
                inGame = false;
                energyScript.energyValue += 15;
                oneTime = true;
            }
            if(level > 3)
            {
                vitoria.SetActive(true);
                some.SetActive(false);
                firstPersonController.enabled = false;
                Cursor.lockState = CursorLockMode.None;
            }
        }
        else
        {
            if(ally.Length == 0)
            {
                derrota.SetActive(true);
                some.SetActive(false);
                firstPersonController.enabled = false;
                Cursor.lockState = CursorLockMode.None;
            }
            inGame = true;
            oneTime = false;
        }

        if (inicia)
        {
            SpawnAllPoints();
            inicia = false;

        }
        if(level == 1)
        {
            display.text = textRounds[0];
        }
        else if(level == 2)
        {
            display.text = textRounds[1];

        }
        else if(level == 3)
        {
            display.text = textRounds[2];
        }
    }


    void SpawnAllPoints()
    {
        if (level == 1)
        {
            for (var i = 0; i < Rounds.Length; i++)
            {
                //Select From Objects To Spawn            
                Instantiate(Rounds[num], posToSpawn[num].position, transform.rotation);
                num++;
                if (num >= Rounds.Length)
                {
                    num = 0;
                    return;
                }
            }
        }
        else if(level == 2)
        {
            for (var i = 0; i < Rounds2.Length; i++)
            {
                //Select From Objects To Spawn            
                Instantiate(Rounds2[num], posToSpawn[num].position, transform.rotation);
                num++;
                if (num >= Rounds2.Length)
                {
                    num = 0;
                    return;
                }
            }
        } 
        else if(level == 3)
        {
            for (var i = 0; i < Rounds3.Length; i++)
            {
                //Select From Objects To Spawn            
                Instantiate(Rounds3[num], posToSpawn[num].position, transform.rotation);
                num++;
                if (num >= Rounds3.Length)
                {
                    num = 0;
                    return;
                }
            }
        }
    }
}
