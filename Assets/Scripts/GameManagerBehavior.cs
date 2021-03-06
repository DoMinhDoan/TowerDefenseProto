﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class GameManagerBehavior : MonoBehaviour
{
    public GameObject goldLabel;
    public GameObject waveLabel;
    public GameObject healthLabel;

    public GameObject[] nextWaveLabels;    
    public GameObject[] healthIndicator;

    public bool isGameOver = false;

    int gold;
    public int Gold
    {
        get
        {
            return gold;
        }
        set
        {
            gold = value;
            goldLabel.GetComponent<TextMeshPro>().text = "GOLD: " + gold.ToString();
        }
    }

    int wave;
    public int Wave
    {
        get
        {
            return wave;
        }
        set
        {
            wave = value;

            if(!isGameOver)
            {
                foreach(var go in nextWaveLabels)
                {
                    go.GetComponent<Animator>().SetTrigger("nextWave");
                }
            }

            waveLabel.GetComponent<TextMeshPro>().text = "WAVE: " + (wave + 1).ToString();
        }
    }

    int health;
    public int Health
    {
        get
        {
            return health;
        }
        set
        {
            if(value < health)
            {
                Camera.main.GetComponent<CameraShake>().Shake();
            }

            health = value;

            if (!isGameOver && health <= 0)
            {
                isGameOver = true;
                GameObject.FindGameObjectWithTag("GameOver").GetComponent<Animator>().SetBool("gameOver", true);
            }
            {
                for(int i = 0; i< healthIndicator.Length; i++)
                {
                    if(i < health)
                    {
                        healthIndicator[i].SetActive(true);
                    }
                    else
                    {
                        healthIndicator[i].SetActive(false);
                    }
                }
            }

            healthLabel.GetComponent<TextMeshPro>().text = "HEALTH: " + (health).ToString();
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        Gold = 10000;
        Wave = 0;
        Health = 5;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
