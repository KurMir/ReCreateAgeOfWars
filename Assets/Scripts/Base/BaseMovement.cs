using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public abstract class BaseMovement : MonoBehaviour
{
  [Header("Unit Settings")]
  public bool attackAvailable;
  public float attackCooldownTime = 1.0f;
  public float attackCooldownTimer = 0f;
  public bool allyOccupied;
  public bool enemyOccupied;
  public float currentMoveSpeed = 0.0f;
  public float moveSpeed = 3f;
  public float meleeDamage = 15.0f;
  public Vector2 direction;

  [Header("GameObjects/Transforms")]
  public AudioSource sourceForMelee;
  public AudioClip meleeClip;
  public Transform attackPoint;
  public float attackRange = 4;

  public GameObject enemyRaycastObject;
  public float enemyRayDistance;

  public GameObject allyRaycastObject;
  public float allyRayDistance;

  public GameObject unitGameObject;

  public Animator unitAnimator;

  public GameSettings gameSettings;

  public LayerMask enemyLayerMask;

  public LayerMask allyLayerMask;
  public LayerMask allyLayerMaskGet;

  public GameObject closestEnemy;

  protected Rigidbody2D rb;

  public void Awake()
  {
    unitAnimator = unitGameObject.GetComponent<Animator>();
    gameSettings.GetComponent<GameSettings>();
    rb = this.gameObject.GetComponent<Rigidbody2D>();
    rb.constraints = RigidbodyConstraints2D.FreezePositionY;

  }

  public void Start()
  {
    attackAvailable = true;
    attackCooldownTimer = 0f;
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
  }

  public void Update()
  {
    bool hasAllyInFront = HasAllyInFront();
    bool hasEnemyInFront = HasEnemyInFront();

    this.UnitMove(hasAllyInFront, hasEnemyInFront);
  }



  protected bool HasAllyInFront()
  {
    float directionValue = GetDirectionValue(); //Direction can also be get directly in the variables, Direction.x (Units uses the same scripts but never the get the components)
    RaycastHit2D allyHit = Physics2D.Raycast(
      allyRaycastObject.transform.position,
      direction * new Vector2(directionValue, 0f),
      allyRayDistance,
      this.allyLayerMask
    );

    bool hasAllyInFront = allyHit.collider != null;

    if (hasAllyInFront)
    {
      if (LayerMask.LayerToName(allyHit.collider.gameObject.layer).Equals("TowerEnemy") || LayerMask.LayerToName(allyHit.collider.gameObject.layer).Equals("TowerPlayer"))
      {
        hasAllyInFront = false;
      }
    }

    if (hasAllyInFront)
    {
      Debug.DrawRay(
        allyRaycastObject.transform.position,
        direction * allyHit.distance * new Vector2(directionValue, 0f),
        Color.blue
      );
    }

    if (LayerMask.LayerToName(this.gameObject.layer).Equals("HeroPlayer") || LayerMask.LayerToName(this.gameObject.layer).Equals("HeroPlayer"))
    {
      hasAllyInFront = false;
    }

    return hasAllyInFront;
  }

  protected bool HasEnemyInFront()
  {
    float directionValue = GetDirectionValue();
    RaycastHit2D enemyHit = Physics2D.Raycast(
      enemyRaycastObject.transform.position,
      direction * new Vector2(directionValue, 0f),
      enemyRayDistance,
      enemyLayerMask
    );

    bool hasEnemyInFront = enemyHit.collider != null;

    if (hasEnemyInFront)
    {
      Debug.DrawRay(
        enemyRaycastObject.transform.position,
        direction * enemyHit.distance * new Vector2(directionValue, 0f),
        Color.blue
      );
    }

    return hasEnemyInFront;
  }



  protected void UnitMove(bool hasAllyInFront, bool hasEnemyInFront)
  {
    allyOccupied = hasAllyInFront;
    enemyOccupied = hasEnemyInFront;

    if (hasAllyInFront)
    {
      unitAnimator.ResetTrigger("Walk");
      currentMoveSpeed = 0f;
      rb.velocity = new Vector2(this.currentMoveSpeed, 0f);

      return;
    }

    if (hasEnemyInFront)
    {
      unitAnimator.ResetTrigger("Walk");
      currentMoveSpeed = 0f;
      rb.velocity = new Vector2(this.currentMoveSpeed, 0f);
      if (LayerMask.LayerToName(this.gameObject.layer) != "ArcherChampion") // Working on it
      {
        MeleeAttack();
      }

      return;
    }

    unitAnimator.SetTrigger("Walk");
    float directionValue = GetDirectionValue();
    this.currentMoveSpeed = moveSpeed * directionValue;
    rb.velocity = new Vector2(this.currentMoveSpeed, 0f);
  }

  public float GetDirectionValue()
  {
    return direction.x;
  }

  public void MeleeAttack()
  {
    if (attackAvailable)
    {
      unitAnimator.ResetTrigger("Attack");
      unitAnimator.SetTrigger("Attack");
      attackCooldownTimer = attackCooldownTime;

      attackAvailable = false;
    }
    closestEnemy = GetClosestEnemy();
    attackCooldownTimer -= Time.deltaTime;
    if (attackCooldownTimer < 0.0f)
    {
      attackAvailable = true;
      sourceForMelee.PlayOneShot(meleeClip);
      closestEnemy.GetComponent<DamageScript>().DamageDealt(meleeDamage);
    }
  }

  public GameObject GetClosestEnemy()
  {
    Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, attackRange, gameSettings.enemyLayerMask);
    Collider2D firstHitEnemy = hitEnemies[0];

    float closestDistance = Vector3.Distance(transform.position, firstHitEnemy.transform.position);
    GameObject closestGameObject = firstHitEnemy.gameObject;

    Collider2D[] hitEnemiesToLoopThrough = hitEnemies.Skip(1).ToArray();

    foreach (Collider2D enemy in hitEnemiesToLoopThrough)
    {
      float currentDistance = Vector3.Distance(transform.position, enemy.transform.position);
      if (currentDistance < closestDistance)
      {
        closestDistance = currentDistance;
        closestGameObject = enemy.gameObject;
      }
    }
    return closestGameObject;
  }
  void OnDrawGizmosSelected() //Drawing Gizmos
  {
    if (attackPoint == null)
    {
      return;
      
    }
    Gizmos.DrawWireSphere(attackPoint.position, attackRange);
  }
}