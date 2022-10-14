using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EconomyScript : MonoBehaviour
{

  [SerializeField] private TMP_Text playerCoinText;
  [SerializeField] private TMP_Text playerExpText;
  [SerializeField] private float spawnCoinsTime; // 
  [SerializeField] private int numberOfCoinsPerSpawnCoinsTime;
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
    playerCoinText.text = this.playerMoney.ToString();
  }
  public int PlayerExp;
  public int EnemyExp;// soon

  void Start()
  {
    spawnCoinsTimer = spawnCoinsTime;
    playerMoney = 150;
    playerCoinText.text = playerMoney.ToString();
    playerExpText.text = PlayerExp.ToString();
    enemyMoney = 150;
    Time.timeScale = 2.0f;
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
    if (tag == "P1")
    {
      playerMoney += coins;
      PlayerExp += exp;
      playerCoinText.text = playerMoney.ToString();
      playerExpText.text = PlayerExp.ToString();
    }
    if (tag == "P2")
    {
      enemyMoney += coins;
      EnemyExp += exp;
    }
  }
}
