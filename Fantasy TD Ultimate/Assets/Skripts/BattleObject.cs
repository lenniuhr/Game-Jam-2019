using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BattleObject : MonoBehaviour {

    public int health = 100;

    public void TakeDamage(int dmg)
    {
        health -= dmg;

        if (health <= 0)
        {
            HandleDeath();
        }
    }

    protected abstract void HandleDeath();

    public bool HasDied()
    {
        return health <= 0;
    }

    public int GetHealth()
    {
        return health;
    }
}
