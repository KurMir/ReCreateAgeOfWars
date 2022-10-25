using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChampionScriptUI : MonoBehaviour
{
  GameObject[] findHeroChampion;
  public GameObject myHeroChampion;
  DamageScript damageScript;
  [SerializeField] private Image heroHP;
  [SerializeField] private Image heroMp;
  [SerializeField] private TMP_Text heroHPText;
  [SerializeField] private TMP_Text heroMPText;
  [SerializeField] private TMP_Text heroNameText;
  [SerializeField] private TMP_Text heroLevelText;
  [SerializeField] private string heroName;
  private string heroLevel;
  private bool isChampionAlive;

  void Start()
  {
    isChampionAlive = false;
  }

  public void FindHeroChampion()
  {
    findHeroChampion = GameObject.FindGameObjectsWithTag("P1");

    foreach(GameObject gameObject in findHeroChampion)
    {
      if(LayerMask.LayerToName(gameObject.layer).Equals("HeroPlayer"))
      {
        myHeroChampion = gameObject;
      }
    }
  }

  public void SetStatus(bool status)
  {
    isChampionAlive = status;
  }

  void Update()
  {
    if(isChampionAlive)
    {
      //damageScript = myHeroChampion.GetComponent<DamageScript>();
      //heroHP.fillAmount = damageScript.GetHealthPoints() / damageScript.GetMaxHealthPoints();
      
      //heroHPText.text = Mathf.RoundToInt(damageScript.GetHealthPoints()).ToString();
    }    
  }
}
