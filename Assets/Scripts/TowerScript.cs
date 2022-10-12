using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerScript : MonoBehaviour
{
  [Header("Tower Settings")]
  public float shootTime = 1;
  public float shootTimer;
  public float bulletForce;
  Vector2 Direction;





  [Header("GameObjects and Transforms")]
  public GameObject bullet;
  public Transform DetectEnemy;
  public Transform AttackPoint;
  public float attackRange;

  public LayerMask enemyLayerMask;
  public Transform closestEnemy;

  void Start()
  {
    shootTimer = 0;
    closestEnemy = null;
  }

  void Update()
  {
    closestEnemy = getClosestEnemy();
    AutoShoot();

  }
  public void AutoShoot()
  {
    if (closestEnemy != null)
    {
      shootTimer -= Time.deltaTime;
      if (shootTimer <= 0.0f)
      {
        shootTimer = shootTime;
        Direction = closestEnemy.GetComponent<Rigidbody2D>().position - (Vector2)AttackPoint.position;
        GameObject bulletShoot = Instantiate(bullet, AttackPoint.position, Quaternion.identity);
        bulletShoot.GetComponent<Rigidbody2D>().AddForce(Direction * bulletForce);
      }
    }
  }

  public Transform getClosestEnemy()
  {
    Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(DetectEnemy.position, attackRange, enemyLayerMask);
    float closestDistance = Mathf.Infinity;
    Transform trans = null;

    foreach (Collider2D enemy in hitEnemies)
    {
      float currentDistance;
      currentDistance = Vector3.Distance(DetectEnemy.position, enemy.transform.position);
      if (currentDistance < closestDistance)
      {
        closestDistance = currentDistance;
        trans = enemy.transform;
      }
    }
    return trans;
  }
  void OnDrawGizmosSelected() //Drawing Gizmos
  {
    if (DetectEnemy == null)
      return;
    Gizmos.DrawWireSphere(DetectEnemy.position, attackRange);
  }

}
