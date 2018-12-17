using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceMonster : MonoBehaviour
{
    public GameObject monsterPrefab;
    GameObject monster;

    GameManagerBehavior gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManagerBehavior>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    bool CanReplaceMonster()
    {
        return monster == null && gameManager.Gold > monsterPrefab.GetComponent<MonsterData>().levels[0].cost;
    }

    void OnMouseUp()
    {
        if(CanReplaceMonster())
        {
            monster = Instantiate(monsterPrefab, this.transform.position, Quaternion.identity);

            // deduct GOLD
            gameManager.Gold -= monster.GetComponent<MonsterData>().CurrentLevel.cost;

            AudioSource audioSource = gameObject.GetComponent<AudioSource>();
            audioSource.PlayOneShot(audioSource.clip);
        }
        else if(CanUpgradeMonster())
        {
            monster.GetComponent<MonsterData>().IncreaseLevel();

            // deduct GOLD
            gameManager.Gold -= monster.GetComponent<MonsterData>().CurrentLevel.cost;

            AudioSource audioSource = gameObject.GetComponent<AudioSource>();
            audioSource.PlayOneShot(audioSource.clip);
        }
    }

    bool CanUpgradeMonster()
    {
        if(monster != null)
        {
            MonsterData monsterData = monster.GetComponent<MonsterData>();
            MonsterLevel nextMonster = monsterData.GetNextLevel();
            
            if (nextMonster != null && gameManager.Gold > nextMonster.cost)
            {
                return true;
            }
        }
        return false;
    }
}
