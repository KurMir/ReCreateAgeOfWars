using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MidRangeMovemnet : BaseMovement
{

  public Vector2 directionArcher;
  public bool isAttackingRange;
  public float arrowAttkCooldownTime = 2f;
  public float arrowAttkCooldownTimer = 0f;
  public float arrowReleaseTime = 2;
  public float arrowReleaseTimer = 0f;
  public bool isReadyToShoot;
  public bool isReadyToRelease;

  [Header("GameObjects/Transforms")]
  public AudioSource source;
  public AudioClip bowChargeClip, bowReleaseClip;
  public GameObject Arrow;
  public Transform ArrowAttackPoint;
  public GameObject archerUnit;
  public GameObject enemyInRangeRaycastObject;
  public float enemyInRangeRayDistance; // adjust Enemy Ray Distance (Range/Bow)

  new public void Start()
  {
    isReadyToRelease = false;
    isReadyToShoot = true;
    this.unitAnimator.GetComponent<Animator>();
    arrowAttkCooldownTimer = arrowAttkCooldownTime;
    attackAvailable = true;
    allyOccupied = false;
    enemyOccupied = false;
    
    closestEnemy = null;
    
    if (this.gameObject.tag == "P1")
    {
      direction = Vector2.right;
      allyLayerMask = gameSettings.allyLayerMask;
      enemyLayerMask = gameSettings.enemyLayerMask;
    }
    if (this.gameObject.tag == "P2")
    {
      direction = Vector2.left;
      enemyRayDistance = enemyRayDistance * -1;
      allyRayDistance = allyRayDistance * -1;
      allyLayerMask = gameSettings.enemyLayerMask;
      enemyLayerMask = gameSettings.allyLayerMask;
    }
    arrowAttkCooldownTimer = 0f;

    if (this.gameObject.tag == "P2")
    {
      enemyInRangeRayDistance = enemyInRangeRayDistance * -1;
    }
  }
  new public void Update()
  {
    DetectInRangedEnemy();
    ArrowRelease();
    bool hasAllyInFront = HasAllyInFront();
    bool hasEnemyInFront = HasEnemyInFront();
    this.UnitMove(hasAllyInFront, hasEnemyInFront);
  }

  void DetectInRangedEnemy()
  {
    float directionValue = this.GetDirectionValue();
    RaycastHit2D EnemyInRangeHit = Physics2D.Raycast(
      enemyInRangeRaycastObject.transform.position, 
      this.direction * new Vector2(directionValue, 0f), 
      enemyInRangeRayDistance, 
      this.enemyLayerMask);
    if (EnemyInRangeHit.collider != null)
    {
     
      if (this.gameObject.tag == "P2") { EnemyInRangeHit.distance = -EnemyInRangeHit.distance; } // For Test/Debug
      Debug.DrawRay(enemyInRangeRaycastObject.transform.position, direction * EnemyInRangeHit.distance * new Vector2(directionValue, 0f), Color.green);
      
      if (!enemyOccupied)
      {
        ArcherAttack();
      }
    }
  }
  void ArcherAttack()
  {
    arrowAttkCooldownTimer -= Time.deltaTime;
    if(isReadyToShoot)
    {
      if (arrowAttkCooldownTimer < 0.0f)
      {
        this.unitAnimator.SetTrigger("Ranged"); // will add walking animation when currentSpeed = 0;
        source.PlayOneShot(bowChargeClip);
        arrowReleaseTimer = arrowReleaseTime;
        isReadyToShoot = false;
        isReadyToRelease = true;
        
      }
    }
    
  }
  void ArrowRelease()
  {

    arrowReleaseTimer -= Time.deltaTime;
    if (isReadyToRelease)
    { 
      if(arrowReleaseTimer < 0.0f)
      {
        Instantiate(Arrow, ArrowAttackPoint.position, Quaternion.identity);
        source.PlayOneShot(bowReleaseClip);
        arrowAttkCooldownTimer = arrowAttkCooldownTime;
        isReadyToShoot = true;
        isReadyToRelease = false;
      }
    }
    
  }
}
