using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

abstract public class PrimaryWeapon : MonoBehaviour, IWeapon
{
    public string Name { get; set; }
    public float ReloadCount { get; set; }
    public float ReloadTime { get; set; }
    public float RateOfFire { get; set; }
    public float Damage { get; set; }
    public int MaxClipAmmo { get; set; }
    public int ClipAmmo { get; set; }
    public int TotalAmmo { get; set; }
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
