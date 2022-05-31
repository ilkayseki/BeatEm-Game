
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private CharacterAnimation enemyAnim;

    private Rigidbody myBody;
    public float speed = 3f;

    private Transform playerTarget;

    public float attackDistance = 1f;
    private float chasePlayerAfterAttack = 0.2f;

    private float defaultAttackTime = 2f;
    private float currentAttackTime;

    private bool followPlayer, attackPlayer;

    private HealthScript healthPlayer;

    private void Awake()
    {
        enemyAnim = GetComponentInChildren<CharacterAnimation>();
        myBody = GetComponent<Rigidbody>();

        playerTarget = GameObject.FindWithTag(Tags.PLAYER_TAG).transform;

        healthPlayer = GameObject.FindWithTag(Tags.PLAYER_TAG).GetComponent<HealthScript>();
    }

    // Start is called before the first frame update
    void Start()
    {
        followPlayer = true;
        currentAttackTime = defaultAttackTime;
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
    }

    private void FixedUpdate()
    {
        FollowTarget();
    }

    //follow target
    private void FollowTarget()
    {
        //if we are not supposed to follow the player
        if (!followPlayer)
            return;
        
        //if player out of distance then follow the player
        if(Vector3.Distance(transform.position, playerTarget.position) > attackDistance)
        {
            transform.LookAt(playerTarget);
            myBody.velocity = transform.forward * speed;
            //if distance between us is big enough so walk
            if(myBody.velocity.sqrMagnitude != 0)
            {
                enemyAnim.Walk(true);
            }
        }
        //if our distance is smaller or equal than attack distance so stop the follow
        else if(Vector3.Distance(transform.position, playerTarget.position) <= attackDistance)
        {
            myBody.velocity = Vector3.zero;
            enemyAnim.Walk(false);

            followPlayer = false;
            attackPlayer = true;
        }
    }//follow target

    //attack
    private void Attack()
    {
        //if we should NOT attack the player
        //exit the function
        if (!attackPlayer)
            return;

        currentAttackTime += Time.deltaTime;

        if(currentAttackTime > defaultAttackTime)
        {
            enemyAnim.EnemyAttack(Random.Range(0, 3));
            currentAttackTime = 0f;
        }

        if(Vector3.Distance(transform.position,playerTarget.position) > attackDistance + chasePlayerAfterAttack)
        {
            attackPlayer = false;
            followPlayer = true;
        }

        //after player died, dont attack or move. 2. way.
        //but we dont need anymore it because in healthscript we made enemymovement disabled.
        //if(healthPlayer.health == 0)
        //{
        //    attackPlayer = false;
        //    followPlayer = false;
        //}
    }

}
