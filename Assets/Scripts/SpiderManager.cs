using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class SpiderManager : Enemy
{
    Rigidbody body;
    Animator spiderAnimator;
    float attackTime = 3f;
    float attackCount = 0f;
    float atkTimer = 0.5f;
    float atkTimerCount = 0;
    float dieCount = 3f;
    Ray rayo;
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        Navigator = GetComponent<NavMeshAgent>();
        Player = GameObject.Find("Player");
        MaxHealth = 50f;
        Health = MaxHealth;
        Movespeed = 1f;
        body = GetComponent<Rigidbody>();
        spiderAnimator = GetComponent<Animator>();
        AttackDamage = 3f;
        spiderAnimator.SetFloat("AttackTime", 1f / atkTimer);
        SetAnimBoolsFalse();
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();
        if (Health > 0) {
        }
        if (Health <= 0)
        {
            Die();
        }
        if (Alive)
        {
            if ((Player.transform.position - transform.position).magnitude > 3f)
            {
                FollowPlayer();
                if (spiderAnimator.GetBool("Idle") != false || spiderAnimator.GetBool("Walking") != true)
                {
                    spiderAnimator.SetBool("Idle", false);
                    spiderAnimator.SetBool("Walking", true);
                }

            }
            else if (attackCount <= 0)
            {
                if (spiderAnimator.GetBool("Idle") != false || spiderAnimator.GetBool("Walking") != false)
                {
                    spiderAnimator.SetBool("Walking", false);
                    spiderAnimator.SetBool("Idle", false);
                }
                spiderAnimator.SetTrigger("Attacking");
                Attack();
            }
            else
            {
                Navigator.SetDestination(transform.position);
                if (atkTimerCount <= 0)
                {
                    if (spiderAnimator.GetBool("Idle") != true || spiderAnimator.GetBool("Walking") != false)
                    {
                        spiderAnimator.SetBool("Walking", false);
                        spiderAnimator.SetBool("Idle", true);
                    }
                }
            }
        }
        else {
            if (Navigator.isOnNavMesh)
            {
                Navigator.SetDestination(transform.position);
            }
        }
       

        if (atkTimerCount > 0) {
            atkTimerCount -= Time.deltaTime;
        }

        if (attackCount > 0) {
            attackCount -= Time.deltaTime;
        }
    }

    void FollowPlayer() {
        Vector3 target = new Vector3 (Player.transform.position.x, transform.position.y, Player.transform.position.z);
        Ray ray = new Ray(transform.position, target - transform.position);
        rayo = ray;
        LayerMask mask = LayerMask.GetMask("Player", "Wall");
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, mask))
        {
            //Debug.Log(hit.collider.gameObject.layer);
            if (hit.collider.gameObject.layer == 11) {
                transform.LookAt(new Vector3(hit.transform.position.x, transform.position.y, hit.transform.position.z));
                Navigator.SetDestination(hit.transform.position);
            }
        }
           
    }
    void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(rayo);
    }

    override public void TakeDamage(float dmg)
    {
        Health -= dmg;
    }

    override public void Die()
    {
        if (Alive == true) {
            spiderAnimator.SetBool("Walking", false);
            spiderAnimator.SetBool("Idle", false);
            spiderAnimator.SetTrigger("Die");
        }
        Alive = false;
        if (dieCount > 0)
        {
            dieCount -= Time.deltaTime;
        }
        else {
            Destroy(this.transform.gameObject);
        }
    }
    override public void Attack() {
        Player.GetComponent<PlayerController>().GetDamage(AttackDamage);
        Debug.Log("Player Attacked");
        attackCount = attackTime;
        atkTimerCount = atkTimer;
    }
    void SetAnimBoolsFalse() {
        foreach (AnimatorControllerParameter p in spiderAnimator.parameters) {
            if (p.type == AnimatorControllerParameterType.Bool) {
                spiderAnimator.SetBool(p.name, false);
            }
        }
    }
}
