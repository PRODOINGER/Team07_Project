using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUpItem : MonoBehaviour
{
    public GameObject item;
    public float speedBoostAmount = 0.5f;
    public float boostDuration = 3f;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ItemManager.Instance.ApplySpeedBoost(collision.gameObject, speedBoostAmount, boostDuration);
            item.SetActive(false); 
        }
    }
}