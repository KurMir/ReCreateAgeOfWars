using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBulletScript : MonoBehaviour
{
  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {

  }
  void OnTriggerEnter2D(Collider2D other)
  {
    if (this.gameObject.tag == "TowerBulletPlayer2" && other.tag == "P2")
    {
      other.gameObject.GetComponent<DamageScript>().DamageDealt(60);
      Destroy(gameObject);
    }
    if (this.gameObject.tag == "TowerBulletPlayer1" && other.tag == "P2")
    {
      other.gameObject.GetComponent<DamageScript>().DamageDealt(100);
      Destroy(gameObject);
    }
    if (this.gameObject.tag == "TowerBulletEnemy1" && other.tag == "P1")
    {
      other.gameObject.GetComponent<DamageScript>().DamageDealt(60);
      Destroy(gameObject);
    }
    if (this.gameObject.tag == "TowerBulletEnemy2" && other.tag == "P1")
    {
      other.gameObject.GetComponent<DamageScript>().DamageDealt(100);
      Destroy(gameObject);
    }
  }
}
