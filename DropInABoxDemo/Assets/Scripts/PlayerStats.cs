using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {

    #region Singleton
    public static PlayerStats instance;

    void Awake()
    {
        instance = this;
    }

    #endregion

    public int maxHealth = 100;
    public int currentHealth = 0;

	// Use this for initialization
	void Start () {
        currentHealth = maxHealth;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //Called to decrement character health
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if(currentHealth <= 0)
        {
            Death();
        }
    }

    //Called to increment character health
    public void ModifyHealth(int healthModifier)
    {
        currentHealth += healthModifier;
    }

    private void Death()
    {
        print("You're dead.");
    }
}
