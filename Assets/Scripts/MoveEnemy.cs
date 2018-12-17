using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEnemy : MonoBehaviour
{
    public GameObject[] waypoints;
    int currentWaypoint = 0;
    float lastWaypointSwitchTime;
    float speed = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        lastWaypointSwitchTime = Time.time;        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 startPosition = waypoints[currentWaypoint].transform.position;
        Vector3 endPosition = waypoints[currentWaypoint + 1].transform.position;

        float pathLength = Vector3.Distance(startPosition, endPosition);
        float totalTimeForPath = pathLength / speed;
        float currentTimeOnPath = Time.time - lastWaypointSwitchTime;
        gameObject.transform.position = Vector2.Lerp(startPosition, endPosition, currentTimeOnPath / totalTimeForPath);

        if (gameObject.transform.position.Equals(endPosition))
        {
            if(currentWaypoint < waypoints.Length - 2)
            {
                currentWaypoint++;
                lastWaypointSwitchTime = Time.time;
            }
            else
            {
                Destroy(gameObject);

                AudioSource audioSource = gameObject.GetComponent<AudioSource>();
                audioSource.PlayOneShot(audioSource.clip);
            }
        }
    }
}
