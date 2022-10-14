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
  public HealthbarBehaviour healthbar;

  void Start()
  {
    findEconomy = GameObject.FindWithTag("EconomyFind");
    economyScript = findEconomy.GetComponent<EconomyScript>();
    maxHealth = healthPoints;
    healthbar.SetHealth(healthPoints, maxHealth);
  }

  public void DamageDealt(float damage)
  {
    healthPoints -= damage;
    healthbar.SetHealth(healthPoints, maxHealth);
    if (healthPoints <= 0)
    {
      if (this.gameObject.tag == "Base")
      {
        Time.timeScale = 0; //Set winning
      }
      else
      {
        economyScript.GetComponent<EconomyScript>().Drops(expDrop, coinDrop, this.gameObject.tag);
        Destroy(gameObject);
      }
    }
  }
  public string UnitName()
  {
    return unitName;
  }
}
