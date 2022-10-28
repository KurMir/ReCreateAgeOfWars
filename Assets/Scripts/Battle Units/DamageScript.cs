using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageScript : MonoBehaviour
{
  private GameObject findEconomy;
  private EconomyScript economyScript;
  public int expDrop;
  public int coinDrop;
  [SerializeField] private float healthPoints;
  [SerializeField] private float maxHealth;
  [SerializeField] private string unitName;

  void Start()
  {
    findEconomy = GameObject.FindWithTag("EconomyFind");
    economyScript = findEconomy.GetComponent<EconomyScript>();
    maxHealth = healthPoints;
    if (LayerMask.LayerToName(this.gameObject.layer).Equals("WarriorPlayer")) { expDrop = economyScript.WarriorExpDrop(); coinDrop = economyScript.WarriorCoinDrop(); }
    if (LayerMask.LayerToName(this.gameObject.layer).Equals("WarriorEnemy")) { expDrop = economyScript.WarriorExpDrop(); coinDrop = economyScript.WarriorCoinDrop(); }
    if (LayerMask.LayerToName(this.gameObject.layer).Equals("ArcherPlayer")) { expDrop = economyScript.ArcherExpDrops(); coinDrop = economyScript.ArcherCoinDrop(); }
    if (LayerMask.LayerToName(this.gameObject.layer).Equals("ArcherEnemy")) { expDrop = economyScript.ArcherExpDrops(); coinDrop = economyScript.ArcherCoinDrop(); }
    if (LayerMask.LayerToName(this.gameObject.layer).Equals("SpearmanPlayer")) { expDrop = economyScript.SpearmanExpDrop(); coinDrop = economyScript.SpearmanCoinDrop(); }
    if (LayerMask.LayerToName(this.gameObject.layer).Equals("SpearmanEnemy")) { expDrop = economyScript.SpearmanExpDrop(); coinDrop = economyScript.SpearmanCoinDrop(); }
    if (LayerMask.LayerToName(this.gameObject.layer).Equals("HeroPlayer")) { expDrop = 450; coinDrop = 350; }
    if (LayerMask.LayerToName(this.gameObject.layer).Equals("HeroEnemy")) { expDrop = 450; coinDrop = 350; }

  }
  public void DamageDealt(float damage)
  {
    healthPoints -= damage;

    if (healthPoints <= 0)
    {
      if (LayerMask.LayerToName(this.gameObject.layer).Equals("HeroPlayer"))
      {
        GameObject championScriptGameObject = GameObject.FindWithTag("ChampionUIFind");
        ChampionScriptUI championScriptUI = championScriptGameObject.GetComponent<ChampionScriptUI>();
        championScriptUI.SetStatus(false);
        economyScript.GetComponent<EconomyScript>().Drops(expDrop, coinDrop, this.gameObject.tag);

        GameObject spawn1GameObject = GameObject.FindWithTag("Spawn1");
        SpawnScript spawnScript = spawn1GameObject.GetComponent<SpawnScript>();
        spawnScript.HeroStatus(true);


        Destroy(gameObject);
      }
      else
      {
        economyScript.GetComponent<EconomyScript>().Drops(expDrop, coinDrop, this.gameObject.tag);

        Destroy(gameObject);
      }

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
