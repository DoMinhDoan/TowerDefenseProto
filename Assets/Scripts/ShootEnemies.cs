using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootEnemies : MonoBehaviour
{
    public List<GameObject> enemiesInRange;

    float lastShotTime;
    MonsterData monsterData;

    // Start is called before the first frame update
    void Start()
    {
        enemiesInRange = new List<GameObject>();

        lastShotTime = Time.time;
        monsterData = gameObject.GetComponentInChildren<MonsterData>();
    }

    // Update is called once per frame
    void Update()
    {
        GameObject target = null;

        // find nearest enemy
        float minimalEnemyDistance = float.MaxValue;
        foreach(var enemy in enemiesInRange)
        {
            float distanceToGold = enemy.GetComponent<MoveEnemy>().DistanceToGold();
            if(distanceToGold < minimalEnemyDistance)
            {
                minimalEnemyDistance = distanceToGold;
                target = enemy;
            }
        }

        // shooting the target
        if (target != null)
        {
            if(Time.time - lastShotTime > monsterData.CurrentLevel.fireRate)
            {
                lastShotTime = Time.time;
                Shoot(target.GetComponent<Collider2D>());
            }

            // correct direction
            Vector3 startPosition = gameObject.transform.position;
            Vector3 targetPosition = target.transform.position;

            Vector3 direction = startPosition - targetPosition;
            //Vector3 direction = targetPosition - startPosition;
            float rotation =  Mathf.Atan2(direction.y, direction.x) * 180 / Mathf.PI;
            gameObject.transform.rotation = Quaternion.AngleAxis(rotation, Vector3.forward);
        }       
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

    void Shoot(Collider2D target)
    {
        GameObject bulletPrefab = monsterData.CurrentLevel.bullet;

        GameObject bullet = Instantiate(bulletPrefab, gameObject.transform.position, Quaternion.identity);

        // Init bullet behavior value
        BulletBehavior bulletBehavior = bullet.GetComponent<BulletBehavior>();
        bulletBehavior.target = target.gameObject;
        bulletBehavior.startPosition = gameObject.transform.position;
        bulletBehavior.targetPosition = target.transform.position;

        // reset z value
        bulletBehavior.startPosition.z = bullet.transform.position.z;
        bulletBehavior.targetPosition.z = bullet.transform.position.z;

        // animation
        Animator animator = monsterData.CurrentLevel.visualization.GetComponent<Animator>();
        animator.SetTrigger("fireShot");

        // sound play
        AudioSource audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.PlayOneShot(audioSource.clip);
    }
}
