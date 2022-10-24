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
      Debug.DrawRay(
        enemyRaycastObject.transform.position,
        direction * ownBaseHit.distance * new Vector2(directionValue, 0f),
        Color.blue
      );
      currentMoveSpeed = 0f;
    }

    return isLookingAtBase;
  }
}
