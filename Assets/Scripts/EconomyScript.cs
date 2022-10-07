using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EconomyScript : MonoBehaviour
{

  [SerializeField] private TMP_Text playerCoinText;
  [SerializeField] private TMP_Text playerExpText;
  private int playerMoney;
  public int getPlayerMoney()
  {
    return playerMoney;
  }
  public void setPlayerMoney(int playerMoney)
  {
    this.playerMoney = playerMoney;
  }
  public int PlayerExp;
  public int EnemyMoney; //soon
  public int EnemyExp;// soon



  void Start()
  {
    playerMoney = 150;
    EnemyMoney = 0;
  }

  void Update()
  {
    playerCoinText.text = playerMoney.ToString();
    playerExpText.text = PlayerExp.ToString();
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
        EnemyMoney += 30;
      }
      if (type.Equals("Archer"))
      {
        EnemyMoney += 60;
      }
      if (type.Equals("Spearman"))
      {
        EnemyMoney += 100;
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
