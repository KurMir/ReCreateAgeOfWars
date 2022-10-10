using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HealthbarBehaviour : MonoBehaviour
{
  public Slider Slider;
  public Color Low;
  public Color High;
  public Vector3 OffSet;

  public void SetHealth(float health, float maxHealth)
  {
    Slider.gameObject.SetActive(health < maxHealth);
    Slider.value = health;
    Slider.maxValue = maxHealth;

    Slider.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(Low, High, Slider.normalizedValue);

  }

  // Update is called once per frame
  void Update()
  {
    Slider.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + OffSet);

  }
}
