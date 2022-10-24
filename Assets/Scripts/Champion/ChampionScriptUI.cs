using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChampionScriptUI : MonoBehaviour
{

  public DamageScript damageScript;
  [SerializeField] private Image heroHP;
  void Start()
  {
    damageScript.GetComponent<DamageScript>();
  }

  void Update()
  {
    heroHP.fillAmount = damageScript.GetHealthPoints() / damageScript.GetMaxHealthPoints();
  }
}
