using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Unity.Collections;

public class HeathBar : MonoBehaviour
{
    public Slider healthSlider;
    public TMP_Text healthBarText;

    Damageable playerDamageable;

    private void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if(player == null)
        {
            Debug.Log("No Player found in this scene, make sure object has tag 'Player'");
        }
        playerDamageable = player.GetComponent<Damageable>();
    }

    void Start()
    {  
        healthSlider.value = CaculateSliderPercentage(playerDamageable.Health, playerDamageable.MaxHealth);
        healthBarText.text = "HP " + playerDamageable.Health + " / " + playerDamageable.MaxHealth;
    }

    private void OnEnable()
    {
        playerDamageable.healthChange.AddListener(OnPlayerHealthChanged);
    }

    private void OnDisable()
    {
        playerDamageable.healthChange.RemoveListener(OnPlayerHealthChanged);
    }

    private float CaculateSliderPercentage(float currenHealth, float maxHeath)
    {
        return currenHealth / maxHeath;
    }

    private void OnPlayerHealthChanged(int newHealth, int maxHealth)
    {
        healthSlider.value = CaculateSliderPercentage(newHealth, maxHealth);
        healthBarText.text = "HP" + newHealth + " / " + maxHealth;
    }
}
