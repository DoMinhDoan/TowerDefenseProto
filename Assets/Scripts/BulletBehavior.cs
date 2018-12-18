using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent (typeof (AudioSource))]
public class BulletBehavior : MonoBehaviour
{
    public BulletConfig bulletConfig;

    private int damage
    {
        get
        {
            return bulletConfig.damage;
        }
    }

    private float speed
    {
        get
        {
            return bulletConfig.speed;
        }
    }

    [HideInInspector]
    [NonSerialized]
    public GameObject target;
    [HideInInspector]
    [NonSerialized]
    public Vector3 startPosition;
    [HideInInspector]
    [NonSerialized]
    public Vector3 targetPosition;

    float distance;
    float startTime;
    //GameManagerBehavior gameManager;
    // Start is called before the first frame update
    void Start()
    {
        //gameManager = GameObject.Find("GameManager").GetComponent<GameManagerBehavior>();
        startTime = Time.time;
        distance = Vector2.Distance(startPosition, targetPosition);
    }

    // Update is called once per frame
    void Update()
    {
        float timeInterval = Time.time - startTime;
        gameObject.transform.position = Vector3.Lerp(startPosition, targetPosition, timeInterval * speed / distance);

        if(gameObject.transform.position.Equals(targetPosition))
        {
            // deduct enemy damage
            if(target != null)
            {
                Transform healthTransfrom = target.transform.Find("HealthBar");
                HealthBar healthBar = healthTransfrom.GetComponent<HealthBar>();
                healthBar.currentHealth -= Mathf.Max(damage, 0);
                
                if(healthBar.currentHealth <= 0)    // die
                {
                    Destroy(target);

                    AudioSource audioSource = gameObject.GetComponent<AudioSource>();
                    audioSource.PlayOneShot(audioSource.clip);
                }
            }

            Destroy(gameObject);
        }
    }
}
