using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScript : MonoBehaviour
{
  [SerializeField] private List<GameObject> spawns = new List<GameObject>();
  [SerializeField] private float autoGenTime = 3.5f; // summons random every 8 sec
  [SerializeField] private GameObject heroSpawn;
  private float autoGenTimer;
  private GameObject economyScriptObject;
  private EconomyScript economyScript;
  public int EnemyMoney; //change to private later

  public float heroSpawnTime = 50f;
  public float heroSpawnTimer;
  public bool isDead;

  void Start()
  {
    heroSpawnTimer = 0f;
    if (this.gameObject.tag == "Spawn1")
    {
      Instantiate(heroSpawn, this.transform.position, Quaternion.Euler(0f, 0f, 0f));

      GameObject championScriptGameObject = GameObject.FindWithTag("ChampionUIFind");
      ChampionScriptUI championScriptUI = championScriptGameObject.GetComponent<ChampionScriptUI>();
      championScriptUI.FindHeroChampion();
      championScriptUI.SetStatus(true);
    }

    economyScriptObject = GameObject.FindWithTag("EconomyFind");
    autoGenTimer = autoGenTime;
  }
  void Update()
  {
    BotAutoSpawn();
    DeathTimer();
  }

  public void HeroStatus(bool isDead)
  {
    this.isDead = isDead;
    heroSpawnTimer = heroSpawnTime;
    GameObject championScriptGameObject = GameObject.FindWithTag("ChampionUIFind");
    ChampionScriptUI championScriptUI = championScriptGameObject.GetComponent<ChampionScriptUI>();
    championScriptUI.FindHeroChampion();
    championScriptUI.SetStatus(false);
  }

  void DeathTimer()
  {
    if (isDead)
    {
      heroSpawnTimer -= Time.deltaTime;
      if (heroSpawnTimer < 0.0f)
      {
        isDead = false;

        Instantiate(heroSpawn, this.transform.position, Quaternion.Euler(0f, 0f, 0f));

        GameObject championScriptGameObject = GameObject.FindWithTag("ChampionUIFind");
        ChampionScriptUI championScriptUI = championScriptGameObject.GetComponent<ChampionScriptUI>();
        championScriptUI.FindHeroChampion();
        championScriptUI.SetStatus(true);
      }
    }
  }


  public void EnemyMoneyUpdate(int EnemyMoney)
  {
    this.EnemyMoney = EnemyMoney;
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
