﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


abstract public class SecondaryWeapon : MonoBehaviour, IWeapon
{
    public string Name { get; set; } 
    public float Cooldown { get; set; }
    public float Damage { get; set; }
    public string Description { get; set; } = "Descripcion";
    public float CdCount { get; set; }
    public bool Equipped { get; set; } = false;
    public Sprite Img { get; set; }
    public bool Shooting { get; set; } = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    abstract public void Activate();
}
