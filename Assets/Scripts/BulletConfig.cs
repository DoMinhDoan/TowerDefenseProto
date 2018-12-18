using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Bullet", fileName = "Bullet.asset")]
public class BulletConfig : ScriptableObject
{
    public float speed;
    public int damage;
}
