using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BasicGun : PrimaryWeapon
{
    int maxClipAmmo = 10;
    new GameObject camera;
    float ROFCount = 0f;
    public GameObject hitPoint;
    float reloadTime = 2.5f;
    public Sprite img;
    SpriteRenderer shootLight;
    float lightTime = 0.03f;
    float lightCount = 0;
    Animator pistolAnimator;
    // Start is called before the first frame update
    void Start()
    {
        Name = "Pistol";
        RateOfFire = 0.5f;
        Damage = 15f;
        camera = GameObject.Find("Main Camera");
        MaxClipAmmo = maxClipAmmo;
        ClipAmmo = MaxClipAmmo;
        ReloadCount = 0f;
        ReloadTime = reloadTime;
        Img = img;
        shootLight = GetComponentInChildren<SpriteRenderer>();
        Shooting = false;
        pistolAnimator = GetComponent<Animator>();
        pistolAnimator.SetFloat("ReloadSpeed", 1f / reloadTime);
        //pistolAnimator.SetFloat("Speed", 1f / lightTime);
    }
    // Update is called once per frame
    void Update()
    {
        if (ROFCount > 0) {
            ROFCount -= Time.deltaTime;
        }

        if (ReloadCount > 0)
        {
            ReloadCount -= Time.deltaTime;
            pistolAnimator.SetBool("Reloading", true);
        }
        else {
            pistolAnimator.SetBool("Reloading", false);
        }

        if (lightCount > 0)
        {
            lightCount -= Time.deltaTime;
            //shootLight.enabled = true;
            Shooting = true;
        }
        else {
            //shootLight.enabled = false;
            Shooting = false;
        }

        if ((Input.GetKeyDown(KeyCode.R) && ClipAmmo < MaxClipAmmo) || ClipAmmo <= 0) {
            ClipAmmo = MaxClipAmmo;
            ReloadCount = ReloadTime;
        }


    }
    override public void Activate(){
        if (ROFCount <= 0 && ReloadCount <= 0 && ClipAmmo > 0) {
            Ray ray = new Ray(camera.transform.position, camera.transform.forward);
            RaycastHit hit;
            LayerMask mask = LayerMask.GetMask("Enemy", "Wall");
            ClipAmmo -= 1;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, mask)){
                //Debug.Log(hit.transform.gameObject.name);
                if (hit.transform.gameObject.layer == 9) {
                    //Debug.Log(hit.transform.gameObject.name);
                    hit.transform.gameObject.GetComponent<Enemy>().TakeDamage(Damage);
                }
            }
            ROFCount = RateOfFire;
            lightCount = lightTime;
            pistolAnimator.SetTrigger("Shooting");

        }
    }
}
