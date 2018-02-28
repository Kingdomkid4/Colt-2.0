using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageReciever : MonoBehaviour
{

    float hitpoints = 100.0f;

    void ApplyDamage(float damage)
    {
        hitpoints -= damage;
        if (hitpoints <= 0)
        {
            Debug.Log("u ded");
        }
    }

}
