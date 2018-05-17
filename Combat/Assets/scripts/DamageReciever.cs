using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageReciever : MonoBehaviour
{
    public GameObject deathScreen;

    float hitpoints = 100.0f;

    private void Start()
    {
        deathScreen.SetActive(false);
    }

    void ApplyDamage(float damage)
    {
        hitpoints -= damage;
        if (hitpoints <= 0)
        {
            Debug.Log("You died");
            deathScreen.SetActive(true);
            Enemy.gameOver = true;
        }
    }

}
