using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChampionScriptUI : MonoBehaviour
{
  GameObject[] findHeroChampion;
  public GameObject myHeroChampion;

  [SerializeField] private Image heroHP;
  [SerializeField] private Image heroMp;
  [SerializeField] private TMP_Text heroHPText;
  [SerializeField] private TMP_Text heroMPText;
  [SerializeField] private TMP_Text heroNameText;
  [SerializeField] private TMP_Text heroLevelText;
  [SerializeField] private string heroName;
  private int heroLevel;
  public bool isChampionAlive;
  public float health;

  void Start()
  {
    heroHP.fillAmount = 1f;
    isChampionAlive = true;
  }

  public void FindHeroChampion()
  {
    findHeroChampion = GameObject.FindGameObjectsWithTag("P1");

    foreach (GameObject gameObject in findHeroChampion)
    {
      if (LayerMask.LayerToName(gameObject.layer).Equals("HeroPlayer"))
      {
        myHeroChampion = gameObject;
      }
    }
  }

  public void SetStatus(bool status)
  {
    isChampionAlive = status;
  }

  public void SetLevel(int heroLevel)
  {
    this.heroLevel = heroLevel;
  }

  void Update()
  {

    if (isChampionAlive)
    {
      if (myHeroChampion != null)
      {
        DamageScript damageScript = myHeroChampion.GetComponent<DamageScript>();
        damageScript = myHeroChampion.GetComponent<DamageScript>();
        health = damageScript.GetHealthPoints();
        heroHP.fillAmount = damageScript.GetHealthPoints() / damageScript.GetMaxHealthPoints();
        heroNameText.text = heroName;

      }
    }
  }

  //

  //heroHPText.text = Mathf.RoundToInt(damageScript.GetHealthPoints()).ToString();

}

