using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MidRangeMovemnet : BaseMovement
{

  public Vector2 directionArcher;
  public bool isAttackingRange;
  public float arrowAttkCooldownTime;
  public float arrowAttkCooldownTimer;
  public float arrowWaitTime;
  public float arrowWaitTimer;
  public bool isReadyToShoot;

  [Header("GameObjects/Transforms")]
  public AudioSource source;
  public AudioClip meleeClip, bowChargeClip, bowReleaseClip;
  public GameObject Arrow;
  public Transform AttackPoint;
  public Transform ArrowAttackPoint;
  public GameObject archerUnit;
  public GameObject enemyInRangeRaycastObject;
  public float enemyInRangeRayDistance; // adjust Enemy Ray Distance (Range/Bow)

  new public void Start()
  {
    arrowWaitTimer = 0;
    arrowAttkCooldownTimer = 0;
    if (this.gameObject.tag == "P1")
    {
      directionArcher = Vector2.right;
    }


    if (this.gameObject.tag == "P2")
    {
      enemyRayDistance *= -1;
      allyRayDistance *= -1;
      enemyInRangeRayDistance *= -1;
      directionArcher = Vector2.left;

    }
  }
  new public void Update()
  {
    ArrowWaitCooldown();
    DetectInRangedEnemy();
  }

  void DetectInRangedEnemy()
  {
    RaycastHit2D EnemyInRangeHit = Physics2D.Raycast(enemyInRangeRaycastObject.transform.position, direction * new Vector2(directionArcher.x, 0f), enemyInRangeRayDistance, enemyLayerMask);
    if (EnemyInRangeHit.collider != null)
    {
      if (this.gameObject.tag == "P2") { EnemyInRangeHit.distance = -EnemyInRangeHit.distance; } // For Test/Debug
      Debug.DrawRay(enemyInRangeRaycastObject.transform.position, direction * EnemyInRangeHit.distance * new Vector2(directionArcher.x, 0f), Color.green);
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
  void ArcherAttack()
  {
    arrowAttkCooldownTimer -= Time.deltaTime;
    if (arrowAttkCooldownTimer <= 0.0f)
    {
      isReadyToShoot = true;
      arrowWaitTimer = arrowWaitTime;
      unitAnimator.SetTrigger("Ranged");
      arrowWaitTimer = arrowWaitTime;
      source.PlayOneShot(bowChargeClip);

    }
  }

  void ArrowWaitCooldown()
  {
    if (isReadyToShoot)
    {
      arrowWaitTimer -= Time.deltaTime;
      if (arrowWaitTimer <= 0.0f)
      {
        source.PlayOneShot(bowReleaseClip);
        Instantiate(Arrow, ArrowAttackPoint.position, Quaternion.identity);
        isReadyToShoot = false;
      }
    }


  }

}
