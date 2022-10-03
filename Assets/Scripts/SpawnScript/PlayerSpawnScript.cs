using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnScript : MonoBehaviour
{
  public List<GameObject> spawns = new List<GameObject>();
  private float cooldownTimer; //
  public float cooldownTime = 150f; // test purpose
  public bool isCoolDown;
  void Start()
  {
    isCoolDown = true;
    cooldownTimer = cooldownTime;
  }

  void Update()
  {
    SpawnCoolDown();
  }

  private void SpawnCoolDown()
  {
    if (isCoolDown)
    {
      // Cooldown of the skill minus Time.Deltatime
      cooldownTimer -= Time.deltaTime;

      if (cooldownTimer < 0.0f)
      {

        isCoolDown = false;
        cooldownTimer = cooldownTime;
        Instantiate(spawns[0], this.transform.position, Quaternion.identity);
        isCoolDown = true;

      }
    }

  }

}
