using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonScript : MonoBehaviour
{
  public GameObject economyScript;
  public GameObject spawnScript;
  public int playerCoin;

  [SerializeField] private Image[] QueueImage = new Image[4];
  [SerializeField] private Slider slider;
  [SerializeField] private Color whiteColor = Color.white;
  public Slider Slider;
  public Color forColor;

  public float queueTime = 5f;
  public float queueTimer;
    
  public int []ArrayQueue = new int [4]; //public to show in inspector
  private int count = 0;
   
  private bool isFull()
  {
    for (int i = 0; i < ArrayQueue.Length; i++){
      if(ArrayQueue[i] != -1){ count++; } 
    }
    if (count == 4){ return true; } 
    else { return false; }
    }
  
  

  void Start()
  {
    playerCoin = economyScript.GetComponent<EconomyScript>().getPlayerMoney();
    for (int i = 0; i < ArrayQueue.Length; i++){ ArrayQueue[i] = -1; } //sets all to -1
  }
  void Update()
  {
      QueueTimeStamp(); //Updates everytime [0] is not null
  }

// ====== Button Clicks in game view ===== //
  public void SpawnWarrior()
  {
    playerCoin = economyScript.GetComponent<EconomyScript>().getPlayerMoney();
    if (playerCoin > 14)
      if(isFull() == false) // if queue is not full, add code later for effects
        economyScript.GetComponent<EconomyScript>().setPlayerMoney(playerCoin -= 15);
        AddQueue(0);
  }
  public void SpawnArcher()
  {
    playerCoin = economyScript.GetComponent<EconomyScript>().getPlayerMoney();
    if (playerCoin > 29)
      if(isFull() == false) // if queue is not full, add code later for effects
        economyScript.GetComponent<EconomyScript>().setPlayerMoney(playerCoin -= 30); 
        AddQueue(1);  
  }
  public void SpawnSpearman()
  {
    playerCoin = economyScript.GetComponent<EconomyScript>().getPlayerMoney();
    if (playerCoin > 49)
      if(isFull() == false) // if queue is not full, add code later for effects
        economyScript.GetComponent<EconomyScript>().setPlayerMoney(playerCoin -= 50);
        AddQueue(2);
  }

  
  void AddQueue(int type) {
        
    for (int i = 0; i < ArrayQueue.Length; i++)
    {
      if(ArrayQueue[i] == -1){
      ArrayQueue[i] = type; 
      for (int j = 0; j < ArrayQueue.Length; j++){ // This For Loop Updates the QueueBoxes in the game view to be brighten if onQueue
        if(ArrayQueue[j] == -1){ 
        var tempColor = QueueImage[j].color;
        tempColor.a = 100f;
        QueueImage[j].color = tempColor;
        }
        else{
        var tempColor = QueueImage[j].color;
        tempColor.a = 255f; 
        QueueImage[j].color = tempColor;
        }
      } 
      break; 
      }else if (isFull()) { break; } 
    }
  }
    
  void RemoveQueue()
  {
      //remove current [0] and add -1 
    for (int i = 0; i < ArrayQueue.Length; i++)
    { 
      if(isFull() == false) // im not sure if !isFull() works
      {
        if(i < ArrayQueue.Length-1){
          ArrayQueue[i] = ArrayQueue[i+1];
        }
      }else { ArrayQueue[count-1] = -1; break; }
    }
     
  }
    void QueueTimeStamp(){

        if(ArrayQueue[0] != -1){ 
            queueTimer -= Time.deltaTime; 
            Slider.value = queueTimer; 
            Slider.maxValue = 5f;

            if (queueTimer <= 0.0f){
                queueTimer = queueTime;
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
