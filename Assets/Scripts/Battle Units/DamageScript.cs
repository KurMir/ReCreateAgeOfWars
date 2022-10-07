using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageScript : MonoBehaviour
{
  public float healthPoints;
  public void DamageDealt(float damage)
  {
    healthPoints -= damage;
    if (healthPoints <= 0)
    {
      //Add Conditions like Points/Gold
      Destroy(gameObject);
    }
  }
}
