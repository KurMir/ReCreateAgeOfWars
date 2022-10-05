using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorMovement : MonoBehaviour
{
  [Header("Warrior Settings")]
  public float cooldownTime;
  private float cooldownTimer;
  public bool isCoolDown; //Cooldown/AttackSpeed not yet implemented
  public bool allyOccupied;
  public bool enemyOccupied;
  private float moveSpeed;
  private Vector2 direction;
  private float directionNumber;
  
  [Header("GameObjects/Transforms")]
  public GameObject enemyRaycastObject;
  public GameObject allyRaycastObject;
  public float enemyRayDistance;
  public float allyRayDistance;
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
    DetectInFrontEnemy();
    DetectInFrontAlly();
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
    WarriorMove(moveSpeed);
  }
  void WarriorMove(float move)
  {
    rb.velocity = new Vector2(move, 0f);
    //transform.Translate(new Vector2(1f, 0f) * move * Time.deltaTime);
  }
  void MeleeAttack()
  {

  }
  private void ShiftCooldown() // For later use
  {
    if (isCoolDown)
    {
      // Cooldown of the skill minus Time.Deltatime
      cooldownTimer -= Time.deltaTime;
      if (cooldownTimer < 0.0f)
      {
        isCoolDown = false;
      }
    }
  }


}
