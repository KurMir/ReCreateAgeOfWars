using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonScript : MonoBehaviour
{
  public GameObject economyScript;
  public GameObject spawnScript;
  [SerializeField] private Image WarriorCooldown;
  [SerializeField] private Image ArcherCooldown;
  [SerializeField] private Image SpearmanCooldown;

  public bool isCooldown;
  public float cooldownTime = 5f;
  public float cooldownTimer;
  public int playerCoin;

  void Start()
  {
    playerCoin = economyScript.GetComponent<EconomyScript>().getPlayerMoney();
    isCooldown = false;
    WarriorCooldown.fillAmount = cooldownTimer / cooldownTime;
    ArcherCooldown.fillAmount = cooldownTimer / cooldownTime;
    SpearmanCooldown.fillAmount = cooldownTimer / cooldownTime;
  }
  void Update()
  {
    ApplyCooldown();
  }

  public void SpawnWarrior()
  {
    playerCoin = economyScript.GetComponent<EconomyScript>().getPlayerMoney();
    if (playerCoin > 14)
      if (!isCooldown)
      {
        economyScript.GetComponent<EconomyScript>().setPlayerMoney(playerCoin -= 15);
        spawnScript.GetComponent<SpawnScript>().SummonWarrior();
        cooldownTimer = cooldownTime;
        isCooldown = true;
      }
  }
  public void SpawnArcher()
  {
    playerCoin = economyScript.GetComponent<EconomyScript>().getPlayerMoney();
    if (playerCoin > 29)
      if (!isCooldown)
      {
        economyScript.GetComponent<EconomyScript>().setPlayerMoney(playerCoin -= 30);
        spawnScript.GetComponent<SpawnScript>().SummonArcher();
        cooldownTimer = cooldownTime;
        isCooldown = true;
      }
  }
  public void SpawnSpearman()
  {
    playerCoin = economyScript.GetComponent<EconomyScript>().getPlayerMoney();
    if (playerCoin > 49)
      if (!isCooldown)
      {
        economyScript.GetComponent<EconomyScript>().setPlayerMoney(playerCoin -= 50);
        spawnScript.GetComponent<SpawnScript>().SummonSpearman();
        cooldownTimer = cooldownTime;
        isCooldown = true;
      }
  }

  private void ApplyCooldown()
  {
    if (isCooldown)
    {
      // Cooldown of the skill minus Time.Deltatime
      cooldownTimer -= Time.deltaTime;

      if (cooldownTimer < 0.0f)
      {
        //After the timer is done, it sets the text to false and fill amount to 0
        isCooldown = false;

        WarriorCooldown.fillAmount = 0.0f;
        ArcherCooldown.fillAmount = 0.0f;
        SpearmanCooldown.fillAmount = 0.0f;
      }
      else
      {
        // Displays the cooldown text and the fill 0.0f -> 1.0f dividing until it reaches the lowest possible
        WarriorCooldown.fillAmount = cooldownTimer / cooldownTime;
        ArcherCooldown.fillAmount = cooldownTimer / cooldownTime;
        SpearmanCooldown.fillAmount = cooldownTimer / cooldownTime;
      }
    }
  }
}
