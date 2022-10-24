using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageScript : MonoBehaviour
{
  private GameObject findEconomy;
  EconomyScript economyScript;
  [SerializeField] private int expDrop;
  [SerializeField] private int coinDrop; // will optimize later updates for Heroes/Champions
  [SerializeField] private float healthPoints;
  [SerializeField] private float maxHealth;
  [SerializeField] private string unitName;

  void Start()
  {
    findEconomy = GameObject.FindWithTag("EconomyFind");
    economyScript = findEconomy.GetComponent<EconomyScript>();
    maxHealth = healthPoints;
  }
  public void DamageDealt(float damage)
  {
    healthPoints -= damage;

    if (healthPoints <= 0)
    {
      economyScript.GetComponent<EconomyScript>().Drops(expDrop, coinDrop, this.gameObject.tag);
      Destroy(gameObject);
    }
  }
  public float GetMaxHealthPoints()
  {
    return maxHealth;
  }
  public float GetHealthPoints()
  {
    return healthPoints;
  }
  public string UnitName()
  {
    return unitName;
  }
}
