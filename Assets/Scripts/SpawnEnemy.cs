using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject[] waypoints;
    public GameObject enermyPrefab;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(enermyPrefab).GetComponent<MoveEnemy>().waypoints = waypoints;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
