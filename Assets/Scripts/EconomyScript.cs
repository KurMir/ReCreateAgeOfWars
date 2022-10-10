using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EconomyScript : MonoBehaviour
{

  [SerializeField] private TMP_Text playerCoinText;
  [SerializeField] private TMP_Text playerExpText;
  public float spawnCoinsTime = 60f;
  public float spawnCoinsTimer;
  private int playerMoney;
  private int enemyMoney;
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
  }
  public int PlayerExp;
  public int EnemyExp;// soon

  void Start()
  {
    spawnCoinsTimer = spawnCoinsTime;
    playerMoney = 150;
    enemyMoney = 150;
    Time.timeScale = 2.0f;
  }

  void Update()
  {
    SpawnCoins();
    playerCoinText.text = playerMoney.ToString(); //will be removed from Update with further coding
    playerExpText.text = PlayerExp.ToString();
  }

  void SpawnCoins()
  {
    spawnCoinsTimer -= Time.deltaTime;
    if (spawnCoinsTimer <= 0)
    {
      spawnCoinsTimer = spawnCoinsTime;
      playerMoney += 50;
      enemyMoney += 50;
    }
  }

  public void CoinDrop(string who, string type)
  {
    if (who.Equals("P2")) //if Enemy unit was killed, Money goes to player
    {
      if (type.Equals("Warrior"))
      {
        playerMoney += 30;
      }
      if (type.Equals("Archer"))
      {
        playerMoney += 60;
      }
      if (type.Equals("Spearman"))
      {
        playerMoney += 100;
      }
    }
    if (who.Equals("P1"))
    {
      if (type.Equals("Warrior"))
      {
        enemyMoney += 30;
      }
      if (type.Equals("Archer"))
      {
        enemyMoney += 60;
      }
      if (type.Equals("Spearman"))
      {
        enemyMoney += 100;
      }
    }
  }

  public void ExpDrop(string who, string type)
  {
    if (who.Equals("P2")) //if Enemy unit was killed, Money goes to player
    {
      if (type.Equals("Warrior"))
      {
        PlayerExp += 60;
      }
      if (type.Equals("Archer"))
      {
        PlayerExp += 120;
      }
      if (type.Equals("Spearman"))
      {
        PlayerExp += 200;
      }
    }
    if (who.Equals("P1"))
    {
      if (type.Equals("Warrior"))
      {
        EnemyExp += 60;
      }
      if (type.Equals("Archer"))
      {
        EnemyExp += 120;
      }
      if (type.Equals("Spearman"))
      {
        EnemyExp += 200;
      }
    }
  }

}
