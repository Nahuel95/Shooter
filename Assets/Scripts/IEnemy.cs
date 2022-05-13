using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public interface IEnemy
{
    float MaxHealth { get; set; }
    float Health { get; set; }
    float AttackDamage { get; set; }
    float Movespeed { get; set; }
    bool Alive { get; set; }
    NavMeshAgent Navigator { get; set; }
    void Attack();
    void Die();
    void TakeDamage(float dmg);
}
