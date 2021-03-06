﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float damage;

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag.Equals("Canister") || other.tag.Equals("Shield")) { }
        else
        {
            // Find the TankHealth script associated with the rigidbody.
            Complete.TankHealth targetHealth = other.GetComponent<Complete.TankHealth>();
            // If there is no TankHealth script attached to the gameobject, go on to the next collider.
            if (!targetHealth)
            {
                Destroy(gameObject);
                return;
            }
            // Deal this damage to the tank.
            targetHealth.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
