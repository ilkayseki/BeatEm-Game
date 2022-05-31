using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour
{
    public float health = 100f;

    private CharacterAnimation animationScript;
    private EnemyMovement enemyMovement;

    private bool characterDied;
    public bool isPlayer;

    private HealthUI healthUI;

    private void Awake()
    {
        animationScript = GetComponentInChildren<CharacterAnimation>();

        if (isPlayer)
        {
            healthUI = GetComponent<HealthUI>();
        }
    }

    //apply damage
    public void ApplyDamage(float damage, bool knockdown)
    {
        if (characterDied)
        {
            return;
        }
        
        health -= damage;

        //display health UI
        if (isPlayer)
        {
            healthUI.DisplayHealth(health);
        }

        if(health <= 0f)
        {
            animationScript.Death();
            characterDied = true;

            //if isPlayer deactivate enemy script
            if (isPlayer)
            {
                GameObject.FindWithTag(Tags.ENEMY_TAG).GetComponent<EnemyMovement>().enabled = false;
            }
            return;
        }

        //if is not Player
        if (!isPlayer)
        {
            if (knockdown)
            {
                if (Random.Range(0, 2) > 0)
                {
                    animationScript.KnockDown();
                }
                else
                {   
                    if (Random.Range(0, 3) > 1)
                    {
                        animationScript.Hit();
                    }
                }
            }
        }
    }
}
