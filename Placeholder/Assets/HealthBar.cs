using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Slider _healthSlider;
    private HealthGeneral _bossHP;

    private void Awake()
    {
        _bossHP = GameObject.Find("boss").GetComponent<HealthGeneral>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_healthSlider.value != _bossHP._currentHealth)
        {
            _healthSlider.value = _bossHP._currentHealth;
        }
        
    }
    
    
    
}
