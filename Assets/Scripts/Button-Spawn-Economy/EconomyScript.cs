using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EconomyScript : MonoBehaviour
{

  [SerializeField] private TMP_Text playerCoinText;
  [SerializeField] private TMP_Text playerExpText;
  [SerializeField] private float spawnCoinsTime;
  [SerializeField] private int numberOfCoinsPerSpawnCoinsTime;
  public float spawnCoinsTimer;
  private int playerMoney;
  private int enemyMoney;
  private int currentArcherExpDrop = 50;
  private int currentArcherCoinDrop = 30;
  private int currentWarriorExpDrop = 30;
  private int currentWarriorCoinDrop = 15;
  private int currentSpearmanExpDrop = 75;
  private int currentSpearmanCoinDrop = 50;

  float increaseTime = 120;
  float increaseTimer;

  public int getEnemyMoney()
  {
    return enemyMoney;
  }
  public void setEnemyMoney(int enemyMoney)
  {
    this.enemyMoney = enemyMoney;
  }
  public int getPlayerMoney()
  {
    return playerMoney;
  }
  public void setPlayerMoney(int playerMoney)
  {
    this.playerMoney = playerMoney;
    playerCoinText.text = this.playerMoney.ToString();
  }
  public int PlayerExp;
  public int EnemyExp;// soon

  void Start()
  {
    increaseTimer = increaseTime;
    spawnCoinsTimer = spawnCoinsTime;
    playerMoney = 150;
    playerCoinText.text = playerMoney.ToString();
    playerExpText.text = PlayerExp.ToString();
    enemyMoney = 150;
    //Removed Timescale increase for better Time.Deltatime/ Time counting 
  }

  void Update()
  {
    SpawnCoins();
  }

  void SpawnCoins()
  {
    spawnCoinsTimer -= Time.deltaTime;
    if (spawnCoinsTimer <= 0)
    {
      spawnCoinsTimer = spawnCoinsTime;
      playerMoney += numberOfCoinsPerSpawnCoinsTime; //50 coins per Time
      enemyMoney += numberOfCoinsPerSpawnCoinsTime;
      playerCoinText.text = playerMoney.ToString();
    }
  }
  public void Drops(int exp, int coins, string tag)
  {
    if (tag == "P2")
    {
      Debug.Log("P2 Died");
      playerMoney += coins;
      PlayerExp += exp;
      playerCoinText.text = playerMoney.ToString();
      playerExpText.text = PlayerExp.ToString();
    }
    if (tag == "P1")
    {
      Debug.Log("P1 Died");
      enemyMoney += coins;
      EnemyExp += exp;
    }
  }


  public int ArcherExpDrops()
  {
    return currentArcherExpDrop;
  }
  public int ArcherCoinDrop()
  {
    return currentArcherCoinDrop;
  }
  public int WarriorExpDrop()
  {
    return currentWarriorExpDrop;
  }
  public int WarriorCoinDrop()
  {
    return currentWarriorCoinDrop;
  }
  public int SpearmanExpDrop()
  {
    return currentSpearmanExpDrop;
  }
  public int SpearmanCoinDrop()
  {
    return currentSpearmanCoinDrop;
  }

   void IncreaseEconomy()
  {
    increaseTimer -= Time.deltaTime;
    if (increaseTimer <= 0.0f)
    {   
      increaseTimer += increaseTime;
      currentArcherExpDrop += 30;
      currentArcherCoinDrop += 15;
      currentWarriorExpDrop += 30;
      currentWarriorCoinDrop += 15;
      currentSpearmanExpDrop += 30;
      currentSpearmanCoinDrop += 15;
    }
  }

}
