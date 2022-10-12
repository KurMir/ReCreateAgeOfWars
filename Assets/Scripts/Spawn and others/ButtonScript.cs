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
  [SerializeField] private Image[] QueueImage = new Image[4];
  public Slider Slider;

  [Header("Button and Queue Script Settings")]
  public float queueTime;
  public float queueTimer;
  private int playerCoin;
  private int[] ArrayQueue = new int[4];
  private int count;

  private bool isFull()
  {
    count = 0;
    for (int i = 0; i < ArrayQueue.Length; i++)
    {
      if (ArrayQueue[i] != -1) { count++; }
    }
    if (count == 4) { return true; }
    else { return false; }
  }



  void Start()
  {
    Slider.maxValue = queueTime;
    Slider.value = 0f;
    queueTimer = 0f;
    playerCoin = economyScript.GetComponent<EconomyScript>().getPlayerMoney();
    for (int i = 0; i < ArrayQueue.Length; i++) { ArrayQueue[i] = -1; } //sets all to -1
  }
  void Update()
  {
    QueueTimeStamp(); //Updates everytime [0] is not null for (int i = 0; i < ArrayQueue.Length; i++)
  }

  // ====== Button Clicks in game view ===== //
  public void SpawnWarrior()
  {
    playerCoin = economyScript.GetComponent<EconomyScript>().getPlayerMoney();
    if (playerCoin > 14)
      if (isFull() == false) // if queue is not full, add code later for effects
        economyScript.GetComponent<EconomyScript>().setPlayerMoney(playerCoin -= 15);
    AddQueue(0);
  }
  public void SpawnArcher()
  {
    playerCoin = economyScript.GetComponent<EconomyScript>().getPlayerMoney();
    if (playerCoin > 29)
      if (isFull() == false) // if queue is not full, add code later for effects
        economyScript.GetComponent<EconomyScript>().setPlayerMoney(playerCoin -= 30);
    AddQueue(1);
  }
  public void SpawnSpearman()
  {
    playerCoin = economyScript.GetComponent<EconomyScript>().getPlayerMoney();
    if (playerCoin > 49)
      if (isFull() == false) // if queue is not full, add code later for effects
        economyScript.GetComponent<EconomyScript>().setPlayerMoney(playerCoin -= 50);
    AddQueue(2);
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
    for (int i = 0; i < ArrayQueue.Length; i++)
    {
      if (isFull() == false) // assuming that the last array is -1 or the arrays below
      {
        if (i < ArrayQueue.Length - 1)
        {
          ArrayQueue[i] = ArrayQueue[i + 1];
        }
      }
      else
      {
        ArrayQueue[count - 1] = -1;
        break;
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
