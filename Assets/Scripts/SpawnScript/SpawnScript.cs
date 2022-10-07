using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScript : MonoBehaviour
{

  public List<GameObject> spawns = new List<GameObject>();

  void Start()
  {
    if (this.gameObject.tag == "Spawn2")
    {
      Instantiate(spawns[1], this.transform.position, Quaternion.Euler(0f, 180f, 0f)); //To test, AI Script still ongoing on AiScript.cs
    }
  }

  public void SummonWarrior()
  {
    Instantiate(spawns[0], this.transform.position, Quaternion.identity);
  }

  public void SummonArcher()
  {
    Instantiate(spawns[1], this.transform.position, Quaternion.identity);
  }

  public void SummonSpearman()
  {
    Instantiate(spawns[2], this.transform.position, Quaternion.identity);
  }
}
