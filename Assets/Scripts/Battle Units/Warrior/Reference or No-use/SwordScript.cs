using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordScript : MonoBehaviour
{
  public string tagOfEnemy;
  public GameObject unit;
  public float damage = 3;

  void OnTriggerEnter2D(Collider2D other)
  {
    if (other.GetComponent<WarriorMovement>().tag == "P2" && unit.gameObject.tag == "P1")
    {
      WarriorMovement warrior = other.GetComponent<WarriorMovement>();
      warrior.Damage(damage);
      tagOfEnemy = warrior.tag;
    }
    if (other.GetComponent<WarriorMovement>().tag == "P2" && unit.gameObject.tag == "P1")
    {
      WarriorMovement warrior = other.GetComponent<WarriorMovement>();
      warrior.Damage(damage);
      tagOfEnemy = warrior.gameObject.tag;
    }
  }

  void OnTriggerExit2D(Collider2D other)
  {
    tagOfEnemy = "";
  }
}
