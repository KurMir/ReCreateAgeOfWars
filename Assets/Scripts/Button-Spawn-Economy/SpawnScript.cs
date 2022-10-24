using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScript : MonoBehaviour
{
  [SerializeField] private List<GameObject> spawns = new List<GameObject>();
  [SerializeField] private float autoGenTime = 2f; // summons random every 8 sec
  private float autoGenTimer;
  private GameObject economyScriptObject;
  private EconomyScript economyScript;
  public int EnemyMoney; //change to private later

  void Start()
  {
    economyScriptObject = GameObject.Find("Economy");
    autoGenTimer = autoGenTime;
  }
  public void EnemyMoneyUpdate(int EnemyMoney)
  {
    this.EnemyMoney = EnemyMoney;
  }

  void Update()
  {
    BotAutoSpawn(); //removed economyscript
  }

  void BotAutoSpawn()
  {
    autoGenTimer -= Time.deltaTime;
    if (autoGenTimer < 0.0)
    {
      autoGenTimer = autoGenTime;
      economyScript = economyScriptObject.GetComponent<EconomyScript>();
      EnemyMoney = economyScript.getEnemyMoney();

      int randomSummon = Random.Range(0, 3);
      if (this.gameObject.tag == "Spawn2")
      {
        if (randomSummon == 2 && EnemyMoney >= 50)
        {
          economyScript.setEnemyMoney(EnemyMoney -= 50);
          Instantiate(spawns[2], this.transform.position, Quaternion.Euler(0f, 180f, 0f));
        }
        else if (randomSummon == 1 && EnemyMoney >= 30)
        {
          economyScript.setEnemyMoney(EnemyMoney -= 30);
          Instantiate(spawns[1], this.transform.position, Quaternion.Euler(0f, 180f, 0f));
        }
        else if (randomSummon == 0 && EnemyMoney >= 15)
        {
          economyScript.setEnemyMoney(EnemyMoney -= 15);
          Instantiate(spawns[0], this.transform.position, Quaternion.Euler(0f, 180f, 0f));
        }
      }
    }
  }

  // ====== Summon From ButtonScript ======= //
  public void SummonUnits(int num)
  {
    Instantiate(spawns[num], this.transform.position, Quaternion.identity);
  }
}
