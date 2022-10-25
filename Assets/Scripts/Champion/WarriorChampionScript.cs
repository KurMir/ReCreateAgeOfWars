using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class WarriorChampionScript : BaseMovement
{
  [Header("WarriorChampion Settings")]
  public float inputValue = 1;
  public TMP_Text EnemyName;
  public LayerMask ownBase;
  public float baseDistanceInput;
  public float baseDistance;

  public bool lookingAtBase;


  new public void Update()
  {
    DetectInputs();
    bool hasAllyInFront = HasAllyInFront();
    bool hasEnemyInFront = HasEnemyInFront();
    this.UnitMove(hasAllyInFront, hasEnemyInFront);
    IsLookingAtBase();
  }

  void DetectInputs()
  {
    if (this.gameObject.tag == "P1")
    {
      if (Input.GetKeyDown(KeyCode.Space))
      {
        direction = Vector2.zero;
      }
      if (Input.GetKeyDown(KeyCode.D))
      {
        direction = Vector2.right;
      }
      if (Input.GetKeyDown(KeyCode.A))
      {
        direction = Vector2.left;
      }
      if (direction == Vector2.left)
      {
        baseDistance = -baseDistanceInput;
        transform.localRotation = Quaternion.Euler(0f, 180f, 0f);
      }
      else
      {
        baseDistance = baseDistanceInput;
        transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
      }
    }
  }
  new protected void UnitMove(bool hasAllyInFront, bool hasEnemyInFront)
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
    this.currentMoveSpeed = (IsLookingAtBase()) ? 0f :moveSpeed * directionValue;
    rb.velocity = new Vector2(this.currentMoveSpeed, 0f);
  }

  new public void MeleeAttack()
  {
    if (attackAvailable)
    {
      unitAnimator.ResetTrigger("Attack");
      int randomSummon = Random.Range(0, 2);
      if(randomSummon == 0){  unitAnimator.SetTrigger("Attack"); }
      if(randomSummon == 1){  unitAnimator.SetTrigger("Attack2"); }
      
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

  bool IsLookingAtBase()
  {
    float directionValue = GetDirectionValue();
    RaycastHit2D ownBaseHit = Physics2D.Raycast(
      enemyRaycastObject.transform.position,
      direction * new Vector2(directionValue, 0f),
      baseDistance,
      ownBase
    );

    bool isLookingAtBase = ownBaseHit.collider != null;

    if (isLookingAtBase)
    {
     if(this.gameObject.tag == "P1") { ownBaseHit.distance = -ownBaseHit.distance; }
      Debug.DrawRay(
        enemyRaycastObject.transform.position,
        direction * ownBaseHit.distance * new Vector2(directionValue, 0f),
        Color.blue
      );
     
      Debug.Log("Base Hit");
    }

    return isLookingAtBase;
  }
}
