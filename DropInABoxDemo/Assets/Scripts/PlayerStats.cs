using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {

    GameController gc;


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
        gc = GameObject.FindWithTag("GameController").GetComponent<GameController>() ;
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

    public int GetHealth()
    {
        return currentHealth;
    }


    public bool IsAlive()
    {
        if (currentHealth > 0)
            return true;
        else
            return false;
    }

    private void Death()
    {
        gc.GameOver();
    }
}
