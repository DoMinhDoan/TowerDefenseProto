using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceMonster : MonoBehaviour
{
    public GameObject monsterPrefab;
    GameObject monster;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    bool CanReplaceMonster()
    {
        return monster == null;
    }

    void OnMouseUp()
    {
        if(CanReplaceMonster())
        {
            monster = Instantiate(monsterPrefab, this.transform.position, Quaternion.identity);

            AudioSource audioSource = gameObject.GetComponent<AudioSource>();
            audioSource.PlayOneShot(audioSource.clip);
        }
    }
}
