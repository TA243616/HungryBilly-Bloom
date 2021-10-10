using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public Animator animator;
    private string currentState ;
    public NavMeshAgent zombie;
    private Transform player;

    bool attacking = false;
    bool dying = false;

    private float health = 100;
    public bool headHit = false;
    public bool bodyHit = false;
    public bool grenadeHit = false;

    private string
        IDLE = "Z_Idle",
        FALL_BACK = "Z_FallingBack",
        FALL_FORWARD = "Z_FallingForward",
        WALK = "Z_Walk1_InPlace",
        FOLLOW = "Z_Walk_InPlace",
        ATTACK = "Z_Attack";

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        zombie.SetDestination(player.position);
        //if (this.GetComponent<Rigidbody>().velocity.magnitude == 0)
        //{
        //    ChangeAnimationState(IDLE);
        //}
        if (!attacking && !dying)
        {
            if (zombie.destination == player.position)
            {
                ChangeAnimationState(FOLLOW);
            }
            else if(zombie.destination != player.position)
            {
                ChangeAnimationState(WALK);
            }
        }
        if (bodyHit)
        {
            DeductHealth(34);
        }
        if (grenadeHit)
        {
            DeductHealth(100);
        }
        if (health <= 0)
        {
            dying = true;
            zombie.isStopped = true;
            ChangeAnimationState(FALL_BACK);
            StartCoroutine(Despawn());
        }
    }

    private void DeductHealth(float amount)
    {
        health -= amount;
        bodyHit = false;
        headHit = false;
        grenadeHit = false;
    }

    private IEnumerator Despawn()
    {
        yield return new WaitForSeconds(4);
        GameManager.spawned -= 1;
        Destroy(this.gameObject);
    }

    private void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;
        animator.Play(newState);
        currentState = newState;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !attacking && !dying)
        {
            attacking = true;
            ChangeAnimationState(ATTACK);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && attacking)
        {
            attacking = false;
        }
    }
}
