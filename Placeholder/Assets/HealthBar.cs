using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider _healthSlider;
    public Slider _EaseHealthSlider;
    private HealthGeneral _bossHP;
    private float _LerpSpeed = 0.05f;
    private float health = 2000f;
    

    private void Awake()
    {
        _bossHP = GameObject.Find("Boss").GetComponent<HealthGeneral>(); // find the boss by name in hierarchy
        _healthSlider.maxValue = _bossHP._maxHealth;
        _EaseHealthSlider.maxValue = _bossHP._maxHealth;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        health = _bossHP._currentHealth;
        if (_healthSlider.value != health)
        {
            _healthSlider.value = health;
        }

        if (_healthSlider.value != _EaseHealthSlider.value)
        {
            _EaseHealthSlider.value = Mathf.Lerp(_EaseHealthSlider.value, health, _LerpSpeed);
        }
        
    }

}
