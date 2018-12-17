using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootEnemies : MonoBehaviour
{
    public List<GameObject> enemiesInRange;
    // Start is called before the first frame update
    void Start()
    {
        enemiesInRange = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnEnemyDesroy(GameObject go)
    {
        enemiesInRange.Remove(go);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            enemiesInRange.Add(collision.gameObject);
            EnemyDestructionDelegate enemyDestruction = collision.gameObject.GetComponent<EnemyDestructionDelegate>();
            enemyDestruction.enemyDelegate += OnEnemyDesroy;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            enemiesInRange.Remove(collision.gameObject);
            EnemyDestructionDelegate enemyDestruction = collision.gameObject.GetComponent<EnemyDestructionDelegate>();
            enemyDestruction.enemyDelegate -= OnEnemyDesroy;
        }
    }
}
