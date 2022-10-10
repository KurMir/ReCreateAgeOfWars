using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISpawn : MonoBehaviour
{

  [SerializeField] private float autoGenTime = 8f;
  [SerializeField] private float autoGenTimer;
  void Start()
  {

  }
  void Update()
  {
    BotAutoSpawn();
  }

  void BotAutoSpawn()
  {
    autoGenTimer -= Time.deltaTime;
    if (autoGenTime <= 0)
    {

    }
  }
}
