using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAIScript : MonoBehaviour
{
  public List<GameObject> spawns = new List<GameObject>();
  void Start()
  {
    Instantiate(spawns[0], this.transform.position, Quaternion.identity);
  }
}
