using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePoint : MonoBehaviour
{
  public GameObject projectileArrow;
  public GameObject shootFrom;
  private void Create()
  {
    Instantiate(projectileArrow, shootFrom.transform.position, Quaternion.identity);

  }
}
