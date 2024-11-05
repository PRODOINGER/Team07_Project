using Supercyan.FreeSample;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUpItem : MonoBehaviour
{
    public GameObject item; 
    public float speedBoostAmount = 0.5f; 
    public float boostDuration = 3f; 
    private bool isBoostActive = false; 

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isBoostActive)
        {
            StartCoroutine(ApplySpeedBoost(collision.gameObject)); 
            item.SetActive(false); 
        }
    }

    private IEnumerator ApplySpeedBoost(GameObject player)
    {
        isBoostActive = true; 
        CharacterControl characterControl = player.GetComponent<CharacterControl>(); 
        if (characterControl != null)
        {
            characterControl.m_moveSpeed *= speedBoostAmount; 
            yield return new WaitForSeconds(boostDuration); 
            characterControl.m_moveSpeed /= speedBoostAmount; 
        }
        isBoostActive = false; 
    }
}
