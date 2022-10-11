using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QueueScript : MonoBehaviour
{

    public GameObject buttonScript;
    [SerializeField] private Image[] QueueImage = new Image[4];

    [SerializeField] private Slider slider;
    [SerializeField] private Color whiteColor = Color.white;
    public Slider Slider;
    public Color forColor;

    public float queueTime = 5f;
    public float queueTimer;
    
    public int []ArrayQueue = new int [4]; //public to show in inspector
   
      private bool isFull()
    {
        int count= 0;
        for (int i = 0; i < ArrayQueue.Length; i++){
            if(ArrayQueue[i] == -1){ count++; }
        }
        if (count == 0){
            return true;
        }else { return false; }
    }

     public bool getStatus(){
        return isFull();
    }

    void Start()
    {
        queueTimer = queueTime;
        /*  For Reference
    ArrayQueue[3] = -1;
    ArrayQueue[2] = 2;
    ArrayQueue[1] = 1;
    ArrayQueue[0] = 0;

    int count = 1;
    for (int i = 0; i < ArrayQueue.Length; i++){
     if(ArrayQueue[i] == -1){ count++; }  
    // if(ArrayQueue[0] == -1){ count = 1; }
    }
    ArrayQueue[ArrayQueue.Length - count] = -1;
    Debug.Log("Count is "+count);

    for (int i = 0; i < ArrayQueue.Length; i++){
      Debug.Log("Array "+i+" = "+ArrayQueue[i]);
    }
  */
    }
 
    void Update()
    {
        QueueTimeStamp();
    }
    public void AddQueue(int type) {
        
        for (int i = 0; i < ArrayQueue.Length; i++)
        {
            if(ArrayQueue[i] == -1){
                ArrayQueue[i] = type; // Adds the type
                for (int j = 0; j < ArrayQueue.Length; j++){ // This For Loop Updates the QueueBoxes in the game view to be brighten if onQueue
                    if(ArrayQueue[j] == -1){ 
                        var tempColor = QueueImage[j].color;
                        tempColor.a = 100f;
                        QueueImage[j].color = tempColor;
                    }
                    else {
                        var tempColor = QueueImage[j].color;
                        tempColor.a = 255f; //fullbright if occupied
                        QueueImage[j].color = tempColor;
                    }
                } 
                break; // breaks the loop after getting the nearest -1 
            }else if (isFull()) { break; } //breaks if there is no -1 or queue is full
            
        }
    }
    
    void RemoveQueue()
    {

    }
    void QueueTimeStamp(){

        if(ArrayQueue[0] != -1){ 
            queueTimer -= Time.deltaTime; 
            Slider.value = queueTimer; // Value Slides base on the timer
            Slider.maxValue = 5f;

            if (queueTimer <= 0.0f){
                queueTimer = queueTime;
                //buttonScript.GetComponent<ButtonScript>().InstantiateUnits(ArrayQueue[0]);
                RemoveQueue();
            }
        }
           
    }
}
