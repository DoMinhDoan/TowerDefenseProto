using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class MonsterLevel
{
    public int cost;
    public GameObject visualization;

    public GameObject bullet;
    public float fireRate;
}


public class MonsterData : MonoBehaviour
{
    public List<MonsterLevel> levels;
    MonsterLevel currentLevel;

    public MonsterLevel CurrentLevel
    {
        get
        {
            return currentLevel;
        }
        set
        {
            currentLevel = value;

            int currentLevelIndex = levels.IndexOf(currentLevel);
            GameObject go = levels[currentLevelIndex].visualization;
            for(int i = 0; i < levels.Count; i++)
            {
                if(go != null)
                {
                    if (i == currentLevelIndex)
                    {
                        levels[i].visualization.SetActive(true);                    
                    }
                    else
                    {
                        levels[i].visualization.SetActive(false);
                    }
                }                
            }
        }
    }

    void OnEnable()
    {
        CurrentLevel = levels[0];
    }

    public MonsterLevel GetNextLevel()
    {
        int currentLevelIndex = levels.IndexOf(currentLevel);
        int maxLevel = levels.Count - 1;
        if(currentLevelIndex < maxLevel)
        {
            return levels[currentLevelIndex + 1];
        }
        else
        {
            return null;
        }
    }

    public void IncreaseLevel()
    {
        int currentLevelIndex = levels.IndexOf(currentLevel);
        int maxLevel = levels.Count - 1;
        if (currentLevelIndex < maxLevel)
        {
            CurrentLevel = levels[currentLevelIndex + 1];
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
