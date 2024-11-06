using Supercyan.FreeSample;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public static ItemManager Instance; 

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ApplySpeedBoost(GameObject player, float speedBoostAmount, float boostDuration)
    {
        StartCoroutine(SpeedBoostCoroutine(player, speedBoostAmount, boostDuration));
    }

    private IEnumerator SpeedBoostCoroutine(GameObject player, float speedBoostAmount, float boostDuration)
    {
        CharacterControl characterControl = player.GetComponent<CharacterControl>();
        if (characterControl != null)
        {
            characterControl.m_moveSpeed -= speedBoostAmount; 
            yield return new WaitForSeconds(boostDuration);
            characterControl.m_moveSpeed += speedBoostAmount; 
        }
    }

}
