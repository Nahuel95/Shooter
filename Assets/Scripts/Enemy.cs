using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Enemy : MonoBehaviour, IEnemy
{
    public float MaxHealth { get; set; } = 50f;
    public float Health { get; set; }
    public float AttackDamage { get; set; }
    public float Movespeed { get; set; }
    public NavMeshAgent Navigator { get; set; }
    public bool Alive { get; set; } = true;
    public GameObject Player { get; set; }
    public Sprite redBar;
    public Sprite greenBar;
    SpriteRenderer healthBar;
    SpriteRenderer maxHealthBar;
    GameObject totalHealthBar;
    GameObject currentHealthBar;


    public void Start(){
        Health = MaxHealth;
        Player = GameObject.Find("Player");
        totalHealthBar = transform.Find("RedBar").gameObject;
        currentHealthBar = transform.Find("GreenBar").gameObject;
        healthBar = currentHealthBar.GetComponent<SpriteRenderer>();
        maxHealthBar = totalHealthBar.GetComponent<SpriteRenderer>();
        maxHealthBar.sprite = redBar;
        healthBar.sprite = greenBar;
        totalHealthBar.transform.localScale = Vector3.one * 0.22f;
        currentHealthBar.transform.localScale = Vector3.one * 0.22f;
    }
    public void Update() {
        maxHealthBar.transform.forward = (Player.transform.position - transform.position).normalized;
        healthBar.transform.forward = (Player.transform.position - transform.position).normalized;
        Vector3 pos = transform.position + new Vector3(0, 2, 0);
        maxHealthBar.transform.position = pos;
        healthBar.transform.position = pos + healthBar.transform.forward * 0.001f;
        float newXScale = (totalHealthBar.transform.localScale.x * Health / MaxHealth);
        if (newXScale < 0) {
            newXScale = 0;
        }
        currentHealthBar.transform.localScale = new Vector3(newXScale, currentHealthBar.transform.localScale.y, currentHealthBar.transform.localScale.z);
    }
    virtual public void Attack()
    {
    }

    virtual public void Die()
    {
        Destroy(this.transform.gameObject);
    }

    virtual public void TakeDamage(float dmg)
    {
        Health -= dmg;
        Debug.Log(Health);
    }
}
