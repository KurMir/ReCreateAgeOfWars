using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonScript : MonoBehaviour
{
  [Header("GameObjects/Images/Sliders")]
  public GameObject economyScript;
  public GameObject spawnScript;
  public GameObject championScript;
  [SerializeField] private Image[] QueueImage = new Image[4];
  [SerializeField] private Image[] WarriorSkills = new Image[2];
  [SerializeField] private TMP_Text[] WarriorSkillsText = new TMP_Text[2];
  public Slider Slider;

  [Header("Button and Queue Script Settings")]
  public float queueTime;
  public float queueTimer;
  private int playerCoin;
  private int[] ArrayQueue = new int[4];
  private int count;
  public bool isFull()
  {
    count = 0;
    for (int i = 0; i < ArrayQueue.Length; i++)
    {
      if (ArrayQueue[i] != -1) { count++; }
    }
    if (count == 4) { return true; }
    else { return false; }
  }
  [Header("Skill Settings")]
  public bool isSkill1Cooldown;
  public bool isSkill2Cooldown;
  public float skill1CooldownTime = 8f;
  public float skill1CooldownTimer;
  public float skill2CooldownTime = 15f;
  public float skill2CooldownTimer;



  void Start()
  {
    WarriorSkillsText[0].gameObject.SetActive(false);
    WarriorSkillsText[1].gameObject.SetActive(false);
    WarriorSkills[0].fillAmount = 0.0f;
    WarriorSkills[1].fillAmount = 0.0f;
    isSkill1Cooldown = true;
    isSkill2Cooldown = true;
    Slider.maxValue = queueTime;
    Slider.value = 0f;
    queueTimer = 0f;
    playerCoin = economyScript.GetComponent<EconomyScript>().getPlayerMoney();
    for (int i = 0; i < ArrayQueue.Length; i++) { ArrayQueue[i] = -1; } //sets all to -1
  }
  void Update()
  {
    QueueTimeStamp(); //Updates everytime [0] is not null for (int i = 0; i < ArrayQueue.Length; i++)
    Skill1Cooldown();
    Skill2Cooldown();

  }

  // ====== Button Clicks in game view ===== //
  public void SpawnWarrior()
  {
    playerCoin = economyScript.GetComponent<EconomyScript>().getPlayerMoney();
    if (playerCoin > 14)
    {
      if (isFull() == false)
      {
        economyScript.GetComponent<EconomyScript>().setPlayerMoney(playerCoin -= 15);
        AddQueue(0);
      } // if queue is not full, add code later for effects
    }
  }
  public void SpawnArcher()
  {
    playerCoin = economyScript.GetComponent<EconomyScript>().getPlayerMoney();
    if (playerCoin > 29)
    {
      if (isFull() == false)
      {
        economyScript.GetComponent<EconomyScript>().setPlayerMoney(playerCoin -= 30);
        AddQueue(1);
      } // if queue is not full, add code later for effects
    }
  }
  public void SpawnSpearman()
  {
    playerCoin = economyScript.GetComponent<EconomyScript>().getPlayerMoney();
    if (playerCoin > 49)
    {
      if (isFull() == false)
      {
        economyScript.GetComponent<EconomyScript>().setPlayerMoney(playerCoin -= 50);
        AddQueue(2);
      } // if queue is not full, add code later for effects
    }
  }
  public void WarriorSkillSmash()
  {
    isSkill1Cooldown = false;
    skill1CooldownTimer = skill1CooldownTime;
    WarriorSkillsText[0].gameObject.SetActive(true);
  }

  public void WarriorSkillBash()
  {
    isSkill2Cooldown = false;
    skill2CooldownTimer = skill2CooldownTime;
    WarriorSkillsText[1].gameObject.SetActive(true);
  }

  void Skill1Cooldown()
  {
    if (!isSkill1Cooldown)
    {
      skill1CooldownTimer -= Time.deltaTime;
      if (skill1CooldownTimer < 0.0f)
      {
        isSkill1Cooldown = true;
        WarriorSkillsText[0].gameObject.SetActive(false);
        WarriorSkills[0].fillAmount = 0.0f;

      }
      else
      {
        WarriorSkillsText[0].text = Mathf.RoundToInt(skill1CooldownTimer).ToString();
        WarriorSkills[0].fillAmount = skill1CooldownTimer / skill1CooldownTime;
      }
    }
  }
  void Skill2Cooldown()
  {
    if (!isSkill2Cooldown)
    {
      skill2CooldownTimer -= Time.deltaTime;
      if (skill2CooldownTimer < 0.0f)
      {
        isSkill2Cooldown = true;
        WarriorSkillsText[1].gameObject.SetActive(false);
        WarriorSkills[1].fillAmount = 0.0f;
      }
      else
      {
        WarriorSkillsText[1].text = Mathf.RoundToInt(skill2CooldownTimer).ToString();
        WarriorSkills[1].fillAmount = skill2CooldownTimer / skill2CooldownTime;
      }
    }
  }
  void AddQueue(int type)
  {

    for (int i = 0; i < ArrayQueue.Length; i++)
    {
      if (ArrayQueue[i] == -1)
      {
        ArrayQueue[i] = type;
        QueueImage[i].GetComponent<Image>().color = new Color32(255, 255, 225, 255);
        break;
      }
      else if (isFull()) { break; }
    }
  }

  void RemoveQueue()
  {

    if (isFull() == false)
    {
      for (int i = 0; i < ArrayQueue.Length; i++)
      {
        if (i < ArrayQueue.Length - 1)
        {
          ArrayQueue[i] = ArrayQueue[i + 1];
        }
      }

      for (int i = 0; i < ArrayQueue.Length; i++)
      {
        if (ArrayQueue[i] == -1)
        {
          //var tempColor = QueueImage[i].color;
          //tempColor.a = 100;
          QueueImage[i].GetComponent<Image>().color = new Color32(255, 255, 225, 100);
        }
      }
    }
    else if (isFull())
    {
      for (int i = 0; i < ArrayQueue.Length; i++)
      {
        if (i < ArrayQueue.Length - 1)
        {
          ArrayQueue[i] = ArrayQueue[i + 1];
        }
      }
      ArrayQueue[ArrayQueue.Length - 1] = -1;
      for (int i = 0; i < ArrayQueue.Length; i++)
      {
        if (ArrayQueue[i] == -1)
        {
          //var tempColor = QueueImage[i].color;
          //tempColor.a = 100;
          QueueImage[i].GetComponent<Image>().color = new Color32(255, 255, 225, 100);
        }
      }
    }
  }
  void QueueTimeStamp()
  {

    if (ArrayQueue[0] != -1)
    {
      queueTimer += Time.deltaTime;
      Slider.value = queueTimer;

      if (queueTimer >= queueTime)
      {
        queueTimer = 0f;
        Slider.value = queueTimer;
        InstantiateUnits(ArrayQueue[0]);
        RemoveQueue();
      }
    }

  }

  void InstantiateUnits(int num)
  {
    spawnScript.GetComponent<SpawnScript>().SummonUnits(num);
  }






}
