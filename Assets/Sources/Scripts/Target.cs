using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour,IDamageFromWeapon
{
    [SerializeField] private float _health;

    public void TakeDamage(float value)
    {
        _health -= value;

        if(_health <= 0)
        {
           _health = 0;
           Die();
        }
    }

    private void Die()
    {
       Destroy(gameObject);
    }
}
