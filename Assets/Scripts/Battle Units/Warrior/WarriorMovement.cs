using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorMovement : MonoBehaviour
{
  [Header("Warrior Settings")]
  public float HealthTest = 100;
  public float attackCooldownTime = 3;
  public float attkCooldownTimer;
  public bool allyOccupied;
  public bool enemyOccupied;
  private float moveSpeed;
  private Vector2 direction;
  private float directionNumber;

  [Header("GameObjects/Transforms")]
  public Transform AttackPoint;
  public float attackRange;
  public GameObject EnemyRaycastObject;
  public GameObject AllyRaycastObject;
  public GameObject WarriorUnit;
  public float enemyRayDistance;
  public float allyRayDistance;
  public LayerMask enemyLayerMask;
  public LayerMask allyLayerMask;
  private Rigidbody2D rb;

  void Awake()
  {
    attkCooldownTimer = 0f;
    allyOccupied = false;
    rb = this.gameObject.GetComponent<Rigidbody2D>();
    rb.constraints = RigidbodyConstraints2D.FreezePositionY;
  }

  void Start()
  {
    if (this.gameObject.tag == "P1")
    {
      direction = Vector2.right;
      directionNumber = 1f;
    }
    if (this.gameObject.tag == "P2")
    {
      direction = Vector2.left;
      directionNumber = -1f;
      enemyRayDistance = enemyRayDistance * -1;
      allyRayDistance = allyRayDistance * -1;
    }
  }

  void Update()
  {
    DetectInFrontEnemy();
    DetectInFrontAlly();
  }
  void DetectInFrontAlly()
  {
    RaycastHit2D AllyHit = Physics2D.Raycast(AllyRaycastObject.transform.position, direction * new Vector2(directionNumber, 0f), allyRayDistance, allyLayerMask);
    if (AllyHit.collider != null)
    {
      allyOccupied = true;
      if (this.gameObject.tag == "P2") { AllyHit.distance = -AllyHit.distance; } // For Test/Debug
      Debug.DrawRay(AllyRaycastObject.transform.position, direction * AllyHit.distance * new Vector2(directionNumber, 0f), Color.blue);
      moveSpeed = 0f;
    }
    else
    {
      allyOccupied = false;
      if (!allyOccupied)
      {
        moveSpeed = (this.gameObject.tag == "P2") ? -0.3f : 0.3f;
      }
    }
  }
  void DetectInFrontEnemy()
  {
    RaycastHit2D EnemyHit = Physics2D.Raycast(EnemyRaycastObject.transform.position, direction * new Vector2(directionNumber, 0f), enemyRayDistance, enemyLayerMask);
    if (EnemyHit.collider != null)
    {
      if (this.gameObject.tag == "P2") { EnemyHit.distance = -EnemyHit.distance; } // For Test/Debug
      Debug.DrawRay(EnemyRaycastObject.transform.position, direction * EnemyHit.distance * new Vector2(directionNumber, 0f), Color.red);
      moveSpeed = 0f;
      enemyOccupied = true;
      if (enemyOccupied)
      {
        MeleeAttack();
      }
    }
    else
    {
      enemyOccupied = false;
      if (!allyOccupied)
      {
        moveSpeed = (this.gameObject.tag == "P2") ? -0.3f : 0.3f;
      }

    }
    WarriorMove(moveSpeed);
  }

  void WarriorMove(float move)
  {
    rb.velocity = new Vector2(move, 0f);
    //transform.Translate(new Vector2(1f, 0f) * move * Time.deltaTime);
  }

  void MeleeAttack()
  {

    attkCooldownTimer -= Time.deltaTime;
    if (attkCooldownTimer <= 0.0f)
    {
      attkCooldownTimer = attackCooldownTime;
      Animator anim = WarriorUnit.GetComponent<Animator>();
      anim.SetTrigger("Attack");

      Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(AttackPoint.position, attackRange, enemyLayerMask);

      foreach (Collider2D enemy in hitEnemies)
      {
        enemy.GetComponent<WarriorMovement>().Damage(3);
      }
    }
  }

  void OnDrawGizmosSelected()
  {
    if (AttackPoint == null)
      return;
    Gizmos.DrawWireSphere(AttackPoint.position, attackRange);
  }
  public void Damage(float dmg)
  {
    HealthTest -= dmg;
    if (HealthTest <= 0) { Debug.Log("Dead"); Destroy(gameObject); } // Change to death animation, add gold to player/enemy
  }
}
