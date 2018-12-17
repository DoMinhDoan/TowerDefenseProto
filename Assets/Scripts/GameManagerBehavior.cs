using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerBehavior : MonoBehaviour
{
    public Text goldLabel;
    public Text waveLabel;
    public GameObject[] nextWaveLabels;
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
            goldLabel.text = "GOLD: " + gold.ToString();
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

            goldLabel.text = "WAVE: " + (gold + 1).ToString();
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        Gold = 1000;
        Wave = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
