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

  [Header("GameObjects/Transforms")]
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
    }
  }
  void Update()
  {
    DetectInRangedEnemy();
    DetectInFrontAlly();
    DetectInFrontEnemy();
  }

  void DetectInRangedEnemy()
  {
    RaycastHit2D EnemyInRangeHit = Physics2D.Raycast(enemyInRangeRaycastObject.transform.position, direction * new Vector2(directionNumber, 0f), enemyInRangeRayDistance, enemyLayerMask);
    if (EnemyInRangeHit.collider != null)
    {
      allyOccupied = true;
      if (this.gameObject.tag == "P2") { EnemyInRangeHit.distance = -EnemyInRangeHit.distance; } // For Test/Debug
      Debug.DrawRay(enemyInRangeRaycastObject.transform.position, direction * EnemyInRangeHit.distance * new Vector2(directionNumber, 0f), Color.green);
      isAttackingRange = true;
      ArcherAttack();
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
    RaycastHit2D EnemyHit = Physics2D.Raycast(enemyRaycastObject.transform.position, direction * new Vector2(directionNumber, 0f), enemyRayDistance, enemyLayerMask);
    if (EnemyHit.collider != null)
    {
      if (this.gameObject.tag == "P2") { EnemyHit.distance = -EnemyHit.distance; } // For Test/Debug
      Debug.DrawRay(enemyRaycastObject.transform.position, direction * EnemyHit.distance * new Vector2(directionNumber, 0f), Color.red);
      moveSpeed = 0f;
      MeleeAttack();
    }
    else
    {
      if (!allyOccupied)
      {
        moveSpeed = (this.gameObject.tag == "P2") ? -0.3f : 0.3f;
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
    //
  }

  void MeleeAttack()
  {

  }


}
