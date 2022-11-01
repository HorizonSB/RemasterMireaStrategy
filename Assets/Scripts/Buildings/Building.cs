using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Building : MonoBehaviour
{
    [Header("Attributes")]
    public int _health;

    [Header("UI")]
    public Slider healthSlider;

    void Start()
    {
        healthSlider.maxValue = _health;
        healthSlider.value = _health; 
    }

    public void TakeDamage(int damage)
    {
        if(!healthSlider.gameObject.activeInHierarchy) healthSlider.gameObject.SetActive(true);
        _health -= damage;
        healthSlider.value = _health;
        if (_health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
