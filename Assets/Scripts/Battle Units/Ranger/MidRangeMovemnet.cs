using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MidRangeMovemnet : MonoBehaviour
{
  [Header("Ranger Settings")]
  public bool allyOccupied;
  public bool enemyOccupied;
  private float moveSpeed;
  private Vector2 direction;
  private float directionNumber;
  public bool isAttackingRange;
  public float meleeAttackCooldownTime = 2.0f;
  public float meleeAttkCooldownTimer;
  public float attackCooldownTime = 1.5f;
  public float attkCooldownTimer;
  public float arrowWaitTime = 0.7f;
  public float arrowWaitTimer;
  public bool isReadyToShoot;

  [Header("GameObjects/Transforms")]
  public GameObject Arrow;
  public Transform AttackPoint;
  public Transform ArrowAttackPoint;
  public float attackRange;
  public GameObject archerUnit;
  public GameObject enemyRaycastObject;
  public GameObject allyRaycastObject;
  public GameObject enemyInRangeRaycastObject;
  public float enemyRayDistance; // adjust Enemy Ray Distance (melee)
  public float allyRayDistance; // adjust Ally Ray Distance
  public float enemyInRangeRayDistance; // adjust Enemy Ray Distance (Range/Bow)
  public LayerMask enemyLayerMask;
  public LayerMask allyLayerMask;
  private Rigidbody2D rb;

  void Awake()
  {
    attkCooldownTimer = 0f;
    arrowWaitTimer = 0;
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
      enemyRayDistance *= -1;
      allyRayDistance *= -1;
      enemyInRangeRayDistance *= -1;
    }
  }
  void Update()
  {
    ArrowWaitCooldown();
    DetectInFrontAlly();
    DetectInFrontEnemy();
    DetectInRangedEnemy();
  }

  void DetectInRangedEnemy()
  {
    RaycastHit2D EnemyInRangeHit = Physics2D.Raycast(enemyInRangeRaycastObject.transform.position, direction * new Vector2(directionNumber, 0f), enemyInRangeRayDistance, enemyLayerMask);
    if (EnemyInRangeHit.collider != null)
    {
      if (this.gameObject.tag == "P2") { EnemyInRangeHit.distance = -EnemyInRangeHit.distance; } // For Test/Debug
      Debug.DrawRay(enemyInRangeRaycastObject.transform.position, direction * EnemyInRangeHit.distance * new Vector2(directionNumber, 0f), Color.green);
      isAttackingRange = true;
      if (!enemyOccupied)
      {
        if (isAttackingRange)
        {
          ArcherAttack();
        }
      }
    }
    else
    {
      isAttackingRange = false;
    }
  }
  void DetectInFrontAlly()
  {
    RaycastHit2D AllyHit = Physics2D.Raycast(allyRaycastObject.transform.position, direction * new Vector2(directionNumber, 0f), allyRayDistance, allyLayerMask);
    if (AllyHit.collider != null)
    {
      allyOccupied = true;
      if (this.gameObject.tag == "P2") { AllyHit.distance = -AllyHit.distance; } // For Test/Debug
      Debug.DrawRay(allyRaycastObject.transform.position, direction * AllyHit.distance * new Vector2(directionNumber, 0f), Color.blue);
      archerUnit.GetComponent<Animator>().enabled = false;
      archerUnit.GetComponent<Animator>().enabled = true;
      moveSpeed = 0f;
    }
    else
    {
      allyOccupied = false;
      if (!allyOccupied)
      {
        moveSpeed = (this.gameObject.tag == "P2") ? -0.6f : 0.6f;
      }
    }
  }
  void DetectInFrontEnemy()
  {
    RaycastHit2D EnemyHit = Physics2D.Raycast(enemyRaycastObject.transform.position, direction * new Vector2(directionNumber, 0f), enemyRayDistance, enemyLayerMask);
    if (EnemyHit.collider != null)
    {
      if (this.gameObject.tag == "P2") { EnemyHit.distance = -EnemyHit.distance; } // For Test/Debug
      Debug.DrawRay(enemyRaycastObject.transform.position, direction * EnemyHit.distance * new Vector2(directionNumber, 0f), Color.red);
      archerUnit.GetComponent<Animator>().enabled = false;
      archerUnit.GetComponent<Animator>().enabled = true;
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
        Animator anim = archerUnit.GetComponent<Animator>();
        anim.SetTrigger("Walk");
        moveSpeed = (this.gameObject.tag == "P2") ? -0.6f : 0.6f;

      }

    }
    ArcherMove(moveSpeed);
  }

  void ArcherMove(float move)
  {
    rb.velocity = new Vector2(move, 0f);
  }

  void ArcherAttack()
  {
    attkCooldownTimer -= Time.deltaTime;
    if (attkCooldownTimer <= 0.0f)
    {
      isReadyToShoot = true;
      arrowWaitTimer = arrowWaitTime;

      attkCooldownTimer = attackCooldownTime;
      Animator anim = archerUnit.GetComponent<Animator>();
      anim.SetTrigger("Ranged");
    }
  }

  void ArrowWaitCooldown()
  {
    if (isReadyToShoot)
    {
      arrowWaitTimer -= Time.deltaTime;
      if (arrowWaitTimer <= 0.0f)
      {
        Instantiate(Arrow, ArrowAttackPoint.position, Quaternion.identity);
        isReadyToShoot = false;
      }
    }


  }

  void MeleeAttack()
  {
    meleeAttkCooldownTimer -= Time.deltaTime;
    if (meleeAttkCooldownTimer <= 0.0f)
    {
      meleeAttkCooldownTimer = meleeAttackCooldownTime;
      Animator anim = archerUnit.GetComponent<Animator>();
      anim.SetTrigger("Attack");
      Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(AttackPoint.position, attackRange, enemyLayerMask);

      foreach (Collider2D enemy in hitEnemies)
      {
        enemy.GetComponent<DamageScript>().DamageDealt(5);
      }
    }
  }

  void OnDrawGizmosSelected() //Drawing Gizmos
  {
    if (AttackPoint == null)
      return;
    Gizmos.DrawWireSphere(AttackPoint.position, attackRange);
  }


}
