using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageScript : MonoBehaviour
{
  private GameObject findEconomy;
  EconomyScript economyScript;
  public float healthPoints;
  public float maxHealth;
  private string myType;
  public HealthbarBehaviour healthbar;

  void Start()
  {
    findEconomy = GameObject.FindWithTag("EconomyFind");
    economyScript = findEconomy.GetComponent<EconomyScript>();
    maxHealth = healthPoints;
    healthbar.SetHealth(healthPoints, maxHealth);
    if (maxHealth == 240) { myType = "Archer"; }
    if (maxHealth == 360) { myType = "Warrior"; }
    if (maxHealth == 400) { myType = "Spearman"; }
    if (maxHealth == 1800) { myType = "Tower"; }
    if (maxHealth == 3600) { myType = "Base"; }
  }

  public void DamageDealt(float damage)
  {
    healthPoints -= damage;
    healthbar.SetHealth(healthPoints, maxHealth);
    if (healthPoints <= 0)
    {
      if (myType.Equals("Base"))
      {
        Time.timeScale = 0; //Set winning
      }
      economyScript.GetComponent<EconomyScript>().CoinDrop(this.gameObject.tag, myType);
      economyScript.GetComponent<EconomyScript>().ExpDrop(this.gameObject.tag, myType);
      Destroy(gameObject);
    }
  }
}
