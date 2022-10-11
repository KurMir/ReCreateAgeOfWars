using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreSameTag : MonoBehaviour
{
  void Start()
  {
    GameObject[] gameObjectsToIgnore = GameObject.FindGameObjectsWithTag(this.gameObject.tag);
    Collider2D selfCollider = this.gameObject.GetComponent<Collider2D>();

    foreach (GameObject gameObject in gameObjectsToIgnore)
    {
      if (gameObject == this.gameObject) continue;
      Collider2D collider = gameObject.GetComponent<Collider2D>();
      Physics2D.IgnoreCollision(selfCollider, collider);
    }
  }
}
